using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UNet.Core.Realization;
using UnityEditor;
using UnityEngine;

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

        public static void SpritesTable<T>(string title,
            IEnumerable<T> list, Action<T> draw,
            Func<T, Sprite> spriteSelector, Action<T, Sprite> spriteSetter,
            Func<T, float> orderXSelector, Action<T, float> orderXSetter,
            Func<T, float> orderYSelector, Action<T, float> orderYSetter,
            Func<Sprite, T> createItem, Action<T> addItem,
            ref bool showAdvanced)
        {
            showAdvanced = EditorGUILayout.BeginFoldoutHeaderGroup(showAdvanced, title);
            if (!showAdvanced)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            var items = (list ?? Enumerable.Empty<T>()).ToList();
            if (items.Count == 0)
                EditorGUILayout.LabelField("Empty");

            var xValues = items.Select(orderXSelector).Distinct().OrderBy(x => x).ToList();
            var yValues = items.Select(orderYSelector).Distinct().OrderBy(y => y).ToList();

            // Заголовки столбцов (X)
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Y\\X", GUILayout.Width(50));
            foreach (var x in xValues)
            {
                var newX = EditorGUILayout.FloatField(x, GUILayout.Width(70));
                if (!Mathf.Approximately(newX, x))
                    foreach (var it in items.Where(i => Mathf.Approximately(orderXSelector(i), x)))
                        orderXSetter(it, newX);
            }
            EditorGUILayout.EndHorizontal();

            // Строки
            foreach (var y in yValues)
            {
                EditorGUILayout.BeginHorizontal();
                var newY = EditorGUILayout.FloatField(y, GUILayout.Width(50));
                if (!Mathf.Approximately(newY, y))
                    foreach (var it in items.Where(i => Mathf.Approximately(orderYSelector(i), y)))
                        orderYSetter(it, newY);

                foreach (var x in xValues)
                {
                    var cell = items.Where(i =>
                        Mathf.Approximately(orderXSelector(i), x) &&
                        Mathf.Approximately(orderYSelector(i), y)).ToList();

                    EditorGUILayout.BeginVertical(GUILayout.Width(70));
                    if (cell.Count == 0)
                        EditorGUILayout.LabelField("—", GUILayout.Width(64), GUILayout.Height(20));
                    else
                        foreach (var item in cell)
                        {
                            var spr = spriteSelector(item);
                            var newSpr = (Sprite)EditorGUILayout.ObjectField(spr, typeof(Sprite), false,
                                GUILayout.Width(64), GUILayout.Height(64));
                            if (newSpr != spr)
                                spriteSetter(item, newSpr);
                            draw?.Invoke(item);
                        }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }

            // Drop zone
            var dropRect = GUILayoutUtility.GetRect(0, 30, GUILayout.ExpandWidth(true));
            GUI.Box(dropRect, "Drop Sprites Here", EditorStyles.helpBox);

            var evt = Event.current;
            if (dropRect.Contains(evt.mousePosition) &&
                (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform))
            {
                var sprites = DragAndDrop.objectReferences.OfType<Sprite>().ToList();
                if (sprites.Count > 0)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        var existing = items.Select(spriteSelector).Where(s => s != null).ToHashSet();
                        foreach (var sprite in sprites.Where(s => !existing.Contains(s)))
                        {
                            var item = createItem(sprite);
                            addItem(item);
                            existing.Add(sprite);
                        }
                    }
                    evt.Use();
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public static void SpritesTableReadonly<T>(string title,
            IEnumerable<T> list, Action<T> draw,
            Func<T, Sprite> spriteSelector,
            Func<T, float> orderXSelector,
            Func<T, float> orderYSelector,
            ref bool showAdvanced)
        {
            showAdvanced = EditorGUILayout.BeginFoldoutHeaderGroup(showAdvanced, title);
            if (!showAdvanced)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            var items = (list ?? Enumerable.Empty<T>()).ToList();
            if (items.Count == 0)
                EditorGUILayout.LabelField("Empty");

            var xValues = items.Select(orderXSelector).Distinct().OrderBy(x => x).ToList();
            var yValues = items.Select(orderYSelector).Distinct().OrderBy(y => y).ToList();

            // Заголовки столбцов (X)
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Y\\X", GUILayout.Width(50));
            foreach (var x in xValues)
                EditorGUILayout.LabelField(x.ToString("0.##"), GUILayout.Width(70));
            EditorGUILayout.EndHorizontal();

            // Строки
            foreach (var y in yValues)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(y.ToString("0.##"), GUILayout.Width(50));

                foreach (var x in xValues)
                {
                    var cell = items.Where(i =>
                        Mathf.Approximately(orderXSelector(i), x) &&
                        Mathf.Approximately(orderYSelector(i), y)).ToList();

                    EditorGUILayout.BeginVertical(GUILayout.Width(70));
                    if (cell.Count == 0)
                        EditorGUILayout.LabelField("—", GUILayout.Width(64), GUILayout.Height(20));
                    else
                        foreach (var item in cell)
                        {
                            var spr = spriteSelector(item);
                            using (new EditorGUI.DisabledScope(true))
                                EditorGUILayout.ObjectField(spr, typeof(Sprite), false, GUILayout.Width(64), GUILayout.Height(64));
                            draw?.Invoke(item);
                        }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}