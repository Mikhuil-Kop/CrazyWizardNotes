using System;
using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    [ExecuteAlways]
    public class HumanHeadVisualizer : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField]
        private HumanVisualizer parent;
        [SerializeField]
        private SpriteRenderer headSprite;

        [Header("Animation")]
        [SerializeField, Range(-1f, 1f)]
        private float width = 0f;
        
        [SerializeField, Range(-1f, 1f)]
        private float height = 0f;

        private float _lastWidth = float.MaxValue;
        private float _lastHeight = float.MaxValue;

        private void Update()
        {
            //if (!Application.isPlaying)
            //{
            //    return;
            //}

            if (Mathf.Approximately(width, _lastWidth) && Mathf.Approximately(height, _lastHeight))
            {
                return;
            }

            _lastWidth = width;
            _lastHeight = height;
            
            ApplyChanges();
        }

        private void OnValidate()
        {
            if (parent == null || headSprite == null)
            {
                return;
            }

            ApplyChanges();
        }

        private void ApplyChanges()
        {
            try
            {
                var best = parent.Skin.HeadSprites.MinBy(x =>
                {
                    var deltaX = x.width - width;
                    var deltaY = x.height - height;

                    return deltaX * deltaX + deltaY * deltaY;
                });

                if (headSprite.sprite != best.sprite)
                {
                    headSprite.sprite = best.sprite;
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