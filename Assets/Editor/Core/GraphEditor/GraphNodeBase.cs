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
            SetPosition(new Rect(position, Vector2.one * 5));

            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        public abstract void Draw();

        #region Инструменты

        protected Label AddTitleField(string title, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            var textField = new Label()
            {
                text = title
            };

            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            textField.AddClasses(
                "ds-node__text-field",
                "ds-node__text-field__hidden",
                "ds-node__filename-text-field"
            );

            titleContainer.Insert(0, textField);

            return textField;
        }

        protected TextField AddTextField(string title, string value, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            var textField = new TextField()
            {
                value = value,
                label = title,
                multiline = false
            };

            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            mainContainer.Add(textField);

            return textField;
        }

        protected TextField AddTextArea(string title, string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
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

        protected Foldout AddFoldoutTextArea(string title, string value, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            var customDataContainer = new VisualElement();

            var foldout = new Foldout()
            {
                text = title,
                value = true
            };

            var textField = new TextField()
            {
                value = value,
                multiline = true
            };

            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            foldout.Add(textField);
            customDataContainer.Add(foldout);
            extensionContainer.Add(customDataContainer);

            customDataContainer.AddToClassList("ds-node__custom-data-container");

            return foldout;
        }

        protected Button AddButton(string title, Action onClick = null)
        {
            var button = new Button(onClick)
            {
                text = title
            };

            mainContainer.Add(button);

            return button;
        }

        protected Button AddOutputButton(string title, Action onClick = null)
        {
            var button = new Button(onClick)
            {
                text = title
            };

            outputContainer.Add(button);

            return button;
        }

        protected Port AddInputPort(string title, Port.Capacity capacity = Port.Capacity.Single)
        {
            var port = InstantiatePort(Orientation.Horizontal, Direction.Input, capacity, typeof(bool));

            port.portName = title;

            inputContainer.Add(port);

            return port;
        }

        protected Port AddOutputPort(string title, Port.Capacity capacity = Port.Capacity.Single)
        {
            var port = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(bool));

            port.portName = title;

            outputContainer.Add(port);

            return port;
        }

        protected Port AddNamedInputPort(string value, Port.Capacity capacity = Port.Capacity.Single)
        {
            var port = InstantiatePort(Orientation.Horizontal, Direction.Input, capacity, typeof(bool));

            var textField = new TextField()
            {
                value = value,
                label = null,
                multiline = false
            };

            port.Add(textField);

            textField.RegisterValueChangedCallback(x =>
            {
                port.userData = x.newValue;
            });

            inputContainer.Add(port);

            return port;
        }

        protected Port AddNamedOutputPort(string value, EventCallback<ChangeEvent<string>> onValueChanged = null, Port.Capacity capacity = Port.Capacity.Single)
        {
            var port = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(bool));

            var textField = new TextField()
            {
                value = value,
                label = null,
                multiline = false,
            };

            port.Add(textField);

            textField.RegisterValueChangedCallback(x =>
            {
                port.userData = x.newValue;
                onValueChanged?.Invoke(x);
            });

            textField.AddClasses(
                "ds-node__text-field",
                "ds-node__text-field__hidden",
                "ds-node__choice-text-field"
            );

            outputContainer.Add(port);

            return port;
        }

        #endregion
    }
}