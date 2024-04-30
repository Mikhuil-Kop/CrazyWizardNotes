using System.Linq;
using UnityEngine;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Common.Services;
using System.Threading.Tasks;

namespace CrazyWizardNotes.Auras.Logic
{
    public class AuraController : SingletonBehavior<AuraController>
    {
        public SpriteRenderer SpriteRenderer;
        private IAura Aura;
        [SerializeField]
        private Texture2D Texture;
        private Sprite Sprite;


        protected override void Awake()
        {
            base.Awake();

            Aura = new FireAura(128, 128);
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AsyncQueueService.Instance.Invoke(UpdateTextureAsync());
            }
        }

        protected async Task UpdateTextureAsync()
        {
            await Aura.UpdateAsync();

            var colors = Aura.GetAuraColors();

            DispatcherService.Instance.Invoke(() =>
            {
                Texture.SetPixels32(colors.Select(x => new Color32(x.R, x.G, x.B, 255)).ToArray());
                Texture.Apply();
            });
        }
    }
}