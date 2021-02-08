using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public Transform keyboard;
    public Vector3 DistanceFromHead;
    public Vector3 Angles;

    public void SetPosition()
    {
        keyboard.rotation = Quaternion.identity * Quaternion.Euler(Angles);
        keyboard.position = head.position + DistanceFromHead;
    }
}
