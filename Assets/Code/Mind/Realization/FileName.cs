using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyWizardNotes.Mind.Realization
{
    /// <summary>
    /// Класс-компонент, вызывающий обработку мыслей с помощью <see cref="MindPipline"/>.
    /// </summary>
    public class MindHandler
    {

    }

    /// <summary>
    /// Класс, обрабатывающий события и имитирующий мыслительные процессы.
    /// </summary>
    public class MindPipline
    {
    }

    public interface IMindFact
    {

    }

    public enum MindFactTypes
    {
        ISee,
        IHate,
        ILike,
        IFeelEmotion,
        IFeelCondition,
        IKnowItsClose,
    }
}
