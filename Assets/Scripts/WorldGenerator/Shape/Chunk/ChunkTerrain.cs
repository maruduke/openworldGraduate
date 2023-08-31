using System;
using UnityEngine;
using Core.Assist;
using Core.Noise;
using Core.Flag;
using Core.Ext;


using Shape.DataShape;

namespace Shape
{
    [Serializable]
    public class ChunkTerrain
    {

        public Vector2 coord;
        public Vector2 biomeCoord;

        public Vector3 size;

        public Face visibleFaces;

        public Biome biome;
        public ChunkTerrainData data;

        public ChunkTerrain(Vector3 size)
        {
            Debug.Log("ChunkTerrainGenerationTest");
            this.coord = Vector2.zero;
            this.size = size;
            this.visibleFaces = Face.All;
            this.data = new ChunkTerrainData(this.size);
        }

        public ChunkTerrain(Vector2 coord, Vector3 size)
        {
            Debug.Log("ChunkTerrainGenerationTest");
            this.coord = coord;


            this.size = size;
            this.visibleFaces = Face.All;
            this.data = new ChunkTerrainData(this.size);

            //this.random = new System.Random(world.seed);
            Debug.Log($"Coord: {this.coord}, {coord}");
            Debug.Log($"Size: {this.size}, {size}");
            Debug.Log($"VisibleFaces: {this.visibleFaces}");
            Debug.Log($"Data: {this.data}");

            this.InitBiome();
            this.InitNoiseData();
        }

        public void InitBiome()
        {
            Debug.Log("InItBiomeTest");

            var biomeTypeNoise = NoiseFunc.BiomeTypeNoise(this.coord);
            var biomeType = (BiomeType)(biomeTypeNoise * Enum.GetValues(typeof(BiomeType)).Length);
            Debug.Log($"BiomeNoise: {biomeTypeNoise}");
            Debug.Log($"BiomeType: {biomeType}");

            var xRandom = new System.Random((int)this.coord.x);
            var yRandom = new System.Random((int)this.coord.y);

            var noise = (NoiseFunc.Perlin2D(coord.x + xRandom.Next(), coord.y + yRandom.Next()) / Enum.GetValues(typeof(BiomeType)).Length);
            //var biomeType = (byte)(NoiseFunc.Perlin2D(coord.x + xRandom.Next(), coord.y + yRandom.Next()) / Enum.GetValues(typeof(BiomeType)).Length);
            this.biome = new MountainBiome();
        }

        public void InitNoiseData()
        {
            Debug.Log($"InItNoiseDataTest: {this.size}");
            var position = new Vector2(this.coord.x * this.size.x, this.coord.y * this.size.z);

            for (int x = 0; x < this.size.x; x++)
                for (int z = 0; z < this.size.z; z++)
                {
                    var biomeNoise = this.biome.noise.GetHeightNoise(position.x + x, position.y + z);

                    var minHeight = this.size.y * this.biome.minHeightRatio;
                    var maxHeight = this.size.y * this.biome.maxHeightRatio;
                    var height = this.biome.baseHeight + minHeight + Mathf.RoundToInt(biomeNoise * (maxHeight - minHeight));

                    // 우선 1을 넣는중임
                    for (int y = 0; y < height; y++)
                    {
                        foreach(var (apperingHeightLocation, id) in this.biome.blockIDs)
                        {
                            if (y < apperingHeightLocation)
                            {
                                this.data.blocks.Set(id, x, y, z);
                                break;
                            }
                        }
                    }
                }
        }


        public Mesh UpdateAndMakeUnityMesh(Face flag)
        {
            Debug.Log($"MaskedFlag: {flag}");
            this.Recalculate(flag);
            return this.data.mesh.ToUnityMesh();
        }

        public void RecalculateRenderFacesAround()
        {
            var position = new Vector2(this.coord.x * this.size.x, this.coord.y * this.size.z);
            for (var x = -1; x < this.size.x + 1; x++)
            {
                for (var z = -1; z < this.size.z + 1; z++)
                {
                    if ((x > -1 && x < this.size.x) && (z > -1 && z < this.size.z))
                        continue;

                    if ((x < 0 || this.size.x <= x) && (z < 0 || this.size.z <= z))
                        continue;

                    var aroundbiomeTypeNoise = NoiseFunc.BiomeTypeNoise(this.coord);
                    var biomeType = (BiomeType)(aroundbiomeTypeNoise * Enum.GetValues(typeof(BiomeType)).Length);
                    var aroundBiome = BiomeTypeAssist.GetBiome(biomeType);
                    Debug.Log($"BiomeType: {biomeType}, {aroundBiome}");

                    //var aroundHeight = aroundBiome.noise.GetHeightNoise(position.x + x, position.y + z);


                    //var height = minHeight + Mathf.RoundToInt(biomeNoise * (maxHeight - minHeight));
                    //var aroundHeight = aroundBiome.noise.GetHeightNoise(position.x + x, position.y + z);

                    var aroundBiomeNoise = aroundBiome.noise.GetHeightNoise(position.x + x, position.y + z);
                    var aroundBiomeMinHeight = this.size.y * aroundBiome.minHeightRatio;
                    var aroundBiomeMaxHeight = this.size.y * aroundBiome.maxHeightRatio;

                    var aroundHeight = aroundBiome.baseHeight + aroundBiomeMinHeight + Mathf.RoundToInt(aroundBiomeNoise * (aroundBiomeMaxHeight - aroundBiomeMinHeight));

                    var directionFlag = Face.None;
                    if (x == -1)
                    {
                        directionFlag = Face.Left;
                    }
                    else if (x == this.size.x)
                    {
                        directionFlag = Face.Right;
                    }

                    if (z == -1)
                    {
                        directionFlag = Face.Backward;
                    }
                    else if (z == this.size.z)
                    {
                        directionFlag = Face.Forward;
                    }

                    var direction = directionFlag.ExtFlip().Value.ExtToVector3().Value.ExtToVector3Int();

                    // 우선 1을 넣는중임
                    for (var y = 0; y < this.size.y; y++)
                    {
                        var faceFlag = this.data.renderFaces.Get(x + direction.x, y + direction.y, z + direction.z);
                        this.data.renderFaces.Set(faceFlag.ExtBitAndNot(directionFlag), x + direction.x, y + direction.y, z + direction.z);
                    }
                }
            }
        }

        public void Recalculate(Face flag)
        {
            this.RecalculateRenderFacesAround();
            this.data.RecalculateRenderFaces(flag);
            this.data.RecalculateMesh();
        }

        public bool ChangeFaceFlag(Face face)
        {
            if (this.visibleFaces == face)
                return false;

            this.visibleFaces = face;
            return true;
        }

        public bool RemoveFace(Face face)
        {
            return this.ChangeFaceFlag(this.visibleFaces.ExtBitAndNot(face));
        }
    }
}