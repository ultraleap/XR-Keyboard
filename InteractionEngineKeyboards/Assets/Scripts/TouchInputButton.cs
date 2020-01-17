using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputButton : TextInputButton
{
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
}
