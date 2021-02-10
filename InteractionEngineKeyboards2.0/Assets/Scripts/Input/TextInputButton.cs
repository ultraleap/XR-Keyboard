using System;
using System.Linq;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;
using static KeyboardManager;

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
        UpdateActiveKey(NeutralKey, KeyboardMode.NEUTRAL);
    }

    public void UpdateActiveKey(KeyCode keyCode, KeyboardMode keyboardMode)
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
            keyCodeText = keyboardMode == KeyboardMode.SHIFT || keyboardMode == KeyboardMode.CAPS ? keyCodeText.ToUpper() : keyCodeText.ToLower();
        }
        switch (keyCode)
        {
            case KeyCode.Return:
                keyCodeText = "RETURN";
                break;
            case KeyCode.LeftShift:
            case KeyCode.RightShift:
                if (keyboardMode == KeyboardMode.NEUTRAL)
                {
                    keyCodeText = "shift";
                }
                else if (keyboardMode == KeyboardMode.SHIFT)
                {
                    keyCodeText = "Shift";
                }
                else if (keyboardMode == KeyboardMode.SHIFT)
                {
                    keyCodeText = "SHIFT";
                }
                break;
        }

        keyTextMesh.text = keyCodeText;
    }

    public void TextPress()
    {
        HandleKeyDown.Invoke(ActiveKey);
    }
}
