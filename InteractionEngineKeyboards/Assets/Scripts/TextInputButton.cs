using Leap.Unity.Interaction;
using UnityEngine;

public class TextInputButton : MonoBehaviour
{
    [SerializeField]
    KeyCode keyCode;
    TextInputReceiver textInputReceiver;
    InteractionButton interactionButton;
    Color pressedColour;
    Color unpressedColour;
    Material m;
    Renderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        textInputReceiver = GameObject.Find("TextField").GetComponent<TextInputReceiver>();
        interactionButton = gameObject.GetComponent<InteractionButton>();
        renderer = GetComponent<Renderer>();

        m = new Material(renderer.sharedMaterial);
        pressedColour = new Color(66f / 255f, 165f / 255f, 245f / 255f);
        unpressedColour = Color.white;

        interactionButton.OnPress += Pressed;
        interactionButton.OnUnpress += Unpressed;
    }

    private void Pressed()
    {
        m.SetColor("_Color", pressedColour);
        renderer.sharedMaterial = m;
    }    
    
    private void Unpressed()
    {
        m.SetColor("_Color", unpressedColour);
        renderer.sharedMaterial = m;
    }

    public void KeyPress()
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
