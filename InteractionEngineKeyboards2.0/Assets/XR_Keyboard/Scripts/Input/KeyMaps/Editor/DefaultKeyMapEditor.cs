using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(DefaultKeyMap))]
public class DefaultKeyMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DefaultKeyMap map = (DefaultKeyMap)target;

        if (GUILayout.Button("Write To Json"))
        {
            WriteToJSON(map);
        }
    } 

    public void WriteToJSON(DefaultKeyMap map)
    {
        if (map.keyMap.Count == 0 || map.keyMap[0].row.Count == 0) 
        {
            map.InitialiseKeyboardMap();
        }
        
        string jsonMap = JsonUtility.ToJson(map, true);
        
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "DefaultMap.json"), jsonMap);
        Debug.Log(jsonMap.ToString());
    }
}
