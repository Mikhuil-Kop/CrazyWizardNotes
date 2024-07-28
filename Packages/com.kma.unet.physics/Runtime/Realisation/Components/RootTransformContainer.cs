using UNet.Physics.Abstractions;

namespace UNet.Physics.Realisation
{
    public class RootTransformContainer : ISpaceContainer
    {
        public ISpaceContainer ParentSpace => null;

        public Vector2D TranslateToParentSpace(Vector2D vector)
        {
            throw new System.Exception($"Unable to get parent space vector. {nameof(RootTransformContainer)} don`t have {nameof(ParentSpace)}.");
        }
    }
}