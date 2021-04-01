using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    [Tooltip("The transform representing the player's head. If left empty it will default to Camera.main")] 
    public Transform head;
    public Transform GrabFollow;
    public Transform GrabGimbal;
    public float lerpSpeed = 30;
    public Transform attachedObject;

    [Tooltip("The transform that position & rotation transformations are applied. If left empty it will default to a child object named AttachedObjectTransformHelper")]
    public Transform attachedObjectTransformHelper;

    [HideInInspector] public Transform targetPosition;
    [HideInInspector] public Quaternion targetRotation;
    private InteractionBehaviour grabBallInteractionBehaviour;
    private Rigidbody rigidBody;

    private void Start()
    {
        if (head == null) head = Camera.main.transform;
        targetPosition = transform;
        grabBallInteractionBehaviour = GetComponent<InteractionBehaviour>();
        rigidBody = GetComponent<Rigidbody>();
        targetRotation = GrabGimbal.rotation;
        if (attachedObjectTransformHelper == null) { gameObject.transform.Find("AttachedObjectTransformHelper"); }
        attachedObjectTransformHelper.transform.position = attachedObject.position;
        attachedObjectTransformHelper.transform.rotation = attachedObject.rotation;
        attachedObjectTransformHelper.transform.parent = GrabGimbal;
    }

    void Update()
    {
        GrabFollow.position = Vector3.Lerp(GrabFollow.position, targetPosition.position, Time.deltaTime * lerpSpeed);

        if (grabBallInteractionBehaviour.isGrasped)
        {
            UpdateTargetRotation();
        }
        if (GrabGimbal.rotation != targetRotation)
        {
            GrabGimbal.rotation = Quaternion.Lerp(GrabGimbal.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        }
        attachedObject.position = attachedObjectTransformHelper.position;
        attachedObject.rotation = attachedObjectTransformHelper.rotation;
    }

    public void UpdateTargetRotation()
    {
        StartCoroutine(WaitNFramesThenLookAtHead());
    }

    private IEnumerator WaitNFramesThenLookAtHead()
    {
        //Wait one frame before rotating as the position is updated in the Physics loop
        yield return null;
        Vector3 position = rigidBody.position;
        position.y = head.position.y;
        Vector3 forward = position - head.position;
        targetRotation = Quaternion.LookRotation(forward, Vector3.up);
    }

}