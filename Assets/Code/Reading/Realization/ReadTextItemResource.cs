using System;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Reading.Realization
{
    [Serializable]
    public class ReadTextItemResource
    {
        [SerializeField]
        public string Text;

        [SerializeField]
        [SerializeOfEnum(typeof(ReadTextResourceLanguage))]
        public string Language;
    }
}
