using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Interface
{
    public class Dragger : MonoBehaviour
    {
        public void OnMouseDown()
        {
            DragAndDropManager.Instance.Drag(this);
        }
    }
}