using UnityEngine;

namespace CrazyWizardNotes.SimpleScripts
{
    [ExecuteInEditMode]
    public class ShaderCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera OriginalCamera;
        [SerializeField]
        private Camera Camera;
        [SerializeField]
        private Material Material;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, Material);
        }

        private void Update()
        {
            Camera.orthographicSize = OriginalCamera.orthographicSize;
        }
    }
}