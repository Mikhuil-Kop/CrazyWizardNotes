using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CrazyWizardNotes.Magic.Realization
{
    [ExecuteAlways]
    public class SpellParticleVisualizer : MonoBehaviour
    {
        public SpriteRenderer ParticleSprite;
        public Light2D ParticleLight;
        public Color Color;

        [Header("Animation Fields")]
        [Range(0f, 1f)]
        public float Alpha;

        public Color AlphaColor => new(Color.r, Color.g, Color.b, Color.a * Alpha);

        public void LateUpdate()
        {
            if (ParticleSprite != null)
            {
                ParticleSprite.color = AlphaColor;
            }

            if (ParticleLight != null)
            {
                ParticleLight.color = AlphaColor;
            }
        }

        public void SetupColor(Color color)
        {
            Color = color;
        }
    }
}
