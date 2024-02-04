using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Spells.Logic
{
    public interface ITaggable
    {
        public List<string> Tags { get; }
    }

    public interface ISpawnable
    {
        public object Spawn();
    }

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

    public interface ITouches
    {
        event Action OnTouch;
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

    public interface IContainsEnergy
    {
        ElementsValues ContainedEnergy { get; }
    }

    public interface IDeflectable
    {
    }

    public interface IVanishable
    {
        void Vanish();
    }

    public interface IDispellable
    {
        void Dispell();
    }

    public interface IExplosive
    {
        void Explode();
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

    public interface IReadable
    {
        public string GetReadText();
    }

    public interface IMagnetic
    {
        float MagneticPower { get; }
    }

    public interface IBook
    {
        string BookName { get; }
    }
}
