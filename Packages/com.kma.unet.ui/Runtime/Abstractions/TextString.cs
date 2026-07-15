using System;
using System.Collections.Generic;
using System.Linq;

namespace UNet.UI.Abstractions
{
    // TODO: доделать. Получилось аляповато.
    public sealed class TextString
    {
        internal string Text { get; }
        internal string Group { get; }
        internal bool Localized { get; }
        internal Dictionary<string, object> Params { get; }
        internal string LocalizedPrefix => Localized ? "@" : "";

        [Obsolete("ИИ")]
        public TextString(string text, Dictionary<string, object> parameters = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                Text = string.Empty;
                Group = null;
                Localized = false;
            }
            else if (text.StartsWith("@"))
            {
                Localized = true;
                var content = text[1..].Trim();

                var dotIndex = content.IndexOf('.');
                if (dotIndex > 0 && dotIndex < content.Length - 1)
                {
                    Group = content[..dotIndex];
                    Text = content[(dotIndex + 1)..];
                }
                else if (dotIndex == content.Length - 1)
                {
                    throw new ArgumentException($"Некорректный формат localized строки: {text}. Ожидается @group.key");
                }
                else
                {
                    Group = null;
                    Text = content;
                }
            }
            else
            {
                Localized = false;
                Group = null;
                Text = text;
            }

            Params = parameters ?? new Dictionary<string, object>();
        }

        public TextString(string text, object parameters) : this(text, parameters.ReflectToDictionary())
        {

        }

        public static implicit operator TextString(string str)
        {
            return new TextString(str);
        }

        public override string ToString()
        {
            return $"{LocalizedPrefix}{Text}({string.Join(";", Params.Select(p => $"{p.Key}={p.Value}"))})";
        }
    }
}
