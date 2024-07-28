namespace UNet.Physics.Abstractions
{
    public static class PhysicsTools
    {
        public static ISpacePoint GetSpacePoint(this Vector2D position, ISpaceContainer space)
        {
            return new VectorSpacePoint()
            {
                ParentSpace = space,
                Position = position,
            };
        }

        public static ISpacePoint GetSpacePoint(this ISpaceContainer space, Vector2D position)
        {
            return new VectorSpacePoint()
            {
                ParentSpace = space,
                Position = position,
            };
        }

        public static ISpacePoint CopySpacePoint(this ISpacePoint point)
        {
            return new VectorSpacePoint()
            {
                ParentSpace = point.ParentSpace,
                Position = point.Position,
            };
        }

        public static ISpaceObject CopySpaceObject(this ISpaceObject spaceObject)
        {
            return new VectorSpaceObject()
            {
                ParentSpace = spaceObject.ParentSpace,
                Position = spaceObject.Position,
                Size = spaceObject.Size,
                Rotation = spaceObject.Rotation,
            };
        }

        private class VectorSpacePoint : ISpacePoint
        {
            public ISpaceContainer ParentSpace { get; set; }
            public Vector2D Position { get; set; }
        }

        private class VectorSpaceObject : ISpaceObject
        {
            public ISpaceContainer ParentSpace { get; set; }
            public Vector2D Position { get; set; }
            public Vector2D Size { get; set; }
            public float Rotation { get; set; }
        }
    }
}
