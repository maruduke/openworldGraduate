using System;
using System.Collections.Generic;
using UnityEngine;

using Core.Flag;
using Core.Assist;
using Core.Ext;

using Shape.CustomMesh;

namespace Shape.DataShape
{
    [Serializable]
    public class ChunkTerrainData
    {
        public Vector3Int size;
        public ChunkTerrainMesh mesh;

        public CuboidData<int> blocks;
        public CuboidData<Face> renderFaces;

        public List<Vector3> visibleRenderFacePositions;

        public ChunkTerrainData(Vector3 size)
        {
            this.size = new Vector3Int((int)size.x, (int)size.y, (int)size.z);
            this.mesh = new ChunkTerrainMesh();
            this.blocks = new CuboidData<int>(this.size.x, this.size.y, this.size.z);
            this.renderFaces = new CuboidData<Face>(this.size.x, this.size.y, this.size.z);

            this.visibleRenderFacePositions = new List<Vector3>();

            this.ResetData();
            //this.RecalculrateRenderFaces();
            //this.RecalculateMeshV2();
        }

        public void ResetData()
        {
            this.blocks.Fill(-1);
            this.renderFaces.Fill(Face.All);
            this.mesh.Clear();
        }

        public Face CalculateFacesOfPositionInBlock(int x, int y, int z)
        {
            if (this.IsIn(x, y, z) is false | this.blocks.Get(x, y, z) < 0)
                return Face.None;

            var face = this.renderFaces.Get(x, y, z);
            foreach (var direction in Vector3Assist.directions3D)
            {
                var neibhor = new int[] { x + (int)direction.x, y + (int)direction.y, z + (int)direction.z };
                if (this.IsIn(neibhor[0], neibhor[1], neibhor[2]) is false)
                    continue;

                if (this.blocks.Get(neibhor[0], neibhor[1], neibhor[2]) < 0)
                    continue;

                face = face.ExtBitAndNot(direction.ExtToFace().Value);
            }

            return face;
        }

        public void RecalculrateRenderFaces(Face flag)
        {
            for (int x = 0; x < this.size.x; x++)
                for (int y = 0; y < this.size.y; y++)
                    for (int z = 0; z < this.size.z; z++)
                    {
                        var value = this.CalculateFacesOfPositionInBlock(x, y, z);
                        if (value.ExtBitAnd(flag) == Face.None)
                            continue;

                        this.visibleRenderFacePositions.Add(new Vector3(x, y, z));
                        this.renderFaces.Set(value, x, y, z);
                    }
        }

        public void RecalculrateRenderFacesOrigin()
        {
            for (int x = 0; x < this.size.x; x++)
                for (int y = 0; y < this.size.z; y++)
                    for (int z = 0; z < this.size.z; z++)
                    {
                        var value = this.CalculateFacesOfPositionInBlock(x, y, z);
                        if (value == Face.None)
                            continue;

                        this.visibleRenderFacePositions.Add(new Vector3(x, y, z));
                        this.renderFaces.Set(value, x, y, z);
                    }
        }

        //public void RecalculrateRenderFacesOrigin()
        //{
        //    this.RecalculateRenderFaces(Face.All);
        //    //for (int x = 0; x < this.size.x; x++)
        //    //for (int y = 0; y < this.size.z; y++)
        //    //for (int z = 0; z < this.size.z; z++)
        //    //{
        //    //    var value = this.CalculateFacesOfPositionInBlock(x, y, z);
        //    //    this.renderFaces.Set(value, x, y, z);
        //    //}
        //}
        
        public Mesh MakeToUnityMeshOrigin()
        {
            this.mesh.Clear();
            for (int x = 0; x < this.size.x; x++)
            for (int y = 0; y < this.size.z; y++)
            for (int z = 0; z < this.size.z; z++)
            {
                this.mesh.Add(
                    this.blocks.Get(x, y, z),
                    this.renderFaces.Get(x, y, z), new Vector3(x, y, z));
            }

            return this.mesh.ToUnityMesh();
        }

        public void RecalculateRenderFaces(Face flag)
        {
            Debug.Log("RecalculateRenderFaces");
            this.visibleRenderFacePositions.Clear();
            this.RecalculrateRenderFaces(flag);
        }

        public void RecalculateMesh()
        {
            Debug.Log($"RecalculateMeshV2 Test: {this.visibleRenderFacePositions.Count}");
            this.mesh.Clear();
            foreach(var position in this.visibleRenderFacePositions)
                this.mesh.Add(this.blocks.Get((int)position.x, (int)position.y, (int)position.z), 
                              this.renderFaces.Get((int)position.x, (int)position.y, (int)position.z), position);
        }

        public bool IsIn(int x, int y, int z)
        {
            if (x < 0 || this.size.x <= x)
                return false;

            if (y < 0 || this.size.y <= y)
                return false;

            if (z < 0 || this.size.z <= z)
                return false;

            return true;
        }
    }
}
