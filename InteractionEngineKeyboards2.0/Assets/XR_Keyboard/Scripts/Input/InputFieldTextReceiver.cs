using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldTextReceiver : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private TMP_InputField _textMesh;
    private string text;

    private void Start()
    {
        if (_textMesh == null) { _textMesh = GetComponentInChildren<TMP_InputField>(); }
        text = _textMesh.text;
    }
    private void OnDisable()
    {
        DisableInput();
    }

    public void OnSelect(BaseEventData eventData)
    {
        EnableInput();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DisableInput();
    }

    public void EnableInput()
    {
        KeyboardManager.HandleKeyDown += HandleKeyDown;
        KeyboardManager.HandleClearTextField += HandleClearTextField;
        KeyboardManager.SpawnKeyboard(transform);
    }

    public void DisableInput()
    {
        KeyboardManager.DespawnKeyboard();
        KeyboardManager.HandleKeyDown -= HandleKeyDown;
        KeyboardManager.HandleClearTextField -= HandleClearTextField;
    }

    public void Clear()
    {
        text = "";
    }

    private void HandleKeyDown(byte[] key)
    {
        string keyDecoded = Encoding.UTF8.GetString(key);

        if (keyDecoded == "\u0008")
        {
            HandleBackspaceDown();
        }
        else if (keyDecoded == "\n")
        {
            HandleReturn();
        }
        else
        {
            text += keyDecoded;
            _textMesh.onValueChanged.Invoke("");
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

    private void HandleReturn()
    {
        if (_textMesh.text != "")
        {
            _textMesh.onEndEdit.Invoke("");
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
    }

    private void UpdateTextMeshText()
    {
        if (_textMesh != null) { _textMesh.text = text; }
    }
}
