using Core.Flag;


using Shape.DataShape;
using System;
using UnityEngine;

namespace Core.Noise
{
    public class NoiseFunc
    {
        public static float BiomeTypeNoise(Vector2 chunkTerrainCoord)
        {
            var scenePosition = new Vector2(
                (chunkTerrainCoord.x) / Biome.size.x,
                (chunkTerrainCoord.y) / Biome.size.y
                );

            //Mathf.FloorToInt(scenePosition.x / this.sceneSize.x) * this.sceneSize.x}

        var noise = NoiseFunc.Perlin2D(scenePosition.x, scenePosition.y, 0.5f);
            return (noise > 1.0f) ? 1.0f : ((noise < 0.0f) ? 0.0f : noise);
        }

        public static float Perlin2D(float x, float y, float offset)
        {
            return Mathf.PerlinNoise(x + offset, y + offset);
        }

        public static float Perlin2D(float x, float y)
        {
            return Mathf.PerlinNoise(x + 0.1f, y + 0.1f);
        }

        // offset 필요함

        public static float Perlin3D(float x, float y, float z, float offset)
        {
            float AB = Mathf.PerlinNoise(x + offset + 0.1f, y + offset + 0.1f);
            float BC = Mathf.PerlinNoise(y + offset + 0.1f, z + offset + 0.1f);
            float AC = Mathf.PerlinNoise(x + offset + 0.1f, z + offset + 0.1f);
            float BA = Mathf.PerlinNoise(y + offset + 0.1f, x + offset + 0.1f);
            float CB = Mathf.PerlinNoise(z + offset + 0.1f, y + offset + 0.1f);
            float CA = Mathf.PerlinNoise(z + offset + 0.1f, x + offset + 0.1f);

            return (AB + BC + AC + BA + CB + CA) / 6f;
        }

        //public static float Perlin2DV2(System.Random seed, float x, float y, float sizeX, float sizeY, float scaleX, float scaleY, int octaves = 2, float persistance = 0.5f, float lacunarity = 0.8f)
        //{
        //    var random = (float)seed.NextDouble();
        //    var random2 = seed.Next(-100, 100);

        //    float noise = Mathf.PerlinNoise(random2 + x / sizeX + 0.01f * scaleX, random + y / sizeY + 0.01f * scaleY);
        //    return noise;
        //}

        public static float Perlin2DV3(
            float x,
            float y,
            float scaleX = 0.6f,
            float scaleY = 0.8f,
            int octaves = 5)
        {

            float noise = Mathf.PerlinNoise(x * scaleX, y * scaleY);

            if (noise > 1f)
                return 1f;

            return noise;
        }

        public static float Perlin2DV2(
            float x,
            float y,
            float scaleX,
            float scaleY,
            float octaves,
            float randomOctaveOffset,
            float randomWeightRatioForOctave)
        {
            var value = octaves + randomOctaveOffset * randomWeightRatioForOctave * octaves; // 0 ~ 1의 값을 내는데

            float noise = Mathf.Pow(Mathf.PerlinNoise(x * scaleX, y * scaleY), value);
            return noise > 1.0f ? 1.0f : noise;
        }


        public static float Perlin2DV2(
            System.Random seed,
            float x,
            float y,
            float scaleX,
            float scaleY,
            float octaves,
            float randomWeightRatioForOctave)
        {
            var random = (float)seed.NextDouble() * randomWeightRatioForOctave * octaves; // 0 ~ 1의 값을 내는데

            float noise = Mathf.Pow(Mathf.PerlinNoise(x * scaleX, y * scaleY), octaves + random);
            return noise > 1.0f ? 1.0f : noise;
        }


        public static float Perlin2DV3(
            float x,
            float y,
            float scaleX,
            float scaleY,
            float octaves
            )
        {
            float noise = Mathf.Pow(Mathf.PerlinNoise(x * scaleX, y * scaleY), octaves);
            return (noise > 1.0f) ? 1.0f : ((noise < 0.0f) ? 0.0f : noise);
        }

        //public static float Perlin2DV2(System.Random seed, Vector2 offset, Vector2 size, int octaves = 2, float persistance = 0.5f, float lacunarity = 0.8f)
        //{
        //    var random = seed.Next(-10000, 10000);
        //    var octaveOffset = new Vector2(random + offset.x, random + offset.y);

        //    float maxNoiseHeight = float.MinValue;
        //    float minNoiseHeight = float.MaxValue;

        //    var halfSize = new Vector2(size.x / 2f, size.y / 2f);

        //    float amplitude = 1;
        //    float frequency = 1;
        //    float noiseHeight = 0;

        //    for (int i = 0; i < octaves; i++)
        //    {
        //        float sampleX = (offset.x - halfSize.x) / size.x * frequency + octaveOffset.x;
        //        float sampleY = (offset.y - halfSize.y) / size.y * frequency + octaveOffset.y;

        //        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
        //        noiseHeight += perlinValue * amplitude;

        //        amplitude *= persistance;
        //        frequency *= lacunarity;
        //    }

        //    if (noiseHeight > maxNoiseHeight)
        //    {
        //        maxNoiseHeight = noiseHeight;
        //    }
        //    else if (noiseHeight < minNoiseHeight)
        //    {
        //        minNoiseHeight = noiseHeight;
        //    }

        //    Debug.Log($"NoiseHeight: {noiseHeight}, Lefp: {Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseHeight)}");
        //    return Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseHeight);
        //}

        //public static bool Perlin3D(Vector3 position, float seed, Vector3 scale, float threshold)
        //{
        //    float x = (position.x + offset.x + 0.1f) * scale;
        //    float y = (position.y + offset.y + 0.1f) * scale;
        //    float z = (position.z + offset.z + 0.1f) * scale;

        //    float AB = Mathf.PerlinNoise(x, y);
        //    float BC = Mathf.PerlinNoise(y, z);
        //    float AC = Mathf.PerlinNoise(x, z);
        //    float BA = Mathf.PerlinNoise(y, x);
        //    float CB = Mathf.PerlinNoise(z, y);
        //    float CA = Mathf.PerlinNoise(z, x);

        //    if ((AB + BC + AC + BA + CB + CA) / 6f > threshold)
        //        return true;
        //    else
        //        return false;
        //}

        //public static float BiomeNoise(int seed, Vector2 position, float size)
        //{
        //    return Perlin2D(seed, )
        //}

        //public static float Perlin2D(System.Random seed, Vector2 offset, Vector2 size, int octaves = 3, float persistance = 1f, float lacunarity = 1f)
        //{
        //    var random = seed.Next(-10000, 10000);
        //    var octaveOffset = new Vector2(random + offset.x, random + offset.y);


        //    float maxNoiseHeight = float.MinValue;
        //    float minNoiseHeight = float.MaxValue;

        //    var halfSize = new Vector2(size.x / 2f, size.y / 2f);

        //    float amplitude = 1;
        //    float frequency = 1;
        //    float noiseHeight = 0;

        //    for (int i = 0; i < octaves; i++)
        //    {
        //        float sampleX = (offset.x - halfSize.x) / size.x * frequency + octaveOffset.x;
        //        float sampleY = (offset.y - halfSize.y) / size.y * frequency + octaveOffset.y;

        //        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
        //        noiseHeight += perlinValue * amplitude;

        //        amplitude *= persistance;
        //        frequency *= lacunarity;
        //    }

        //    if (noiseHeight > maxNoiseHeight)
        //    {
        //        maxNoiseHeight = noiseHeight;
        //    }
        //    else if (noiseHeight < minNoiseHeight)
        //    {
        //        minNoiseHeight = noiseHeight;
        //    }
        //    noiseMap[x, y] = noiseHeight;

        //    for (int y = 0; y < mapHeight; y++)
        //    {
        //        for (int x = 0; x < mapWidth; x++)
        //        {
        //            noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
        //        }
        //    }
        //}

        public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
        {
            float[,] noiseMap = new float[mapWidth, mapHeight];

            System.Random prng = new System.Random(seed);
            Vector2[] octaveOffsets = new Vector2[octaves];
            for (int i = 0; i < octaves; i++)
            {
                float offsetX = prng.Next(-100000, 100000) + offset.x;
                float offsetY = prng.Next(-100000, 100000) + offset.y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }

            if (scale <= 0)
            {
                scale = 0.0001f;
            }

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            float halfWidth = mapWidth / 2f;
            float halfHeight = mapHeight / 2f;


            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {

                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (int i = 0; i < octaves; i++)
                    {
                        float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                        float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseHeight += perlinValue * amplitude;

                        amplitude *= persistance;
                        frequency *= lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }
                    else if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }
                    noiseMap[x, y] = noiseHeight;
                }
            }

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
                }
            }

            return noiseMap;
        }
    }

    public class NoiseFuncV2
    {
        //public static float Get2DPerlin(Vector2 position, float offset, float scale)
        //{
        //    return Mathf.PerlinNoise((position.x + 0.1f) / VoxelData.ChunkWidth * scale + offset, (position.y + 0.1f) / VoxelData.ChunkWidth * scale + offset);
        //}

        public static float Get3DPerlin(Vector3 position, float offset, float scale, float threshold)
        {
            float x = (position.x + offset + 0.1f) * scale;
            float y = (position.y + offset + 0.1f) * scale;
            float z = (position.z + offset + 0.1f) * scale;

            float AB = Mathf.PerlinNoise(x, y);
            float BC = Mathf.PerlinNoise(y, z);
            float AC = Mathf.PerlinNoise(x, z);
            float BA = Mathf.PerlinNoise(y, x);
            float CB = Mathf.PerlinNoise(z, y);
            float CA = Mathf.PerlinNoise(z, x);

            return (AB + BC + AC + BA + CB + CA) / 6f;
        }
    }

}
