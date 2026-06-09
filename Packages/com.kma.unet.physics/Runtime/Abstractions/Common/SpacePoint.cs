namespace UNet.Physics.Abstractions
{
    public struct SpacePoint
    {
        public ISpaceContainer ParentSpace { get; set; }
        public Vector2D Position { get; set; }

        public SpacePoint(ISpaceContainer parentSpace, Vector2D position)
        {
            ParentSpace = parentSpace;
            Position = position;
        }

        public SpacePoint(SpaceObject spaceObject)
        {
            ParentSpace = spaceObject.ParentSpace;
            Position = spaceObject.Position;
        }
    }
}