using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public TargetTransformManager TargetTransformManager;
    public Vector3 DistanceFromHead;
    public float xAngle = 35;

    public void SetPosition()
    {
        TargetTransformManager.TargetXAngle = xAngle;
        TargetTransformManager.TargetPosition = head.position + DistanceFromHead;
    }

}
