using System;
using UnityEngine;

namespace UNet.Animations.Realization
{
    [Obsolete("Только для копирования кода")]
    internal class AnimatorExample
    {
        private Animator Animator;

        #region Int

        private readonly static int _AAAHash = Animator.StringToHash("AAA");
        private int _AAA;
        public int AAA
        {
            get => _AAA;
            set
            {
                if (_AAA != value)
                {
                    _AAA = value;
                    Animator.SetInteger(_AAAHash, value);
                }
            }
        }

        #endregion


        #region Bool

        private readonly static int _BBBHash = Animator.StringToHash("BBB");
        private bool _BBB;
        public bool BBB
        {
            get => _BBB;
            set
            {
                if (_BBB != value)
                {
                    _BBB = value;
                    Animator.SetBool(_BBBHash, value);
                }
            }
        }

        #endregion


        #region Float

        private readonly static int _CCCHash = Animator.StringToHash("CCC");
        private float _CCC;
        public float CCC
        {
            get => _CCC;
            set
            {
                if (_CCC != value)
                {
                    _CCC = value;
                    Animator.SetFloat(_CCCHash, value);
                }
            }
        }

        #endregion
    }
}
