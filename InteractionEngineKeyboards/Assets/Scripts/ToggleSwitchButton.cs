using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class ToggleSwitchButton : MonoBehaviour
{
    TextInputReceiver textInputReceiver;
    protected InteractionButton interactionButton;
    Material m;
    Renderer _renderer;

    [SerializeField] 
    bool _colourModification = true;
    [SerializeField] 
    Color onColour = new Color(0.258823529f, 0.647058824f, 0.960784314f, 1);

    Color offColour;

    public bool On = false;

    // Start is called before the first frame update
    void Awake()
    {
        interactionButton = GetComponent<InteractionButton>();
        _renderer = GetComponentInChildren<Renderer>();


        m = new Material(_renderer.sharedMaterial);
        offColour = m.color;
    }

    private void OnEnable()
    {
        interactionButton.OnPress += Press;
    }

    private void OnDisable()
    {
        interactionButton.OnPress -= Press;
    }

    protected void Press()
    {
        On = !On;
        if (_colourModification)
        {
            if (On)
            {
                m.color = onColour;

            }
            else
            {
                m.color = offColour;
            }
            _renderer.sharedMaterial = m;
        }
    }
}
