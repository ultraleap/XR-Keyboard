using TMPro;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextInputPreview : MonoBehaviour
{
    private TextMeshProUGUI _UITextMesh;

    private string text;

    public int maxWords = 0;

    private void OnEnable()
    {
        if (_UITextMesh == null) { _UITextMesh = GetComponent<TextMeshProUGUI>(); }
    }

    public void ClearField()
    {
        _UITextMesh.text = "";
    }

    public void UpdatePreview(string newText)
    {
        text = newText;
        if (maxWords > 0)
        {
            text = Truncate(text);
        }

        _UITextMesh.text = text;
    }

    private string Truncate(string sentence)
    {
        string[] words = sentence.Split(' ');
        
        if (words.Length > maxWords)
        {
            int offset = words.Length - maxWords;

            return string.Join(" ", words.Skip(offset));
        }
        else
        {
            return sentence;
        }
    }
}
