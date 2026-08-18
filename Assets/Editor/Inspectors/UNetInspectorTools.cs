using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UNet.Core.Realization;
using UnityEditor;

namespace CrazyWizardNotes.Editor
{
    public static class UNetInspectorTools
    {
        public static void LayoutProvider(UNetServiceProvider serviceProvider, ref bool showAdvanced)
        {
            showAdvanced = EditorGUILayout.BeginFoldoutHeaderGroup(showAdvanced, "ServiceProvider");

            if (!showAdvanced)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            if (serviceProvider == null)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            foreach (var service in serviceProvider.GetServicesInfo().Where(x => x.IsCreated))
            {
                EditorGUILayout.LabelField($"- {service.TargetType.Name} ({service.Key ?? "NULL"}) - {service.OriginalType?.Name ?? "Factory"}", service.IsCreated ? EditorStyles.boldLabel : EditorStyles.label);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public static void LayoutList<T>(IEnumerable<T> list, Func<T, string> foo, ref bool showAdvanced)
        {
            showAdvanced = EditorGUILayout.BeginFoldoutHeaderGroup(showAdvanced, "Components");

            if (!showAdvanced)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            foreach (var service in list ?? new T[0])
            {
                EditorGUILayout.LabelField($"- {foo(service)}");
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public static void LayoutEnum<T, E>(Expression<Func<T, string>> property) where T : class where E : Enum
        {
        }

        public static bool Field(string name, bool value)
        {
            return EditorGUILayout.Toggle(name, value);
        }

        public static int Field(string name, int value)
        {
            return EditorGUILayout.IntField(name, value);
        }

        public static float Field(string name, float value)
        {
            return EditorGUILayout.FloatField(name, value);
        }

        public static string Field(string name, string value)
        {
            return EditorGUILayout.TextField(name, value);
        }
    }
}