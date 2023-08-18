using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using UnityEngine.AddressableAssets;

using System.IO;


public class GroupSave : MonoBehaviour
{
    /*
        본인 오브젝트의 자식 오브젝트를 특정 폴더 내부에 저장

    */

    //label 설정으로 해결

    public void save()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        
        var obj = this.gameObject.transform;
        var group = settings.FindGroup(obj.name);

        string path = $"Assets/testing/{obj.name}/";

        
        CheckAndCreateFolder(path);

        // addressable group 존재 체크 및 확인
        if (!group)
            group = settings.CreateGroup(obj.name, false, false, true, null, typeof(ContentUpdateGroupSchema), typeof(BundledAssetGroupSchema));

        // addressable label 추가
        if (!settings.GetLabels().Contains(obj.name))
            settings.AddLabel(obj.name);
        
        // addressable 그룹에 항목 저장 및 labels 지정
        if (settings)
        {

            for(int i = 0; i< obj.childCount; i++) 
            {
                var child = obj.GetChild(i).gameObject;
                child = PrefabUtility.SaveAsPrefabAsset(child, $"{path}{i}.prefab");

                var assetpath = AssetDatabase.GetAssetPath(child.GetInstanceID());
                var guid = AssetDatabase.AssetPathToGUID(assetpath);
   

                var assetentry = settings.CreateOrMoveEntry(guid, group);

                assetentry.labels.Add(obj.name);
                settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryModified, assetentry, true);
            }
    
        }



    }

    void CheckAndCreateFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("Folder created at path: " + path);
        }

        else
        {
            Debug.Log("Folder already exists at path: " + path);
        }        
    }    


}


[CustomEditor(typeof(GroupSave))]
public class Test : Editor
{
    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();

        GroupSave myTarget = (GroupSave) target;
        if(GUILayout.Button("test")) {
            myTarget.save();
        }

       
    }

}




