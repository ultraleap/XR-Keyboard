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
    void Awake()
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

        if (!keyPrefab.transform.GetComponentInChildren<TextInputButton>())
        {
            throw new System.Exception("Ensure prefab contains an object with the TextInputButton component");
        }

        var keyMap = keyboardMap.GetKeyMap();
        foreach(Transform row in keyboardRows)
        {
            for(int i = row.childCount - 1; i >= 0; i--)
            {
                if (row.GetChild(i).GetComponentInChildren<TextInputButton>())
                {
                    DestroyImmediate(row.GetChild(i).gameObject);
                }
            }
        }
        GenerateKeyboard();
    }

    public void GenerateKeyboard()
    {
        var keyMap = keyboardMap.GetKeyMap();
        for(int i = 0; i < keyboardRows.Length; i++)
        {
            foreach (var key in keyMap[i])
            {
                GameObject newKey = Instantiate(keyPrefab, keyboardRows[i]);
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
