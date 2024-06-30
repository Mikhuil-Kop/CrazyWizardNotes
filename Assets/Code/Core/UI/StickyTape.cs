using CrazyWizardNotes.Common.Services;
using UnityEngine;

namespace CrazyWizardNotes.Interface
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Animator))]
    public class StickyTape : MonoBehaviour, IStickyTape
    {
        private RectTransform Rect { get; set; }

        private Animator Animator { get; set; }

        [SerializeField]
        private RectTransform MinAncor;

        [SerializeField]
        private RectTransform MaxAncor;

        [SerializeField]
        private float Value;

        [SerializeField]
        private Vector2 MinPosition;

        [SerializeField]
        private Vector2 MaxPosition;


        private void Awake()
        {
            Rect = GetComponent<RectTransform>();
            Animator = GetComponent<Animator>();
            Animator.speed = 0;

            Value = 0f;
            MinPosition = MinAncor.position;
            MaxPosition = MaxAncor.position;
        }

        private void Update()
        {
            Value = 0f;
            MinPosition = MinAncor.position;
            MaxPosition = MaxAncor.position;

            var inputService = InputService.Instance;

            var pos1 = inputService.ScreenPointer;
            var dir1 = pos1 - MinPosition;
            var dir2 = MaxPosition - MinPosition;

            Debug.Log(pos1);

            Value = Vector2.Dot(dir1.normalized, dir2.normalized) * dir1.magnitude / dir2.magnitude;

            Debug.Log(Value);

            Animator.Play("Main", 0, Value);
        }
    }

    public interface IStickyTape
    {
        //float Value { get; }
        //Vector2 MinPosition { get; }
        //Vector2 MaxPosition { get; }
    }
}
