using System;
using UNet.Core.Abstractions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UNet.Core.Realization
{
    // Возможно, этот функционал стоит перенести в UNetScene. Но он занимал слишком много места и я не хотел перегружать UNetScene.
    // TODO: Стоит поделить сервис на загрузчик сцен и на загрузчик ресурсов о сцене. Сейчас пофиг.
    [Obsolete()]
    public class SceneLoadService : ISceneLoadService, IHandlesStart, IHandlesUpdate
    {
        private IActorsCollectionService ActorsCollectionService { get; }
        private IResourcesService ResourceService { get; }
        private ISceneResource TestSceneResource { get; }
        private object TestRuntimeData { get; }
        private UNetSceneLoadingProcess CurrentProcess { get; set; }

        private UNetSceneInfo ParentSceneInfo { get; set; }
        private UNetSceneInfo CurrentSceneInfo { get; set; }
        private UNetSceneData CurrentSceneData { get; set; }

        ISceneInfo ISceneLoadService.ParentSceneInfo => ParentSceneInfo;
        ISceneInfo ISceneLoadService.CurrentSceneInfo => CurrentSceneInfo;
        ISceneData ISceneLoadService.CurrentSceneData => CurrentSceneData;

        private static StaticBuffer Buffer { get; set; }

        public SceneLoadService(IActorsCollectionService actorsCollectionService, IResourcesService resourcesService, SceneLoadServiceConfig config)
        {
            ActorsCollectionService = actorsCollectionService;
            ResourceService = resourcesService;
            TestSceneResource = config.TestSceneResource;
            TestRuntimeData = config.TestRuntimeData;
        }

        public ISceneLoadingProcess LoadScene(string nextSceneName, object nextSceneData)
        {
            if (CurrentProcess != null)
            {
                Debug.LogWarning($"Сцена уже в процессе загрузки: {CurrentProcess.LoadingSceneInfo.Name} -> {nextSceneName}");
            }

            var resource = ResourceService.Load<ISceneResource>("Scenes/" + nextSceneName);

            Debug.Log("Load Scenes/" + nextSceneName + "-" + resource);

            var info = new UNetSceneInfo()
            {
                Name = nextSceneName,
            };

            var data = new UNetSceneData()
            {
                PreloadRawData = nextSceneData,
                ResourceRawData = resource.SceneData,
            };

            Buffer = new StaticBuffer()
            {
                PrevSceneInfo = CurrentSceneInfo,
                PrevSceneData = CurrentSceneData,
                NextSceneInfo = info,
                NextSceneData = data
            };

            var op = SceneManager.LoadSceneAsync(resource.SceneName);
            op.allowSceneActivation = false;

            CurrentProcess = new UNetSceneLoadingProcess()
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
                // Тестовый запуск не из редактора
                Debug.Log("Тестовый запуск из редактора (возможно боевой)))))");

                if (TestSceneResource == null)
                {
                    throw new Exception("Для запуска сцены необходим TestSceneResource");
                }

                ParentSceneInfo = null;
                CurrentSceneInfo = new UNetSceneInfo()
                {
                    Name = TestSceneResource.SceneName
                };
                CurrentSceneData = new UNetSceneData()
                {
                    ResourceRawData = TestSceneResource.SceneData,
                    PreloadRawData = TestRuntimeData,
                };
            }
            else
            {
                Debug.Log("Запуск новой сцены.");

                ParentSceneInfo = Buffer.PrevSceneInfo;
                CurrentSceneInfo = Buffer.NextSceneInfo;
                CurrentSceneData = Buffer.NextSceneData;
                Buffer = null;
            }

            Debug.Log("OnSceneLoaded");
            foreach (var a in UNetScene.Instance.Components.As<IHandlesSceneLoaded>())
            {
                a.OnSceneLoaded(CurrentSceneInfo, CurrentSceneData);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (CurrentProcess != null)
            {
                var prevents = false;

                Debug.Log("OnNextSceneLoading");
                foreach (var a in UNetScene.Instance.Components.As<IHandlesNextSceneLoading>())
                {
                    var context = new SceneLoadingContext()
                    {
                        SceneLoadingProcess = CurrentProcess,
                        PreventOpenNextScene = false
                    };

                    a.OnNextSceneLoading(context);

                    prevents |= context.PreventOpenNextScene;
                }

                if (CurrentProcess.Progress >= 0.9f && !prevents)
                {
                    Open(CurrentProcess);
                }
            }
        }

        private void Open(UNetSceneLoadingProcess process)
        {
            Debug.Log("OnSceneUnloaded");
            foreach (var a in UNetScene.Instance.Components.As<IHandlesSceneUnloaded>())
            {
                a.OnSceneUnloaded(CurrentProcess.LoadingSceneInfo, CurrentProcess.LoadingSceneData);
            }

            process.AsyncOperation.allowSceneActivation = true;
        }

        [Obsolete]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void ClearStatic()
        {
            Buffer = null;
            Debug.Log("Статика очищена");
        }

        #region Внутренние классы

        private class UNetSceneLoadingProcess : ISceneLoadingProcess
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


        [Obsolete]
        public class SceneLoadServiceConfig
        {
            public ISceneResource TestSceneResource;
            public object TestRuntimeData;
        }

        #endregion
    }
}