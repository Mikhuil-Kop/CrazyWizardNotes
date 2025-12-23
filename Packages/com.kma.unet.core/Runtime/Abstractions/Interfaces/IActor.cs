using System.Collections.Generic;
using System.Linq;
using UNet.Common;

namespace UNet.Core.Abstractions
{
    public interface IActor
    {
        bool Is<T>() where T : class;
        T As<T>() where T : class;
        IReadonlyTags Tags { get; }
    }

    public static class ActorsExtensions
    {
        public static IEnumerable<T> ActorsAs<T>(this IEnumerable<IActor> subjects) where T : class
        {
            return subjects.Select(x => x?.As<T>()).Where(x => x != null);
        }
    }
}
