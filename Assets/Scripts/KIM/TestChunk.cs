using Shape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
public class TestChunk : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("RLBTBF");
        var chunk = new Chunk(Vector2.zero, new Vector3(5, 5, 5), null);
        chunk.GenerateChunkObject();
        chunk.UpdateGameObjectMesh();
        this.GenerateSeed();
        
    }
    public void GenerateSeed()
    {
        //var guid_32bit = Guid.NewGuid();
        //Debug.Log(guid_32bit.GetHashCode());
        //Debug.Log($"GUID 32BIT: {guid_32bit}");
        //Debug.Log($"STRING 32BIT: {guid_32bit.ToShortString()}");
        //Debug.Log($"INT 32BIT: {Int32.Parse(guid_32bit.ToShortString(32))}");

        //var random = UnityEngine.Random.RandomRange(0, 1000);
        //Debug.Log($"RANDOM RANGE: {random}, RASHCORD: {random.GetHashCode()}");

    }

    private void Update()
    {
        
    }
}