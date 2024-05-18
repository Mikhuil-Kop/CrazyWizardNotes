using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.UI.Realisation
{
    public class DialogUI : MonoBehaviour, IDialogUI
    {
        [SerializeField]
        private RectTransform Transform;
        [SerializeField]
        private TextMeshProUGUI Text;

        private Queue<DialogItem> DialogItems { get; set; }

        public void StartDialog(XElement dialogData)
        {
            var dialogText = dialogData.Element("Text");
            DialogItems = new Queue<DialogItem>();

            foreach (var element in dialogText.Elements())
            {
                var item = Parse(element);
                DialogItems.Enqueue(item);
            }
        }

        public void SetPosition(Vector2 position)
        {
            Transform.anchoredPosition = position;
        }

        private void Update()
        {
            if (DialogItems.Count == 0)
            {
                return;
            }

            var args = new DialogUpdateEventArgs()
            {
                DeltaTime = Time.deltaTime,
                Text = Text.text,
                Finished = false
            };

            var dialogItem = DialogItems.Peek();
            dialogItem.Update(args);

            Text.text = args.Text;

            if (args.Finished)
            {
                DialogItems.Dequeue();
            }
        }

        private DialogItem Parse(XElement element)
        {
            if (element.Name == "p")
            {
                return new TextDialogItem(element.Value)
                {
                    Speed = element.Attribute("speed")?.Value?.ParseToFloat() ?? 1,
                };
            }

            if (element.Name == "delay")
            {
                return new DelayDialogItem(element.Attribute("time")?.Value?.ParseToFloat() ?? 0);
            }

            throw new Exception($"Неизвестный элемен диалога: {element.Name}");
        }

        #region Классы

        private class DialogUpdateEventArgs
        {
            public float DeltaTime { get; set; }
            public bool Finished { get; set; }
            public string Text { get; set; }
        }

        private abstract class DialogItem
        {
            protected float FullTime { get; set; } = 0;

            public virtual void Update(DialogUpdateEventArgs args)
            {
                FullTime += args.DeltaTime;
            }
        }

        private class TextDialogItem : DialogItem
        {
            public float Speed { get; set; }

            private string FullText { get; set; }
            private string CurrentText { get; set; }
            private const float SymbolsPerSecond = 10;

            public TextDialogItem(string fullText)
            {
                FullText = fullText;
            }

            public override void Update(DialogUpdateEventArgs args)
            {
                base.Update(args);

                var symbols = Mathf.FloorToInt(FullTime * Speed * SymbolsPerSecond);

                if (FullText.Length > symbols)
                {
                    args.Text = FullText.Substring(0, symbols);
                }
                else
                {
                    args.Text = FullText;
                    args.Finished = true;
                }
            }
        }

        private class DelayDialogItem : DialogItem
        {
            private float DelayTime { get; set; }

            public DelayDialogItem(float delayTime)
            {
                DelayTime = delayTime;
            }

            public override void Update(DialogUpdateEventArgs args)
            {
                base.Update(args);

                if (FullTime >= DelayTime)
                {
                    args.Finished = true;
                }
            }
        }

        #endregion
    }
}