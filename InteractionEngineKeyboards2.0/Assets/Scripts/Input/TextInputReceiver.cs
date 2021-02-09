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
    private bool shift = false;

    public void Append(char input)
    {
        if(shift){
            input = char.ToUpper(input);
            shift = false;
        } else {
            input = char.ToLower(input);
        }
        text += input;
    }

    public void Backspace()
    {
        if (text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
    }

    public void Reset()
    {
        text = string.Empty;
    }

    public void Shift(){
        shift = !shift;
    }

    void UpdateTM()
    {
        _textMesh.text = text + "|";
    }
}
