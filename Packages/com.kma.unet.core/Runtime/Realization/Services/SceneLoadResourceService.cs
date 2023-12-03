using UNet.Core.Abstractions;
using ILogger = UNet.Core.Abstractions.ILogger;

namespace UNet.Core.Realization
{
    public class SceneLoadResourceService : ISceneLoadResourceService
    {
        private ILogger Logger { get; }
        private IResourcesService ResourceService { get; }
        private ISceneLoadService SceneLoadService { get; }

        ISceneInfo ISceneLoadResourceService.ParentSceneInfo => SceneLoadService.ParentSceneInfo;
        ISceneInfo ISceneLoadResourceService.CurrentSceneInfo => SceneLoadService.CurrentSceneInfo;
        ISceneData ISceneLoadResourceService.CurrentSceneData => SceneLoadService.CurrentSceneData;

        public SceneLoadResourceService(ILogger logger, IResourcesService resourceService, ISceneLoadService sceneLoadService)
        {
            Logger = logger;
            ResourceService = resourceService;
            SceneLoadService = sceneLoadService;
        }

        public ISceneLoadingProcess LoadScene(string nextSceneName, object nextSceneData)
        {
            var resource = ResourceService.Load<ISceneResource>("Scenes/" + nextSceneName);

            return SceneLoadService.LoadScene(resource.SceneName, resource.SceneData, nextSceneData);
        }
    }
}