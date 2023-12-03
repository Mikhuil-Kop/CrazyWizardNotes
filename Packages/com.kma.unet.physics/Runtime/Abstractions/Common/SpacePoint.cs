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

        public static SpacePoint operator +(SpacePoint a) => new(a.ParentSpace, a.Position);
        public static SpacePoint operator -(SpacePoint a) => new(a.ParentSpace, a.Position);
        public static SpacePoint operator +(SpacePoint a, Vector2D b) => new(a.ParentSpace, a.Position + b);
        public static SpacePoint operator -(SpacePoint a, Vector2D b) => new(a.ParentSpace, a.Position - b);
        public static SpacePoint operator *(SpacePoint a, Vector2D b) => new(a.ParentSpace, a.Position * b);
        public static SpacePoint operator /(SpacePoint a, Vector2D b) => new(a.ParentSpace, a.Position / b);
        public static SpacePoint operator *(SpacePoint a, float b) => new(a.ParentSpace, a.Position * b);
        public static SpacePoint operator /(SpacePoint a, float b) => new(a.ParentSpace, a.Position / b);
        public static SpacePoint operator *(float b, SpacePoint a) => new(a.ParentSpace, a.Position * b);
        public static SpacePoint operator /(float b, SpacePoint a) => new(a.ParentSpace, a.Position / b);
    }
}