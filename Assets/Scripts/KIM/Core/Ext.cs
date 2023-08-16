using Core.Flag;
using UnityEngine;

namespace Core.Ext
{
    public static class ExtVector2
    {
        public static Vector2 ExtVector2XZ(this Vector3 target)
        {
            return new Vector2(target.x, target.z);
        }
        public static Vector2 ExtGetOpposite(this Vector2 vector)
        {
            return new Vector2(-vector.x, -vector.y);
        }
    }

    public static class ExtVector3
    {
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

        public static Face? ExtToFace(this Vector3 vec)
        {
            if (Assist.Vector3Assist.vectorToFaceDict.ContainsKey(vec) is false)
                return null;

            return Assist.Vector3Assist.vectorToFaceDict[vec];
        }

        public static int? ExtToCubeMeshDataRectIndex(this Vector3 vec)
        {
            if (Assist.Vector3Assist.directionToCubeMeshDataRectIndex.ContainsKey(vec) is false)
                return null;

            return Assist.Vector3Assist.directionToCubeMeshDataRectIndex[vec];
        }
    }

    public static class ExtFace
    {
        public static Face ExtFlip(this Face target)
        {
            return Face.Bottom;
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

        public static Vector3? ExtToVector(this Face flag)
        {
            if (Assist.FaceAssist.faceToVectorDict.ContainsKey(flag) is false)
                return null;

            return Assist.FaceAssist.faceToVectorDict[flag];
        }
    }
}