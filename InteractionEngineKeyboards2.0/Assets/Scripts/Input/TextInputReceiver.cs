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

    private void OnEnable()
    {
        if (_textMesh == null) { _textMesh = GetComponentInChildren<TextMeshPro>(); }
        if (_UITextMesh == null) { _UITextMesh = GetComponentInChildren<TextMeshProUGUI>(); }
        KeyboardManager.HandleKeyDown += HandleKeyDown;
        KeyboardManager.HandleBackspaceDown += HandleBackspaceDown;
        KeyboardManager.HandleClearTextField += HandleClearTextField;
    }

    private void OnDisable()
    {

        KeyboardManager.HandleKeyDown -= HandleKeyDown;
        KeyboardManager.HandleBackspaceDown -= HandleBackspaceDown;
        KeyboardManager.HandleClearTextField -= HandleClearTextField;
    }

    private void HandleKeyDown(string key)
    {
        text += key;
        UpdateTextMeshText();
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

    public void Reset()
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
