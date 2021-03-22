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

    private void Start()
    {
        if (_textMesh == null) { _textMesh = GetComponentInChildren<TMP_InputField>(); }
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
        KeyboardManager.HandleKeyUp += HandleKeyDown;
        KeyboardManager.HandleClearTextField += HandleClearTextField;
        KeyboardManager.SpawnKeyboard(transform);
    }

    public void DisableInput()
    {
        KeyboardManager.DespawnKeyboard();
        KeyboardManager.HandleKeyUp -= HandleKeyDown;
        KeyboardManager.HandleClearTextField -= HandleClearTextField;
    }

    public void Clear()
    {
        if (_textMesh != null) { _textMesh.text = ""; }
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
            _textMesh.onValueChanged.Invoke("");
            UpdateTextMeshText(keyDecoded);
        }
    }

    private void HandleBackspaceDown()
    {
        if (_textMesh == null)
        {
            return;
        }
        if (_textMesh.text.Length > 0)
        {
            _textMesh.text = _textMesh.text.Substring(0, _textMesh.text.Length - 1);
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
        if (_textMesh != null) { _textMesh.text = string.Empty; }
    }

    private void UpdateTextMeshText(string _appendChar)
    {
        if (_textMesh != null) { _textMesh.text += _appendChar; }
    }
}
