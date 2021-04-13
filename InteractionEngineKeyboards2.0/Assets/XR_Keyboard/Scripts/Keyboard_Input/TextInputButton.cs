using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KeyboardMode = Keyboard.KeyboardMode;

public class TextInputButton : MonoBehaviour
{
    public delegate void KeyUp(KeyCode keyCode, Keyboard sourceKeyboard);
    public static event KeyUp HandleKeyUp;
    public delegate void KeyUpSpecialChar(KeyCodeSpecialChar keyCode, Keyboard sourceKeyboard);
    public static event KeyUpSpecialChar HandleKeyUpSpecialChar;
    public delegate void LongPress(List<KeyCodeSpecialChar> specialChars, Keyboard sourceKeyboard);
    public static event LongPress HandleLongPress;
    public KeyCode NeutralKey;
    public KeyCode Symbols1Key;
    public KeyCodeSpecialChar ActiveSpecialChar = KeyCodeSpecialChar.NONE;
    public Vector2 keyScale = Vector3.one;

    public bool UseSpecialChar = false;
    public float longPressTime = 0.5f;
    private KeyCode ActiveKey;
    private InteractionButton interactionButton;
    private Button button;
    private TextMeshPro keyTextMesh;
    private TextMeshProUGUI keyTextMeshGUI;
    private TextMeshProUGUI accentLabelTextMeshGUI;
    private IEnumerator LongPressDetectorCoroutine, LongPressCoroutine;
    private Keyboard parentKeyboard;
    private bool longPressed = false;

    // Start is called before the first frame update
    public void Awake()
    {
        interactionButton = GetComponentInChildren<InteractionButton>();
        if (interactionButton != null)
        {
            interactionButton.OnPress += LongPressStart;
            interactionButton.OnUnpress += TextPress;
        }
        button = GetComponentInChildren<Button>();
        if (button != null) { button.onClick.AddListener(TextPress); }

        UpdateActiveKey(NeutralKey, KeyboardMode.NEUTRAL);

        parentKeyboard = GetComponentInParent<Keyboard>();
    }

    public void UpdateActiveKey(KeyCode keyCode, KeyboardMode keyboardMode)
    {
        if (keyTextMesh == null)
        {
            keyTextMesh = transform.GetComponentInChildren<TextMeshPro>();
        }
        
        var textMeshGUIs = transform.GetComponentsInChildren<TextMeshProUGUI>();
        if (keyTextMeshGUI == null && textMeshGUIs.Length > 0)
        {
            keyTextMeshGUI = textMeshGUIs[0];
        }

        if (accentLabelTextMeshGUI == null && textMeshGUIs.Length > 1)
        {
            // TODO, how to do this better?
            accentLabelTextMeshGUI = textMeshGUIs[1];
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

        // Special Symbol List ⌫⏎↑⇧⇪ 
        switch (keyCode)
        {
            case KeyCode.Backspace:
                keyCodeText = "";
                break;
            case KeyCode.Return:
                keyCodeText = "";
                break;
            case KeyCode.LeftShift:
            case KeyCode.RightShift:
                if (keyboardMode == KeyboardMode.NEUTRAL)
                {
                    keyCodeText = "";
                }
                else if (keyboardMode == KeyboardMode.SHIFT)
                {
                    keyCodeText = "";
                }
                else if (keyboardMode == KeyboardMode.CAPS)
                {
                    keyCodeText = "";
                }
                break;
            case KeyCode.Escape:
                keyCodeText = " ";
                break;
        }
        UpdateKeyState(keyCodeText);
        
        if (accentLabelTextMeshGUI != null)
        {
            accentLabelTextMeshGUI.text = KeyboardCollections.CharacterToSpecialChars.ContainsKey(keyCode) ? "…" : "";
        }
    }

    private void UpdateKeyState(string text)
    {
        bool enabled = text.Length > 0;
        foreach(var image in GetComponentsInChildren<Image>())
        {
            image.enabled = enabled;
        }

        if (interactionButton != null) interactionButton.controlEnabled = enabled;
        UpdateKeyText(text);
    }

    protected void UpdateKeyText(string text)
    {
        if (keyTextMesh != null) { keyTextMesh.text = text; }
        if (keyTextMeshGUI != null) { keyTextMeshGUI.text = text; }
    }

    public void TextPress()
    {
        if (!longPressed)
        {
            KeyUpEvent();
        }
        else
        {
            longPressed = false;
        }
    }

    private void LongPressStart()
    {
        longPressed = false;
        LongPressDetectorCoroutine = LongpressDetection();
        StartCoroutine(LongPressDetectorCoroutine);
    }

    private IEnumerator LongpressDetection()
    {
        float longpressThreshold = Time.time + longPressTime;
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
                    parentKeyboard.AccentKeyAnchor = transform;
                    HandleLongPress.Invoke(KeyboardCollections.CharacterToSpecialChars[ActiveKey], parentKeyboard);
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
                KeyUpEvent();
            }
            yield return null;
        }
    }

    private void KeyUpEvent()
    {
        if (UseSpecialChar)
        {
            HandleKeyUpSpecialChar(ActiveSpecialChar, parentKeyboard);
        }
        else
        {
            HandleKeyUp.Invoke(ActiveKey, parentKeyboard);
        }
    }

    public float GetKeyScale()
    {
        return (ActiveKey == NeutralKey) ? keyScale.x : keyScale.y;
    }
}
