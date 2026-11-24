using System;
using System.Linq;
using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    [ExecuteAlways]
    public class HumanEyeVisualizer : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField]
        private HumanVisualizer parent;
        [SerializeField]
        private SpriteRenderer eyeSprite;
        [SerializeField]
        private bool isLeft;

        [Header("Animation")]
        [SerializeField, Range(-1f, 1f)]
        private float height = 0f;
        [SerializeField, Range(1, 3)]
        private byte mood = 1;

        private float _lastHeight = float.MaxValue;
        private byte _lastMood = byte.MaxValue;

        private void Update()
        {
            //if (!Application.isPlaying)
            //{
            //    return;
            //}

            if (Mathf.Approximately(height, _lastHeight) && mood == _lastMood)
            {
                return;
            }

            _lastHeight = height;
            _lastMood = mood;

            ApplyChanges();
        }

        private void OnValidate()
        {
            if (parent == null || eyeSprite == null)
            {
                return;
            }

            ApplyChanges();
        }

        private void ApplyChanges()
        {
            try
            {
                var eyeSprites = isLeft
                    ? parent.Skin.LeftEyeSprites
                    : parent.Skin.RightEyeSprites;

                var best = eyeSprites
                    .Where(x => x.mood == mood)
                    .MinBy(x => Mathf.Abs(x.height - height));

                if (eyeSprite.sprite != best.sprite)
                {
                    eyeSprite.sprite = best.sprite;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Ошибка при анмации персонажа.");
                Debug.LogWarning(e);
            }
        }
    }
}