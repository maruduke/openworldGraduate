using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

using Core.Assist;
using Shape;
using Core.Flag;
using Core.Ext;
using UnityEditorInternal;
using System.Drawing;
using System.Diagnostics.Tracing;

public class World : MonoBehaviour
{
    public int seed;
    public Material material;

    public GameObject player;
    public Vector2 beforePlayerCoord;
    public Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();
    public Dictionary<Vector2, bool> visibleChunkCoords = new Dictionary<Vector2, bool>();

    public Circle visibleArea = new Circle(Vector2.zero, 1f);
    public Vector3 chunkSize = new Vector3(5, 5, 5);

    // Start is called before the first frame update
    void Start()
    {
        this.beforePlayerCoord = Vector2.negativeInfinity;
        this.GenerateSeed();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateVisibleChunks();
    }

    public void UpdateVisibleChunks()
    {
        var playerCoord = player.transform.position.ExtDivide(this.chunkSize).ExtFloorToInt().ExtVector2XZ();
        if(playerCoord == this.beforePlayerCoord)
            return;

        this.beforePlayerCoord = playerCoord;
        var visibleCoords = this.GetVisibleCoords(playerCoord);

        this.RemoveVisibleChunk(visibleCoords);
        this.GenerateVisibleChunks(visibleCoords);
        this.CalculateFaceOfVisibleChunksFromPlayer(player.transform);
        this.UpdateMeshOfVisibleChunks();
    }
    public bool FindChunk(Vector2 coord)
    {
        return this.chunks.ContainsKey(coord);
    }

    public bool GenerateChunk(Vector2 coord)
    {
        if (this.FindChunk(coord) is true)
            return false;

        var chunk = new Chunk(coord, this.chunkSize, this);
        this.chunks.Add(coord, chunk);

        return true;
    }

    public bool AreTwoChunksTouching(Vector2 chunkCoordA, Vector2 chunkCoordB)
    {
        if ((chunkCoordA - chunkCoordB).sqrMagnitude < 1)
            return false;

        if ((this.FindChunk(chunkCoordA) & this.FindChunk(chunkCoordB)) is false)
            return false;

        return true;
    }

    public void RemoveVisibleChunk(List<Vector2> visibleCoords)
    {
        var keys = new List<Vector2>(this.visibleChunkCoords.Keys);

        foreach(var key in keys)
        {
            if(visibleCoords.Contains(key) is true)
                continue;

            if (this.visibleChunkCoords.Remove(key) is false)
            {
                continue;
            }
        }
    }



    public void GenerateVisibleChunks(List<Vector2> visibleCoords)
    {
        foreach (var coord in visibleCoords)
        {
            if(this.GenerateChunk(coord) is false)
                continue;

            this.visibleChunkCoords.Add(coord, true);
        }
    }

    public void CalculateFaceOfVisibleChunksFromPlayer(Transform player)
    {
        var keys = new List<Vector2>(this.visibleChunkCoords.Keys);
        foreach (var coord in keys)
        {
            var flag = CalculateFaceOfChunkFromPlayer(coord, player.transform);
            if (this.chunks[coord].ChangeFaceFlag(flag) is false)
            {
                this.visibleChunkCoords[coord] = false;
                continue;
            }

            this.visibleChunkCoords[coord] = true;
        }
    }

    public void UpdateMeshOfVisibleChunks()
    {
        foreach (var (coord, isChanged) in this.visibleChunkCoords)
        {
            if (isChanged is false)
                continue;

            this.chunks[coord].UpdateGameObjectMesh();
        }
    }

    public Face CalculateFaceOfChunkFromPlayer(Vector2 chunkCoord, Transform player)
    {
        if (this.FindChunk(chunkCoord) is false)
            return (Face)0;

        var resizePlayerPosition = player.position.ExtDivide(this.chunkSize);

        var flag = (Face)0;
        
        if(chunkCoord.x - resizePlayerPosition.x >= 0) flag |= Face.Forward;
        else                                           flag |= Face.Backward;

        if(chunkCoord.y - resizePlayerPosition.z >= 0) flag |= Face.Left;
        else                                           flag |= Face.Right;

        if(resizePlayerPosition.y >= 0)                flag |= Face.Top;
        else                                           flag |= Face.Bottom;

        return flag;
    }

    public List<Vector2> GetVisibleCoords(Vector2 center)
    {
        var coords = new List<Vector2>();

        var radiusInt = (int)this.visibleArea.radius;
        for (var x = -radiusInt; x <= radiusInt; x++)
        for (var y = -radiusInt; y <= radiusInt; y++)
        {
            var pos = new Vector2(x, y);
            if(this.visibleArea.IsIn(pos) is false)
                continue;

            coords.Add(pos + center);
        }

        return coords;
    }

    public void GenerateSeed()
    {
        //var guid_32bit = Guid.NewGuid().ToShortString(32);
        //Debug.Log($"GUID 32BIT: {guid_32bit}");
        //Debug.Log($"INT 32BIT: {Int32.Parse(guid_32bit)}");
    }
}
