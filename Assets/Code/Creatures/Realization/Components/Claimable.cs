using CrazyWizardNotes.Creatures.Abstractions;
using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class SimpleClaimable : IClaimable
    {
        private Dictionary<string, object> ClaimGroups { get; }

        public SimpleClaimable()
        {
            ClaimGroups = new();
        }

        public bool IsClaimed(string group, object owner)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            return ClaimGroups.TryGetValue(group, out var claim) && claim != owner;
        }

        public bool TryClaim(string group, object owner, out IDisposable disposable)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            if(ClaimGroups.TryGetValue(group, out var claim) && claim != owner)
            {
                disposable = null;

                return false;
            }
            else
            {
                ClaimGroups[group] = owner;

                disposable = new ClaimToken
                {
                    Claimable = this,
                    Group = group
                };

                return true;
            }
        }

        private void UnclaimToken(ClaimToken group)
        {
            ClaimGroups.Remove(group.Group);
        }

        private class ClaimToken : IDisposable
        {
            public SimpleClaimable Claimable { get; set; }
            public string Group { get; set; }
            private bool Disposed { get; set; }

            public void Dispose()
            {
                if (Disposed)
                {
                    return;
                }

                Claimable.UnclaimToken(this);

                Disposed = true;
            }
        }
    }
}