using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Keyboard : MonoBehaviour
{
    public enum KeyboardMode
    {
        NEUTRAL, SHIFT, CAPS, SYMBOLS
    }

    public delegate void KeyUp(byte[] key);
    public event KeyUp HandleKeyUp;

    public delegate void ClearTextField();
    public event ClearTextField HandleClearTextField;

    [HideInInspector] public KeyboardMode keyboardMode;

    [Header("Keyboard Panels")]
    [SerializeField] private AccentOverlayPanel accentOverlay;
    [SerializeField] private TextInputPreview textInputPreview;
    
    [SerializeField] private KeyboardPanel alphaNumericPanel;
    [SerializeField] private KeyboardPanel symbolsPanel;

    // Start is called before the first frame update
    void Start()
    {
        SetMode(KeyboardMode.NEUTRAL);
        alphaNumericPanel.ShowPanel();
        if (symbolsPanel.isActiveAndEnabled) symbolsPanel.HidePanel();
    }

    void Awake()
    {
        if (accentOverlay == null) accentOverlay = transform.GetComponentInChildren<AccentOverlayPanel>();
        if (textInputPreview == null) textInputPreview = transform.root.GetComponentInChildren<TextInputPreview>();

        TextInputButton.HandleKeyUp += HandleTextInputButtonKeyUp;
        TextInputButton.HandleKeyUpSpecialChar += HandleTextInputButtonKeyUpSpecialChar;
        TextInputButton.HandleLongPress += ShowAccentOverlay;
    }

    private void OnDestroy()
    {
        TextInputButton.HandleKeyUp -= HandleTextInputButtonKeyUp;
        TextInputButton.HandleKeyUpSpecialChar -= HandleTextInputButtonKeyUpSpecialChar;
    }

    #region Event Handlers
    private void HandleTextInputButtonKeyUp(KeyCode _keyCode, Keyboard source)
    {
        if (source != this) { return; };
        
        if (KeyboardCollections.ModeShifters.Contains(_keyCode))
        {
            ModeSwitch(_keyCode);
        }
        else if (_keyCode == KeyCode.Escape)
        {
            if (AccentPanelActive())
            {
                accentOverlay.DismissAccentPanel();
            }
        }
        else
        {
            string keyCodeString = KeyboardCollections.KeyCodeToString[_keyCode];
            HandleKeyUpEncoding(keyCodeString);
        }
    }

    private void HandleTextInputButtonKeyUpSpecialChar(KeyCodeSpecialChar _keyCodeSpecialChar, Keyboard source)
    {
        if (source != this) { return; };

        string keyCodeString = KeyboardCollections.KeyCodeSpecialCharToString[_keyCodeSpecialChar];
        HandleKeyUpEncoding(keyCodeString);
    }

    private void HandleKeyUpEncoding(string _keyCodeString)
    {
        bool upperCase = keyboardMode == KeyboardMode.SHIFT || keyboardMode == KeyboardMode.CAPS;
        _keyCodeString = upperCase ? _keyCodeString.ToUpper() : _keyCodeString.ToLower();
        
        if (HandleKeyUp != null)
        {
            HandleKeyUp.Invoke(Encoding.UTF8.GetBytes(_keyCodeString));
        }

        if (keyboardMode == KeyboardMode.SHIFT)
        {
            SetMode(KeyboardMode.NEUTRAL);
        }

        if (AccentPanelActive())
        {
            accentOverlay.DismissAccentPanel();
        }
    }

    public void InvokeClearTextField()
    {
        HandleClearTextField.Invoke();
    }

    #endregion

    public bool AccentPanelActive()
    {
        return accentOverlay.panel.gameObject.activeInHierarchy;
    }

    public void SetMode(KeyboardMode _keyboardMode)
    {
        UpdateTextInputButtons(_keyboardMode);
        keyboardMode = _keyboardMode;
    }

    public void ModeSwitch(KeyCode _keyCode)
    {
        switch (_keyCode)
        {
            case KeyCode.LeftShift:
            case KeyCode.RightShift:
                if (keyboardMode == KeyboardMode.NEUTRAL)
                {
                    SetMode(KeyboardMode.SHIFT);
                }
                else if (keyboardMode == KeyboardMode.SHIFT)
                {
                    SetMode(KeyboardMode.CAPS);
                }
                else if (keyboardMode == KeyboardMode.CAPS)
                {
                    SetMode(KeyboardMode.NEUTRAL);
                }
                break;
            case KeyCode.LeftAlt:
            case KeyCode.RightAlt:
                alphaNumericPanel.HidePanel();
                symbolsPanel.ShowPanel();
                break;
            case KeyCode.LeftControl:
            case KeyCode.RightControl:
                alphaNumericPanel.HidePanel();
                symbolsPanel.ShowPanel();
                break;
            case KeyCode.Alpha0:
                alphaNumericPanel.ShowPanel();
                symbolsPanel.HidePanel();
                break;
        }
    }
    
    private void UpdateTextInputButtons(KeyboardMode _keyboardMode)
    {
        TextInputButton[] textInputButtons = this.GetComponentsInChildren<TextInputButton>();

        foreach (TextInputButton inputButton in textInputButtons)
        {
            switch (_keyboardMode)
            {
                case KeyboardMode.NEUTRAL:
                    inputButton.UpdateActiveKey(inputButton.keyCode, _keyboardMode);
                    break;
                case KeyboardMode.SHIFT:
                case KeyboardMode.CAPS:
                    inputButton.UpdateActiveKey(inputButton.keyCode, _keyboardMode);
                    break;
            }
        }
    }

    public void ShowAccentOverlay(List<KeyCodeSpecialChar> specialChars, Keyboard source, Transform keyTransform = null)
    {
        if (source != this) { return; };
        if (keyTransform != null) accentOverlay.AccentKeyAnchor = keyTransform;
        accentOverlay.ShowAccentPanel(specialChars);
    }  

    public void ClearPreview()
    {
        if (textInputPreview != null) textInputPreview.ClearField();
    }

    public void UpdatePreview(string previewText)
    {
        if (textInputPreview != null) textInputPreview.UpdatePreview(previewText);
    }
}
