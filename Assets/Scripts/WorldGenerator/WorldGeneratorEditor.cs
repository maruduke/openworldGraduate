using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[ExecuteInEditMode]
[CustomEditor(typeof(WorldGenerator))]
public class ChunkTerrainTestScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var enableMethodNames = new List<string>() {
            "Work",
            "Load",
        };

        if (enableMethodNames is not null)
            foreach (var name in enableMethodNames)
                if (GUILayout.Button(name))
                    typeof(WorldGenerator).GetMethod(name).Invoke(target, null);
    }
}
#endif