using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CrazyWizardNotes.Common.Services;

namespace CrazyWizardNotes.SimpleScripts
{
    public class ParalaxMapEffect : MonoBehaviour
    {
        [SerializeField]
        private float BaseLine;
        [SerializeField]
        private Vector2 BaseCam;
        [SerializeField]
        private Vector2 Modifier;

        private IInputService Input { get; set; }

        private List<(Transform, Vector3)> Childs { get; set; }


        private void Start()
        {
            Input = InputService.Instance;

            Childs = transform.GetChildren()
                .Select(x => (x, x.transform.position))
                .ToList();
        }

        private void Update()
        {
            var modif = (Vector3)(Input.CameraPosition - BaseCam).Multiply(Modifier);

            foreach (var t in Childs)
            {
                t.Item1.position = t.Item2 + (t.Item2.z - BaseLine) * modif;
            }
        }
    }
}