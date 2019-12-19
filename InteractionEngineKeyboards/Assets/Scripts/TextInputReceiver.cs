using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInputReceiver : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshPro _textMesh;

    public void Append(char input)
    {
        _textMesh.text += input;
    }

    public void Backspace()
    {
        _textMesh.text = _textMesh.text.Substring(0, _textMesh.text.Length - 1);
    }
}
