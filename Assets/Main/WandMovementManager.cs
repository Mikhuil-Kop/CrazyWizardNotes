using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Main.Interface;
using UnityEngine;
using UnityEngine.UIElements;

public class WandMovementManager : MonoBehaviour
{
    public static WandMovementManager Instance { get; set; }
    public Camera Camera;

    private CapacityStack<WandPoint> WandPoints;
    private float WandPointInsertTimer;
    public float WandPointInsertTime;

    public void Awake()
    {
        Instance = this;
        WandPoints = new CapacityStack<WandPoint>(100);
    }

    public void Update()
    {
        Adding();
        Analize();
        DebugFoo();
    }

    public void Adding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WandPoints.Clear();
        }

        if (Input.GetMouseButtonDown(0) || WandPointInsertTimer >= WandPointInsertTime)
        {
            WandPointInsertTimer = 0;

            if (Input.GetMouseButton(0))
            {
                var position = Camera.ScreenToWorldPoint(Input.mousePosition);

                WandPoints.Push(new WandPoint()
                {
                    Position = position,
                    StartsLine = Input.GetMouseButtonDown(0)
                });
            }
        }
        else
        {
            WandPointInsertTimer += Time.deltaTime;
        }
    }

    public void Analize()
    {
        var points = WandPoints.GroupBy(x => x, new WandPointEqualityComparer());
        var pointsCenter = points.Select(p => (Vector3)(p.Aggregate(Vector2.zero, (x, y) => x + y.Position)) / p.Count());
        pointsCenter = pointsCenter.WhereInContext((x1, x0, x2) => ((x1 - x0).normalized + (x2 - x0).normalized).magnitude > 0.2f);

        foreach (var vec in pointsCenter)
        {
            Debug.DrawLine(vec + Vector3.up / 5, vec + Vector3.down / 5, Color.black);
            Debug.DrawLine(vec + Vector3.left / 5, vec + Vector3.right / 5, Color.black);
        }
    }

    public void DebugFoo()
    {
        var prevPoint = WandPoints.First;
        var i = 0;

        foreach (var point in WandPoints)
        {
            Debug.DrawLine((Vector3)prevPoint.Position + Vector3.back, (Vector3)point.Position + Vector3.back, new Color((100 - i++) / 100f, 0, 0));

            prevPoint = point;
        }
    }

    public void OnDestroy()
    {
        if (Instance == null)
        {
            return;
        }
    }
}

public class WandPoint
{
    public Vector2 Position { get; set; }
    public bool StartsLine { get; set; }
}

class WandPointEqualityComparer : IEqualityComparer<WandPoint>
{
    public bool Equals(WandPoint x, WandPoint y)
    {
        return (x.Position - y.Position).magnitude < 1;
    }

    public int GetHashCode(WandPoint obj)
    {
        return Mathf.Round((obj.Position.x + obj.Position.y) / 5f).GetHashCode();
    }
}
