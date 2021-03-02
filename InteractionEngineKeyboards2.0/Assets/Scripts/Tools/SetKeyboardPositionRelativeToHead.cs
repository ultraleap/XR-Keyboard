using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKeyboardPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public Transform GrabBall;
    public Transform KeyboardCentre;
    public Vector3 DistanceFromHead;
    public GrabGimbal grabGimbal;
    public void SetPosition()
    {
        Vector3 offset = KeyboardCentre.position - (head.position + DistanceFromHead);
        GrabBall.position -= offset;
        grabGimbal.UpdateTargetRotation();
    }

}
