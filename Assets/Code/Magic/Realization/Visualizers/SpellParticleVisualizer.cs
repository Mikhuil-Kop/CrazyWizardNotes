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

        public void LateUpdate()
        {
            if (ParticleSprite != null)
            {
                ParticleSprite.color = Color;
            }

            if (ParticleLight != null)
            {
                ParticleLight.color = Color;
            }
        }

        public void SetupColor(Color color)
        {
            Color = color;
        }
    }
}
