using System.Linq;
using CrazyWizardNotes.Interface;
using CrazyWizardNotes.Realisation;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class DragAndDropService : SingletonBehavior<IDragAndDropService>, IDragAndDropService
    {
        private IDragger CurrentDragger;
        private Vector2 CurrentPivot;


        private void Drag(IDragger dragger)
        {
            if (CurrentDragger != null)
            {
                Drop(null);
            }

            if (!dragger.CanDrag())
            {
                return;
            }

            if (dragger?.CurrentDropper?.CurrentDragger != null)
            {
                dragger.CurrentDropper.CurrentDragger = null;
            }

            if (dragger?.CurrentDropper != null)
            {
                dragger.CurrentDropper = null;
            }

            CurrentDragger = dragger;
            CurrentPivot = new Vector2();

            dragger.ToTop();
        }

        private void Drop(IDropper dropper)
        {
            if (CurrentDragger == null)
            {
                return;
            }

            if (dropper != null && dropper.CanDrop(CurrentDragger) && CurrentDragger.CanDrop(dropper))
            {
                CurrentDragger.Position = dropper.Position;

                CurrentDragger.CurrentDropper = dropper;
                dropper.CurrentDragger = CurrentDragger;

                CurrentDragger.OnDrop(dropper);
                dropper.OnDrop(CurrentDragger);
            }

            CurrentDragger = null;
        }

        private void Update()
        {
            var inputService = InputService.Instance;

            if (CurrentDragger != null)
            {
                CurrentDragger.Position = inputService.ScreenPointer + CurrentPivot;
            }

            if (inputService.GetButtonDown("mouse"))
            {
                var dragger = inputService.ScreenPointedTargets
                    .Select(x => x as IDragger)
                    .FirstOrDefault(x => x != null);

                if (dragger != null)
                {
                    Drag(dragger);

                    if (CurrentDragger != null)
                    {
                        CurrentPivot = CurrentDragger.Position - inputService.ScreenPointer;
                    }
                }
            }

            if (inputService.GetButtonUp("mouse"))
            {
                var dropper = inputService.ScreenPointedTargets
                    .Select(x => x as IDropper)
                    .FirstOrDefault(x => x != null);

                Drop(dropper);
            }
        }
    }
}

