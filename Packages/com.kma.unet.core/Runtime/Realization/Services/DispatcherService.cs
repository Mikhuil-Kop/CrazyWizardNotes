using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Realisation
{
    public class DispatcherService : IDispatcherService, IHandlesUpdate
    {
        private Queue<Action> IncomeQueue { get; set; }
        private Queue<Action> ActiveQueue { get; set; }

        public DispatcherService()
        {
            IncomeQueue = new Queue<Action>();
            ActiveQueue = new Queue<Action>();
        }

        public void Invoke(Action action)
        {
            lock (IncomeQueue)
            {
                IncomeQueue.Enqueue(action);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var action in ActiveQueue)
            {
                action.Invoke();
            }

            ActiveQueue.Clear();

            lock (IncomeQueue)
            {
                var change = IncomeQueue;
                IncomeQueue = ActiveQueue;
                ActiveQueue = IncomeQueue;
            }
        }
    }
}