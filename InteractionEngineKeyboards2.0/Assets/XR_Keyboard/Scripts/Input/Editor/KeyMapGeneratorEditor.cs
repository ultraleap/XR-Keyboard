using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyMapGenerator))]
public class KeyMapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        KeyMapGenerator generator = (KeyMapGenerator)target;

        
        GUILayout.Label("");
        if (GUILayout.Button("Regenerate Keyboard"))
        {
            if (PrefabUtility.IsPartOfAnyPrefab(generator.transform.GetComponentInChildren<TextInputButton>().gameObject))
            {
                string newPrefab = RegenerateKeyboardPrefab(generator);
                Debug.Log("Prefab alert! Created: " + newPrefab);
            }
            else
            {
                generator.RegenerateKeyboard();
            }
        }
        EditorGUILayout.HelpBox("Good news! This regenerator button does some stuff that helps " +
           " your prefabs work without creating thousands of overrides.", MessageType.Info);
    }

    private string RegenerateKeyboardPrefab(KeyMapGenerator generator)
    {
        // Find the root of the prefab
        GameObject root = PrefabUtility.GetOutermostPrefabInstanceRoot(generator.gameObject);
        string rootAssetPath = "";
        string childAssetPath = "";

        GameObject childPrefab = null;
        // Unlink the outer prefab
        if (root != null)
        {
            rootAssetPath = NewAssetPath(root);
            PrefabUtility.UnpackPrefabInstance(root, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);
        }
        
        // Check if it already has keys in
        if (ContainsKeys(generator.transform, out childPrefab))
        {
            if (childPrefab != null)
            {
                childAssetPath = NewAssetPath(childPrefab);
                PrefabUtility.UnpackPrefabInstance(childPrefab, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);
            }
           
        }
        
        // Regenerate keymap
        generator.RegenerateKeyboard();

        if (childPrefab != null)
        {
            // Save a new prefab with "regenerated" name extension (override if exists)
            PrefabUtility.SaveAsPrefabAssetAndConnect(childPrefab, childAssetPath, InteractionMode.UserAction);
        }
        if (root != null)
        {
            // Save a new prefab with "regenerated" name extension (override if exists)
            PrefabUtility.SaveAsPrefabAssetAndConnect(root, rootAssetPath, InteractionMode.UserAction);
        }

        return "Root: " + rootAssetPath + " | Child: "  + childAssetPath;
    }

    private bool ContainsKeys(Transform prefab, out GameObject childPrefab)
    {
        TextInputButton[] keys = prefab.transform.GetComponentsInChildren<TextInputButton>();

        childPrefab = PrefabUtility.GetNearestPrefabInstanceRoot(keys[0]);

        return keys.Length > 0;
    }

    /// <Summary>
    /// Takes a prefab Game Object and generates a new path marked as regenerated to avoid overriding the original asset.
    /// Only one regenerated asset will be created, overriding the previous one.
    /// </Summary>
    private string NewAssetPath(GameObject prefabAsset)
    {
        string assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefabAsset);
        string[] splitPath = assetPath.Split('.');

        if (!splitPath[0].Contains("Regenerated"))
        {
            splitPath[0] += "-Regenerated";
        }

        assetPath = splitPath[0] + "." + splitPath[1];

        return assetPath;
    }
}
