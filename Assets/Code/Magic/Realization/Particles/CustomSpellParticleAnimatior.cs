using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization
{
    public sealed class CustomSpellParticleAnimatior
    {
        private Animator Animator { get; }

        public CustomSpellParticleAnimatior(Animator animator)
        {
            Animator = animator;
        }

        #region Поля аниматора

        private readonly static int _IsBendingHash = Animator.StringToHash("_IsBending");
        private bool _IsBending;
        public bool IsBending
        {
            get => _IsBending;
            set
            {
                if (_IsBending != value)
                {
                    _IsBending = value;
                    Animator.SetBool(_IsBendingHash, value);
                }
            }
        }

        private readonly static int _IsVanishRequestedHash = Animator.StringToHash("_IsVanishRequested");
        private bool _IsVanishRequested;
        public bool IsVanishRequested
        {
            get => _IsVanishRequested;
            set
            {
                if (_IsVanishRequested != value)
                {
                    _IsVanishRequested = value;
                    Animator.SetBool(_IsVanishRequestedHash, value);
                }
            }
        }

        #endregion
    }
}
