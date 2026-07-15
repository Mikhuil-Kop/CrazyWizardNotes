using System;
using UNet.Core.Abstractions;
using UNet.UI.Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace UNet.UI.Realization
{
    public class TooltipService : ITooltipService, IHandlesStart, IHandlesLateUpdate
    {
        private TooltipUIMonoBehaviour Tooltip { get; }
        private TooltipCanvasMonoBehaviour Canvas { get; }

        public TooltipService(TooltipUIMonoBehaviour tooltip, TooltipCanvasMonoBehaviour canvas)
        {
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
            var targetActor = eventData.pointerEnter.GetComponent<IActor>();
            var tooltipInfo = targetActor?.As<IHasTooltipInfo>()?.GetTooltipInfo();

            if (tooltipInfo != null)
            {
                Tooltip.Title.text = GetLocalizedString(tooltipInfo.Title);
                Tooltip.Description.text = GetLocalizedString(tooltipInfo.Description);

                Tooltip.gameObject.SetActive(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.gameObject.SetActive(false);
        }

        [Obsolete("Перенести и обощить")]
        private static string GetLocalizedString(TextString text)
        {
            if (!text.Localized)
            {
                return text.Text;
            }

            var str = new LocalizedString(text.Group ?? "default", text.Text);

            foreach (var param in text.Params)
            {
                var key = param.Key;
                var value = param.Value;

                switch (value)
                {
                    case string s:
                        str.Add(key, new StringVariable { Value = s });
                        break;
                    case int i:
                        str.Add(key, new IntVariable { Value = i });
                        break;
                    case float f:
                        str.Add(key, new FloatVariable { Value = f });
                        break;
                    case bool b:
                        str.Add(key, new BoolVariable { Value = b });
                        break;
                    case double d:
                        str.Add(key, new DoubleVariable { Value = d });
                        break;
                    case long l:
                        str.Add(key, new LongVariable { Value = l });
                        break;
                    default:
                        str.Add(key, new StringVariable { Value = value?.ToString() ?? "" });
                        break;
                }
            }

            var result = str.GetLocalizedString();

            if (result.StartsWith("No translation found"))
            {
                Debug.LogWarning("No translation found for text: " + text.Text);
                return "!!!@" + text.Text;
            }

            return result;
        }
    }
}
