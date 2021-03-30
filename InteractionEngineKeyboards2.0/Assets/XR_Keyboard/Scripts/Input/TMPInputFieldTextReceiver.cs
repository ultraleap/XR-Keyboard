using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.EventSystems;

public class TMPInputFieldTextReceiver : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private TMP_InputField _textMesh;
    public bool previewLastKeypress = false;

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
        KeyboardManager.textInputPreview.UpdatePreview(PreviewText());
    }

    public void DisableInput()
    {
        KeyboardManager.DespawnKeyboard();
        KeyboardManager.HandleKeyUp -= HandleKeyDown;
        KeyboardManager.HandleClearTextField -= HandleClearTextField;
        KeyboardManager.textInputPreview.ClearField();
    }

    public void Clear()
    {
        Reset();
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
            KeyboardManager.textInputPreview.UpdatePreview(PreviewText());
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
        KeyboardManager.textInputPreview.ClearField();
    }

    private void UpdateTextMeshText(string _appendChar)
    {
        if (_textMesh != null) { _textMesh.text += _appendChar; }
        KeyboardManager.textInputPreview.UpdatePreview(PreviewText());
        _textMesh.MoveTextEnd(false);
    }

    private string PreviewText()
    {
        string text = _textMesh.textComponent.text;
        if (previewLastKeypress)
        {
            text = ExposeLastKeypress(text);
        }

        return text;
    }

    private string ExposeLastKeypress(string source)
    {
        string result = source;

        if (source.Length > 2)
        {
            result = source.Substring(0, source.Length-2) + _textMesh.text.Substring(_textMesh.text.Length - 1);
        }
        else if (source.Length > 0)
        {
            result = _textMesh.text;
        }

        return result;
    }
}
