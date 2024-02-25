using UnityEngine;

namespace Test
{
    public class RopeTest : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D Target1;
        [SerializeField]
        private Rigidbody2D Target2;

        private void FixedUpdate()
        {
            var velocity = (Target1.velocity + Target2.velocity) / 2;
            var totalForce = (Target1.totalForce + Target2.totalForce);
            var angularVelocity = (Target1.angularVelocity + Target2.angularVelocity) / 2;

            Target1.velocity = Target2.velocity = velocity;
            Target1.totalForce = Target2.totalForce = totalForce;
            Target1.angularVelocity = Target2.angularVelocity = angularVelocity;

        }
    }
}