using UnityEngine;
using System.Text;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TMPInputFieldTextReceiver : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private TMP_InputField _textMesh;
    [SerializeField] private InputField _textInput;
    public bool exposeLastKeypress = false;
    public float previewLastTimeout = 1;
    private Coroutine exposureTimeout;
    private Keyboard keyboard;


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
        KeyboardManager.Instance.HandleKeyUp += HandleKeyPress;
        KeyboardManager.Instance.HandleClearTextField += HandleClearTextField;
        keyboard = KeyboardManager.Instance.SpawnKeyboard(transform);
        KeyboardManager.Instance.UpdatePreview(PreviewText(false), keyboard);
    }

    public void DisableInput()
    {
        KeyboardManager.Instance.DespawnKeyboard();
        KeyboardManager.Instance.HandleKeyUp -= HandleKeyPress;
        KeyboardManager.Instance.HandleClearTextField -= HandleClearTextField;

        KeyboardManager.Instance.ClearPreview(keyboard);
        if (exposureTimeout != null) StopCoroutine(exposureTimeout);
    }

    private void HandleKeyPress(byte[] key)
    {
        string keyDecoded = Encoding.UTF8.GetString(key);

        if (keyDecoded == "\u0008")
        {
            HandleBackspacePress();
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

    private void HandleBackspacePress()
    {
        if (_textMesh == null)
        {
            return;
        }
        if (_textMesh.text.Length > 0)
        {
            _textMesh.text = _textMesh.text.Substring(0, _textMesh.text.Length - 1);
            KeyboardManager.Instance.UpdatePreview(PreviewText(exposeLastKeypress), keyboard);
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
        Clear();
    }

    private void UpdateTextMeshText(string _appendChar)
    {
        if (_textMesh != null) { _textMesh.text += _appendChar; }
        KeyboardManager.Instance.UpdatePreview(PreviewText(exposeLastKeypress), keyboard);
        _textMesh.MoveTextEnd(false);
    }

    public void Clear()
    {
        if (_textMesh != null) { _textMesh.text = string.Empty; }
        KeyboardManager.Instance.ClearPreview(keyboard);
    }
    
    private string PreviewText(bool exposeLast)
    {
        string text = _textMesh.textComponent.text;
        if (exposeLast)
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
        RestartTimeout();

        return result;
    }

    private void RestartTimeout()
    {
        if (exposureTimeout != null) StopCoroutine(exposureTimeout);

        exposureTimeout = StartCoroutine(HideLastKeypressAfter(previewLastTimeout));
    }

    private IEnumerator HideLastKeypressAfter(float time)
    {
        yield return new WaitForSeconds(time);

        KeyboardManager.Instance.UpdatePreview(_textMesh.textComponent.text, keyboard);
    }
}
