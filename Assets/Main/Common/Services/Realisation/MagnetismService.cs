using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using static UnityEngine.GraphicsBuffer;

namespace CrazyWizardNotes.Common.Services
{
    public class MagnetismService : SingletonBehavior<MagnetismService>, IMagnetismService
    {
        private LinkedList<object> RegistredObjects { get; set; }

        protected override void Awake()
        {
            base.Awake();

            RegistredObjects = new LinkedList<object>();
        }

        protected void FixedUpdate()
        {
            AsyncQueueService.Instance.Invoke(SimulateMagnetism());
        }

        public void Register<T>(T target) where T : IMagnetic, ISpacePoint
        {
            lock (RegistredObjects)
            {
                RegistredObjects.AddLast(target);
            }
        }

        public void Unregister<T>(T target) where T : IMagnetic, ISpacePoint
        {
            lock (RegistredObjects)
            {
                RegistredObjects.Remove(target);
            }
        }

        private async Task SimulateMagnetism()
        {
            MagneticItem[] items = null;

            lock (RegistredObjects)
            {
                items = RegistredObjects
                    .Select(x => new MagneticItem()
                    {
                        Target = x,
                        MagneticPower = (x as IMagnetic).MagneticPower,
                        Position = (x as ISpacePoint).Position
                    })
                    .ToArray();
            }

            await Task.Yield();

            var itemForces = items
                .Select(x =>
                {
                    var force = items
                        .Where(y => y != x)
                        .Select(y =>
                        {
                            var delta = x.Position - y.Position;
                            var distance = delta.magnitude;
                            var direction = delta / distance;

                            return direction * (x.MagneticPower * y.MagneticPower / (distance * distance));
                        })
                        .Aggregate(Vector2.zero, (a, b) => a + b);

                    return new MagneticItemForce()
                    {
                        Item = x,
                        MagneticForce = force
                    };
                })
                .ToArray();

            PhysicsDispatcherService.Instance.Invoke(() =>
            {
                foreach (var itemForce in itemForces)
                {
                    (itemForce.Item.Target as IPhysical)?.ApplyForce(itemForce.MagneticForce);
                }
            });
        }

        private class MagneticItem
        {
            public object Target { get; set; }
            public float MagneticPower { get; set; }
            public Vector2 Position { get; set; }
        }

        private class MagneticItemForce
        {
            public MagneticItem Item { get; set; }
            public Vector2 MagneticForce { get; set; }
        }
    }
}