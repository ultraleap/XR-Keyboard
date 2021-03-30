using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextInputPreview : MonoBehaviour
{
    private TextMeshProUGUI _UITextMesh;

    private string text;

    private void OnEnable()
    {
        if (_UITextMesh == null) { _UITextMesh = GetComponent<TextMeshProUGUI>(); }
    }

    public void ClearField()
    {
        _UITextMesh.text = "|";
    }

    public void UpdatePreview(string newText)
    {
        text = newText;
        _UITextMesh.text = text + "|";
    }
}
