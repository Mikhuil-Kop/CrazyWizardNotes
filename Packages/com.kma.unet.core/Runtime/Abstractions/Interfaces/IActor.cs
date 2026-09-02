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
    }

    public static class ActorsExtensions
    {
        public static T AsRequired<T>(this IActor actor) where T : class
        {
            if (actor == null) throw new ArgumentNullException(nameof(actor));

            return actor.As<T>() ?? throw new Exception($"Actor is not of type ({typeof(T).Name}).");
        }

        public static IEnumerable<T> ActorsAs<T>(this IEnumerable<IActor> actors) where T : class
        {
            if (actors == null) throw new ArgumentNullException(nameof(actors));

            return actors.Select(x => x?.As<T>()).Where(x => x != null);
        }
        
        public static bool IsNot<T>(this IActor actor) where T : class
        {
            return !actor.Is<T>();
        }

        #region ActorWithComponents

        public static ActorWithComponents<T1> WithComponents<T1>(this IActor actor)
            where T1 : class
            => actor is not null
                ? new ActorWithComponents<T1>(actor, actor.As<T1>())
                : throw new ArgumentNullException(nameof(actor));

        public static ActorWithComponents<T1, T2> WithComponents<T1, T2>(this IActor actor)
            where T1 : class where T2 : class
            => actor is not null
                ? new ActorWithComponents<T1, T2>(actor, actor.As<T1>(), actor.As<T2>())
                : throw new ArgumentNullException(nameof(actor));

        public static ActorWithComponents<T1, T2, T3> WithComponents<T1, T2, T3>(this IActor actor)
            where T1 : class where T2 : class where T3 : class
            => actor is not null
                ? new ActorWithComponents<T1, T2, T3>(actor, actor.As<T1>(), actor.As<T2>(), actor.As<T3>())
                : throw new ArgumentNullException(nameof(actor));

        public static ActorWithComponents<T1, T2, T3, T4> WithComponents<T1, T2, T3, T4>(this IActor actor)
            where T1 : class where T2 : class where T3 : class where T4 : class
            => actor is not null
                ? new ActorWithComponents<T1, T2, T3, T4>(actor, actor.As<T1>(), actor.As<T2>(), actor.As<T3>(), actor.As<T4>())
                : throw new ArgumentNullException(nameof(actor));

        public static IEnumerable<ActorWithComponents<T1>> WithComponents<T1>(this IEnumerable<IActor> actors)
            where T1 : class
            => actors?.Select(a => new ActorWithComponents<T1>(a, a.As<T1>()))
                ?? throw new ArgumentNullException(nameof(actors));

        public static IEnumerable<ActorWithComponents<T1, T2>> WithComponents<T1, T2>(this IEnumerable<IActor> actors)
            where T1 : class where T2 : class
            => actors?.Select(a => new ActorWithComponents<T1, T2>(a, a.As<T1>(), a.As<T2>()))
               ?? throw new ArgumentNullException(nameof(actors));

        public static IEnumerable<ActorWithComponents<T1, T2, T3>> WithComponents<T1, T2, T3>(this IEnumerable<IActor> actors)
            where T1 : class where T2 : class where T3 : class
            => actors?.Select(a => new ActorWithComponents<T1, T2, T3>(a, a.As<T1>(), a.As<T2>(), a.As<T3>()))
               ?? throw new ArgumentNullException(nameof(actors));

        public static IEnumerable<ActorWithComponents<T1, T2, T3, T4>> WithComponents<T1, T2, T3, T4>(this IEnumerable<IActor> actors)
            where T1 : class where T2 : class where T3 : class where T4 : class
            => actors?.Select(a => new ActorWithComponents<T1, T2, T3, T4>(a, a.As<T1>(), a.As<T2>(), a.As<T3>(), a.As<T4>()))
               ?? throw new ArgumentNullException(nameof(actors));

        public class ActorWithComponents<T1>
        {
            public IActor Actor { get; }
            public T1 Component1 { get; }

            public ActorWithComponents(IActor actor, T1 component1)
            {
                Actor = actor;
                Component1 = component1;
            }

            public bool AllComponentsPresent => Component1 != null;
        }

        public class ActorWithComponents<T1, T2>
            where T1 : class
            where T2 : class
        {
            public IActor Actor { get; }
            public T1 Component1 { get; }
            public T2 Component2 { get; }

            public ActorWithComponents(IActor actor, T1 component1, T2 component2)
            {
                Actor = actor;
                Component1 = component1;
                Component2 = component2;
            }

            public bool AllComponentsPresent => Component1 != null && Component2 != null;
        }

        public class ActorWithComponents<T1, T2, T3>
            where T1 : class
            where T2 : class
            where T3 : class
        {
            public IActor Actor { get; }
            public T1 Component1 { get; }
            public T2 Component2 { get; }
            public T3 Component3 { get; }

            public ActorWithComponents(IActor actor, T1 component1, T2 component2, T3 component3)
            {
                Actor = actor;
                Component1 = component1;
                Component2 = component2;
                Component3 = component3;
            }

            public bool AllComponentsPresent =>
                Component1 != null && Component2 != null && Component3 != null;
        }

        public class ActorWithComponents<T1, T2, T3, T4>
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
        {
            public IActor Actor { get; }
            public T1 Component1 { get; }
            public T2 Component2 { get; }
            public T3 Component3 { get; }
            public T4 Component4 { get; }

            public ActorWithComponents(IActor actor, T1 component1, T2 component2, T3 component3, T4 component4)
            {
                Actor = actor;
                Component1 = component1;
                Component2 = component2;
                Component3 = component3;
                Component4 = component4;
            }

            public bool AllComponentsPresent =>
                Component1 != null && Component2 != null && Component3 != null && Component4 != null;
        }

        #endregion
    }
}
