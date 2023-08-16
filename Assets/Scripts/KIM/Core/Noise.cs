using UnityEngine;

namespace Core.Noise
{
    public class NoiseFunc
    {
        public static float Perlin2D(int seed, float x, float y)
        {
            return Mathf.PerlinNoise(x + seed, y + seed);
        }
    }
}
