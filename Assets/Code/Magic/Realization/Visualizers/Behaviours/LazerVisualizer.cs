using UNet;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public class LazerVisualizer : MonoBehaviour, ILazerVisualizer
    {
        public Animator Animator;
        public Transform Beam;
        public Transform Hitpoint;

        #region Animator

        private static readonly int IsEnabledHash = Animator.StringToHash("IsEnabled");
        private static readonly int IsActiveHash = Animator.StringToHash("IsActive");
        private static readonly int LevelHash = Animator.StringToHash("Level");

        private bool _isEnabled;
        private bool _isActive;
        private int _level;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    Animator?.SetBool(IsEnabledHash, value);
                }
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    Animator?.SetBool(IsActiveHash, value);
                }
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    _level = value;
                    Animator?.SetInteger(LevelHash, value);
                }
            }
        }

        #endregion

        public void UpdateTarget(Vector2D distance)
        {
            //Beam.localScale = new Vector3(distance.Magnitude, 1, 1);
            Beam.localScale = new Vector3(100f, 1, 1);
            Beam.rotation = Quaternion.Euler(0f, 0f, -90f - Mathf.Atan2(distance.X, distance.Y) * Mathf.Rad2Deg);

            Hitpoint.localPosition = -distance;
        }
    }
}
