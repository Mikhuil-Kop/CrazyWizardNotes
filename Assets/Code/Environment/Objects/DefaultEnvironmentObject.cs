using System;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using CrazyWizardNotes.Environment.Components.Interfaces;
using CrazyWizardNotes.Model;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Spells;
using CrazyWizardNotes.Model.EventArguments;

namespace CrazyWizardNotes.Environment.Objects
{
    public class DefaultEnvironmentObject : CustomEnvironmentObjectBase,
        IPhysical, ISpaceObject, ICopyble, IFlammable, IJoint, IVanishable, IDispellable, IExplosive, IDestructible, ITouches
    {
        #region Поля

        protected IPhysicalComponent PhysicalComponent { get; set; }
        protected ISpaceObjectComponent SpaceObjectComponent { get; set; }
        protected ICopybleComponent CopybleComponent { get; set; }
        protected IFlammableComponent FlammableComponent { get; set; }
        protected IJointComponent JointComponent { get; set; }
        protected IDestructionComponent DestructionComponent { get; set; }

        #endregion

        #region Логика

        protected override void LocateComponents(IRealisationProvider provider)
        {
            PhysicalComponent = provider.Get<IPhysicalComponent>();
            SpaceObjectComponent = provider.Get<ISpaceObjectComponent>();
            CopybleComponent = provider.Get<ICopybleComponent>();
            FlammableComponent = provider.Get<IFlammableComponent>();
            JointComponent = provider.Get<IJointComponent>();
            DestructionComponent = provider.Get<IDestructionComponent>();
        }

        #endregion

        #region Интерфейсы

        Vector2 ISpaceObject.Size => SpaceObjectComponent.Size;
        float ISpaceObject.Rotation => SpaceObjectComponent.Rotation;
        Vector2 ISpacePoint.Position => SpaceObjectComponent.Position;

        void IPhysical.ApplyForce(Vector2 force) => PhysicalComponent.ApplyForce(force);
        void IPhysical.ApplyImpulse(Vector2 force) => PhysicalComponent.ApplyImpulse(force);

        event Action<OnTouchEventArgs> ITouches.OnTouch
        {
            add { }
            remove { PhysicalComponent.O}
        }

        object ICopyble.Copy() => CopybleComponent.Copy();

        float IFlammable.FuelEnergy
        {
            get => FlammableComponent.FuelEnergy;
            set => FlammableComponent.FuelEnergy = value;
        }
        float IFlammable.FuelEnergyBurnoutSpeed
        {
            get => FlammableComponent.FuelEnergyBurnoutSpeed;
            set => FlammableComponent.FuelEnergyBurnoutSpeed = value;
        }
        float IFlammable.EnergyToStartFire
        {
            get => FlammableComponent.EnergyToStartFire;
            set => FlammableComponent.EnergyToStartFire = value;
        }

        event OnJointBreakDelegate IJoint.OnJointBreak
        {
            add => JointComponent.OnJointBreak += value;
            remove => JointComponent.OnJointBreak -= value;
        }
        void IJoint.Connect(IPhysical target1, IPhysical target2) => JointComponent.Connect(target1, target2);

        bool IVanishable.CanVanish => DestructionComponent.CanVanish;
        void IVanishable.Vanish() => DestructionComponent.Vanish();
        event Action IVanishable.OnVanish
        {
            add => DestructionComponent.OnVanish += value;
            remove => DestructionComponent.OnVanish -= value;
        }

        bool IDispellable.CanDispell => DestructionComponent.CanDispell;
        void IDispellable.Dispell() => DestructionComponent.Dispell();
        event Action IDispellable.OnDispell
        {
            add => DestructionComponent.OnDispell += value;
            remove => DestructionComponent.OnDispell -= value;
        }

        bool IExplosive.CanExplode => DestructionComponent.CanExplode;
        void IExplosive.Explode() => DestructionComponent.Explode();
        event Action IExplosive.OnExplode
        {
            add => DestructionComponent.OnExplode += value;
            remove => DestructionComponent.OnExplode -= value;
        }

        bool IDestructible.CanDestruct => DestructionComponent.CanDestruct;
        void IDestructible.Destruct() => DestructionComponent.Destruct();
        event Action IDestructible.OnDestruct
        {
            add => DestructionComponent.OnDestruct += value;
            remove => DestructionComponent.OnDestruct -= value;
        }

        #endregion
    }
}