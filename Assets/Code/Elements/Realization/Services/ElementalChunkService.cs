using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace CrazyWizardNotes.Elements.Realization
{
    public class ElementalChunkService : ISpaceChunkActorFactory, IHandlesCustomUpdate
    {
        private IPhysicsService PhysicsService { get; }
        private ElementalChunkServiceConfig Config { get; }

        public ElementalChunkService(IPhysicsService physicsService, ElementalChunkServiceConfig config)
        {
            PhysicsService = physicsService;
            Config = config;
        }

        public IActor CreateActorForChunk(ISpaceChunk spaceChunk)
        {
            return new ElementalChunkActor(spaceChunk);
        }

        public float OnCustomUpdateDelay => 1f;
        public void OnCustomUpdate()
        {

            var spacePoint = PhysicsService.GetDefaultSpacePoint(default);
            
            if (Config.UseDebugSprite)
            {
                VisualizeChunks(spacePoint.ParentSpace.SpaceChunks, Config.DebugSprite);
            }
        }

        private Color VisualizeChunk(ISpaceChunks chunks, ISpaceChunk chunk)
        {
            var actor = chunk.As<ElementalChunkActor>();

            if (actor == null)
            {
                return Color.magenta;
            }

            return new Color(
                0,
                0.5f - (0.5f * chunk.ChunkIndexX / chunks.ChunksCountX),
                0.5f - (0.5f * chunk.ChunkIndexY / chunks.ChunksCountY));
        }

        // ИИ
        private void VisualizeChunks(ISpaceChunks chunks, SpriteRenderer spriteRenderer)
        {
            var width = chunks.ChunksCountX;
            var height = chunks.ChunksCountY;

            if (width <= 0 || height <= 0)
            {
                return;
            }

            var texture = spriteRenderer.sprite != null ? spriteRenderer.sprite.texture : null;

            if (texture == null || texture.width != width || texture.height != height)
            {
                if (spriteRenderer.sprite != null)
                {
                    var oldTexture = spriteRenderer.sprite.texture;
                    UObject.Destroy(spriteRenderer.sprite);
                    if (oldTexture != null)
                        UObject.Destroy(oldTexture);
                }

                texture = new Texture2D(width, height, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Point,
                    wrapMode = TextureWrapMode.Clamp
                };

                var origin = chunks[0, 0];
                var pixelsPerUnit = 1f / origin.ChunkSize.X;
                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0f, 0f, width, height), Vector2.zero, pixelsPerUnit, 0, SpriteMeshType.FullRect);
                spriteRenderer.transform.localScale = Vector3.one;
                spriteRenderer.transform.position = new Vector3(origin.ChunkPosition.X, origin.ChunkPosition.Y, spriteRenderer.transform.position.z);
            }

            var pixels = new Color32[width * height];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var chunk = chunks[x, y];
                    if (chunk == null)
                        continue;

                    pixels[x + y * width] = VisualizeChunk(chunks, chunk);
                }
            }

            texture.SetPixels32(pixels);
            texture.Apply(false, false);
        }
    }
}
