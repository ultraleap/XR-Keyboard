using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMapGenerator : MonoBehaviour
{
    public Transform[] keyboardRows = new Transform[5];

    public KeyMap keyboardMap;
    public GameObject keyPrefab;

    public UIKeyboardResizer numberResizer;
    public UIKeyboardResizer keyboardResizer;

    // Start is called before the first frame update
    void Start()
    {
        if (keyboardMap == null)
        {  
            keyboardMap = gameObject.AddComponent<DefaultKeyMap>();
        }
        keyboardMap.SetKeyRows(keyboardRows);

        if (!keyPrefab.transform.GetComponentInChildren<TextInputButton>())
        {
            throw new System.Exception("Ensure prefab contains an object with the TextInputButton component");
        }

        GenerateKeyboard();
    }


    public void GenerateKeyboard()
    {
        var keyMap = keyboardMap.GetKeyMap();
        foreach(KeyValuePair<Transform, List<KeyMap.KeyboardKey> > row in keyMap)
        {
            foreach (var key in row.Value)
            {
                GameObject newKey = Instantiate(keyPrefab, row.Key);
                TextInputButton button = newKey.GetComponentInChildren<TextInputButton>();
                button.NeutralKey = key.neutralKey;
                button.Symbols1Key = key.symbols1Key;
                button.Symbols2Key = key.symbols2Key;
                button.UpdateActiveKey(button.NeutralKey, KeyboardManager.KeyboardMode.NEUTRAL);
                newKey.name = button.NeutralKey.ToString();
            }
        }
        numberResizer.ResizeKeyboard();
        keyboardResizer.ResizeKeyboard();
    }
}
