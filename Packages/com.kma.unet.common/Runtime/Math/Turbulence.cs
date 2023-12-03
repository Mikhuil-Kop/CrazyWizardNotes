using Random = UnityEngine.Random;
using Mathf = UnityEngine.Mathf;

namespace UNet.Common
{
    public class Turbulence
    {
        public Vector2D Strength { get; set; }
        public Vector2D Frequency { get; set; }
        public Vector2D Seed { get; set; }
        private Vector2D Time { get; set; }

        public Turbulence()
        {
            Strength = new Vector2D(1f, 1f);
            Frequency = new Vector2D(1f, 1f);

            Seed = new Vector2D(Random.Range(0f, 100f), Random.Range(0f, 100f));
            Time = new Vector2D(Random.Range(0f, 100f), Random.Range(0f, 100f));
        }

        public Vector2D Tick(float deltaTime)
        {
            Time += Frequency * deltaTime;

            var nx = Mathf.PerlinNoise(Time.X + Seed.X, Seed.Y) * 2 - 1;
            var ny = Mathf.PerlinNoise(Time.Y + Seed.X + 1000f, Seed.Y + 2000f) * 2 - 1;

            return new Vector2D(nx, ny) * Strength;
        }
    }
}