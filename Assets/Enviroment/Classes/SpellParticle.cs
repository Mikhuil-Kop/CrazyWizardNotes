using UnityEngine;
using System.Collections;
using CrazyWizardNotes.Spells.Logic;

public class SpellParticle : MonoBehaviour, IPhysical, ISpaceObject, ICopyble, IContainsEnergy
{
    #region Поля

    public Rigidbody2D Rigidbody;

    public Animator Animator;

    public ElementsValues Energy;

    #endregion

    #region Обычное поведение

    public void Update()
    {
        //Energy.
    }

    #endregion

    #region Поведение заклинаний

    Vector2 ISpacePoint.Position => transform.position;
    Vector2 ISpaceObject.Size => transform.localScale;
    float ISpaceObject.Rotation => transform.localRotation.eulerAngles.x;

    void IPhysical.ApplyForce(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Force);
    void IPhysical.ApplyImpulse(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);
    object ICopyble.Copy() => Instantiate(this);
    ElementsValues IContainsEnergy.ContainedEnergy => Energy;

    #endregion
}

