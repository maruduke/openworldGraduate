using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;

using Shape.CustomMesh;
using Core.Assist;
using Core.Noise;
using Core.Flag;
using Core.Ext;
using UnityEngine.UI;

namespace Shape
{

    public class Chunk
    {
        public Field data;
        public GameObject chunkObject;

        public Vector2 coord;
        public Vector3 size;

        public Vector3 bottomCenter;

        public Face visibleFaces;

        public World world;
        public bool activate;

        public Chunk(Vector2 coord, Vector3 size, World world)
        {
            this.coord = coord;
            this.size = size;
            this.world = world; // parent
            this.visibleFaces = Face.All;
            this.activate = true;
            this.chunkObject = null;

            this.InitMapData();
            this.InitNoiseDataToMap();
            this.InitGameObejct();
        }

        public void InitNoiseDataToMap()
        {
            for (int x = 0; x < this.size.x; x++)
            for (int z = 0; z < this.size.z; z++)
            {
                var height = Mathf.FloorToInt(NoiseFunc.Perlin2D(1000, this.coord.x + x, this.coord.y + z) * this.size.y);
                for (int y = 0; y < height; y++)
                {
                    this.data.blocks.Set(10, x, y, z);
                }
            }
        }

        public void InitMapData()
        {
            this.data = new Field(this.size);
        }
        public void InitGameObejct()
        {
            //var go = new GameObject("Chunk");
            //go.AddComponent<MeshRenderer>();
            //go.AddComponent<MeshFilter>();
            //go.transform.parent = this.world.transform;

            //go.transform.position = new Vector3(this.coord.x * this.size.x, 0, this.coord.y * this.size.z);
            //this.chunkObject = go;
        }

        public void GenerateChunkObject()
        {
            var go = new GameObject("Chunk");
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>();
            //go.transform.parent = Vector3.zero;

            go.transform.position = new Vector3(this.coord.x * this.size.x, 0, this.coord.y * this.size.z);
            this.chunkObject = go;
        }

        public GameObject CreateGameObject()
        {
            var go = new GameObject("Chunk");
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>();
            go.transform.position = new Vector3(this.coord.x * this.size.x, 0, this.coord.y * this.size.z);
            return go;
        }

        public void DeleteChunkObject()
        {
            //UnityEngine.Object.DestroyObject(this.chunkObject);
        }

        public Mesh MakeUnityMesh(Face face)
        {
            this.data.RecalculraterenderFaces();
            this.data.RecalculateMesh();
            return this.data.mesh.ToUnityMesh(face);
        }

        public void UpdateGameObjectMesh()
        {
            if(this.chunkObject == null)
                this.chunkObject = this.CreateGameObject();

            //this.chunkObject.GetComponent<MeshFilter>().mesh = MakeMesh();
            this.chunkObject.GetComponent<MeshFilter>().mesh.Clear();
            this.chunkObject.GetComponent<MeshFilter>().mesh = MakeUnityMesh(this.visibleFaces);
        }

        public bool ChangeFaceFlag(Face face)
        {
            if (this.visibleFaces == face)
                return false;

            this.visibleFaces = face;
            return true;
        }

        public bool RemoveFace(Face face)
        {
            return this.ChangeFaceFlag(this.visibleFaces.ExtBitAndNot(face));
        }
    }
}