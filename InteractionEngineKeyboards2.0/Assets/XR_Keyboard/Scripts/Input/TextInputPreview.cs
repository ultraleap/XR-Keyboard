using TMPro;
using UnityEngine;

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
        _UITextMesh.text = "|";
    }

    public void UpdatePreview(string newText)
    {
        text = newText;
        if (maxWords > 0)
        {
            text = Truncate(text);
        }

        _UITextMesh.text = text + "|";
    }

    private string Truncate(string sentence)
    {
        string[] words = sentence.Split(' ');
        
        if (words.Length > maxWords)
        {
            string result = "";
            int offset = words.Length - maxWords;
            for(int i = 0; i < maxWords; i++)
            {
                result += words[i + offset] + " ";
            }

            return result.Substring(0, result.Length - 2);
        }
        else
        {
            return sentence;
        }
    }
}
