using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = UNet.Core.Abstractions.ILogger;

namespace UNet.Core.Realization
{
    public class SceneLoadService : ISceneLoadService, IHandlesStart, IHandlesUpdate
    {
        private ILogger Logger { get; }
        private SceneLoadServiceConfig Config { get;}

        private UNetSceneInfo ParentSceneInfo { get; set; }
        private UNetSceneInfo CurrentSceneInfo { get; set; }
        private UNetSceneData CurrentSceneData { get; set; }

        ISceneInfo ISceneLoadService.ParentSceneInfo => ParentSceneInfo;
        ISceneInfo ISceneLoadService.CurrentSceneInfo => CurrentSceneInfo;
        ISceneData ISceneLoadService.CurrentSceneData => CurrentSceneData;

        private SceneLoadingProcess CurrentProcess { get; set; }
        private static StaticBuffer Buffer { get; set; }

        public SceneLoadService(ILogger logger, SceneLoadServiceConfig config)
        {
            Logger = logger;
            Config = config;
        }

        public ISceneLoadingProcess LoadScene(string nextSceneName, object sceneData1, object sceneData2)
        {
            if (CurrentProcess != null)
            {
                Logger.LogWarning($"Сцена уже в процессе загрузки: {CurrentProcess.LoadingSceneInfo.Name} -> {nextSceneName}");
            }

            Logger.LogInfo($"Loading \"Scenes/{nextSceneName}\"");

            var info = new UNetSceneInfo()
            {
                Name = nextSceneName,
            };

            var data = new UNetSceneData()
            {
                ResourceRawData = sceneData1,
                PreloadRawData = sceneData2
            };

            Buffer = new StaticBuffer()
            {
                PrevSceneInfo = CurrentSceneInfo,
                PrevSceneData = CurrentSceneData,
                NextSceneInfo = info,
                NextSceneData = data
            };

            var op = SceneManager.LoadSceneAsync(nextSceneName);
            op.allowSceneActivation = false;

            CurrentProcess = new SceneLoadingProcess()
            {
                SceneLoadService = this,
                AsyncOperation = op,
                LoadingSceneInfo = info,
                LoadingSceneData = data,
            };

            return CurrentProcess;
        }

        public void OnStart()
        {
            if (Buffer == null)
            {
                if (Config.TestSceneData1 == null)
                {
                    throw new Exception("Для запуска сцены необходим TestSceneResource");
                }

                ParentSceneInfo = null;
                CurrentSceneInfo = new UNetSceneInfo()
                {
                    Name = Config.TestSceneName
                };

                CurrentSceneData = new UNetSceneData()
                {
                    ResourceRawData = Config.TestSceneData1,
                    PreloadRawData = Config.TestSceneData2,
                };
            }
            else
            {
                ParentSceneInfo = Buffer.PrevSceneInfo;
                CurrentSceneInfo = Buffer.NextSceneInfo;
                CurrentSceneData = Buffer.NextSceneData;
                Buffer = null;
            }

            foreach (var actor in GetComponents<IHandlesSceneLoaded>())
            {
                actor.OnSceneLoaded(CurrentSceneInfo, CurrentSceneData);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (CurrentProcess != null)
            {
                var prevents = false;

                foreach (var actor in GetComponents<IHandlesNextSceneLoading>())
                {
                    var context = new SceneLoadingContext()
                    {
                        SceneLoadingProcess = CurrentProcess,
                        PreventOpenNextScene = false
                    };

                    actor.OnNextSceneLoading(context);

                    prevents |= context.PreventOpenNextScene;
                }

                if (CurrentProcess.Progress >= 0.9f && !prevents)
                {
                    Open(CurrentProcess);
                }
            }
        }

        private void Open(SceneLoadingProcess process)
        {
            foreach (var actor in GetComponents<IHandlesSceneUnloaded>())
            {
                actor.OnSceneUnloaded(CurrentProcess.LoadingSceneInfo, CurrentProcess.LoadingSceneData);
            }

            process.AsyncOperation.allowSceneActivation = true;
        }

        private IEnumerable<T> GetComponents<T>() where T : class
        {
            return UNetScene.Instance.Components.As<T>();
        }

        #region Unity Очистка статики

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void ClearStatic()
        {
            Buffer = null;
        }

        #endregion

        #region Внутренние классы

        private class SceneLoadingProcess : ISceneLoadingProcess
        {
            public SceneLoadService SceneLoadService { get; internal set; }
            public AsyncOperation AsyncOperation { get; internal set; }

            public UNetSceneInfo LoadingSceneInfo { get; internal set; }
            public UNetSceneData LoadingSceneData { get; internal set; }

            ISceneInfo ISceneLoadingProcess.LoadingSceneInfo => LoadingSceneInfo;
            ISceneData ISceneLoadingProcess.LoadingSceneData => LoadingSceneData;

            public float Progress => AsyncOperation.progress;

            public void ForceOpen() => SceneLoadService.Open(this);
        }

        private class StaticBuffer
        {
            public UNetSceneInfo NextSceneInfo { get; set; }
            public UNetSceneData NextSceneData { get; set; }
            public UNetSceneInfo PrevSceneInfo { get; set; }
            public UNetSceneData PrevSceneData { get; set; }
        }

        #endregion
    }
}