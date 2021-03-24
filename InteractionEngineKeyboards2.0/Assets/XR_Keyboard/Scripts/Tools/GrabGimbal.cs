using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using NaughtyAttributes;
using UnityEngine;

public class GrabGimbal : MonoBehaviour
{
    public Transform head;
    public Transform grabBall;
    public Transform grabGimbal;
    [HideInInspector] public Vector3 TargetPosition;
    public float lerpSpeed = 30;
    public Quaternion targetRotation;
    private InteractionBehaviour grabBallInteractionBehaviour;

    private void Start()
    {
        if (head == null) head = Camera.main.transform;
        grabBallInteractionBehaviour = grabBall.GetComponent<InteractionBehaviour>();
        targetRotation = grabGimbal.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabBallInteractionBehaviour.isGrasped)
        {
            UpdateTargetRotation();
        }
        if (grabGimbal.rotation != targetRotation)
        {
            grabGimbal.rotation = Quaternion.Lerp(grabGimbal.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        }
    }

    public void UpdateTargetRotation()
    {
        StartCoroutine(WaitNFramesThenLookAtHead());
    }

    private IEnumerator WaitNFramesThenLookAtHead()
    {
        //Wait one frame before rotating as the position is updated in the Physics loop
        yield return null;
        Vector3 pos = grabBall.GetComponent<Rigidbody>().position;
        pos.y = head.position.y;
        Vector3 forward = pos - head.position;
        targetRotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
