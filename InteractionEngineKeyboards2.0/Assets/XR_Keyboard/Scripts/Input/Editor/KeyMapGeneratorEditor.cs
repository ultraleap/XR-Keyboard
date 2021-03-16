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
                string newPrefabName = RegenerateKeyboardPrefab(generator);
                Debug.Log("Prefab alert! Created: " + newPrefabName);
            }
            else
            {
                generator.RegenerateKeyboard();
            }
        }
        EditorGUILayout.HelpBox(
            "Regeneration will result in new prefabs being created to avoid " + 
            "overriding the currently active one in the scene. Prefab names will " +
            "be appended with the name of the key prefab.", 
            MessageType.Info);
    }

    /// <Summary>
    /// Unpacks the existing parent (grab handles) and child (keyboard) prefabs where
    /// necessary and saves new prefabs after generating the keyboard.
    /// </Summary>
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
            rootAssetPath = NewAssetPath(root, generator.keyPrefab.name);
            PrefabUtility.UnpackPrefabInstance(root, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);

            root.name = "GeneratedKeyboardParentWith" + generator.keyPrefab.name;
        }
        
        // Check if it already has keys in
        if (ContainsKeys(generator.transform, out childPrefab))
        {
            if (childPrefab != null)
            {
                childAssetPath = NewAssetPath(childPrefab, generator.keyPrefab.name);
                PrefabUtility.UnpackPrefabInstance(childPrefab, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);
                
                childPrefab.name = "GeneratedKeyboardWith" + generator.keyPrefab.name;
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

    /// <Summary>
    /// Check to see if the prefab already contains Keyboard Keys. 
    /// Outputs a reference to the keyboard prefab if present.
    /// </Summary>
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
    private string NewAssetPath(GameObject prefabAsset, string extension = null)
    {
        string assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefabAsset);
        string[] splitPath = assetPath.Split('.');
        extension = extension == null ? "Regenerated" : extension;

        string[] splitName = splitPath[0].Split('-');

        splitPath[0] = splitName[0] + "-" + extension;

        assetPath = splitPath[0] + "." + splitPath[1];

        return assetPath;
    }
}
