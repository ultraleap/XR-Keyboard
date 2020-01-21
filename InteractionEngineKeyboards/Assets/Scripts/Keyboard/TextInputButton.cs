using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class TextInputButton : MonoBehaviour
{
    TextInputReceiver _textInputReceiver;
    protected InteractionButton _interactionButton;
    Material m;
    Renderer _renderer;

    [SerializeField] bool _colourModification = true;
    [SerializeField] Color pressedColour = new Color(0.258823529f, 0.647058824f, 0.960784314f, 1);
    Color unpressedColour;

    [SerializeField] public bool ScaleModification = false;
    Vector3 _origScale;
    [SerializeField] public Vector3 ScaleModifier = Vector3.one;


    // Start is called before the first frame update
    void Awake()
    {
        _textInputReceiver = FindObjectOfType<TextInputReceiver>();
        _interactionButton = GetComponent<InteractionButton>();
        _renderer = GetComponentInChildren<Renderer>();

        _origScale = _renderer.transform.localScale;

        m = new Material(_renderer.sharedMaterial);
        unpressedColour = m.color;
    }

    protected void VisualPress()
    {
        if (_colourModification)
        {
            m.color = pressedColour;
            _renderer.sharedMaterial = m;
        }
        if(ScaleModification)
        {
            _renderer.transform.localScale = Vector3.Scale(_origScale, ScaleModifier);
        }
    }    
    
    protected void VisualUnpress()
    {
        if (_colourModification)
        {
            m.color = unpressedColour;
            _renderer.sharedMaterial = m;
        }
        if (ScaleModification)
        {
            _renderer.transform.localScale = _origScale;
        }
    }

    protected void TextPress()
    {
        if (gameObject.name == "Space")
        {
            _textInputReceiver.Append(' ');
        }
        else if (gameObject.name == "Backspace")
        {
            _textInputReceiver.Backspace();
        } 
        else if (gameObject.name == "SwitchType")
        {
            KeyboardButtonManager kbm =  gameObject.GetComponentInParent<KeyboardButtonManager>();
            kbm.KeyType = kbm.KeyType == KeyboardType.PINCH ? KeyboardType.PUSH : KeyboardType.PINCH;
        } else if (gameObject.name.Contains("Suggestion"))
        {
            string correctWord = gameObject.GetComponentInChildren<TextMeshPro>().text;
            _textInputReceiver.ReplaceWord(correctWord);
        }
        else
        {
            _textInputReceiver.Append(gameObject.name.ToString()[0]);
        }
    }
}
