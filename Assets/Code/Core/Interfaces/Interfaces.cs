using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Model;
using CrazyWizardNotes.Model.EventArguments;

namespace CrazyWizardNotes.Interfaces
{
    public interface IIntelegent
    {

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
        bool Burning { get; set; }
        float MaxFuelEnergy { get; set; }
        float CurrentFuelEnergy { get; set; }
        float FuelEnergyBurnoutSpeed { get; set; }
        float EnergyToStartBurning { get; set; }
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
