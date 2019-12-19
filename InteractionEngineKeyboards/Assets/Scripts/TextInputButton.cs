using Leap.Unity.Interaction;
using UnityEngine;

public class TextInputButton : MonoBehaviour
{
    TextInputReceiver textInputReceiver;
    InteractionButton interactionButton;
    Material m;
    Renderer _renderer;

    [SerializeField] bool _colourModification = false;
    [SerializeField] Color pressedColour = new Color(0.258823529f, 0.647058824f, 0.960784314f, 1);
    Color unpressedColour;

    [SerializeField] bool _scaleModification = false;
    Vector3 _origScale;
    [SerializeField] Vector3 _scaleModifier = Vector3.one;


    // Start is called before the first frame update
    void Awake()
    {
        textInputReceiver = FindObjectOfType<TextInputReceiver>();
        interactionButton = GetComponent<InteractionButton>();
        _renderer = GetComponentInChildren<Renderer>();

        _origScale = _renderer.transform.localScale;

        m = new Material(_renderer.sharedMaterial);
        unpressedColour = m.color;
    }

    private void OnEnable()
    {
        interactionButton.OnPress += TextPress;
        interactionButton.OnPress += VisualPress;
        interactionButton.OnUnpress += VisualUnpress;
    }

    private void OnDisable()
    {
        interactionButton.OnPress -= TextPress;
        interactionButton.OnPress -= VisualPress;
        interactionButton.OnUnpress -= VisualUnpress;
    }

    private void VisualPress()
    {
        if (_colourModification)
        {
            m.color = pressedColour;
            _renderer.sharedMaterial = m;
        }
        if(_scaleModification)
        {
            _renderer.transform.localScale = Vector3.Scale(_origScale, _scaleModifier);
        }
    }    
    
    private void VisualUnpress()
    {
        if (_colourModification)
        {
            m.color = unpressedColour;
            _renderer.sharedMaterial = m;
        }
        if (_scaleModification)
        {
            _renderer.transform.localScale = _origScale;
        }
    }

    public void TextPress()
    {
        if (gameObject.name == "Space")
        {
            textInputReceiver.Append(' ');
        }
        else if (gameObject.name == "Backspace")
        {
            textInputReceiver.Backspace();
        }
        else
        {
            textInputReceiver.Append(gameObject.name.ToString()[0]);
        }
    }
}
