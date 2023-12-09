using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;

public class PhysicalObject : MonoBehaviour, IPhysical, ISpaceObject, ICopyble
{
    #region Поля

    public Rigidbody2D Rigidbody;

    #endregion

    #region Обычное поведение


    #endregion

    #region Поведение заклинаний

    Vector2 ISpacePoint.Position => transform.position;
    Vector2 ISpaceObject.Size => transform.localScale;
    float ISpaceObject.Rotation => transform.localRotation.eulerAngles.x;

    void IPhysical.ApplyForce(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Force);
    void IPhysical.ApplyImpulse(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);
    object ICopyble.Copy() => Instantiate(this);

    #endregion
}