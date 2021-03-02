using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public Transform target;
    public Vector3 DistanceFromHead;
    public void SetPosition()
    {
        target.position = head.position + DistanceFromHead;
    }

}
