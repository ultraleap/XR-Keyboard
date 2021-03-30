using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextInputPreview : MonoBehaviour
{
    private TextMeshProUGUI _UITextMesh;
    private TMP_InputField _targetInputField;

    private string text;

    private GameObject currentObject;

    private void OnEnable()
    {
        if (_UITextMesh == null) { _UITextMesh = GetComponent<TextMeshProUGUI>(); }
        KeyboardManager.HandleKeyUp += HandleKeyPress;
    }

    private void OnDisable()
    {
        KeyboardManager.HandleKeyUp -= HandleKeyPress;
    }

    private void HandleKeyPress(byte[] key)
    {
        StartCoroutine(UpdateText());
    }

    private IEnumerator UpdateText()
    {
        yield return new WaitForEndOfFrame();

        if (_targetInputField != null)
        {
            text = _targetInputField.textComponent.text;
            if (_targetInputField.contentType == TMP_InputField.ContentType.Password && text.Length > 2)
            {
                text = text.Substring(0,text.Length-2) + _targetInputField.text.Substring(_targetInputField.text.Length - 1);
            }
        }
        
         _UITextMesh.text = text + "|";
    }

    public void SetField(TMP_InputField field)
    {
        if (field != null)
        {
            _targetInputField = field;
            StartCoroutine(UpdateText());
        }
    }

    public void ClearField()
    {
        _UITextMesh.text = "|";
    }
}
