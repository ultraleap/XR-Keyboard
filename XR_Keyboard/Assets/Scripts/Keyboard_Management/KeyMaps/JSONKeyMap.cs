using UnityEngine;
using System.IO;
using NaughtyAttributes;

public class JSONKeyMap : KeyMap
{
    [Tooltip("Provide a path to a JSON keymap file.")]
    public string path;

    [Button]
    public void LoadFromJSON()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else
        {
            Debug.LogError("Path does not exist for keymap file.");
        }
        ValidateKeyMap();
    }

    [Button]
    public void WriteNewJSON()
    {
        WriteToJSON(this);
    }

    public void WriteToJSON(JSONKeyMap map)
    {
        if (map.keyMap.Count == 0 || map.keyMap[0].row.Count == 0)
        {
            Debug.LogWarning("No map to write");
            return;
        }

        string jsonMap = JsonUtility.ToJson(map, true);

        File.WriteAllText(Path.Combine(Application.streamingAssetsPath + "/XR_Keyboard/KeyMaps", description + ".json"), jsonMap);
        Debug.Log(jsonMap.ToString());
    }
}
