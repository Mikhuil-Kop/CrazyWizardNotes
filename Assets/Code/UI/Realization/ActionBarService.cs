using CrazyWizardNotes.UI.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UNet.UI.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.UI.Realization
{
    /// <inheritdoc/>
    public class ActionBarService : IActionBarService, IHandlesAwake
    {
        #region Поля

        private ILocalizationService LocalizationService { get; }
        private IInputService InputService { get; }
        private ActionBarServiceItem ItemPrefab { get; }
        private RectTransform BarRectTransform { get; }
        private RectTransform ContainerRectTransform { get; }
        private Dictionary<string, ActionBarServiceItem> Actions { get; }

        /// <inheritdoc/>
        public ActionBarService(ActionsBarServiceConfig config, ILocalizationService localizationService, IInputService inputService)
        {
            LocalizationService = localizationService;
            InputService = inputService;

            ItemPrefab = config.ItemPrefab;
            BarRectTransform = config.BarRectTransform;
            ContainerRectTransform = config.ContainerRectTransform;

            Actions = new Dictionary<string, ActionBarServiceItem>();
        }

        #endregion

        #region Перечисление

        /// <inheritdoc/>
        public byte Count => (byte)Actions.Count;
        /// <inheritdoc/>
        public IActionBarServiceItem this[string actionName] => Actions[actionName];
        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, IActionBarServiceItem>> GetEnumerator() => Actions.Select(x => new KeyValuePair<string, IActionBarServiceItem>(x.Key, x.Value)).GetEnumerator();
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Функционал

        public void OnAwake()
        {
            foreach (var transform in ContainerRectTransform.GetChildren().ToList())
            {
                GameObject.Destroy(transform.gameObject);
            }
        }

        /// <inheritdoc/>
        public IActionBarServiceItem AddAction(string actionName)
        {
            if(!Actions.TryGetValue(actionName, out var action))
            {
                var actionInfo = Resources.Load<ActionBarServiceActionInfo>($"Actions/{actionName}");
                var gameObject = GameObject.Instantiate(ItemPrefab.gameObject, ContainerRectTransform);

                action = Actions[actionName] = gameObject.GetComponentRequired<ActionBarServiceItem>();

                action.Initialize(LocalizationService, InputService);

                action.ActionName = actionName;
                action.ActionIcon = actionInfo.Icon;
                action.ActionTitle = actionInfo.Title;
                action.ActionDescription = actionInfo.Description;
                action.ActionHotkey = GetHotkey();
                action.ActionClick += ActionClick; 

                RefreshActions();
            }

            return action;
        }

        /// <inheritdoc/>
        public bool RemoveAction(string actionName)
        {
            var removed = Actions.Remove(actionName, out var action);

            if (action != null)
            {
                GameObject.Destroy(action.ActionRectTransform.gameObject);

                RefreshActions();
            }

            return removed;
        }

        private void RefreshActions()
        {
            var i = 0;
            var width = 120;
            var count = Actions.Count;

            foreach (var action in Actions)
            {
                action.Value.ActionRectTransform.anchoredPosition = new Vector2(width * (i + 0.5f - count / 2f), 0);

                i++;
            }
        }

        private char GetHotkey()
        {
            var hotkeys = "qwerty";

            var result = hotkeys.Except(Actions.Values.Select(a => a.ActionHotkey)).FirstOrDefault();

            if (result == '\0')
            {
                return '-';
            }
            else
            {
                return result;
            }
        }

        private void ActionClick(ActionBarServiceItem obj)
        {
            Debug.Log($"ДЕЙСТВУЮ: {obj.ActionName}");
        }

        #endregion
    }
}
