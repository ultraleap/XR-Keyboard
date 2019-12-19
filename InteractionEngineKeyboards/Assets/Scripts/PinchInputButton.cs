using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchInputButton : TextInputButton
{
    public void Pressed()
    {
        TextPress();
        VisualPress();
    }

    public void Unpressed()
    {
        VisualUnpress();
    }
}
