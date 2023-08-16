using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Regacy.Core.Data
{

    public class RectMesh
    {
        public List<Vector3> vertices;
        public List<int> triangles;
        public List<Vector2> uv;

        public RectMesh()
        {
            this.vertices = new List<Vector3>();
            this.triangles = new List<int>();
            uv = new List<Vector2>();
        }
    }


    public static class CubeMeshData
    {
        public static readonly Vector3 center = new Vector3(0.0f, 0.0f, 0.0f);
        public static readonly int interval = 4;

        //public static readonly Vector3[] vertices = new Vector3[8]
        //{
        //    // 블록을 형성하는 점의 위치
        //    // 서로 상반되는 위치의 벡터들을 기준으로 정렬되어있음
        //    new Vector3( 0.5f,  0.5f,  0.5f), // 1 1 1 뒤
        //    new Vector3(-0.5f, -0.5f, -0.5f), // 0 0 0 앞 0

        //    new Vector3( 0.5f,  0.5f, -0.5f), // 1 1 0    
        //    new Vector3(-0.5f, -0.5f,  0.5f), // 0 0 1

        //    new Vector3( 0.5f, -0.5f,  0.5f), // 1 0 1
        //    new Vector3(-0.5f,  0.5f, -0.5f), // 0 1 0

        //    new Vector3(-0.5f,  0.5f,  0.5f), // 0 1 1
        //    new Vector3( 0.5f, -0.5f, -0.5f), // 1 0 0
        //};

        public static readonly Vector3[] vertices = new Vector3[8]
        {
        // 블록을 형성하는 점의 위치
        // 서로 상반되는 위치의 벡터들을 기준으로 정렬되어있음
        new Vector3(-0.5f, -0.5f, -0.5f), // 0 0 0
        new Vector3( 0.5f, -0.5f, -0.5f), // 1 0 0
        new Vector3(-0.5f,  0.5f, -0.5f), // 0 1 0
        new Vector3( 0.5f,  0.5f, -0.5f), // 1 1 0  
        new Vector3(-0.5f, -0.5f,  0.5f), // 0 0 1
        new Vector3( 0.5f, -0.5f,  0.5f), // 1 0 1
        new Vector3(-0.5f,  0.5f,  0.5f), // 0 1 1
        new Vector3( 0.5f,  0.5f,  0.5f), // 1 1 1
        };

        ///
        ///   y         z
        /// 
        ///     7   8
        ///   3   4
        ///     5   6   
        ///   1   2     x
        /// 

        public static readonly List<int[]> rectIndexes = new List<int[]>(6)
    {
        new int[4]{ 3, 4, 2, 1 }, // 앞
        new int[4]{ 8, 7, 5, 6 }, // 뒤
        new int[4]{ 7, 8, 4, 3 }, // 위
        new int[4]{ 1, 2, 6, 5 }, // 아래
        new int[4]{ 7, 3, 1, 5 }, // 좌
        new int[4]{ 4, 8, 6, 2 }, // 우
    };

        ///
        ///     y
        /// 
        ///     3   7
        ///   4   8
        ///     1   5   z
        ///   2   6
        /// x

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

        //public static readonly List<int[]> rectIndexes = new List<int[]>(6)
        //{
        //    new int[4]{ 2, 6, 3, 8 }, // 앞
        //    new int[4]{ 1, 5, 7, 4 }, // 뒤
        //    new int[4]{ 7, 6, 1, 3 }, // 위
        //    new int[4]{ 2, 4, 8, 5 }, // 아래
        //    new int[4]{ 7, 4, 6, 2 }, // 좌
        //    new int[4]{ 3, 8, 1, 5 }, // 우
        //};

        //public static readonly List<int[]> rectIndexes = new List<int[]>(6)
        //{
        //    new int[4]{ 4, 8, 6, 2 }, // 앞
        //    new int[4]{ 7, 3, 1, 5 }, // 뒤
        //    new int[4]{ 3, 7, 8, 4 }, // 위
        //    new int[4]{ 2, 6, 5, 1 }, // 아래
        //    new int[4]{ 3, 4, 2, 1 }, // 좌
        //    new int[4]{ 8, 7, 5, 6 }, // 우
        //};

        //public static readonly List<int[]> rectIndexes = new List<int[]>(6)
        //{
        //    new int[4]{ 6, 2, 3, 8 }, // 앞
        //    new int[4]{ 1, 5, 7, 4 }, // 뒤
        //    new int[4]{ 7, 6, 1, 3 }, // 위
        //    new int[4]{ 2, 4, 8, 5 }, // 아래
        //    new int[4]{ 7, 4, 6, 2 }, // 좌
        //    new int[4]{ 3, 8, 1, 5 }, // 우
        //};

        public static readonly Dictionary<Vector3, int> directionToRectIndex = new Dictionary<Vector3, int>()
    {
        {Vector3.forward, 0},
        {Vector3.back, 1},
        {Vector3.up, 2},
        {Vector3.down, 3},
        {Vector3.left, 4},
        {Vector3.right, 5},
    };

        public static readonly int[] triangles = new int[6] { 0, 1, 3, 3, 1, 2 };
        //public static readonly int[] triangles = new int[6] { 0, 1, 2, 2, 1, 3 };
        /// <summary>
        /// 0 1
        /// 3 2
        /// 
        /// 0 1
        /// 2 3
        /// </summary>
        public static readonly Vector2[] uvs = new Vector2[4]
        {
        // TL을 (0, 0) BR을 (1, 1)로 규정한다
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 1.0f),
        };

        public static List<RectMesh> rectMeshes = new List<RectMesh>()
    {
        new RectMesh()
    };

        public static RectMesh GetRectMesh(Vector3 faceDirection)
        {
            Debug.Log("GetRectMesh");
            var index = directionToRectIndex[faceDirection];

            var result = new RectMesh();
            Debug.Log($"index: {index}");
            result.vertices.AddRange(rectIndexes[index].Select(e => vertices[e - 1]).ToList());
            result.triangles.AddRange(triangles.Select(e => e).ToList());
            result.uv.AddRange(uvs.Select(e => e).ToList());

            return result;
        }
    }
}