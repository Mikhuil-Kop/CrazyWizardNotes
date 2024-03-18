using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXTest : MonoBehaviour
{
    public VisualEffect Effect;

    public void Start()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Effect.SendEvent("Explode");
        }
    }
}
