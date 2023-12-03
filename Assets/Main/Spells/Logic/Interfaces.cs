using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    public interface ISpacePoint
    {
        Vector2 Position { get; }
    }

    public interface ISpaceObject : ISpacePoint
    {
        Vector2 Size { get; }
        float Rotation { get; }
    }

    public interface IPhysics
    {
        void ApplyForce(Vector2 force);
        void ApplyImpulse(Vector2 force);
    }

    public interface IIndex
    {
        IEnumerable<T> Filter<T>(IEnumerable<T> values);
    }

    public interface IDistanceIndex : IIndex
    {
    }

    public interface IDamageIndex : IIndex
    {
    }

    public interface ITimeIndex : IIndex
    {
    }

    public interface ISpaceIndex : IIndex
    {
    }

    public interface IMultiplier
    {
        float Multiply(float value);
    }

    public interface IDistanceMultiplier : IMultiplier
    {
    }

    public interface IDamageMultiplier : IMultiplier
    {
    }

    public interface ITimeMultiplier : IMultiplier
    {
    }

    public interface IDispellable
    {
    }

    public interface IConsumable
    {
        void Consume(ElementsValues energy);
        ElementsValues Contain { get; }
    }

    public interface IAccumulatable
    {
        void Accumulate(ElementsValues energy);
        ElementsValues Accumulated { get; }
    }

    public interface IDeflectable
    {
        void Accumulate(ElementsValues energy);
        ElementsValues GetAccumulated();
    }

    public interface ISplitable
    {

    }

    public interface ICopyble
    {
        public object Copy(object obj);
    }

    public interface ICopyble<T> : ICopyble
    {
        public T Copy(T obj);
    }

    public interface ILinkable
    {

    }

    public interface IEatable
    {

    }

    public interface IRottable
    {

    }

    public interface IEffect
    {
        object Target { get; }
        Action<object> Action { get; }
    }

    public interface IChangableEffect : IEffect
    {
        new Action<object> Action { set; }
    }

    public interface IPublicEffect : IEffect
    {
    }

    public static class SpellsExtension
    {
        //public static IEnumerable<T> OfType<T, V>(this IEnumerable<V> values) where T : class
        //{
        //    return values.Select(x => x as T).Where(x => x != null);
        //}

        public static IEnumerable<T> Where<T>(this IEnumerable<T> values, IIndex index)
        {
            if (index == null)
            {
                return values;
            }

            return index.Filter(values);
        }

        public static float Multiply(this float value, IMultiplier multiplier)
        {
            return multiplier.Multiply(value);
        }

        public static int Multiply(this int value, IMultiplier multiplier)
        {
            return (int)multiplier.Multiply(value);
        }

        public static IEnumerable<float> Multiply(this IEnumerable<float> value, IMultiplier multiplier)
        {
            return value.Select(x => multiplier.Multiply(x));
        }

        public static IEnumerable<int> Multiply(this IEnumerable<int> value, IMultiplier multiplier)
        {
            return value.Select(x => (int)multiplier.Multiply(x));
        }

        public static void Invoke(this IEffect effect)
        {
            effect.Action.Invoke(effect.Target);
        }
    }
}
