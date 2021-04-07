using TMPro;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextInputPreview : MonoBehaviour
{
    private TextMeshProUGUI _UITextMesh;

    private string text;

    [Tooltip("Limit the number of words to display in the text preview. Value of 0 is unlimited")]
    public int maxWords = 0;

    [Tooltip("Limit the number of characters to display in the text preview. Value of 0 is unlimited")]
    public int maxCharacters = 0;

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
            text = TruncateSentence(text);
        }
        if (maxCharacters > 0)
        {
            text = TruncateCharacters(text);
        }

        _UITextMesh.text = text;
    }

    private string TruncateSentence(string sentence)
    {
        string[] words = sentence.Split(' ');
        
        if (words.Length > maxWords)
        {
            int offset = words.Length - maxWords;

            sentence = string.Join(" ", words.Skip(offset));
        }
        
        return sentence;
        
    }

    private string TruncateCharacters(string word)
    {
        if (word.Length > maxCharacters)
        {
            int offset = word.Length - maxCharacters;
            word = word.Substring(offset);
        }
        
        return word;
        
    }
}
