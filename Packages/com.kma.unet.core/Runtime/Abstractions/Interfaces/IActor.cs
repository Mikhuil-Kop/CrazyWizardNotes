using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Common;

namespace UNet.Core.Abstractions
{
    public interface IActor
    {
        IReadonlyTags Tags { get; }
        bool Is<T>() where T : class;
        T As<T>() where T : class;
        void InsertContext(params Action<IActorCreateContext>[] context);
    }

    public static class ActorsExtensions
    {
        public static IEnumerable<T> ActorsAs<T>(this IEnumerable<IActor> subjects) where T : class
        {
            return subjects.Select(x => x?.As<T>()).Where(x => x != null);
        }
    }
}
