using System;
using DS.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrazyWizardNotes.Editor
{
    public abstract class GraphNodeBase : Node
    {
        public virtual void Initialize(Vector2 position)
        {
            SetPosition(new Rect(position, Vector2.zero));

            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        public abstract void Draw();

        #region Инструменты

        protected TextField AddTitleField(string title, string value, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            var textField = new TextField()
            {
                value = value,
                label = title
            };

            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            titleContainer.Add(textField);

            return textField;
        }

        protected TextField AddTextField(string title, string value, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            var textField = new TextField()
            {
                value = value,
                label = title
            };

            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            mainContainer.Add(textField);

            return textField;
        }

        protected TextField AddTextArea(string label, string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            var textField = new TextField()
            {
                value = value,
                label = title,
                multiline = true
            };

            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            mainContainer.Add(textField);

            return textField;
        }

        protected Button AddButton(string title, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = title
            };

            mainContainer.Add(button);

            return button;
        }

        protected Foldout AddFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        protected Port AddInputPort(string title, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = InstantiatePort(Orientation.Horizontal, Direction.Input, capacity, typeof(bool));

            port.portName = title;

            inputContainer.Add(port);

            return port;
        }

        protected Port AddOutputPort(string title, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(bool));

            port.portName = title;

            outputContainer.Add(port);

            return port;
        }

        #endregion
    }
}