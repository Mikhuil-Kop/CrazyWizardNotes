using System;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtension
{
    public static IEnumerable<Transform> GetChildren(this Transform transform)
    {
        foreach (Transform t in transform)
        {
            yield return t;
        }
    }

    public static IEnumerable<RectTransform> GetChildren(this RectTransform transform)
    {
        foreach (RectTransform t in transform)
        {
            yield return t;
        }
    }

    public static Vector3 Multiply(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector2 Multiply(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public static Vector2 Multiply(this Vector2 a, Vector3 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public static Vector2 Multiply(this Vector3 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public static T GetComponentRequired<T>(this GameObject gameObject) where T : class
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject), "GameObject is null");
        }

        var component = gameObject.GetComponent<T>();

        if (component == null)
        {
            throw new MissingComponentException(
                $"Required component '{typeof(T).Name}' not found on GameObject '{gameObject.name}'");
        }

        return component;
    }
}