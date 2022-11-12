using TMPro;
using UnityEngine;

namespace Leap.Unity.Interaction.Keyboard
{
    public class TextInputReceiver : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textMesh;
        [SerializeField] private TextMeshProUGUI _UITextMesh;
        private string text;

        private void Start()
        {
            if (_textMesh == null) { _textMesh = GetComponentInChildren<TextMeshPro>(); }
            if (_UITextMesh == null) { _UITextMesh = GetComponentInChildren<TextMeshProUGUI>(); }
            KeyboardManager.Instance.ActiveKeyboard().HandleKeyUp += HandleKeyDown;
            KeyboardManager.Instance.ActiveKeyboard().HandleClearTextField += HandleClearTextField;
        }

        private void OnDestroy()
        {
            KeyboardManager.Instance.ActiveKeyboard().HandleKeyUp -= HandleKeyDown;
            KeyboardManager.Instance.ActiveKeyboard().HandleClearTextField -= HandleClearTextField;
        }

        private void HandleKeyDown(byte[] key)
        {
            string keyDecoded = System.Text.Encoding.UTF8.GetString(key);

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
            KeyboardManager.Instance.ActiveKeyboard().UpdatePreview(text);
            if (_textMesh != null) { _textMesh.text = text + "|"; }
            if (_UITextMesh != null) { _UITextMesh.text = text + "|"; }
        }
    }
}
