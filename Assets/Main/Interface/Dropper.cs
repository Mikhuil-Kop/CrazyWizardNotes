using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Interface
{
    public class Dropper : MonoBehaviour
    {
        public void OnMouseUp()
        {
            DragAndDropManager.Instance.Drop(this);
        }
    }
}
