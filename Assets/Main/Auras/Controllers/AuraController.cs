using System.Linq;
using UnityEngine;
using CrazyWizardNotes.Common;

namespace CrazyWizardNotes.Auras.Logic
{
    public class AuraController : SingletonBehavior<AuraController>
    {
        public SpriteRenderer SpriteRenderer;
        private IAura Aura;
        private Texture2D Texture;
        private Sprite Sprite;


        protected override void Awake()
        {
            base.Awake();

            Aura = new FireAura(128, 128);
            Texture = new Texture2D(Aura.Width, Aura.Height);
            Sprite = Sprite.Create(Texture, new Rect(Vector2.zero, new Vector2(Aura.Width, Aura.Height)), Vector2.one / 2);
            SpriteRenderer.sprite = Sprite;
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log(Texture);

                Aura.UpdateAsync().Wait();

                var colors = Aura.GetAuraColors();

                Texture.SetPixels32(colors.Select(x => new Color32(x.R, x.G, x.B, 255)).ToArray());
            }
        }
    }
}