using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LiquidContainer : MonoBehaviour
{
    public PolygonCollider2D Collider2D;

    public List<Vector2> Rays { get; private set; }

    public void Start()
    {
        //var rays = Enumerable.Range(0, 360)
        //    .Select(x => new Vector2(Mathf.Sin(x), Mathf.Cos(x)))
        //    .Select(x => Physics2D.Raycast(transform.position, x, 100f))
        //    .Where(x => x.collider != null)
        //    .Select(x => x.point);

        //Rays = rays.ToList();
        //Debug.Log(Rays);

        //Collider2D.enabled = true;
    }

    //public void Update()
    //{
    //    foreach (var i in Rays)
    //    {
    //        Debug.DrawLine(i + Vector2.up * 0.1f, i + Vector2.down * 0.1f, Color.black);
    //        Debug.DrawLine(i + Vector2.left * 0.1f, i + Vector2.right * 0.1f, Color.black);
    //    }

    //    var collision = new Collision2D();

    //}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var particle = collision.gameObject.GetComponent<LiquidParticleTest>();

        if (particle == null)
        {
            return;
        }
    }
}

