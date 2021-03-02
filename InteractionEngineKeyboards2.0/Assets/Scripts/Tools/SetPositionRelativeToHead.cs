using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public TargetTransformManager TargetTransformManager;
    public Vector3 DistanceFromHead;
    public Vector3 Angles;

    public void SetPosition()
    {
        TargetTransformManager.TargetRotation = Quaternion.identity * Quaternion.Euler(Angles);
        TargetTransformManager.TargetPosition = head.position + DistanceFromHead;
    }

}
