using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static KeyboardManager;

public class TextInputButton : MonoBehaviour
{
    public delegate void KeyDown(KeyCode keyCode);
    public static event KeyDown HandleKeyDown;
    public KeyCode NeutralKey;
    public KeyCode Symbols1Key;
    public KeyCode Symbols2Key;
    private KeyCode ActiveKey;
    private InteractionButton interactionButton;
    private Button button;
    private TextMeshPro keyTextMesh;
    private TextMeshProUGUI keyTextMeshGUI;

    // Start is called before the first frame update
    public void Start()
    {
        interactionButton = GetComponentInChildren<InteractionButton>();
        if (interactionButton != null) { interactionButton.OnPress += TextPress; }

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

        string keyCodeText = "";
        KeyboardCollections.KeyCodeToString.TryGetValue(keyCode, out keyCodeText);

        if (KeyboardCollections.AlphabetKeyCodes.Contains(keyCode))
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
        HandleKeyDown.Invoke(ActiveKey);
    }
}
