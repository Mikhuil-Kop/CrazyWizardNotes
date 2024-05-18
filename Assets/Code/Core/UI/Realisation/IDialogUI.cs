using System.Xml.Linq;
using UnityEngine;

namespace CrazyWizardNotes.UI.Realisation
{
    public interface IDialogUI
    {
        void SetPosition(Vector2 position);
        void StartDialog(XElement dialogData);
    }
}