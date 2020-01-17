using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInputReceiver : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshPro _textMesh;

    private string actualText = "";
    public string text {
        get { return actualText; } 
        set { actualText = value; UpdateTM(); } 
    }

    public void Append(char input)
    {
        text += input;
    }

    public void Backspace()
    {
        if (text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
    }

    void UpdateTM()
    {
        _textMesh.text = text + "|";
    }
}
