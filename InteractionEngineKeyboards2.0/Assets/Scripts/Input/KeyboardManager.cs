using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public enum KeyboardMode
    {
        NEUTRAL, SHIFT, CAPS, SYMBOLS_1, SYMBOLS_2
    }
    public delegate void KeyDown(byte[] key);
    public static event KeyDown HandleKeyDown;

    public delegate void ClearTextField();
    public static event ClearTextField HandleClearTextField;

    public List<GameObject> KeyboardParents;
    private KeyboardMode keyboardMode;

    private void Awake()
    {
        TextInputButton.HandleKeyDown += HandleTextInputButtonKeyDown;
    }

    private void Start()
    {
        SetMode(KeyboardMode.NEUTRAL);
    }

    private void OnDestroy()
    {
        TextInputButton.HandleKeyDown -= HandleTextInputButtonKeyDown;
    }

    private void HandleTextInputButtonKeyDown(KeyCode _keyCode)
    {
        if (KeyboardCollections.ModeShifters.Contains(_keyCode))
        {
            ModeSwitch(_keyCode);
        }
        else
        {
            string KeyCodeString = KeyboardCollections.KeyCodeToString[_keyCode];
            KeyCodeString = keyboardMode == KeyboardMode.SHIFT || keyboardMode == KeyboardMode.CAPS ? KeyCodeString.ToUpper() : KeyCodeString.ToLower();
            if (HandleKeyDown != null)
            {
                HandleKeyDown.Invoke(Encoding.UTF8.GetBytes(KeyCodeString));
            }
            if (keyboardMode == KeyboardMode.SHIFT)
            {
                SetMode(KeyboardMode.NEUTRAL);
            }
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
}