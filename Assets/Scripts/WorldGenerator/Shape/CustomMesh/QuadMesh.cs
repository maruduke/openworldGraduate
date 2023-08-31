using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shape.CustomMesh
{
    public class QuadMesh : BaseMesh   
    {
        public Guid id;
        public Vector3 position;
        public Vector3 direction;

        public List<Vector3> normals;
        public QuadMesh(Guid guid, Vector3 position, Vector3 direction) : base()
        {
            this.id = guid;
            this.position = position;
            this.direction = direction;
        }
        public Mesh ToUnityMesh()
        {
            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();

            vertices.AddRange(this.vertices.Select(e => e + this.position));
            triangles.AddRange(this.triangles.Select(e => e));
            uvs.AddRange(this.uvs);

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
