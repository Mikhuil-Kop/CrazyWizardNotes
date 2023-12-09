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

    public interface IPhysical
    {
        void ApplyForce(Vector2 force);
        void ApplyImpulse(Vector2 force);
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
    }

    public interface ISplitable
    {

    }

    public interface ICopyble
    {
        public object Copy();
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
}
