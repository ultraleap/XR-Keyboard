using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputButton : TextInputButton
{
    private void OnEnable()
    {
        _interactionButton.OnPress += TextPress;
        _interactionButton.OnPress += VisualPress;
        _interactionButton.OnUnpress += VisualUnpress;
    }

    private void OnDisable()
    {
        _interactionButton.OnPress -= TextPress;
        _interactionButton.OnPress -= VisualPress;
        _interactionButton.OnUnpress -= VisualUnpress;
    }
}
