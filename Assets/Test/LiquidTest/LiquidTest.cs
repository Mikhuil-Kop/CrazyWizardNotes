using System.Linq;
using UnityEngine;

public class LiquidTest : MonoBehaviour
{
    public LiquidParticleTest[] Particles;
    private SpriteRenderer SpriteRenderer;
    private Sprite Sprite;
    private Texture2D Texture2D;

    public void Start()
    {
        Particles = GameObject.FindObjectsOfType<LiquidParticleTest>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D = new Texture2D(256, 256);
        Sprite = Sprite.Create(Texture2D, new Rect(Vector2.zero, Vector2.one * 256), Vector2.one / 2f);
        SpriteRenderer.sprite = Sprite;

        Debug.Log(Texture2D.GetPixels32().Length);
        Debug.Log(256 * 256 - 1);

        var col = new Color32(200, 200, 255, 255);
        Texture2D.SetPixels32(Enumerable.Range(0, 256 * 256).Select(x => col).ToArray());
    }

    public void Update()
    {
        var vecs = Particles.Select(x => GetPos(x)).ToArray();

        Sprite.OverrideGeometry(vecs, new ushort[] { 0, 1, 2 });
    }

    private Vector2 GetPos(LiquidParticleTest particle)
    {
        return ((Vector2)particle.transform.position + Vector2.one * 32) * 4;
    }
}
