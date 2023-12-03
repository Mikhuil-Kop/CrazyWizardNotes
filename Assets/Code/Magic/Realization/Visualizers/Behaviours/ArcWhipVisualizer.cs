using UNet;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public class ArcWhipVisualizer : MonoBehaviour, IArcWhipVisualizer
    {
        private static readonly int IsConnectedHash = Animator.StringToHash("IsConnected");
        private static readonly int IsMinRadiusHash = Animator.StringToHash("IsMinRadius");
        private static readonly int IsActiveHash = Animator.StringToHash("IsActive");
        public Animator Animator;
        public Transform ArcTrail;
        public Transform ArcRadius;
        public float ArcLenght;

        private bool IsConnected;
        private bool IsMinRadius;
        private bool IsActive;

        public void UpdateTarget(Vector2D distance, float minRadius, bool isActive)
        {
            if (!IsConnected)
            {
                IsConnected = true;
                Animator.SetBool(IsConnectedHash, IsConnected);
            }

            var isMinRadius = distance.Magnitude < minRadius;
            
            if (IsMinRadius != isMinRadius)
            {
                IsMinRadius = isMinRadius;
                Animator.SetBool(IsMinRadiusHash, IsMinRadius);
            }

            if (IsActive != isActive)
            {
                IsActive = isActive;
                Animator.SetBool(IsActiveHash, IsActive);
            }

            if (ArcRadius.transform.localScale.x != minRadius * 2)
            {
                ArcRadius.transform.localScale = minRadius * 2 * Vector2.one;
            }

            var arcMagnitude = distance.Magnitude * ArcLenght;
            var arcRotation = Quaternion.Euler(0f, 0f, -90f - Mathf.Atan2(distance.X, distance.Y) * Mathf.Rad2Deg);

            ArcTrail.transform.localScale = new Vector3(arcMagnitude, 1, 1);
            ArcTrail.transform.rotation = arcRotation;
        }
    }
}
