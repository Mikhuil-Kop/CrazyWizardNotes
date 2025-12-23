using CrazyWizardNotes.Reading.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Reading.Realization
{
    public class ReadHandler : IReadHandler
    {
    }

    public class ReadSource : IReadSource
    {
        public string GetReadText(IReadHandler reading)
        {
            return @"
                dkls [dl askd '[pals\ das das d
                a s
                d a
                s
                 da #{123}
                 da #{345}
                s d #{567}SDsdasdads{}sdas#dasda#{789}
                as dals pofka poskfopa kspfoa sf
                 asf
                 asf ask foas poias ofiapos ifpas ";
        }
    }
}
