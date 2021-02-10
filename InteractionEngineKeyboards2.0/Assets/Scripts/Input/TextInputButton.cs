using System;
using System.Linq;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class TextInputButton : MonoBehaviour
{
    public delegate void KeyDown(KeyCode keyCode);
    public static event KeyDown HandleKeyDown;
    public KeyCode NeutralKey;
    public KeyCode Symbols1Key;
    public KeyCode Symbols2Key;
    private KeyCode ActiveKey;
    TextMeshPro keyTextMesh;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateActiveKey(NeutralKey, false);
    }

    public void UpdateActiveKey(KeyCode keyCode, bool shift)
    {
        if (keyTextMesh == null)
        {
            keyTextMesh = transform.GetComponentInChildren<TextMeshPro>();
        }
        
        ActiveKey = keyCode;

        string keyCodeText = "";
        KeyboardCollections.KeyCodeToString.TryGetValue(keyCode, out keyCodeText);

        if (KeyboardCollections.AlphabetKeyCodes.Contains(keyCode))
        {
            keyCodeText = shift ? keyCodeText.ToUpper() : keyCodeText.ToLower();
        }
        if (keyCodeText == "\n") { keyCodeText = "RETURN"; }

        keyTextMesh.text = keyCodeText;
    }

    public void TextPress()
    {
        HandleKeyDown.Invoke(ActiveKey);
    }
}
