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
    public delegate void KeyUp(byte[] key, Keyboard sourceKeyboard);
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
            Debug.LogWarning("Detecting keyboards in the scene. If you require access to a specific keyboard assign them manually in the inspector.");
            keyboards = new List<Keyboard>();
            keyboards.AddRange(FindObjectsOfType<Keyboard>());
        }
        
        if (keyboards.Count == 0)
        {
            Debug.LogWarning("No Keyboards Found. Make sure there is an object with a keyboard component in the scene.");
        }
        else
        {
            defaultKeyboard = keyboards[0];
        }

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
        bool upperCase = sourceKeyboard.keyboardMode == KeyboardMode.SHIFT || sourceKeyboard.keyboardMode == KeyboardMode.CAPS;
        _keyCodeString = upperCase ? _keyCodeString.ToUpper() : _keyCodeString.ToLower();
        
        if (HandleKeyUp != null)
        {
            HandleKeyUp.Invoke(Encoding.UTF8.GetBytes(_keyCodeString), sourceKeyboard);
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