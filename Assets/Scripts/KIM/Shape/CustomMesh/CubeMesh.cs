using Core.Assist;
using Core.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UI;
using UnityEngine;

using Core.Data;
using Core.Flag;

namespace Shape.CustomMesh
{
    public class CubeMesh
    {
        public Dictionary<Vector3, List<Guid>> directions;
        public Dictionary<Vector3, List<Guid>> positions;
        
        public Dictionary<Guid, QuadMesh> quadMeshes;

        public CubeMesh()
        {
            this.directions = new Dictionary<Vector3, List<Guid>>();
            this.positions  = new Dictionary<Vector3, List<Guid>>();
            this.quadMeshes = new Dictionary<Guid, QuadMesh>();
        }

        public void Clear()
        {
            this.quadMeshes.Clear();
            this.positions.Clear();
            this.directions.Clear();
        }

        public void AddListValueTypeOfDictionary(ref Dictionary<Vector3, List<Guid>> dict, Vector3 key, Guid Value)
        {
            if (dict.ContainsKey(key) is false)
            {
                dict.Add(key, new List<Guid>());
            }

            dict[key].Add(Value);
        }

        public void Add(Face flag, Vector3 position)
        {
            foreach (var direction in Vector3Assist.directions3D)
            {
                var face = direction.ExtToFace();
                if (face == null)
                    continue;

                if (flag.ExtContains(face.Value) is false)
                    continue;

                var quadMesh = new QuadMesh(Guid.NewGuid(), position, direction);
                var index = direction.ExtToCubeMeshDataRectIndex();

                quadMesh.vertices.AddRange(CubeMeshData.rectIndexes[index.Value].Select(e => CubeMeshData.vertices[e - 1]));
                quadMesh.triangles.AddRange(CubeMeshData.triangles);
                quadMesh.uvs.AddRange(CubeMeshData.uvs);
                
                this.AddListValueTypeOfDictionary(ref this.positions, position, quadMesh.id);
                this.AddListValueTypeOfDictionary(ref this.directions, direction, quadMesh.id);
                this.quadMeshes.Add(quadMesh.id, quadMesh);
            }
        }

        public Mesh ToUnityMesh(Face flag)
        {
            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();

            int pointCount = 0;
            foreach(var direction in Vector3Assist.directions3D)
            {
                var face = direction.ExtToFace();
                if(face is null)
                    continue;

                if(flag.ExtContains(face.Value) is false)
                    continue;

                if (this.directions.ContainsKey(direction) is false)
                    continue;

                foreach (var guid in this.directions[direction])
                {
                    var quadMesh = this.quadMeshes[guid];
                    
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
