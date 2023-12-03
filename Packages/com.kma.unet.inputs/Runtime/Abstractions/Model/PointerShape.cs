namespace UNet.Inputs.Abstractions
{
    public class PointerShape
    {
        public Vector2D[] Points { get; set; }
        public PointerShapeType ShapeType { get; set; }
        public Vector2D Size { get; set; }
        public Vector2D Center { get; set; }
        public float Rotation { get; set; }
        public float Accuracy { get; set; }

        public static PointerShape Default => new()
        {
            Points = new Vector2D[0],
            ShapeType = PointerShapeType.Rectangle,
            Size = new Vector2D(0, 0),
            Center = new Vector2D(0, 0),
            Rotation = 0,
            Accuracy = 0,
        };
    }
}