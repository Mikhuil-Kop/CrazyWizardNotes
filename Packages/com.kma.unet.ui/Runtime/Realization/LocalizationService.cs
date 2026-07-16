using UNet.Core.Abstractions;
using UNet.UI.Abstractions;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace UNet.UI.Realization
{
    public class LocalizationService : ILocalizationService
    {
        public ILogger Logger { get; set; }
        public string GetLocalizedString(TextString text)
        {
            if (!text.Localized)
            {
                return text.Text;
            }

            var str = new LocalizedString(text.Group ?? "default", text.Text);

            foreach (var param in text.Params)
            {
                var key = param.Key;
                var value = param.Value;

                switch (value)
                {
                    case string s:
                        str.Add(key, new StringVariable { Value = s });
                        break;
                    case int i:
                        str.Add(key, new IntVariable { Value = i });
                        break;
                    case float f:
                        str.Add(key, new FloatVariable { Value = f });
                        break;
                    case bool b:
                        str.Add(key, new BoolVariable { Value = b });
                        break;
                    case double d:
                        str.Add(key, new DoubleVariable { Value = d });
                        break;
                    case long l:
                        str.Add(key, new LongVariable { Value = l });
                        break;
                    default:
                        str.Add(key, new StringVariable { Value = value?.ToString() ?? "" });
                        break;
                }
            }

            var result = str.GetLocalizedString();

            if (result.StartsWith("No translation found"))
            {
                Logger.LogInfo("No translation found for text: " + text.Text);
                return "!!!@" + text.Text;
            }

            return result;
        }

    }
}
