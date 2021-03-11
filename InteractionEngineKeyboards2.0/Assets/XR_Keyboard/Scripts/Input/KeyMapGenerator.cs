using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[ExecuteInEditMode]
public class KeyMapGenerator : MonoBehaviour
{
    [OnValueChanged("RegenerateKeyboard")]
    public GameObject keyPrefab;

    [OnValueChanged("RegenerateKeyboard")]
    public KeyMap keyboardMap;

    [BoxGroup("Keyboard Connections")]
    public Transform[] keyboardRows = new Transform[5];


    [BoxGroup("Keyboard Connections")]
    public UIKeyboardResizer numberResizer;
    [BoxGroup("Keyboard Connections")]
    public UIKeyboardResizer keyboardResizer;

    // Start is called before the first frame update
    void Start()
    {
        RegenerateKeyboard();
    }

    public void RegenerateKeyboard()
    {
        if (keyboardMap == null)
        {  
            keyboardMap = GetComponent<KeyMap>();
            if (keyboardMap == null)
            {
                keyboardMap = gameObject.AddComponent<DefaultKeyMap>();
            }
        }

        keyboardMap.SetKeyRows(keyboardRows);

        if (!keyPrefab.transform.GetComponentInChildren<TextInputButton>())
        {
            throw new System.Exception("Ensure prefab contains an object with the TextInputButton component");
        }

        var keyMap = keyboardMap.GetKeyMap();
        foreach(KeyValuePair<Transform, List<KeyMap.KeyboardKey> > row in keyMap)
        {
            for(int i = row.Key.childCount - 1; i >= 0; i--)
            {
                if (row.Key.GetChild(i).GetComponentInChildren<TextInputButton>())
                {
                    DestroyImmediate(row.Key.GetChild(i).gameObject);
                }
            }
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
