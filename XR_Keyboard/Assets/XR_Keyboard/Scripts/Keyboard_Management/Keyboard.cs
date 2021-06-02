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

    [HideInInspector] public KeyboardMode ActivekeyboardMode;

    [Header("Longpress Customisation")]
    [Tooltip("The string that appears on a key to indicate that it can be longpressed")]
    public string LongPressIndicator = "";
    [Tooltip("The colour the key turns when it has been longpressed")]
    public Color LongPressColour;

    [Tooltip("The amount of time a user has to release backspace after it has been longpressed before the backspace coroutine begins")]
    public float BackspaceLongpressGracePeriod = 0.3f;

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
        symbolsPanel.HidePanel();
    }

    void Awake()
    {
        if (accentOverlay == null) accentOverlay = transform.GetComponentInChildren<AccentOverlayPanel>();
        if (textInputPreview == null) textInputPreview = transform.root.GetComponentInChildren<TextInputPreview>();

        TextInputButton.HandleKeyUp += HandleTextInputButtonKeyUp;
        TextInputButton.HandleLongPress += ShowAccentOverlay;

    }

    private void OnDestroy()
    {
        TextInputButton.HandleKeyUp -= HandleTextInputButtonKeyUp;
        TextInputButton.HandleLongPress -= ShowAccentOverlay;
    }

    #region Event Handlers
    private void HandleTextInputButtonKeyUp(string _key, Keyboard source)
    {
        if (source != this) { return; };

        if (KeyboardCollections.ModeShifters.Contains(_key))
        {
            ModeSwitch(_key);
        }
        else if (_key == "accentPanelDismiss")
        {
            if (AccentPanelActive())
            {
                accentOverlay.DismissAccentPanel();
            }
        }
        else
        {
            HandleKeyUpEncoding(_key);
        }
    }

    private void HandleKeyUpEncoding(string _keyCodeString)
    {

        if (KeyboardCollections.NonCharIdentifierToStringChar.TryGetValue(_keyCodeString, out string nonStandardKeyCodeText))
        {
            _keyCodeString = nonStandardKeyCodeText;
        }

        bool upperCase = ActivekeyboardMode == KeyboardMode.SHIFT || ActivekeyboardMode == KeyboardMode.CAPS;
        _keyCodeString = upperCase ? _keyCodeString.ToUpper() : _keyCodeString.ToLower();

        if (HandleKeyUp != null)
        {
            HandleKeyUp?.Invoke(Encoding.UTF8.GetBytes(_keyCodeString));
        }

        if (ActivekeyboardMode == KeyboardMode.SHIFT)
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
        HandleClearTextField?.Invoke();
    }

    #endregion

    public bool AccentPanelActive()
    {
        return accentOverlay.panel.gameObject.activeInHierarchy;
    }

    public void SetMode(KeyboardMode _keyboardMode)
    {
        UpdateTextInputButtons(_keyboardMode);
        ActivekeyboardMode = _keyboardMode;
    }

    public void ModeSwitch(string _key)
    {
        switch (_key)
        {
            case "shift":
                if (ActivekeyboardMode == KeyboardMode.NEUTRAL)
                {
                    SetMode(KeyboardMode.SHIFT);
                }
                else if (ActivekeyboardMode == KeyboardMode.SHIFT)
                {
                    SetMode(KeyboardMode.CAPS);
                }
                else if (ActivekeyboardMode == KeyboardMode.CAPS)
                {
                    SetMode(KeyboardMode.NEUTRAL);
                }
                break;
            case "switch_symbols":
                alphaNumericPanel.HidePanel();
                symbolsPanel.ShowPanel();
                break;
            case "switch_letters":
                symbolsPanel.HidePanel();
                alphaNumericPanel.ShowPanel();
                break;
        }
    }

    private void UpdateTextInputButtons(KeyboardMode _keyboardMode)
    {
        TextInputButton[] textInputButtons = GetComponentsInChildren<TextInputButton>();

        foreach (TextInputButton inputButton in textInputButtons)
        {
            switch (_keyboardMode)
            {
                case KeyboardMode.NEUTRAL:
                    inputButton.UpdateActiveKey(inputButton.Key, _keyboardMode);
                    break;
                case KeyboardMode.SHIFT:
                case KeyboardMode.CAPS:
                    inputButton.UpdateActiveKey(inputButton.Key, _keyboardMode);
                    break;
            }
        }
    }

    public void ShowAccentOverlay(List<string> specialChars, Keyboard source, Transform keyTransform = null)
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
