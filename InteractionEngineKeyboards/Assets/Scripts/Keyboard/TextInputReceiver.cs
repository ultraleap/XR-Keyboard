using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class TextInputReceiver : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _textMesh;
    [SerializeField]
    private Levenshtein _levenshtein;
    [SerializeField]
    NGramGenerator _wordPredictor;

    private string actualText = "";
    public string text
    {
        get { return actualText; }
        set
        {
            actualText = value;
            UpdateTM();
        }
    }

    public void Append(char input)
    {
        text += input;
        if (input == ' ')
        {
            _levenshtein.ClearLabelPredictions();
        }
        else
        {
            StartCoroutine(_levenshtein.RunAutoComplete(text));
        }
    }

    public void Backspace()
    {
        if (text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
            StartCoroutine(_levenshtein.RunAutoComplete(text));
        }
    }

    public void Reset()
    {
        text = string.Empty;
        _levenshtein.ClearLabelPredictions();
    }

    public void ReplaceWord(string correctWord)
    {
        List<string> inputText = new List<string>();
        StringBuilder builder = new StringBuilder();
        string input = text;
        string[] parts = input.Split(' ');
        parts = input.Split(' ').Take(parts.Length - 1).ToArray();

        for (int i = 0; i < parts.Length; i++)
        {
            inputText.Add(parts[i]);
        }

        inputText.Add(correctWord);

        foreach (string w in inputText)
        {
            builder.Append(w).Append(" ");
        }
        text = builder.ToString();
        _levenshtein.ClearLabelPredictions();
        //StartCoroutine(_wordPredictor.PredictNextWords(correctWord));
    }

    void UpdateTM()
    {
        _textMesh.text = text + "|";
    }
}
