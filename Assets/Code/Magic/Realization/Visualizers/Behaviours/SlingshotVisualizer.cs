using UNet;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public class SlingshotVisualizer : MonoBehaviour, ISlingshotVisualizer
    {
        public Animator Animator;
        public Transform Target;
        public Transform Line;
        public Transform Attack;

        #region Animator

        private static readonly int IsEnabledHash = Animator.StringToHash("IsEnabled");
        private static readonly int IsActiveHash = Animator.StringToHash("IsActive");
        private static readonly int IsReleasedHash = Animator.StringToHash("IsReleased");
        private static readonly int LevelHash = Animator.StringToHash("Level");

        private bool _isEnabled;
        private bool _isActive;
        private bool _isReleased;
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

        public bool IsReleased
        {
            get => _isReleased;
            set
            {
                if (_isReleased != value)
                {
                    _isReleased = value;
                    Animator?.SetBool(IsReleasedHash, value);
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

        public void UpdateTarget(Vector2D pull)
        {
            var angle = Mathf.Atan2(pull.X, pull.Y) * Mathf.Rad2Deg;

            Target.localPosition = -pull;

            Line.localScale = new Vector3(pull.Magnitude, 1, 1);
            Line.rotation = Quaternion.Euler(0f, 0f, -90f - angle);

            Attack.rotation = Quaternion.Euler(0f, 0f, +90f - angle);
        }
    }
}
