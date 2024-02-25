using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Model;
using CrazyWizardNotes.Model.EventArguments;

namespace CrazyWizardNotes.Spells.Logic
{
    public interface ITaggable
    {
        List<string> Tags { get; }
    }

    public interface ISpawnable
    {
        object Spawn();
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
        event Action<OnTouchEventArgs> OnTouch;
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

    public interface IDispellable
    {
        bool CanDispell { get; }
        void Dispell();
        event Action OnDispell;
    }

    public interface IVanishable
    {
        bool CanVanish { get; }
        void Vanish();
        event Action OnVanish;
    }

    public interface IExplosive
    {
        bool CanExplode { get; }
        void Explode();
        event Action OnExplode;
    }

    public interface IDestructible
    {
        bool CanDestruct { get; }
        void Destruct();
        event Action OnDestruct;
    }

    public interface IJoint
    {
        void Connect(IPhysical target1, IPhysical target2);
        event OnJointBreakDelegate OnJointBreak;
    }

    public interface ISplitable
    {

    }

    public interface ICopyble
    {
        public object Copy();
    }

    public interface IFlammable
    {
        float FuelEnergy { get; set; }
        float FuelEnergyBurnoutSpeed { get; set; }
        float EnergyToStartFire { get; set; }
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
