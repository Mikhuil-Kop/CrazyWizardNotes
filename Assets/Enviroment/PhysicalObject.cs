using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spells;

public class PhysicalObject : MonoBehaviour, Spells.IPhysics, Spells.ISpaceObject
{
    public Vector2 Position => transform.position;

    public Vector2 Size => transform.localScale;

    public float Rotation => transform.localRotation.eulerAngles.x;


    public void ApplyForce(Vector2 force)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyImpulse(Vector2 force)
    {
        throw new System.NotImplementedException();
    }
}