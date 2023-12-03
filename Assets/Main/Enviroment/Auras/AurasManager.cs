using UnityEngine;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AurasManager : MonoBehaviour
{
    public static AurasManager Instance { get; set; }
    private List<IAura> Auras { get; set; }
    public Texture2D texture;

    public void Awake()
    {
        Instance = this;

        Auras = new List<IAura>() { new LiquidAura() };
    }

    public void Update()
    {
        var tasks = Auras.Select(x => x.UpdateAsync());
        Task.WaitAll(tasks.ToArray());

        texture.SetPixels32(Auras.First().GetColors(), 0);
        texture.Apply();
    }

    public void OnDestroy()
    {
        if (Instance == null)
        {
            return;
        }
    }
}

