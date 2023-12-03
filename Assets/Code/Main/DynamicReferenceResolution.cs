using UnityEngine;
using UnityEngine.Rendering.Universal; // для URP

namespace CrazyWizardNotes.Main
{
    [RequireComponent(typeof(PixelPerfectCamera))]
    [ExecuteAlways]
    public class DynamicReferenceResolution : MonoBehaviour
    {
        [SerializeField] float targetOrthoSize = 5f;   // желаемый Size (±5-6)
        [SerializeField] int assetsPPU = 16;           // твой PPU

        PixelPerfectCamera ppc;
        int lastWidth, lastHeight;

        void Awake()
        {
            ppc = GetComponent<PixelPerfectCamera>();
            ppc.assetsPPU = assetsPPU;
            UpdateReference();
        }

        void Update()
        {
            if (Screen.width != lastWidth || Screen.height != lastHeight)
                UpdateReference();
        }

        void UpdateReference()
        {
            lastWidth = Screen.width;
            lastHeight = Screen.height;

            // Базовый расчёт под targetOrthoSize
            int refY = Mathf.RoundToInt(targetOrthoSize * 2f * assetsPPU);
            int refX = Mathf.RoundToInt(refY * ((float)Screen.width / Screen.height));

            // Минимальные значения
            refX = Mathf.Max(refX, 1);
            refY = Mathf.Max(refY, 1);

            ppc.refResolutionX = refX;
            ppc.refResolutionY = refY;
        }
    }
}