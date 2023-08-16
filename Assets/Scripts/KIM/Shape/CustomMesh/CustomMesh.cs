using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
namespace Shape.CustomMesh
{
    public class CustomMesh
    {
        public List<Vector3> vertices;
        public List<int> triangles;
        public List<Vector2> uvs;

        public CustomMesh()
        {
            this.vertices = new List<Vector3>();
            this.triangles = new List<int>();
            this.uvs = new List<Vector2>();
        }
    }
}
