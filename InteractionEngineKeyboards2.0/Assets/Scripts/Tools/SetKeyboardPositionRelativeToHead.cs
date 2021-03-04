using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKeyboardPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public Transform GrabBall;
    public GrabGimbal GrabGimbal;
    public Transform KeyboardCentre;
    public Vector3 DistanceFromHead;
    private bool reorient = false;

    private void Update()
    {
        if (reorient)
        {
            
            reorient = false;
        }
    }
    public void SetPosition()
    {
        //As the grab ball's pivot point isn't in the centre of the keyboard, 
        // we want to work out how far we'd need to move the keyboard to be offset from the head
        // and then apply that offset to the grab ball
        Vector3 offset = KeyboardCentre.position - (head.position + DistanceFromHead);
        GrabBall.position -= offset;
        GrabGimbal.UpdateTargetRotation();
    }

}
