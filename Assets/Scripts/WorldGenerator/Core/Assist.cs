
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core.Flag;
using System;
using Core.Data;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using Shape.DataShape;

namespace Core.Assist
{

    public static class FaceAssist
    {
        public static readonly Dictionary<Face, Vector3> faceToVectorDict = new Dictionary<Face, Vector3>()
        {
            {Face.Top, Vector3.up},
            {Face.Bottom, Vector3.down},
            {Face.Forward, Vector3.forward},
            {Face.Backward, Vector3.back},
            {Face.Left, Vector3.left},
            {Face.Right, Vector3.right},
        };

        public static readonly Dictionary<Face, Face> acrossFaces = new Dictionary<Face, Face>()
        {
            {Face.Forward, Face.Backward},
            {Face.Backward, Face.Forward},
            {Face.Top, Face.Bottom},
            {Face.Bottom, Face.Top},
            {Face.Left, Face.Right},
            {Face.Right, Face.Left},
        };
    }

    public static class Vector3Assist
    {
        public static readonly Dictionary<Vector3, Face> vectorToFaceDict =
            Generic.FlipDictionary(FaceAssist.faceToVectorDict);

        public static readonly Dictionary<Vector3, int> directionToCubeMeshDataRectIndex = new Dictionary<Vector3, int>()
        {
            {Vector3.forward, 0},
            {Vector3.back, 1},
            {Vector3.up, 2},
            {Vector3.down, 3},
            {Vector3.left, 4},
            {Vector3.right, 5},
        };

        public static readonly List<Vector3> directions3D = new List<Vector3>()
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back,
        };
    }

    public static class Vector2Assist
    {
        public static readonly List<Vector2> directions2D = new List<Vector2>()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };
    }

    public static class Generic
    {
        public static Dictionary<V, K> FlipDictionary<K, V>(Dictionary<K, V> dict)
        {
            return dict.ToDictionary(e => e.Value, e => e.Key);
        }

    }

    public static class UnityAssist
    {
        public static Scene CreateScene(string name)
        {
            var scene = SceneManager.CreateScene(name);
            return scene;
        }

        public static GameObject CreateBaseChunkObject(string name) 
        {
            var go = new GameObject(name);
            go.AddComponent<MeshCollider>();
            go.AddComponent<MeshFilter>();

            var render = go.AddComponent<MeshRenderer>();
            render.material = new Material(Shader.Find("Standard"));
            return go;
        }

        public static GameObject CreateBaseChunkObject(string name, Material material)
        {
            var go = new GameObject(name);
            go.AddComponent<MeshCollider>();
            go.AddComponent<MeshFilter>();

            var render = go.AddComponent<MeshRenderer>();
            render.material = new Material(Shader.Find("Standard"));
            return go;
        }
    }

    public static class BiomeTypeAssist
    {
        public static Biome GetBiome(BiomeType type)
        {
            if(type == BiomeType.Grassland)
            {
                return null;
            }

            if (type == BiomeType.Desert)
            {
                return new DesertBiome();
            }

            if (type == BiomeType.Mountain)
            {
                return new MountainBiome();
            }

            return null;
        }
    }
}
