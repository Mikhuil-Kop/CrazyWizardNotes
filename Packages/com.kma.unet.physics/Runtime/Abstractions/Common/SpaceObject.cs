namespace UNet.Physics.Abstractions
{
    public struct SpaceObject
    {
        public ISpaceContainer ParentSpace { get; set; }
        public Vector2D Position { get; set; }
        public Vector2D Size { get; set; }
        public float Rotation { get; set; }

        public SpaceObject(ISpaceContainer parentSpace, Vector2D position, Vector2D size, float rotation)
        {
            ParentSpace = parentSpace;
            Position = position;
            Size = size;
            Rotation = rotation;
        }

        public SpaceObject(SpacePoint point, Vector2D size, float rotation)
        {
            ParentSpace = point.ParentSpace;
            Position = point.Position;
            Size = size;
            Rotation = rotation;
        }
    }
}
