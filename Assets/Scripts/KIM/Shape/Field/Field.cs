using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Shape.CustomMesh;
using Core.Flag;
using Core.Assist;
using Core.Ext;

namespace Shape
{
    public class Field
    {
        public int width;
        public int height;
        public int depth;

        public CubeMesh mesh;

        public Cube<int> blocks;
        public Cube<Face> renderFaces;

        public Field(Vector3 mapSize)
        {
            this.InitSize(mapSize);

            this.mesh = new CubeMesh();
            this.blocks = new Cube<int>(width, height, depth);
            this.renderFaces = new Cube<Face>(width, height, depth);

            this.ResetChunkData();
        }

        private void InitSize(Vector3 mapSize)
        {
            this.width = (int)mapSize.x;
            this.height = (int)mapSize.y;
            this.depth = (int)mapSize.z;
        }

        public void ResetChunkData()
        {
            //this.blocks.Fill(0);
            this.blocks.Fill(1);
            //this.blocks.Fill(-1);
            this.renderFaces.Fill(Face.All);
            //this.renderFaces.Fill(Face.All | (Face.Forward | Face.Backward));
            //this.renderFaces.Fill(FaceAssist.GetAllFace());
            this.mesh.Clear();
        }

        //public Face CalculateFacesOfPositionInblock(int x, int y, int z)
        //{
        //    // 현재 위치로 부터 주변에 블록이 있는지 확인을 하여 렌더링을 한다
        //    if (this.blocks.Get(x, y, z) < 0)
        //        return 0;

        //    var flag = FaceAssist.GetAllFace();
        //    // x: -left    +right
        //    // y: -bottom  +top
        //    // z: -forward +backward

        //    // left에 블록이 있을 경우
        //    if (x - 1 >= 0)
        //        if (this.blocks.Get(x - 1, y, z) is not -1)
        //            flag ^= Face.Left;

        //    // right에 블록이 있을 경우
        //    if (x + 1 < this.width)
        //        if (this.blocks.Get(x + 1, y, z) is not -1)
        //            flag ^= Face.Right;

        //    // bottom에 블록이 있을 경우
        //    if (y - 1 >= 0)
        //        if (this.blocks.Get(x, y - 1, z) is not -1)
        //            flag ^= Face.Bottom;

        //    // top에 블록이 있을 경우
        //    if (y + 1 < this.height)
        //        if (this.blocks.Get(x, y + 1, z) is not -1)
        //            flag ^= Face.Top;

        //    // forward에 블록이 있을 경우
        //    if (z - 1 >= 0)
        //        if (this.blocks.Get(x, y, z - 1) is not -1)
        //            flag ^= Face.Forward;

        //    // backward에 블록이 있을 경우
        //    if (z + 1 < this.depth)
        //        if (this.blocks.Get(x, y, z + 1) is not -1)
        //            flag ^= Face.Backward;

        //    return flag;
        //}

        public Face CalculateFacesOfPositionInBlockV2(int x, int y, int z)
        {
            // 현재 위치로 부터 주변에 블록이 있는지 확인을 하여 렌더링을 한다
            if (this.IsIn(x, y, z) is false)
                return Face.None;

            var face = this.renderFaces.Get(x, y, z);
            foreach (var direction in Vector3Assist.directions3D)
            {
                var neibhor = new int[] {x + (int)direction.x, y + (int)direction.y, z + (int)direction.z };
                if (this.IsIn(neibhor[0], neibhor[1], neibhor[2]) is false)
                    continue;
                
                if (this.blocks.Get(neibhor[0], neibhor[1], neibhor[2]) < 0)
                    continue;

                face = face.ExtBitAndNot(direction.ExtToFace().Value);
            }

            return face;
        }

        public void RecalculraterenderFaces()
        {
            for (int x = 0; x < this.width; x++)
                for (int y = 0; y < this.height; y++)
                    for (int z = 0; z < this.depth; z++)
                    {
                        var value = this.CalculateFacesOfPositionInBlockV2(x, y, z);

                        this.renderFaces.Set(value, x, y, z);
                    }
        }

        //public void Recalculraterenders()
        //{
        //    for (int x = 0; x < this.width; x++)
        //    for (int y = 0; y < this.height; y++)
        //    for (int z = 0; z < this.depth; z++)
        //    {
        //        var value = this.CalculateFacesOfPositionInBlockV2(x, y, z);
        //        Debug.Log($"RENDER VALUE: {value}");
        //        this.renderFaces.Set(value, x, y, z);
        //    }
        //}

        public void RecalculateMesh()
        {
            this.mesh.Clear();
            for (int x = 0; x < this.width; x++)
                for (int y = 0; y < this.height; y++)
                    for (int z = 0; z < this.depth; z++)
                    {
                        var renderValue = this.renderFaces.Get(x, y, z);
                        this.mesh.Add(renderValue, new Vector3(x, y, z));
                    }
        }

        public bool IsIn(int x, int y, int z)
        {
            if (x < 0 || this.width <= x)
                return false;

            if (y < 0 || this.height <= y)
                return false;

            if (z < 0 || this.depth <= z)
                return false;

            return true;
        }
    }
}
