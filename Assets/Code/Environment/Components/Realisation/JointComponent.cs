using CrazyWizardNotes.Environment.Components.Interfaces;
using CrazyWizardNotes.Model;
using CrazyWizardNotes.Spells.Logic;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components.Realisation
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JointComponent : ObjectComponentBase, IJointComponent
    {
        private Joint2D Joint1 { get; set; }
        private Joint2D Joint2 { get; set; }
        private IPhysical Target1 { get; set; }
        private IPhysical Target2 { get; set; }

        public event OnJointBreakDelegate OnJointBreak;

        private void Awake()
        {
            var joints = GetComponents<Joint2D>();

            if (joints.Length != 2)
            {
                throw new System.Exception("JointComponent требует наличие 2 элементов Joint2D");
            }

            Joint1 = joints[0];
            Joint2 = joints[1];
        }

        private void AttachRigidbody(Joint2D joint, Rigidbody2D rigidbody)
        {
            joint.connectedBody = rigidbody;
        }

        #region Интерфейс

        void IJointComponent.Connect(IPhysical target1, IPhysical target2)
        {
            var rigidbody1 = (target1 as MonoBehaviour).GetComponent<Rigidbody2D>();
            var rigidbody2 = (target2 as MonoBehaviour).GetComponent<Rigidbody2D>();

            AttachRigidbody(Joint1, rigidbody1);
            AttachRigidbody(Joint2, rigidbody2);

            Target1 = target1;
            Target2 = target2;
        }

        #endregion
    }
}