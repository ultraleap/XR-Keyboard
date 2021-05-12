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

    [Tooltip("Limit the number of lines to display in the text preview. Value of 0 is unlimited")]
    public int maxLines = 0;

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
        if (maxLines > 0)
        {
            text = TruncateOnCharacterCount(text, '\n', maxLines);
        }
        if (maxWords > 0)
        {
            text = TruncateOnCharacterCount(text, ' ', maxWords);
        }
        if (maxCharacters > 0)
        {
            text = TruncateCharacters(text);
        }

        _UITextMesh.text = text;
    }

    private string TruncateOnCharacterCount(string sentence, char character, int maxCharacterCount)
    {
        string[] words = sentence.Split(character);

        if (words.Length > maxCharacterCount)
        {
            int offset = words.Length - maxCharacterCount;

            sentence = string.Join(character.ToString(), words.Skip(offset));
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
