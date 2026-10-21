using CrazyWizardNotes.Elements.Abstractions;
using CrazyWizardNotes.Creatures.Abstractions;
using UNet.Core.Realization;
using UnityEditor;
using Inspector = UnityEditor.Editor;

namespace CrazyWizardNotes.Editor
{
    [CustomEditor(typeof(UNetActor))]
    public class UNetActorInspector : Inspector
    {
        private UNetActor actor;
        private static bool showProvider = false;
        private static bool showComponents = false;

        private void OnEnable()
        {
            actor = (UNetActor)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UNetInspectorTools.LayoutProvider(actor.ServiceProvider as UNetServiceProvider, ref showProvider);
            UNetInspectorTools.LayoutList(actor.Components, x => x.GetType().Name, ref showComponents);

            ShowElements();
            ShowCreatures();
        }

        private void ShowElements()
        {
            var t = actor.As<IHasTemperature>();

            if (t != null)
            {
                EditorGUILayout.LabelField("IHasTemperature");
                UNetInspectorTools.Field(nameof(t.Temperature), t.Temperature);
                UNetInspectorTools.Field(nameof(t.ThermalEnergy), t.ThermalEnergy);
                UNetInspectorTools.Field(nameof(t.ThermalCapacity), t.ThermalCapacity);
            }

            var m = actor.As<IHasMoisture>();

            if (m != null)
            {
                EditorGUILayout.LabelField("IHasMoisture");
                UNetInspectorTools.Field(nameof(m.Moisture), m.Moisture);
                UNetInspectorTools.Field(nameof(m.MoistureCapacity), m.MoistureCapacity);
            }

            var e = actor.As<IElectricConductor>();

            if (e != null)
            {
                EditorGUILayout.LabelField("IElectricConductor");
                UNetInspectorTools.Field(nameof(e.ElectricCharge), e.ElectricCharge);
                UNetInspectorTools.Field(nameof(e.ElectricCapacity), e.ElectricCapacity);
            }

            var f = actor.As<IFlammable>();

            if (f != null)
            {
                EditorGUILayout.LabelField("IFlammable");
                UNetInspectorTools.Field(nameof(f.IsBurning), f.IsBurning);
                UNetInspectorTools.Field(nameof(f.Flammability), f.Flammability);
                UNetInspectorTools.Field(nameof(f.FlashPointTemperature), f.FlashPointTemperature);
                UNetInspectorTools.Field(nameof(f.AutoignitionTemperature), f.AutoignitionTemperature);
            }
        }

        private void ShowCreatures()
        {
            var c = actor.As<ICreature>();

            if (c != null)
            {
                EditorGUILayout.LabelField("ICreature");
                //UNetInspectorTools.Field(nameof(c.), c.);
            }

            var e = actor.As<ICanEat>();

            if (e != null)
            {
                EditorGUILayout.LabelField("ICanEat");
                UNetInspectorTools.Field(nameof(e.CaloriesPerBite), e.CaloriesPerBite);
                UNetInspectorTools.Field(nameof(e.EatingIsFull), e.EatingIsFull);
            }

            var f = actor.As<IEatable>();

            if (f != null)
            {
                EditorGUILayout.LabelField("IEatable");
                UNetInspectorTools.Field(nameof(f.CaloriesLeft), f.CaloriesLeft);
                UNetInspectorTools.Field(nameof(f.CaloriesMax), f.CaloriesMax);
            }
        }
    }
}