
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core.Flag;
using System;
using Core.Data;
namespace Core.Assist
{
    //public static class Convert
    //{
    //    public static Vector3? ConvertFaceToVector(Face flag)
    //    {
    //        if (faceToVectorDict.ContainsKey(flag) is false)
    //            return null;

    //        return faceToVectorDict[flag];
    //    }
    //    public static int? ConvertVectorToCubeMeshDataRectIndex(Vector3 vec)
    //    {
    //        if (directionToRectIndex.ContainsKey(vec) is false)
    //            return null;

    //        return directionToRectIndex[vec];
    //    }

    //    public static Face? ConvertVectorToFace(Vector3 vec)
    //    {
    //        if (vectorToFaceDict.ContainsKey(vec) is false)
    //            return null;

    //        return vectorToFaceDict[vec];
    //    }
    //}

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
}
