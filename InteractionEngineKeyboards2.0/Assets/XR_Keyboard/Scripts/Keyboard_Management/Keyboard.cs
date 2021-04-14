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

    public enum AccentKeysPosition
    {
        MIDDLE,
        ADJACENT,
        NUM_ROW
    }

    public delegate void KeyUp(byte[] key);
    public event KeyUp HandleKeyUp;

    public delegate void ClearTextField();
    public event ClearTextField HandleClearTextField;


    [Header("External Connections")]
    [SerializeField] private AccentOverlayPanel accentOverlay;
    [SerializeField] private TextInputPreview textInputPreview;

    [Header("AccentKeys")]
    public AccentKeysPosition accentKeysPosition = AccentKeysPosition.MIDDLE;
    public Transform AccentKeysMiddleAnchor;
    [HideInInspector] public Transform AccentKeyAnchor;
    public Transform NumberRow;
    public float accentPanelHideDelay = 0.25f;
    [HideInInspector] public KeyboardMode keyboardMode;

    [Header("Keyboard Panels")]
    public KeyboardPanel alphaNumericPanel;
    public KeyboardPanel symbolsPanel;

    private Coroutine hidePanelRoutine;
    private Coroutine timeoutPanelRoutine;

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
        else if (_key == "\u001B")
        {
            if (AccentPanelActive())
            {
                DismissAccentPanel();
            }
        }
        else
        {
            HandleKeyUpEncoding(_key);
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
            DismissAccentPanel();
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

    public void ModeSwitch(string _key)
    {
        switch (_key)
        {
            case "shift":
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
            case "switch_symbols":
                alphaNumericPanel.HidePanel();
                symbolsPanel.ShowPanel();
                break;
            case "switch_letters":
                alphaNumericPanel.HidePanel();
                symbolsPanel.ShowPanel();
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

    public void ShowAccentOverlay(List<string> specialChars, Keyboard source)
    {
        if (source != this) { return; };

        switch (accentKeysPosition)
        {
            case AccentKeysPosition.MIDDLE:
                accentOverlay.ShowAccentPanel(specialChars, AccentKeysMiddleAnchor);
                accentOverlay.SetOverlayColour();
                break;
            case AccentKeysPosition.NUM_ROW:
                NumberRow.gameObject.SetActive(false);
                accentOverlay.SetInlineColour();
                accentOverlay.transform.SetParent(NumberRow.transform.parent);
                accentOverlay.transform.SetAsFirstSibling();
                accentOverlay.ShowAccentPanel(specialChars, NumberRow);
                break;
            case AccentKeysPosition.ADJACENT:
                accentOverlay.SetOverlayColour();
                accentOverlay.ShowAccentPanel(specialChars, AccentKeyAnchor, true);
                break;
        }

        if (timeoutPanelRoutine != null)
        {
            StopCoroutine(timeoutPanelRoutine);
        }
        timeoutPanelRoutine = StartCoroutine(TimeOutPanel(accentOverlay.timeout));
    }

    public IEnumerator HidePanelAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideAccentPanel();
    }

    public IEnumerator TimeOutPanel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideAccentPanel();
    }

    private void HideAccentPanel()
    {
        accentOverlay.HideAccentPanel();
        if (accentKeysPosition == AccentKeysPosition.NUM_ROW)
        {
            accentOverlay.transform.SetParent(accentOverlay.transform.parent.parent);
            if (!NumberRow.gameObject.activeInHierarchy)
            {
                NumberRow.gameObject.SetActive(true);
            }
        }
    }

    public void DismissAccentPanel()
    {
        accentOverlay.DisableInput();
        hidePanelRoutine = StartCoroutine(HidePanelAfter(accentPanelHideDelay));
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
