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

namespace Shape.CustomMesh
{
    public class QuadMesh : CustomMesh
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
    }
}
