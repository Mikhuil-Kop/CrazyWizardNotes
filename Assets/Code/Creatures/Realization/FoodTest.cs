using CrazyWizardNotes.Creatures.Abstractions;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class FoodTest : IHandlesUpdate
    {
        public FoodTest(SpriteRenderer spriteRenderer, IDestroyable destroyable, IEatable eatable)
        {
            SpriteRenderer = spriteRenderer;
            Destroyable = destroyable;
            Eatable = eatable;
        }

        private SpriteRenderer SpriteRenderer { get; }
        private IDestroyable Destroyable { get; }
        private IEatable Eatable { get; }

        public void OnUpdate(float deltaTime)
        {
            SpriteRenderer.color = Color.Lerp(Color.red, Color.green, Eatable.CaloriesLeft / Eatable.CaloriesMax);

            if (Eatable.CaloriesLeft / Eatable.CaloriesMax <= 0.01f)
            {
                Destroyable.Destroy();
            }
        }
    }
}
