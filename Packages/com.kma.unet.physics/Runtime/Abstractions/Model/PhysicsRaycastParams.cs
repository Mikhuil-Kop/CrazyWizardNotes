namespace UNet.Physics.Abstractions
{
    public class PhysicsRaycastParams
    {
        public SpacePoint From { get; set; }
        public SpacePoint To { get; set; }
        public bool One { get; set; }
    }
}
