using CrazyWizardNotes.Totems.Abstractions;
using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Totems.Realization
{
    public class TotemBase : ITotem, IHandlesUpdate
    {
        protected IActor Actor { get; }
        protected ITotemVisualizer TotemVisualizer { get; }

        private float _timer;
        private TimeSpan _activationTimeGap;
        private bool _isEnabled;
        private bool _isActive;

        public event TotemActivationHandler OnActivated;
        public event TotemActivationHandler OnDeactivated;
        public event TotemEnabledHandler OnEnabled;
        public event TotemEnabledHandler OnDisabled;

        public TotemBase(IActor actor, ITotemVisualizer totemVisualizer)
        {
            Actor = actor;
            TotemVisualizer = totemVisualizer;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_isActive || !_isEnabled || _activationTimeGap.Ticks <= 10)
            {
                return;
            }

            _timer += deltaTime;

            while (_timer >= _activationTimeGap.TotalSeconds)
            {
                _timer -= (float)_activationTimeGap.TotalSeconds;

                RaiseOnActivated();
                TotemVisualizer.RunActivateRaiseAnimation();
            }
        }

        private void RaiseOnActivated()
        {
            OnActivated?.Invoke(new TotemActivationEventArgs(Actor, this));
        }

        private void RaiseOnDeactivated()
        {
            OnDeactivated?.Invoke(new TotemActivationEventArgs(Actor, this));
        }

        private void RaiseOnEnabled()
        {
            OnEnabled?.Invoke(new TotemEnabledEventArgs(Actor, this));
        }

        private void RaiseOnDisabled()
        {
            OnDisabled?.Invoke(new TotemEnabledEventArgs(Actor, this));
        }

        #region Активации и деактивации

        public TimeSpan ActivationTimeGap { get => _activationTimeGap; set => SetActivationTimeGap(value); }
        public bool IsEnabled { get => _isEnabled; set => SetEnabled(value); }
        public bool IsActive { get => _isActive; set => SetActive(value); }

        public void SetEnabled(bool enabled)
        {
            if (enabled)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }

        public void Enable()
        {
            if (!_isEnabled)
            {
                RaiseOnEnabled();
                TotemVisualizer.RunEnableAnimation();

                _isEnabled = true;
            }
        }

        public void Disable()
        {
            if (_isEnabled)
            {
                RaiseOnDisabled();
                TotemVisualizer.RunDisableAnimation();

                _isEnabled = false;
            }
        }

        public void ActivateOnce()
        {
            if (!_isEnabled)
            {
                return;
            }

            _timer = 0f;

            RaiseOnActivated();

            if (_isActive)
            {
                TotemVisualizer.RunActivateRaiseAnimation();
            }
            else
            {
                TotemVisualizer.RunActivateOnceAnimation();
            }
        }

        public void Activate()
        {
            if (!_isEnabled)
            {
                return;
            }

            if (!_isActive)
            {
                _timer = 0f;

                RaiseOnActivated();
                TotemVisualizer.RunActivateAnimation();

                _isActive = true;
            }
        }

        public void Deactivate()
        {
            if (!_isEnabled)
            {
                return;
            }

            if (_isActive)
            {
                _timer = 0f;

                RaiseOnDeactivated();
                TotemVisualizer.RunDeactivateAnimation();

                _isActive = false;
            }
        }

        public void SetActivationTimeGap(TimeSpan timeGap)
        {
            _activationTimeGap = timeGap;
        }

        #endregion
    }
}
