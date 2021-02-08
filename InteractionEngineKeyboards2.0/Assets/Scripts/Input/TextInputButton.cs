using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class TextInputButton : MonoBehaviour
{
    public KeyCode key;
    TextInputReceiver _textInputReceiver;

    // Start is called before the first frame update
    void Awake()
    {
        _textInputReceiver = FindObjectOfType<TextInputReceiver>();
    }

    public void TextPress()
    {
        switch (key)
        {
            case KeyCode.Space:
                _textInputReceiver.Append(' ');
                break;
            case KeyCode.Backspace:
                _textInputReceiver.Backspace();
                break;
            case KeyCode.Return:
                _textInputReceiver.Append('\n');
                break;
            case KeyCode.LeftAlt:
            case KeyCode.RightAlt:
                break;
            case KeyCode.LeftShift:
            case KeyCode.RightShift:
                break;
            default:
                _textInputReceiver.Append(key.ToString()[0]);
                break;
        }
    }
}
