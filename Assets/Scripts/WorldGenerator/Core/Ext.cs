using Core.Assist;
using Core.Flag;
using System;
using UnityEngine;

namespace Core.Ext
{
    public static class ExtVector2
    {
        public static Vector2 ExtGetOpposite(this Vector2 vector)
        {
            return new Vector2(-vector.x, -vector.y);
        }

        public static Vector3 ExtToVector3XZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static Face? ExtToFace(this Vector2 vector)
        {
            return (Face?)vector.ExtToVector3XZ().ExtToFace();
        }
    }

    public static class ExtVector3
    {
        public static Vector3Int ExtToVector3Int(this Vector3 target)
        {
            return new Vector3Int((int)target.x, (int)target.y, (int)target.z);
        }

        public static Vector2 ExtToVector2XZ(this Vector3 target)
        {
            return new Vector2(target.x, target.z);
        }

        public static Vector3 ExtDivide(this Vector3 target, Vector3 value)
        {
            return new Vector3(target.x / value.x, target.y / target.y, target.z / value.z);
        }

        public static Vector3 ExtFloorToInt(this Vector3 target)
        {
            return new Vector3(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y), Mathf.FloorToInt(target.z));
        }
        public static Vector3 ExtGetOpposite(this Vector3 vector)
        {
            return new Vector3(-vector.x, -vector.y, -vector.z);
        }

        public static Face? ExtToFace(this Vector3 vector)
        {
            if (Assist.Vector3Assist.vectorToFaceDict.ContainsKey(vector) is false)
                return null;

            return (Face?)Assist.Vector3Assist.vectorToFaceDict[vector];
        }

        public static int? ExtToCubeMeshDataRectIndex(this Vector3 vector)
        {
            if (Assist.Vector3Assist.directionToCubeMeshDataRectIndex.ContainsKey(vector) is false)
                return null;

            return (int?)Assist.Vector3Assist.directionToCubeMeshDataRectIndex[vector];
        }
    }

    public static class ExtFace
    {
        public static Face? ExtFlip(this Face target)
        {
            if (FaceAssist.acrossFaces.ContainsKey(target) is false)
                return null;

            return (Face?)FaceAssist.acrossFaces[target];
        }

        public static Face ExtNot(this Face target)
        {
            return Face.All & ~target;
        }

        public static Face ExtBitAndNot(this Face target, Face mask)
        {
            return ExtBitAnd(target, ~mask);
        }

        public static Face ExtBitAnd(this Face target, Face mask)
        {
            return target & mask;
        }

        public static bool ExtContains(this Face target, Face mask)
        {
            return (target & mask) == mask;
        }

        public static Vector3? ExtToVector3(this Face flag)
        {
            if (Assist.FaceAssist.faceToVectorDict.ContainsKey(flag) is false)
                return null;

            return (Vector3?)Assist.FaceAssist.faceToVectorDict[flag];
        }
    }

    public static class ExtNoise
    {
        public static BiomeType? ExtNoiseToBiomeType(this float noise)
        {
            // noise has 0.0 ~ 1.0
            // it made from perline noise generation function

            var value = Mathf.CeilToInt(noise * Enum.GetValues(typeof(BiomeType)).Length);
            foreach (var type in (Enum.GetValues(typeof(BiomeType))))
            {
                if ((int)type != value)
                    continue;

                return (BiomeType?)type;
            }

            return null;
        }
    }
}