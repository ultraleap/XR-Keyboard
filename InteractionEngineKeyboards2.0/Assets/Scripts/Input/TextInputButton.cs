using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class TextInputButton : MonoBehaviour
{
    public KeyCode Key;
    TextInputReceiver _textInputReceiver;

    // Start is called before the first frame update
    void Awake()
    {
        _textInputReceiver = FindObjectOfType<TextInputReceiver>();
    }

    public void TextPress()
    {
        switch (Key)
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
            _textInputReceiver.Shift();
                break;
            default:
                _textInputReceiver.Append(Key.ToString()[0]);
                break;
        }
    }
}
