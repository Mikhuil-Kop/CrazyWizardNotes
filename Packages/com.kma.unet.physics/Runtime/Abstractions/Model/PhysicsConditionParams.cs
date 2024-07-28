namespace UNet.Physics.Abstractions
{
    public class PhysicsConditionParams
    {
        public PhysicsShapeType ShapeType { get; set; }
        public ISpacePoint ShapeCenter { get; set; }
        public Vector2D ShapeSize { get; set; }
    }
}
