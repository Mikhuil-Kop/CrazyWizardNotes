using CrazyWizardNotes.Creatures.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class SimpleClaimable : IClaimable, IDisposable
    {
        private ConcurrentDictionary<string, SemaphoreSlim> Semaphores { get; }
        private int MaxClaimCount { get; }
        private bool Disposed { get; set; }

        //public SimpleClaimable(int maxClaimCount)
        public SimpleClaimable()
        {
            var maxClaimCount = 1; // TODO: ПОПРАВИТЬ.
            if (maxClaimCount < 1) throw new ArgumentOutOfRangeException(nameof(maxClaimCount));

            Semaphores = new();
            MaxClaimCount = maxClaimCount;
            Disposed = false;
        }

        public bool IsClaimed(string group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            return Semaphores.TryGetValue(group, out var semaphore) && semaphore.CurrentCount == 0;
        }

        public bool TryClaim(string group, out IDisposable disposable)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            var semaphore = Semaphores.GetOrAdd(group, _ => new SemaphoreSlim(MaxClaimCount, MaxClaimCount));

            if (semaphore.Wait(0))
            {
                disposable = new ClaimToken
                {
                    Owner = this,
                    Semaphore = semaphore,
                    Group = group
                };

                return true;
            }
            else
            {
                disposable = null;
                return false;
            }
        }

        private void UnclaimToken(ClaimToken group)
        {
            group.Semaphore.Release();
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;

            foreach (var semaphore in Semaphores.Values)
            {
                semaphore.Dispose();
            }

            Semaphores.Clear();
        }

        private class ClaimToken : IDisposable
        {
            public SimpleClaimable Owner { get; set; }
            public SemaphoreSlim Semaphore { get; set; }
            public string Group { get; set; }
            private bool Disposed { get; set; }

            public void Dispose()
            {
                if (Disposed)
                {
                    return;
                }

                Owner.UnclaimToken(this);

                Disposed = true;
            }
        }
    }
}