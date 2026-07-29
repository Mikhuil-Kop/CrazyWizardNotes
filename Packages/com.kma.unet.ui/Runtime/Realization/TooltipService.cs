using System;
using UNet.Core.Abstractions;
using UNet.UI.Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UNet.UI.Realization
{
    public class TooltipService : ITooltipService, IHandlesStart, IHandlesLateUpdate
    {
        private TooltipUIMonoBehaviour Tooltip { get; }
        private TooltipCanvasMonoBehaviour Canvas { get; }
        private ILocalizationService LocalizationService { get; }

        public TooltipService(ILocalizationService localizationService, TooltipUIMonoBehaviour tooltip, TooltipCanvasMonoBehaviour canvas)
        {
            LocalizationService = localizationService;
            Tooltip = tooltip;
            Canvas = canvas;
        }

        public void OnStart()
        {
            Canvas.OnPointerEnterEvent += OnPointerEnter;
            Canvas.OnPointerExitEvent += OnPointerExit;
        }

        [Obsolete("Написано ИИ")]
        public void OnLateUpdate(float deltaTime)
        {
            if (Tooltip.gameObject.activeSelf)
            {
                var mousePosition = Input.mousePosition;
                var tooltipRectTransform = Tooltip.RectTransform;
                var canvasRectTransform = Canvas.RectTransform;
                // Convert mouse position to local position in the canvas
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, null, out Vector2 localPoint);
                // Calculate the pivot based on the mouse position
                var pivot = new Vector2(
                    Mathf.Clamp01(localPoint.x / canvasRectTransform.rect.width),
                    Mathf.Clamp01(localPoint.y / canvasRectTransform.rect.height)
                );
                tooltipRectTransform.pivot = pivot;
                // Set the position of the tooltip
                tooltipRectTransform.position = mousePosition;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var target = eventData.pointerEnter.GetComponent<IActor>()?.As<IHasTooltipInfo>() ?? eventData.pointerEnter.GetComponent<IHasTooltipInfo>();
            var tooltipInfo = target?.GetTooltipInfo();

            if (tooltipInfo != null)
            {
                Tooltip.Title.text = LocalizationService.GetLocalizedString(tooltipInfo.Title);
                Tooltip.Description.text = LocalizationService.GetLocalizedString(tooltipInfo.Description);

                Tooltip.gameObject.SetActive(true);
                return;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.gameObject.SetActive(false);
        }
    }
}
