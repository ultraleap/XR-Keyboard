using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class GrabGimbal : MonoBehaviour
{

    public Transform head;
    public Transform grabBall;
    public Transform grabGimbal;
    [HideInInspector] public Vector3 TargetPosition;
    public float lerpSpeed = 30;
    private Quaternion targetRotation;
    private InteractionBehaviour grabBallInteractionBehaviour;

    private void Start()
    {
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
        grabGimbal.rotation = Quaternion.Lerp(grabGimbal.rotation, targetRotation, Time.deltaTime * lerpSpeed);
    }

    public void UpdateTargetRotation()
    {
        Vector3 pos = transform.position;
        pos.y = head.position.y;
        Vector3 forward = pos - head.position;

        targetRotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
