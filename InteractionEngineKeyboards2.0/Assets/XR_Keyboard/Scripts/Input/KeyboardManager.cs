using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public enum KeyboardMode
    {
        NEUTRAL, SHIFT, CAPS, SYMBOLS_1, SYMBOLS_2
    }

    public static KeyboardManager Instance;
    public delegate void KeyUp(byte[] key);
    public event KeyUp HandleKeyUp;

    public delegate void ClearTextField();
    public event ClearTextField HandleClearTextField;

    public List<Keyboard> keyboards;
    private Keyboard defaultKeyboard;
    private KeyboardSpawner keyboardSpawner;

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

        if (keyboards.Count == 0) 
        {
            keyboards.Add(FindObjectOfType<Keyboard>());
        }
        defaultKeyboard = keyboards[0];

        keyboardSpawner = GetComponent<KeyboardSpawner>();
    }

    private void OnDestroy()
    {
        TextInputButton.HandleKeyUp -= HandleTextInputButtonKeyUp;
        TextInputButton.HandleKeyUpSpecialChar -= HandleTextInputButtonKeyUpSpecialChar;
    }

    private void HandleTextInputButtonKeyUp(KeyCode _keyCode, Keyboard sourceKeyboard)
    {
        if (KeyboardCollections.ModeShifters.Contains(_keyCode))
        {
            sourceKeyboard.ModeSwitch(_keyCode);
        }
        else
        {
            string keyCodeString = KeyboardCollections.KeyCodeToString[_keyCode];
            HandleKeyUpEncoding(keyCodeString, sourceKeyboard);
        }
    }

    private void HandleTextInputButtonKeyUpSpecialChar(KeyCodeSpecialChar _keyCodeSpecialChar, Keyboard sourceKeyboard)
    {
        string keyCodeString = KeyboardCollections.KeyCodeSpecialCharToString[_keyCodeSpecialChar];
        HandleKeyUpEncoding(keyCodeString, sourceKeyboard);
    }

    private void HandleKeyUpEncoding(string _keyCodeString, Keyboard sourceKeyboard)
    {
        _keyCodeString = sourceKeyboard.keyboardMode == KeyboardMode.SHIFT || sourceKeyboard.keyboardMode == KeyboardMode.CAPS ? _keyCodeString.ToUpper() : _keyCodeString.ToLower();
        if (HandleKeyUp != null)
        {
            HandleKeyUp.Invoke(Encoding.UTF8.GetBytes(_keyCodeString));
        }

        if (sourceKeyboard.keyboardMode == KeyboardMode.SHIFT)
        {
            sourceKeyboard.SetMode(KeyboardMode.NEUTRAL);
        }

        if (sourceKeyboard.AccentPanelActive())
        {
            sourceKeyboard.DismissAccentPanel();
        }
    }

    public void InvokeClearTextField()
    {
        Instance.HandleClearTextField.Invoke();
    }

    // Currently only supporting spawning of one keyboard, but this could pick from
    // a collection of keyboards and return an appropriate one.
    public Keyboard SpawnKeyboard(Transform currentlySelected)
    {
        if (keyboardSpawner != null)
        {
            keyboardSpawner.SpawnKeyboard(currentlySelected);
        }

        return defaultKeyboard;
    }

    public void DespawnKeyboard()
    {
        if (keyboardSpawner != null)
        {
            keyboardSpawner.DespawnKeyboard();
        }
    }

    public void ShowAccentOverlay(List<KeyCodeSpecialChar> specialChars, Keyboard sourceKeyboard)
    {
        sourceKeyboard.ShowAccentOverlay(specialChars);
    }

    public void ClearPreview(Keyboard sourceKeyboard)
    {
        if (sourceKeyboard == null) sourceKeyboard = defaultKeyboard;
        sourceKeyboard.ClearPreview();
    }

    public void UpdatePreview(string previewText, Keyboard sourceKeyboard)
    {
        if (sourceKeyboard != null) sourceKeyboard.UpdatePreview(previewText);
    }

}