using CrazyWizardNotes.UI.Abstractions;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UNet.Core.Abstractions;
using UNet.Core.Realization;

namespace CrazyWizardNotes.UI.Realization
{
    public class ActiveLogger : DebugLogger, IActiveLogger, IHandlesUpdate
    {
        private TextMeshProUGUI UIText { get; }
        private LinkedList<Log> Logs { get; }
        // TODO: переделать на словарь

        public ActiveLogger(TextMeshProUGUI uIText)
        {
            UIText = uIText;
            Logs = new LinkedList<Log>();
        }

        public void LogActive(float lifetime, string key, string message)
        {
            var current = Logs.FirstOrDefault(x => x.Key == key);

            if (current != null)
            {
                current.Message = message;
                current.Time = lifetime;
            }
            else
            {
                Logs.AddLast(new Log()
                {
                    Time = lifetime,
                    Key = key,
                    Message = message
                });
            }

            UpdateText();
        }

        public void OnUpdate(float deltaTime)
        {
            var updated = false;

            foreach (var log in Logs.ToArray())
            {
                log.Time -= deltaTime;

                if (log.Time <= 0f)
                {
                    Logs.Remove(log);
                    updated = true;
                }
            }

            if (updated)
            {
                UpdateText();
            }
        }

        private void UpdateText()
        {
            UIText.text = string.Join('\n', Logs.OrderBy(x => x.Key).Select(x => $"{x.Key}: {x.Message}"));
        }

        private class Log
        {
            public float Time { get; set; }
            public string Key { get; set; }
            public string Message { get; set; }
        }
    }
}
