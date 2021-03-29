using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextInputPreview : MonoBehaviour
{
    private TextMeshProUGUI _UITextMesh;
    private TMP_InputField _targetInputMesh;

    private EventSystem eventSystem;
    private string text;

    private GameObject currentObject;

    void Update()
    {
        if (currentObject != eventSystem.currentSelectedGameObject)
        {
            currentObject = eventSystem.currentSelectedGameObject;
            if (currentObject != null)
            {
                _targetInputMesh = currentObject.GetComponentInChildren<TMP_InputField>();
                StartCoroutine(UpdateText());
            }
        }
    }

    private void OnEnable()
    {
        if (_UITextMesh == null) { _UITextMesh = GetComponent<TextMeshProUGUI>(); }
        eventSystem = EventSystem.current;
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

        if (_targetInputMesh != null)
        {
            text = _targetInputMesh.textComponent.text;
            if (_targetInputMesh.contentType == TMP_InputField.ContentType.Password && text.Length > 2)
            {
                text = text.Substring(0,text.Length-2) + _targetInputMesh.text.Substring(_targetInputMesh.text.Length - 1);
            }
        }
        
         _UITextMesh.text = text + "|";
    }
}
