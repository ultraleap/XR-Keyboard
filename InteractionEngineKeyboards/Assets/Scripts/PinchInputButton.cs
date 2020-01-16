using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchInputButton : TextInputButton
{
    public void Pinched()
    {
        TextPress();
        VisualPress();
    }

    public void Unpinched()
    {
        VisualUnpress();
    }
}
