using CrazyWizardNotes.Magic.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization
{
    public class AntimagicField : IAntimagicField
    {
        private ITouches Touches { get; }
        private AntimagicFieldConfig Config { get; }

        public bool IsActive { get; set; }

        public float Radius
        {
            get => Config.SphereSpriteRenderer.transform.localScale.x / 2f;
            set => Config.SphereSpriteRenderer.transform.localScale = Vector3.one * (2 * value);
        }

        public AntimagicField(ITouches touches, AntimagicFieldConfig config)
        {
            Touches = touches;
            Config = config;

            Touches.OnTouch += OnTouch;
        }

        private void OnTouch(OnTouchEventArgs obj)
        {
            Debug.Log("TOUCH111");
            if (IsActive)
            {
                Debug.Log("TOUCH222");
                foreach (var actor in obj.Touched)
                {
                    Debug.Log("TOUCH123");
                    actor.As<IVanishable>()?.Vanish();
                }
            }
        }
    }
}
