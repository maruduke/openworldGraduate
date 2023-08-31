using System.Collections.Generic;
using UnityEngine;

namespace Shape.CustomMesh
{
    public class BaseMesh
    {
        public List<Vector3> vertices;
        public List<int> triangles;
        public List<Vector2> uvs;

        public BaseMesh()
        {
            this.vertices = new List<Vector3>();
            this.triangles = new List<int>();
            this.uvs = new List<Vector2>();
        }
    }
}