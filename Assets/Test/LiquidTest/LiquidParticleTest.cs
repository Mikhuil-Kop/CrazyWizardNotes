using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticleTest : MonoBehaviour
{
    private Rigidbody2D Rigidbody;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

}
