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
            if (generator.keyboardMap.ValidateKeyMap())
            {
                if (PrefabUtility.IsPartOfAnyPrefab(generator.KeyboardGameObject.GetComponentInChildren<TextInputButton>().gameObject))
                {
                    string newPrefabName = RegenerateKeyboardPrefab(generator);
                    Debug.Log("Prefab alert! Created: " + newPrefabName);
                }
                else
                {
                    generator.RegenerateKeyboard();
                }
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
        List<GameObject> parentPrefabs = new List<GameObject>();
        List<string> sourceParentPrefabAssetPaths = new List<string>();
        List<string> parentPrefabAssetPaths = new List<string>();

        string sourceKeyboardPrefabAssetPath = "";
        string keyboardAssetPath = "";

        string extension = generator.keyboardMap.description + "-" + generator.keyPrefab.name;

        GameObject keyboardPrefab = PrefabUtility.GetNearestPrefabInstanceRoot(generator.KeyboardGameObject);

        //Loop through & unpack all parent prefabs of the keyboard 
        while (!PrefabUtility.IsOutermostPrefabInstanceRoot(keyboardPrefab))
        {
            GameObject currentRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(generator.gameObject);
            parentPrefabs.Add(currentRoot);
            parentPrefabAssetPaths.Add(NewAssetPath(currentRoot, extension));
            sourceParentPrefabAssetPaths.Add(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(currentRoot));

            PrefabUtility.UnpackPrefabInstance(currentRoot, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

            if (!generator.overWritePrefab)
            {
                currentRoot.name = "Generated_" + currentRoot.name;
            }
        }

        // Check if it already has keys in
        if (ContainsKeys(generator.KeyboardGameObject.transform, out keyboardPrefab))
        {
            if (keyboardPrefab != null)
            {
                sourceKeyboardPrefabAssetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(keyboardPrefab);
                keyboardAssetPath = NewAssetPath(keyboardPrefab, extension);
                PrefabUtility.UnpackPrefabInstance(keyboardPrefab, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                if (!generator.overWritePrefab)
                {
                    keyboardPrefab.name = "Generated " + generator.keyboardMap.description + " " + generator.keyPrefab.name + " Keyboard";
                }
            }
        }

        // Regenerate keymap
        generator.RegenerateKeyboard();

        string newPrefabName = "";

        if (keyboardPrefab != null)
        {
            keyboardAssetPath = generator.overWritePrefab ? sourceKeyboardPrefabAssetPath : keyboardAssetPath;
            PrefabUtility.SaveAsPrefabAssetAndConnect(keyboardPrefab, keyboardAssetPath, InteractionMode.AutomatedAction);
            newPrefabName = " | Child: " + keyboardAssetPath;
        }

        //Repack all parent prefabs in correct order
        for (int i = parentPrefabs.Count - 1; i >= 0; i--)
        {
            if (parentPrefabs[i] != null)
            {
                parentPrefabAssetPaths[i] = generator.overWritePrefab ? sourceParentPrefabAssetPaths[i] : parentPrefabAssetPaths[i];
                PrefabUtility.SaveAsPrefabAssetAndConnect(parentPrefabs[i], parentPrefabAssetPaths[i], InteractionMode.AutomatedAction);
                newPrefabName = " | Root: " + parentPrefabAssetPaths[i] + newPrefabName;
            }
        }
        newPrefabName = newPrefabName.Substring(3);
        return newPrefabName;
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
        string[] splitName = splitPath[0].Split('-');

        splitPath[0] = splitName[0] + "-" + extension;

        assetPath = splitPath[0] + "." + splitPath[1];

        return assetPath;
    }
}
