using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class DispatcherService : SingletonBehavior<IDispatcherService>, IDispatcherService
    {
        private Queue<Action> IncomeQueue { get; set; }
        private Queue<Action> ActiveQueue { get; set; }

        protected override void Awake()
        {
            base.Awake();
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

        protected void Update()
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