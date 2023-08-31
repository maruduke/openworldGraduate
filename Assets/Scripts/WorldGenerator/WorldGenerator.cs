using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

using Core.Flag;
using Core.Noise;
using System.Drawing;
using System;
using Shape;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
[ExecuteInEditMode]
#endif

#if UNITY_EDITOR
public class WorldGenerator : MonoBehaviour
{
    public Vector2 from;
    public Vector2 to;
    public Vector3 chunkTerrainSize = new Vector3(20, 40, 20);
    public int childrenResetCount;

    public Vector2 sceneSize = new Vector2(100, 100);

    public string firstName = "Terrain";
    public string chunkTerrainMapPath = "Assets/Resources/Maps/";
    public string materialpath = "Materials/material";

    public string addressableAssetsGroupName = "World";

    public void Work()
    {
        Debug.Log("WorkTest");
        var material = Resources.Load<Material>(this.materialpath);

        for (var x = from.x; x <= to.x; x++)
        {
            for (var z = from.y; z <= to.y; z++)
            {
                //this.Remove(this.childrenResetCount);

                var coord = new Vector2(x, z);
                var scenePosition = new Vector2(x * this.chunkTerrainSize.x, z * this.chunkTerrainSize.z);

                var folderName = $"Map" +
                    $"{Mathf.FloorToInt(scenePosition.x / this.sceneSize.x) * this.sceneSize.x}" +
                    $"_" +
                    $"{Mathf.FloorToInt(scenePosition.y / this.sceneSize.y) * this.sceneSize.y}";
                var folderFullName = $"{this.chunkTerrainMapPath}{folderName}/";
                var meshAssetName = $"Terrain{coord}.asset";
                var gameObjectPrefabName = $"Terrain{coord}.prefab";

                if (AssetDatabase.IsValidFolder(folderFullName) is false)
                    AssetDatabase.CreateFolder(this.chunkTerrainMapPath.Substring(0, this.chunkTerrainMapPath.Length - 1), folderName);

                var chunkTerrainAgent = new ChunkTerrain(coord, this.chunkTerrainSize);
                var mesh = chunkTerrainAgent.UpdateAndMakeUnityMesh(Core.Flag.Face.All);

                AssetDatabase.CreateAsset(mesh, folderFullName + meshAssetName);
                AssetDatabase.Refresh();

                var go = this.GenerateTerrain(coord, this.chunkTerrainSize);
                go.GetComponent<MeshRenderer>().material = material;
                go.GetComponent<MeshCollider>().sharedMesh = mesh;
                go.GetComponent<MeshFilter>().mesh = mesh;

                PrefabUtility.SaveAsPrefabAsset(go, folderFullName + gameObjectPrefabName);
            }
        }
    }

    public GameObject GenerateTerrain(Vector2 coord, Vector3 size)
    {
        var go = new GameObject($"ChunkTerrain,{coord}");
        var newPosition = new Vector3(coord.x * size.x, 0, coord.y * size.z);
        go.transform.position = newPosition;
        go.transform.parent = this.gameObject.transform;

        go.AddComponent<MeshRenderer>();
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshCollider>();
        //go.AddComponent<TerrainScript>();

        return go;
    }

    public void RemoveChildren(int thresholodChildrenCount)
    {
        Debug.Log("RemoveTest");
        if (this.transform.childCount <= thresholodChildrenCount)
            return;

        while (this.transform.childCount > 0)
        {
            var child = this.transform.GetChild(0);
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    public void Load()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings)
        {
            var di = new DirectoryInfo(Application.dataPath + this.chunkTerrainMapPath.Split("Assets")[1]);
            foreach (FileInfo file in di.GetFiles("*.prefab", SearchOption.AllDirectories))
            {
                var labelName = file.Directory.Name;
                var groupName = file.Directory.Name;

                var group = settings.FindGroup(groupName);
                if (!group)
                    group = settings.CreateGroup(groupName, false, false, true, null, typeof(ContentUpdateGroupSchema), typeof(BundledAssetGroupSchema));

                // addressable label Ãß°¡
                if (!settings.GetLabels().Contains(labelName))
                    settings.AddLabel(labelName);

                var go = AssetDatabase.LoadAssetAtPath("Assets" + file.FullName.Split("Assets")[1], typeof(GameObject));
                var assetPath = AssetDatabase.GetAssetPath(go);

                var guid = AssetDatabase.AssetPathToGUID(assetPath);
                var entry = settings.CreateOrMoveEntry(guid, group);

                entry.labels.Add(labelName);
                settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryModified, entry, true);
            }
        }
    }

    public void DeleteAssetInFolder(string filter, string folderPath)
    {
        foreach (var asset in AssetDatabase.FindAssets(filter, new string[] { folderPath }))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }
    }
}
#endif