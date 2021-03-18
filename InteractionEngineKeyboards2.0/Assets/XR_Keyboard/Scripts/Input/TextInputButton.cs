using System.Collections;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static KeyboardManager;

public class TextInputButton : MonoBehaviour
{
    public delegate void KeyDown(KeyCode keyCode);
    public static event KeyDown HandleKeyDown;

    public delegate void KeyDownSpecialChar(KeyCodeSpecialChar keyCode);
    public static event KeyDownSpecialChar HandleKeyDownSpecialChar;
    public KeyCode NeutralKey;
    public KeyCode Symbols1Key;
    public KeyCode Symbols2Key;
    public KeyCodeSpecialChar ActiveSpecialChar = KeyCodeSpecialChar.NONE;
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

        if (ActiveKey == KeyCode.Alpha2)
        {
            KeyboardCollections.KeyCodeSpecialCharToString.TryGetValue(ActiveSpecialChar, out keyCodeText);
        }
        else
        {
            KeyboardCollections.KeyCodeToString.TryGetValue(keyCode, out keyCodeText);
        }

        if (KeyboardCollections.AlphabetKeyCodes.Contains(keyCode) || ActiveKey == KeyCode.Alpha2)
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
                LongPress();
                longPressed = true;
            }
            yield return null;
        }
    }

    private void LongPress()
    {
        switch (ActiveKey)
        {
            case KeyCode.Backspace:
                LongPressCoroutine = BackspaceLongPress();
                StartCoroutine(LongPressCoroutine);
                break;
            default:
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
        if (ActiveKey == KeyCode.Alpha2)
        {
            HandleKeyDownSpecialChar(ActiveSpecialChar);
        }
        else
        {
            HandleKeyDown.Invoke(ActiveKey);
        }
    }
}
