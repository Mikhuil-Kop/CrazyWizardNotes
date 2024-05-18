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
    public class TalkingService : SingletonBehavior<ITalkingService>, ITalkingService
    {
        [SerializeField]
        private RectTransform DialogParent;
        [SerializeField]
        private DialogUI DialogPrefab;

        private ICameraService CameraService { get; set; }

        private Dictionary<ISubject, OpenedDialog> OpenedDialogs { get; set; }


        protected override void Awake()
        {
            base.Awake();

            CameraService = Locate<ICameraService>();

            OpenedDialogs = new Dictionary<ISubject, OpenedDialog>();
        }

        public void StartDialog(ISubject subject, string dialogName)
        {
            if (!subject.Is<ISpacePoint>())
            {
                return;
            }

            var dialogContent = Resources.Load<XmlData>("Dialogs/" + dialogName).XElement;
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

            openedDialog.DialogUI.StartDialog(openedDialog.DialogContent);
        }

        private void Update()
        {
            foreach (var dialog in OpenedDialogs.Values)
            {
                dialog.DialogUI.SetPosition(GetDialogPosition(dialog));
            }
        }

        private Vector2 GetDialogPosition(OpenedDialog dialog)
        {
            var worldPoint = dialog.SpacePoint.Position;
            var screenPoint = CameraService.WorldToScreen(worldPoint);

            return screenPoint;
        }

        private class OpenedDialog
        {
            public IDialogUI DialogUI { get; set; }
            public XElement DialogContent { get; set; }
            public ISpacePoint SpacePoint { get; set; }
        }
    }
}