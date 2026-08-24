using CrazyWizardNotes.Totems.Abstractions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CrazyWizardNotes.Buildings.Realization
{
    [ExecuteAlways]
    public class TotemVisualizer : MonoBehaviour, ITotemVisualizer
    {
        private static readonly int EnableHash = Animator.StringToHash("TotemEnable");
        private static readonly int DisableHash = Animator.StringToHash("TotemDisable");
        private static readonly int ActivateHash = Animator.StringToHash("TotemActivate");
        private static readonly int ActivateOnceHash = Animator.StringToHash("TotemActivateOnce");
        private static readonly int ActivateRaiseHash = Animator.StringToHash("TotemActivateRaise");
        private static readonly int DeactivateHash = Animator.StringToHash("TotemDeactivate");

        [Header("Components")]
        public Animator Animator;
        public SpriteRenderer CrystalSprite;
        public Light2D CrystalLight;

        [Header("Base Values")]
        [Range(0f, 1f)]
        public float HueBase = 1f;
        [Range(0f, 2f)]
        public float SaturationBase = 1f;
        [Range(0f, 2f)]
        public float ValueBase = 1f;
        [Range(0f, 2f)]
        public float LightBase = 1f;

        [Header("Animation Values")]
        [Range(0f, 2f)]
        public float Saturation = 1f;
        [Range(0f, 2f)]
        public float Value = 1f;
        [Range(0f, 2f)]
        public float Light = 1f;

        private MaterialPropertyBlock Properties;
        private int HueId;
        private int SaturationId;
        private int ValueId;

        public void Start()
        {
            Properties = new MaterialPropertyBlock();
            HueId = Shader.PropertyToID("_Hue");
            SaturationId = Shader.PropertyToID("_Saturation");
            ValueId = Shader.PropertyToID("_Value");
        }

        public void LateUpdate()
        {
            if (CrystalSprite != null && Properties != null)
            {
                CrystalSprite.GetPropertyBlock(Properties);

                Properties.SetFloat(HueId, HueBase);
                Properties.SetFloat(SaturationId, SaturationBase * SaturationBase);
                Properties.SetFloat(ValueId, ValueBase * Value);

                CrystalSprite.SetPropertyBlock(Properties);
            }

            if (CrystalLight != null)
            {
                CrystalLight.color = Color.HSVToRGB(HueBase, SaturationBase * SaturationBase, 1f);
                CrystalLight.intensity = LightBase * Light;
            }
        }

        public void RunEnableAnimation()
        {
            Animator.CrossFade(EnableHash, 0.1f);
        }

        public void RunDisableAnimation()
        {
            Animator.CrossFade(DisableHash, 0.1f);
        }

        public void RunActivateAnimation()
        {
            Animator.CrossFade(ActivateHash, 0.1f);
        }

        public void RunActivateOnceAnimation()
        {
            Animator.CrossFade(ActivateOnceHash, 0.1f);
        }

        public void RunActivateRaiseAnimation()
        {
            Animator.CrossFade(ActivateRaiseHash, 0.1f);
        }

        public void RunDeactivateAnimation()
        {
            Animator.CrossFade(DeactivateHash, 0.1f);
        }
    }
}
