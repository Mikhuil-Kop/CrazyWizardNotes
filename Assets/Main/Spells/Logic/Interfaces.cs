using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Spells.Logic
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
}
