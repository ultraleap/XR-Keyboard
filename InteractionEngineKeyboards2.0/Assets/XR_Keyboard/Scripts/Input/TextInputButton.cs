using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static KeyboardManager;

public class TextInputButton : MonoBehaviour
{
    public delegate void KeyDown(KeyCode keyCode);
    public static event KeyDown HandleKeyDown;
    public delegate void KeyUp();
    public static event KeyUp HandleKeyUp;
    public delegate void KeyDownSpecialChar(KeyCodeSpecialChar keyCode);
    public static event KeyDownSpecialChar HandleKeyDownSpecialChar;
    public delegate void LongPress(List<KeyCodeSpecialChar> specialChars);
    public static event LongPress HandleLongPress;
    public KeyCode NeutralKey;
    public KeyCode Symbols1Key;
    public KeyCode Symbols2Key;
    public KeyCodeSpecialChar ActiveSpecialChar = KeyCodeSpecialChar.NONE;
    public bool UseSpecialChar = false;
    public float longPressTime = 1f;
    private KeyCode ActiveKey;
    private InteractionButton interactionButton;
    private Button button;
    private TextMeshPro keyTextMesh;
    private TextMeshProUGUI keyTextMeshGUI;
    private IEnumerator LongPressDetectorCoroutine, LongPressCoroutine;


    // Start is called before the first frame update
    public void Awake()
    {
        interactionButton = GetComponentInChildren<InteractionButton>();
        if (interactionButton != null)
        {
            interactionButton.OnPress += TextPress;
            interactionButton.OnUnpress += ()=> HandleKeyUp.Invoke();
        }
        button = GetComponentInChildren<Button>();
        if (button != null) { button.onClick.AddListener(TextPress); }

        UpdateActiveKey(NeutralKey, KeyboardMode.NEUTRAL);
    }

    public void UpdateActiveKey(KeyCode keyCode, KeyboardMode keyboardMode)
    {
        if (keyTextMesh == null)
        {
            keyTextMesh = transform.GetComponentInChildren<TextMeshPro>();
        }

        if (keyTextMeshGUI == null)
        {
            keyTextMeshGUI = transform.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (keyTextMesh == null && keyTextMeshGUI == null)
        {
            return;
        }

        ActiveKey = keyCode;
        string keyCodeText;

        if (UseSpecialChar)
        {
            KeyboardCollections.KeyCodeSpecialCharToString.TryGetValue(ActiveSpecialChar, out keyCodeText);
        }
        else
        {
            KeyboardCollections.KeyCodeToString.TryGetValue(keyCode, out keyCodeText);
        }

        if (KeyboardCollections.AlphabetKeyCodes.Contains(keyCode) || UseSpecialChar)
        {
            keyCodeText = keyboardMode == KeyboardMode.SHIFT || keyboardMode == KeyboardMode.CAPS ? keyCodeText.ToUpper() : keyCodeText.ToLower();
        }
        switch (keyCode)
        {
            case KeyCode.Backspace:
                keyCodeText = "<-";
                break;
            case KeyCode.Return:
                keyCodeText = "return";
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
        UpdateKeyText(keyCodeText);
    }
    protected void UpdateKeyText(string text)
    {
        if (keyTextMesh != null) { keyTextMesh.text = text; }
        if (keyTextMeshGUI != null) { keyTextMeshGUI.text = text; }

    }
    public void TextPress()
    {
        LongPressDetectorCoroutine = LongpressDetection();
        StartCoroutine(LongPressDetectorCoroutine);

        KeyDownEvent();
    }

    private IEnumerator LongpressDetection()
    {
        float longpressThreshold = Time.time + longPressTime;
        bool longPressed = false;
        while (interactionButton.isPressed && !longPressed)
        {
            if (Time.time > longpressThreshold)
            {
                InvokeLongPress();
                longPressed = true;
            }
            yield return null;
        }
    }

    private void InvokeLongPress()
    {
        switch (ActiveKey)
        {
            case KeyCode.Backspace:
                LongPressCoroutine = BackspaceLongPress();
                StartCoroutine(LongPressCoroutine);
                break;
            default:
                if (KeyboardCollections.CharacterToSpecialChars.ContainsKey(ActiveKey))
                {
                    KeyboardManager.AccentKeyAnchor = transform;
                    HandleLongPress.Invoke(KeyboardCollections.CharacterToSpecialChars[ActiveKey]);
                }
                break;
        }
    }

    private IEnumerator BackspaceLongPress()
    {
        float timeStep = 0.1f;
        float nextPress = 0;
        while (interactionButton.isPressed)
        {
            if (Time.time > nextPress)
            {
                nextPress = Time.time + timeStep;
                KeyDownEvent();
            }
            yield return null;
        }
    }

    private void KeyDownEvent()
    {
        if (UseSpecialChar)
        {
            HandleKeyDownSpecialChar(ActiveSpecialChar);
        }
        else
        {
            HandleKeyDown.Invoke(ActiveKey);
        }
    }
}
