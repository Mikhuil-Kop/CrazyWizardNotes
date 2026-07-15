using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UNet.UI.Realization
{
    public class TooltipCanvasMonoBehaviour : MonoBehaviour
    {
        public RectTransform RectTransform;
        public event Action<PointerEventData> OnPointerEnterEvent;
        public event Action<PointerEventData> OnPointerExitEvent;

        public void OnPointerEnter(BaseEventData eventData)
        {
            if (eventData is not PointerEventData)
            {
                Debug.LogWarning("Event data is not a PointerEventData");
            }

            OnPointerEnterEvent?.Invoke(eventData as PointerEventData);
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            if (eventData is not PointerEventData)
            {
                Debug.LogWarning("Event data is not a PointerEventData");
            }

            OnPointerExitEvent?.Invoke(eventData as PointerEventData);
        }
    }
}
