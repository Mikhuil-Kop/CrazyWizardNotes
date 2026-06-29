using CrazyWizardNotes.Creatures.Abstractions;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    internal class FoodTest : IHandlesUpdate
    {
        public FoodTest(SpriteRenderer spriteRenderer, IEatable eatable)
        {
            SpriteRenderer = spriteRenderer;
            Eatable = eatable;
        }

        private SpriteRenderer SpriteRenderer { get; }
        private IEatable Eatable { get; }

        public void OnUpdate(float deltaTime)
        {
            SpriteRenderer.color = Color.Lerp(Color.red, Color.green, Eatable.CaloriesLeft / Eatable.CaloriesMax);
        }
    }
}
