using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;


[RequireComponent(typeof(InteractionBehaviour))]
public class AngleGrabBall : MonoBehaviour
{
    public Transform targetObject;
    public Transform grabGimbal;
    public float lerpSpeed = 20;

    public float offsetDistance = 0.25f;

    public float startAngle = 35;

    public Vector3 localOffset;
    private InteractionBehaviour behaviour;

    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<InteractionBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!behaviour.isGrasped)
        {
            Vector3 targetPosition = targetObject.TransformPoint(localOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        }
    }

    public void UpdateAngle()
    {
        Vector3 targetPosition = grabGimbal.InverseTransformPoint(transform.position);
        targetPosition.x = 0;
        Vector3 targetDirection = targetPosition - grabGimbal.InverseTransformPoint(targetObject.position);
        Quaternion rotation = Quaternion.FromToRotation(grabGimbal.up, targetDirection);
        targetObject.localRotation = rotation;
    }
}
