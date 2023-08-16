using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Data
{
    public static class CubeMeshData
    {
        public static readonly Vector3 center = new Vector3(0f, 0f, 0f);
        public static readonly int numberOfEdge = 4;

        public static readonly Vector3[] vertices = new Vector3[8]
        {
            // 블록을 형성하는 점의 위치
            // 서로 상반되는 위치의 벡터들을 기준으로 정렬되어있음
            new Vector3(0f, 0f, 0f), // 0 0 0
            new Vector3(1f, 0f, 0f), // 1 0 0
            new Vector3(0f, 1f, 0f), // 0 1 0
            new Vector3(1f, 1f, 0f), // 1 1 0  
            new Vector3(0f, 0f, 1f), // 0 0 1
            new Vector3(1f, 0f, 1f), // 1 0 1
            new Vector3(0f, 1f, 1f), // 0 1 1
            new Vector3(1f, 1f, 1f), // 1 1 1
        };
        ///
        ///   y         z(앞)
        /// 
        ///     7   8
        ///   3   4
        ///     5   6   
        ///   1   2     x
        ///

        public static readonly List<int[]> rectIndexes = new List<int[]>(6)
        {
            new int[4]{ 8, 7, 5, 6 }, // 앞
            new int[4]{ 3, 4, 2, 1 }, // 뒤
            new int[4]{ 7, 8, 4, 3 }, // 위
            new int[4]{ 1, 2, 6, 5 }, // 아래
            new int[4]{ 7, 3, 1, 5 }, // 좌
            new int[4]{ 4, 8, 6, 2 }, // 우
        };

        /// <summary>
        /// 0 0 0
        /// 0 0 1
        /// 0 1 0
        /// 0 1 1
        /// 1 0 0
        /// 1 0 1
        /// 1 1 0
        /// 1 1 1
        /// </summary>

        public static readonly int[] triangles = new int[6] { 0, 1, 3, 3, 1, 2 };
        /// <summary>
        /// 0 1
        /// 3 2
        /// 사각형의 꼭지점이 이런 순서로 되어 있을 경우
        /// 0 1 3을 하나의 삼각형으로, 3 1 2를 하나의 삼각형으로 형성한다.
        /// </summary>

        public static readonly Vector2[] uvs = new Vector2[4]
        {
            // TL을 (0, 0) BR을 (1, 1)로 규정한다
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
        };
    }
}
