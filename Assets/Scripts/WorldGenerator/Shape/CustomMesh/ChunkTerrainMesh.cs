using Core.Assist;
using Core.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Core.Data;
using Core.Flag;

namespace Shape.CustomMesh
{
    public class ChunkTerrainMesh
    {
        private List<List<QuadMesh>> quadMeshes;

        public ChunkTerrainMesh()
        {
            this.quadMeshes = new List<List<QuadMesh>>();
        }

        public void Clear()
        {
            this.quadMeshes.Clear();
        }

        public void AddOrigin(Face flag, Vector3 position)
        {
            var cubeMesh = new List<QuadMesh>();
            foreach (var direction in Vector3Assist.directions3D)
            {
                var face = direction.ExtToFace().Value;

                if (flag.ExtContains(face) is false)
                    continue;

                var quadMesh = new QuadMesh(Guid.NewGuid(), position, direction);
                var index = direction.ExtToCubeMeshDataRectIndex().Value;

                quadMesh.vertices.AddRange(CubeMeshData.rectIndexes[index].Select(e => CubeMeshData.vertices[e - 1]));
                quadMesh.triangles.AddRange(CubeMeshData.triangles);
                quadMesh.uvs.AddRange(CubeMeshData.uvs);

                cubeMesh.Add(quadMesh);
            }
            this.quadMeshes.Add(cubeMesh);
        }

        public void Add(int blockID, Face flag, Vector3 position)
        {
            //var blockInfo = AllBlockInfo.blockNumbers[blockID];
            //var textureNumbers = blockInfo.textrueNumbers;
            
            var cubeMesh = new List<QuadMesh>();
            foreach (var direction in Vector3Assist.directions3D)
            {
                var face = direction.ExtToFace().Value;

                if (flag.ExtContains(face) is false)
                    continue;

                var quadMesh = new QuadMesh(Guid.NewGuid(), position, direction);
                var index = direction.ExtToCubeMeshDataRectIndex().Value;

                quadMesh.vertices.AddRange(CubeMeshData.rectIndexes[index].Select(e => CubeMeshData.vertices[e - 1]));
                quadMesh.triangles.AddRange(CubeMeshData.triangles);

                quadMesh.uvs.AddRange(CubeMeshData.uvs);

                cubeMesh.Add(quadMesh);
            }
            this.quadMeshes.Add(cubeMesh);
        }


        public Mesh ToUnityMesh()
        {
            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();

            int pointCount = 0;

            Debug.Log($"CubeMesh Count: {this.quadMeshes.Count}");
            foreach (var cubeMesh in this.quadMeshes)
            {
                foreach (var quadMesh in cubeMesh)
                {
                    vertices.AddRange(quadMesh.vertices.Select(e => e + quadMesh.position));
                    triangles.AddRange(quadMesh.triangles.Select(e => e + pointCount));
                    uvs.AddRange(quadMesh.uvs);
                    pointCount += CubeMeshData.numberOfEdge;
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
