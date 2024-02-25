using System;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components.Interfaces
{
    public interface IObjectComponent
    {

    }

    public interface ISpaceObjectComponent : IObjectComponent
    {
        Vector2 Size { get; }
        float Rotation { get; }
        Vector2 Position { get; }
    }

    public interface ICopybleComponent : IObjectComponent
    {
        object Copy();
    }

    public interface IFlammableComponent : IObjectComponent
    {
        float FuelEnergy { get; set; }
        float FuelEnergyBurnoutSpeed { get; set; }
        float EnergyToStartFire { get; set; }
    }
}
