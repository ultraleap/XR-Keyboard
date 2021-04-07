using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class TextInputReceiver : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMesh;
    [SerializeField] private TextMeshProUGUI _UITextMesh;
    private string text;

    public Keyboard keyboard;
    private void OnEnable()
    {
        if (_textMesh == null) { _textMesh = GetComponentInChildren<TextMeshPro>(); }
        if (_UITextMesh == null) { _UITextMesh = GetComponentInChildren<TextMeshProUGUI>(); }
        KeyboardManager.Instance.ActiveKeyboard().HandleKeyUp += HandleKeyDown;
        KeyboardManager.Instance.ActiveKeyboard().HandleClearTextField += HandleClearTextField;
    }

    private void OnDisable()
    {

        KeyboardManager.Instance.ActiveKeyboard().HandleKeyUp -= HandleKeyDown;
        KeyboardManager.Instance.ActiveKeyboard().HandleClearTextField -= HandleClearTextField;
    }

    private void HandleKeyDown(byte[] key)
    {
        string keyDecoded = Encoding.UTF8.GetString(key);

        if (keyDecoded == "\u0008")
        {
            HandleBackspaceDown();
        }
        else
        {
            text += keyDecoded;
            UpdateTextMeshText();
        }
    }

    private void HandleBackspaceDown()
    {
        if (text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
            UpdateTextMeshText();
        }
    }

    private void HandleClearTextField()
    {
        Reset();
    }

    private void Reset()
    {
        text = string.Empty;
        if (_textMesh != null) { _textMesh.text = text; }
        if (_UITextMesh != null) { _UITextMesh.text = text; }
    }

    private void UpdateTextMeshText()
    {
        if (_textMesh != null) { _textMesh.text = text + "|"; }
        if (_UITextMesh != null) { _UITextMesh.text = text + "|"; }
    }
}
