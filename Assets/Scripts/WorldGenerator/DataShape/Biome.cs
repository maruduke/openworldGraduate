using Core.Noise;
using System.Collections.Generic;
using UnityEngine;

namespace Shape.DataShape
{
    /// <summary>
    /// 인터페이스
    /// </summary>

    public interface IBiomeNoise
    {
        public float GetHeightNoise(float x, float z);
        public float GetFieldNoise(float x, float y, float z);
    }

    public interface IBiome
    {
        // 바이옴은 바이옴만의 노이즈와 데이터가 필요함
        public void GetBlockID(int value) { }
    }

    /// <summary>
    /// block정보
    /// </summary>
    public class BlockInfo
    {
        public string name;
        public int id;
        public List<int> uvs;

        public BlockInfo(string name, List<int> uvs, int id)
        {
            this.name = name;
            this.id = id;
            this.uvs = uvs;
        }
    }

    public static class AllBlockInfo
    {
        public static Dictionary<int, BlockInfo> blockNumbers = new Dictionary<int, BlockInfo>()
        {
            {1, new BlockInfo("Sand", new List<int>{1, 1, 1, 1, 1, 1}, 1)},
            {2, new BlockInfo("Sand", new List<int>{1, 1, 1, 1, 1, 1}, 2)},
            {3, new BlockInfo("Sand", new List<int>{1, 1, 1, 1, 1, 1}, 3)},
            {4, new BlockInfo("Sand", new List<int>{1, 1, 1, 1, 1, 1}, 4)},
            {5, new BlockInfo("Sand", new List<int>{1, 1, 1, 1, 1, 1}, 5)},
        };
    }

    /// <summary>
    /// 노이즈 정보
    /// </summary>

    public class BiomeNoise : IBiomeNoise
    {
        public Vector2 scale = new Vector2(0.01f, 0.01f);
        public float octaves = 1;
        public float min = 0f;
        public float max = 1f;

        /// <summary>
        /// 모호한것들
        /// </summary>
        public float persistance = 1f;
        public float lacunarity = 1f;
        public float difference = 0f;
        public float absDifference = 0f;

        public float StretchWithNoiseRange(float noise)
        {
            return noise * (this.min + this.max) - this.min;
        }

        public float GetHeightNoise(float x, float z)
        {
            // -1 ~ 1사이의 값을 가진다
            //var noise = NoiseFunc.Perlin2DV2(this.seed, x, z, this.scale.x, this.scale.y, this.octaves, this.randomWeightRatio);
            var noise = NoiseFunc.Perlin2DV3(x, z, this.scale.x, this.scale.y, this.octaves);
            //Debug.Log($"Noise Test: {noise}, SretchNoiseTest: {this.StretchWithNoiseRange(noise)}");
            noise = this.StretchWithNoiseRange(noise);
            return noise;
        }

        //public float GetHeightNoise(float x, float z)
        //{
        //    // -1 ~ 1사이의 값을 가진다
        //    var noise = NoiseFunc.Perlin2DV2(this.seed, x, z, this.scale.x, this.scale.y, this.octaves, this.randomWeightRatio);
        //    //Debug.Log($"Noise Test: {noise}, SretchNoiseTest: {this.StretchWithNoiseRange(noise)}");
        //    noise = this.StretchWithNoiseRange(noise);
        //    return noise;
        //}

        public float GetFieldNoise(float x, float y, float z)
        {
            var noise = NoiseFunc.Perlin2DV3(x, z, this.scale.x, this.scale.y, this.octaves);
            return 1f;
        }
    }

    public class DesertNoise : BiomeNoise
    {
        public DesertNoise() : base()
        {
            this.scale = new Vector2(0.02f, 0.04f);
            this.octaves = 3;
            //this.min = -0.5f;
        }
    }


    public class HillNoise : BiomeNoise
    {
        public HillNoise() : base()
        {
            this.scale = new Vector2(0.02f, 0.04f);
            this.octaves = 2.5f;
        }
    }

    public class GrassLandNoise : BiomeNoise
    {
        public GrassLandNoise() : base()
        {
            this.scale = new Vector2(0.01f, 0.01f);
            this.octaves = 2;
            this.difference = 0;
        }
    }


    public class MountainNoise : BiomeNoise
    {
        public MountainNoise() : base()
        {
            this.scale = new Vector2(0.02f, 0.04f);
            this.octaves = 3;
        }
    }

    /// <summary>
    /// 바이옴 정보
    /// </summary>
    public class Biome : IBiome
    {
        public static Vector2 size = new Vector2(100, 100);
        public int baseHeight = 3;

        public Vector2 coord;
        public Dictionary<int, int> blockIDs;
        public BiomeNoise noise;

        public float minHeightRatio;
        public float maxHeightRatio;

        public Biome()
        {
            this.blockIDs = new Dictionary<int, int>();
            this.noise = new BiomeNoise();

            this.minHeightRatio = 0.1f;
            this.maxHeightRatio = 0.4f;

            this.blockIDs.Add(1, 1);
        }

        public int GetBlockID(int height)
        {
            foreach(var (heightWeight, blockInfoKey) in this.blockIDs)
            {
                Debug.Log($"WorkTest: {height}, {heightWeight}");
                if (height >= heightWeight)
                    continue;

                return blockInfoKey;
            }

            return -1;
        }
    }

    public class DesertBiome : Biome
    {
        public DesertBiome() : base()
        {
            Debug.Log("DesertBiome");
            this.noise = new DesertNoise() as BiomeNoise;

            this.minHeightRatio = 0.2f;
            this.maxHeightRatio = 0.3f;

            Debug.Log($"Noise: {this.noise}, {this.noise is null}");

            this.blockIDs.Add(10, 2);
            this.blockIDs.Add(40, 3);
            this.blockIDs.Add(80, 4);
            this.blockIDs.Add(100, 5);
        }
    }

    public class GrassLandBiome : Biome
    {
        public GrassLandBiome() : base()
        {
            Debug.Log("DesertBiome");
            this.noise = new GrassLandNoise() as BiomeNoise;

            this.minHeightRatio = 0.2f;
            this.maxHeightRatio = 0.4f;

            Debug.Log($"Noise: {this.noise}, {this.noise is null}");

            this.blockIDs.Add(10, 2);
            this.blockIDs.Add(40, 3);
            this.blockIDs.Add(80, 4);
            this.blockIDs.Add(100, 5);
        }
    }


    public class MountainBiome : Biome
    {
        public MountainBiome() : base()
        {
            this.noise = (new MountainNoise()) as BiomeNoise;

            this.minHeightRatio = 0.1f;
            this.maxHeightRatio = 0.85f;

            this.blockIDs.Add(10, 2);
            this.blockIDs.Add(40, 3);
            this.blockIDs.Add(80, 4);
            this.blockIDs.Add(100, 5);
        }
    }
}
