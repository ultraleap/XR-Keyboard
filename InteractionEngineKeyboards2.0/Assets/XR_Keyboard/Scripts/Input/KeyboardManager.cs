using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    public enum KeyboardMode
    {
        NEUTRAL, SHIFT, CAPS, SYMBOLS_1, SYMBOLS_2
    }

    public enum AccentKeysPosition
    {
        MIDDLE,
        ADJACENT,
        NUM_ROW
    }

    public static KeyboardManager Instance;
    public delegate void KeyUp(byte[] key);
    public event KeyUp HandleKeyUp;

    public delegate void ClearTextField();
    public event ClearTextField HandleClearTextField;

    public List<GameObject> KeyboardParents;
    private KeyboardMode keyboardMode;
    [Header("AccentKeys")]
    public AccentKeysPosition accentKeysPosition = AccentKeysPosition.MIDDLE;
    public Transform AccentKeysMiddleAnchor;
    public Transform AccentKeyAnchor;
    public Transform NumberRow;
    private Coroutine hidePanelRoutine;
    private Coroutine timeoutPanelRoutine;
    public float accentPanelHideDelay = 0.25f;

    public KeyboardSpawner keyboardSpawner;
    public AccentOverlayPanel accentOverlay;

    public TextInputPreview textInputPreview;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        TextInputButton.HandleKeyUp += HandleTextInputButtonKeyUp;
        TextInputButton.HandleKeyUpSpecialChar += HandleTextInputButtonKeyUpSpecialChar;
        TextInputButton.HandleLongPress += ShowAccentOverlay;
        keyboardSpawner = FindObjectOfType<KeyboardSpawner>();
        accentOverlay = FindObjectOfType<AccentOverlayPanel>();
        textInputPreview = FindObjectOfType<TextInputPreview>();
    }

    private void Start()
    {
        SetMode(KeyboardMode.NEUTRAL);
    }

    private void OnDestroy()
    {
        TextInputButton.HandleKeyUp -= HandleTextInputButtonKeyUp;
        TextInputButton.HandleKeyUpSpecialChar -= HandleTextInputButtonKeyUpSpecialChar;
    }

    private void HandleTextInputButtonKeyUp(KeyCode _keyCode)
    {
        if (KeyboardCollections.ModeShifters.Contains(_keyCode))
        {
            ModeSwitch(_keyCode);
        }
        else
        {
            string keyCodeString = KeyboardCollections.KeyCodeToString[_keyCode];
            HandleKeyUpEncoding(keyCodeString);
        }
    }

    private void HandleTextInputButtonKeyUpSpecialChar(KeyCodeSpecialChar _keyCodeSpecialChar)
    {
        string keyCodeString = KeyboardCollections.KeyCodeSpecialCharToString[_keyCodeSpecialChar];
        HandleKeyUpEncoding(keyCodeString);
    }

    private void HandleKeyUpEncoding(string _keyCodeString)
    {
        _keyCodeString = keyboardMode == KeyboardMode.SHIFT || keyboardMode == KeyboardMode.CAPS ? _keyCodeString.ToUpper() : _keyCodeString.ToLower();
        if (HandleKeyUp != null)
        {
            HandleKeyUp.Invoke(Encoding.UTF8.GetBytes(_keyCodeString));
        }

        if (keyboardMode == KeyboardMode.SHIFT)
        {
            SetMode(KeyboardMode.NEUTRAL);
        }

        if (accentOverlay.panel.gameObject.activeInHierarchy)
        {
            accentOverlay.DisableInput();
            hidePanelRoutine = StartCoroutine(HidePanelAfter(accentPanelHideDelay));
        }
    }

    private void ModeSwitch(KeyCode _keyCode)
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
                SetMode(KeyboardMode.SYMBOLS_1);
                break;
            case KeyCode.LeftControl:
            case KeyCode.RightControl:
                SetMode(KeyboardMode.SYMBOLS_2);
                break;
            case KeyCode.Alpha0:
                SetMode(KeyboardMode.NEUTRAL);
                break;
        }
    }

    private void SetMode(KeyboardMode _keyboardMode)
    {

        if (KeyboardParents != null & KeyboardParents.Count != 0) { UpdateTextInputButtons(_keyboardMode, KeyboardParents); }
        keyboardMode = _keyboardMode;
    }

    private void UpdateTextInputButtons(KeyboardMode _keyboardMode, List<GameObject> _keyboardParents)
    {
        foreach (GameObject keyboardParent in _keyboardParents)
        {
            List<TextInputButton> textInputButtons = keyboardParent.GetComponentsInChildren<TextInputButton>().ToList();

            foreach (TextInputButton inputButton in textInputButtons)
            {
                switch (_keyboardMode)
                {
                    case KeyboardMode.NEUTRAL:
                        inputButton.UpdateActiveKey(inputButton.NeutralKey, _keyboardMode);
                        break;
                    case KeyboardMode.SHIFT:
                    case KeyboardMode.CAPS:
                        inputButton.UpdateActiveKey(inputButton.NeutralKey, _keyboardMode);
                        break;
                    case KeyboardMode.SYMBOLS_1:
                        inputButton.UpdateActiveKey(inputButton.Symbols1Key, _keyboardMode);
                        break;
                    case KeyboardMode.SYMBOLS_2:
                        inputButton.UpdateActiveKey(inputButton.Symbols2Key, _keyboardMode);
                        break;
                }
            }
        }
    }

    public void InvokeClearTextField()
    {
        HandleClearTextField.Invoke();
    }

    public void SpawnKeyboard(Transform currentlySelected)
    {
        keyboardSpawner.SpawnKeyboard(currentlySelected);
    }

    public void DespawnKeyboard()
    {
        if (keyboardSpawner != null)
        {
            keyboardSpawner.DespawnKeyboard();
        }
    }

    public void ShowAccentOverlay(List<KeyCodeSpecialChar> specialChars)
    {
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
        StartCoroutine(HidePanelAfter(accentPanelHideDelay));
    }

}