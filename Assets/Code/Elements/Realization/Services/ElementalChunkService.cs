using CrazyWizardNotes.Elements.Abstractions;
using System;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace CrazyWizardNotes.Elements.Realization
{
    public class ElementalChunkService : IHandlesCustomUpdate
    {
        private IActorsCollectionService ActorsCollectionService { get; }
        private IPhysicsService PhysicsService { get; }
        private ElementalChunkServiceConfig Config { get; }

        public ElementalChunkService(IActorsCollectionService actorsCollectionService, IPhysicsService physicsService, ElementalChunkServiceConfig config)
        {
            ActorsCollectionService = actorsCollectionService;
            PhysicsService = physicsService;
            Config = config;
        }

        public float OnCustomUpdateDelay => 0.1f;
        public void OnCustomUpdate()
        {
            var deltaTime = OnCustomUpdateDelay;

            var spacePoint = PhysicsService.GetDefaultSpacePoint(default);
            var spaceContainer = spacePoint.ParentSpace;

            var actors = ActorsCollectionService.GetAll()
                .WithComponents<IHasSpacePoint>()
                .Where(x => x.AllComponentsPresent)
                .GroupBy(x => new ActorKey((int)x.Component1.SpacePoint.Position.X, (int)x.Component1.SpacePoint.Position.Y))
                .ToDictionary(x => x.Key, x => x.Select(y => y.Actor).ToList());

            foreach (var chunk in spaceContainer.SpaceChunks)
            {
                var elementalChunk = chunk.As<ElementalChunkActor>();

                if (elementalChunk == null)
                {
                    continue;
                }

                if (actors.TryGetValue(new ActorKey(chunk.ChunkIndexX, chunk.ChunkIndexY), out var actorInChunk))
                {
                    foreach (var actor in actorInChunk.ActorsAs<IHasTemperature>())
                    {
                        var delta = deltaTime * (elementalChunk.Temperature - actor.Temperature) / 10f;

                        actor.Temperature += delta;
                        elementalChunk.Temperature -= delta;
                    }
                }
            }

            foreach (var chunk in spaceContainer.SpaceChunks)
            {
                var elementalChunk = chunk.As<ElementalChunkActor>();

                if (elementalChunk == null)
                {
                    return;
                }

                elementalChunk.DeltaTemperature = 0f;
            }

            foreach (var chunk in spaceContainer.SpaceChunks)
            {
                var elementalChunk = chunk.As<ElementalChunkActor>();

                if (elementalChunk == null)
                {
                    return;
                }

                Exchange(elementalChunk, chunk.Up()?.As<ElementalChunkActor>(), deltaTime * 100f);
                Exchange(elementalChunk, chunk.Right()?.As<ElementalChunkActor>(), deltaTime * 100f);
            }

            foreach (var chunk in spaceContainer.SpaceChunks)
            {
                var elementalChunk = chunk.As<ElementalChunkActor>();

                if (elementalChunk == null)
                {
                    return;
                }

                elementalChunk.Temperature += elementalChunk.DeltaTemperature;
            }

            if (Config.UseDebugSprite)
            {
                VisualizeChunks(spaceContainer.SpaceChunks, Config.DebugSprite);
            }
        }

        private static void Exchange(ElementalChunkActor a, ElementalChunkActor b, float step)
        {
            if (a == null || b == null)
            {
                return;
            }

            var diff = a.Temperature - b.Temperature;
            if (diff.AlmostEquals(0f, 40f))
                return;

            var transfer = MathF.Min(step, MathF.Abs(diff) * 0.5f);
            var signed = MathF.Sign(diff) * transfer;
            a.DeltaTemperature -= signed;
            b.DeltaTemperature += signed;
        }

        private struct ActorKey
        {
            public int X;
            public int Y;

            public ActorKey(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        #region Дебаг

        private Color VisualizeChunk(ISpaceChunks chunks, ISpaceChunk chunk)
        {
            var actor = chunk.As<ElementalChunkActor>();

            if (actor == null)
            {
                return Color.magenta;
            }

            var temp = Math.Clamp(actor.Temperature / 1000f, 0f, 1f);

            return new Color(1f, 0.3f, 0, temp);
        }

        // Выделить в отдельный дебаг класс
        // ИИ
        private void VisualizeChunks(ISpaceChunks chunks, SpriteRenderer spriteRenderer)
        {
            var minX = chunks.MinX;
            var minY = chunks.MinY;
            var width = chunks.MaxX - minX + 1;
            var height = chunks.MaxY - minY + 1;

            if (width <= 0 || height <= 0)
                return;

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

                var origin = chunks[minX, minY];
                var pixelsPerUnit = 1f / origin.ChunkSize.X;
                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0f, 0f, width, height), Vector2.one * 0.5f, pixelsPerUnit, 0, SpriteMeshType.FullRect);
                spriteRenderer.transform.localScale = Vector3.one;
            }

            var min = chunks[minX, minY];
            var max = chunks[chunks.MaxX, chunks.MaxY];

            spriteRenderer.transform.position = new Vector3(
                (min.ChunkPosition.X + max.ChunkPosition.X) * 0.5f,
                (min.ChunkPosition.Y + max.ChunkPosition.Y) * 0.5f,
                spriteRenderer.transform.position.z);

            var pixels = new Color32[width * height];

            for (var y = minY; y <= chunks.MaxY; y++)
            {
                for (var x = minX; x <= chunks.MaxX; x++)
                {
                    var chunk = chunks[x, y];
                    if (chunk == null)
                        continue;

                    pixels[(x - minX) + (y - minY) * width] = VisualizeChunk(chunks, chunk);
                }
            }

            texture.SetPixels32(pixels);
            texture.Apply(false, false);
        }

        #endregion
    }
}
