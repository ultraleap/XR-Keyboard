using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class TextInputReceiver : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _textMesh;
    private string text;

    private void OnEnable()
    {
        if (_textMesh == null) { _textMesh = GetComponentInChildren<TextMeshPro>(); }
        KeyboardManager.HandleKeyDown += HandleKeyDown;
        KeyboardManager.HandleBackspaceDown += HandleBackspaceDown;
    }

    private void OnDisable()
    {

        KeyboardManager.HandleKeyDown -= HandleKeyDown;
        KeyboardManager.HandleBackspaceDown -= HandleBackspaceDown;
    }

    private void HandleKeyDown(string key)
    {
        text += key;
        _textMesh.text = text + "|";
    }

    private void HandleBackspaceDown()
    {
        if (text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
            _textMesh.text = text + "|";
        }
    }

    public void Reset()
    {
        text = "";
        _textMesh.text = string.Empty;
    }
}
