using CrazyWizardNotes.Elements.Abstractions;
using UNet.Core.Abstractions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CrazyWizardNotes.Elements.Realization
{
    [RequireComponent(typeof(IActor))]
    public class ElementalMaterialVisualizer : MonoBehaviour
    {
        public MaterialConfigScriptable MaterialConfig;
        private Light2D Light { get; set; }
        private MaterialPropertyBlock Properties { get; set; }
        private SpriteRenderer[] SpriteRenderers { get; set; }
        private IActor Actor { get; set; }
        private IHasTemperature ActorTemperature { get; set; }

        private static int IsMetalicId { get; } = Shader.PropertyToID("_IsMetalic");
        private static int TemperatureId { get; } = Shader.PropertyToID("_Temperature");
        public bool IsMetalic;
        public float Temperature;

        public void Start()
        {
            SpriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
            Properties = new MaterialPropertyBlock();
            Actor = gameObject.GetComponent<IActor>();
            ActorTemperature = Actor.As<IHasTemperature>();

            if (MaterialConfig.Value.IsMetalicVisibly)
            {
                Light = gameObject.AddComponent<Light2D>();

                Light.color = Color.lightYellow;
                Light.pointLightInnerRadius = 0;
                Light.pointLightOuterRadius = 2;
                Light.falloffIntensity = 0.8f;
                Light.intensity = 0f;
            }

            PullProperties();
            ApplyProperties();
        }

        public void LateUpdate()
        {
            PullProperties();
            ApplyProperties();
        }

        private void PullProperties()
        {
            if (MaterialConfig != null)
            {
                IsMetalic = MaterialConfig.Value.IsMetalicVisibly;
            }

            var t1 = -250f;
            var m1 = -50f;
            var m2 = 100f;
            var t2 = 1000f;

            if (ActorTemperature != null)
            {
                var temperature = ActorTemperature.Temperature;

                if (temperature < m1)
                {
                    if (temperature <= t1)
                    {
                        Temperature = -1;
                    }
                    else
                    {
                        Temperature = Mathf.Pow((temperature - t1) / (m1 - t1), 1f) - 1; // y\ =\left(\frac{x-t_{1}}{m_{1}-t_{1}}\right)^{\frac{1}{4}}-1
                    }
                }
                else if (temperature > m2)
                {
                    if (temperature >= t2)
                    {
                        Temperature = 1;
                    }
                    else
                    {
                        Temperature = 1 - Mathf.Pow((temperature - t2) / (m2 - t2), 1f); // y=1-\left(\frac{x-t_{2}}{m_{2}-t_{2}}\right)^{\frac{1}{4}}
                    }
                }
                else
                {
                    Temperature = 0f;
                }
            }
        }

        private void ApplyProperties()
        {
            if (Light  != null)
            {
                Light.intensity = Temperature / 2f;
            }

            if (SpriteRenderers != null && Properties != null)
            {
                foreach (var sprite in SpriteRenderers)
                {
                    if (sprite == null)
                    {
                        continue;
                    }

                    sprite.GetPropertyBlock(Properties);

                    Properties.SetFloat(TemperatureId, Temperature);
                    Properties.SetFloat(IsMetalicId, IsMetalic ? 1f : 0f);

                    sprite.SetPropertyBlock(Properties);
                }
            }
        }
    }
}