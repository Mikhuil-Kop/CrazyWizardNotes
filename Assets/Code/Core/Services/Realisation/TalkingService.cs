using System.Collections.Generic;
using System.Xml.Linq;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Inventory;
using CrazyWizardNotes.UI.Realisation;
using UnityEngine;
using CrazyWizardNotes.Common.Services;

namespace CrazyWizardNotes.Services
{
    public class TalkingService : SingletonBehavior<TalkingService>, ITalkingService
    {
        [SerializeField]
        private RectTransform DialogParent;
        [SerializeField]
        private DialogUI DialogPrefab;

        private Dictionary<ISubject, OpenedDialog> OpenedDialogs;

        public void StartDialog(ISubject subject, string dialogName)
        {
            if (!subject.Is<ISpacePoint>())
            {

            }

            var dialogContent = Resources.Load<XmlData>("Dialogues/" + dialogName).XElement;
            var dialogUI = Instantiate(DialogPrefab.gameObject, DialogParent).GetComponent<DialogUI>();

            var openedDialog = new OpenedDialog()
            {
                DialogUI = dialogUI,
                DialogContent = dialogContent,
                SpacePoint = subject.As<ISpacePoint>()
            };

            if (OpenedDialogs.ContainsKey(subject))
            {
                OpenedDialogs[subject] = openedDialog;
            }
            else
            {
                OpenedDialogs.Add(subject, openedDialog);
            }
        }

        private void Update()
        {
            foreach (var dialog in OpenedDialogs.Values)
            {
                dialog.DialogUI.StartDialog(dialog.DialogContent);
                dialog.DialogUI.SetPosition(GetDialogPosition(dialog));
            }
        }

        private Vector2 GetDialogPosition(OpenedDialog dialog)
        {
            return dialog.SpacePoint.Position;
        }

        private class OpenedDialog
        {
            public DialogUI DialogUI { get; set; }
            public XElement DialogContent { get; set; }
            public ISpacePoint SpacePoint { get; set; }
        }
    }
}