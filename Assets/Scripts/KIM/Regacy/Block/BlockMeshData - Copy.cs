//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class BlockMeshDataIndex {
//    public List<int> verticeIndexes;
//    public List<int> uvsIndexes; 
//    public int trianleIndex;

//    public BlockMeshDataIndex(List<Vector3> verticeIndexes, List<Vector3> uvsIndexes, int triangleIndex)
//    {
//    }

//    public static BlockMeshDataIndex CreateBlockMesh()
//    {
//        return null;
//    }
//}

//public class BlockMeshData
//{
//    public static readonly Vector3 center = new Vector3(0.0f, 0.0f, 0.0f);

//    public static readonly List<Vector3> points = new List<Vector3>()
//    {
//        // 블록을 형성하는 점의 위치
//        // 서로 상반되는 위치의 벡터들을 기준으로 정렬되어있음
//        new Vector3( 0.5f,  0.5f,  0.5f),
//        new Vector3(-0.5f, -0.5f, -0.5f),

//        new Vector3( 0.5f,  0.5f, -0.5f),
//        new Vector3(-0.5f, -0.5f,  0.5f),

//        new Vector3( 0.5f, -0.5f,  0.5f),
//        new Vector3(-0.5f,  0.5f, -0.5f),

//        new Vector3(-0.5f,  0.5f,  0.5f),
//        new Vector3( 0.5f, -0.5f, -0.5f),
//    };

//    public static readonly List<Vector2> uvs = new List<Vector2>()
//    {
//        // TL을 (0, 0) BR을 (1, 1)로 규정한다
//        new Vector2(0.0f, 0.0f),
//        new Vector2(0.0f, 1.0f),
//        new Vector2(1.0f, 0.0f),
//        new Vector2(1.0f, 1.0f),
//    };

//    public static readonly Dictionary<Vector3, int[]> verticesIndex = new Dictionary<Vector3, int[]>()
//    {
//        {Vector3.forward, new int[] { 6, 2, 3, 8 } }, // 앞
//        {Vector3.back,    new int[] { 1, 5, 7, 4 } }, // 뒤
//        {Vector3.up,      new int[] { 7, 6, 1, 3 } }, // 위
//        {Vector3.down,    new int[] { 2, 4, 8, 5 } }, // 아래
//        {Vector3.left,    new int[] { 7, 4, 6, 2 } }, // 좌
//        {Vector3.right,   new int[] { 3, 8, 1, 5 } }, // 우
//    };

//    public static readonly List<int[]> verticesIndex2 = new List<int[]>()
//    {
//        { new int[] { 6, 2, 3, 8 } }, // 앞
//        { new int[] { 1, 5, 7, 4 } }, // 뒤
//        { new int[] { 7, 6, 1, 3 } }, // 위
//        { new int[] { 2, 4, 8, 5 } }, // 아래
//        { new int[] { 7, 4, 6, 2 } }, // 좌
//        { new int[] { 3, 8, 1, 5 } }, // 우
//    };

//    public static readonly Dictionary<Vector3, BlockMeshDataIndex> test = new Dictionary<Vector3, BlockMeshDataIndex>()
//    {
//        {Vector3.forward, new BlockMeshDataIndex(new List<Vector3>(){ }) }
//    };

//    public static readonly List<int[]> triangles = new List<int[]>()
//    {
//        // xy 좌표를 앞으로 규정하여 삼각형을 그린다
//        { new int[]{ 6, 2, 3, 3, 2, 8 } }, // 앞
//        { new int[]{ 1, 5, 7, 7, 5, 4 } }, // 뒤

//        { new int[]{ 7, 6, 1, 1, 6, 3 } }, // 위
//        { new int[]{ 2, 4, 8, 8, 4, 5 } }, // 아래

//        { new int[]{ 7, 4, 6, 6, 4, 2 } }, // 좌
//        { new int[]{ 3, 8, 1, 1, 8, 5 } }, // 우
//    };

//    //public static readonly Dictionary<Vector3, >

//    public static Mesh GetQuadMesh(Vector3 unitVector)
//    {
//        var result = new Mesh();
//        result.vertices = GetVertices(unitVector);
//        result.triangles = triangles(unitVector);
//        result.uvs = uvs.ToArray();

//        return result;
//    }

//    private static Vector3[] GetVertices(Vector3 unitVector)
//    {
//        var result = new List<Vector3>() { };
//        foreach(var idx in verticesIndex[unitVector])
//        {
//            result.Add(points[idx]);
//        }
//        return result.ToArray();
//    }

//    public static void GetMesh()
//    {

//    }
//}