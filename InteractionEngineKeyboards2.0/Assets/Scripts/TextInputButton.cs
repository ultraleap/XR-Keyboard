using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class TextInputButton : MonoBehaviour
{
    TextInputReceiver _textInputReceiver;

    // Start is called before the first frame update
    void Awake()
    {
        _textInputReceiver = FindObjectOfType<TextInputReceiver>();
    }

    public void TextPress()
    {
        if (gameObject.name == "Space Key")
        {
            _textInputReceiver.Append(' ');
        }
        else if (gameObject.name == "Backspace")
        {
            _textInputReceiver.Backspace();
        } 
        else
        {
            _textInputReceiver.Append(gameObject.name.ToString()[0]);
        }
    }
}
