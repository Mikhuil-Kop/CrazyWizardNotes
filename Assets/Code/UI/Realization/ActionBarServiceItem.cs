using CrazyWizardNotes.UI.Abstractions;
using System;
using TMPro;
using UNet.Inputs.Abstractions;
using UNet.UI.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace CrazyWizardNotes.UI.Realization
{
    /// <inheritdoc/>
    public class ActionBarServiceItem : MonoBehaviour, IActionBarServiceItem, IHasTooltipInfo
    {
        public RectTransform ActionRectTransform;
        public TextMeshProUGUI ActionTitleText;
        public TextMeshProUGUI ActionHotkeyText;
        public Image ActionImage;

        private ILocalizationService LocalizationService;
        private IInputService InputService;

        private string _actionName;
        private TextString _actionTitle;
        private TextString _actionDescription;
        private char _actionHotkey;
        private string _actionHotkeyButton;
        private bool _isEnabled;

        public void Initialize(ILocalizationService localizationService, IInputService inputService)
        {
            LocalizationService = localizationService;
            InputService = inputService;
        }

        public event Action<ActionBarServiceItem> ActionClick;

        /// <inheritdoc/>
        public string ActionName
        {
            get => _actionName;
            set => _actionName = value;
        }

        /// <inheritdoc/>
        public TextString ActionTitle
        {
            get => _actionTitle;
            set
            {
                _actionTitle = value;
                ActionTitleText.text = LocalizationService.GetLocalizedString(value);
            }
        }

        /// <inheritdoc/>
        public TextString ActionDescription
        {
            get => _actionDescription;
            set => _actionDescription = value;
        }

        /// <inheritdoc/>
        public char ActionHotkey
        {
            get => _actionHotkey;
            set
            {
                _actionHotkey = value;
                _actionHotkeyButton = ActionHotkey.ToString().ToUpper();
                ActionHotkeyText.text = value.ToString().ToUpper();
            }
        }

        /// <inheritdoc/>
        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        public Sprite ActionIcon
        {
            get => ActionImage.sprite;
            set => ActionImage.sprite = value;
        }

        /// <inheritdoc/>
        public TooltipInfo GetTooltipInfo()
        {
            return new TooltipInfo()
            {
                Title = _actionTitle,
                Description = _actionDescription,
            };
        }
    
        public void OnActionClick()
        {
            ActionClick?.Invoke(this);
        }

        public void Update()
        {
            if (InputService.GetButtonDown(_actionHotkeyButton))
            {
                ActionClick?.Invoke(this);
            }
        }
    }
}