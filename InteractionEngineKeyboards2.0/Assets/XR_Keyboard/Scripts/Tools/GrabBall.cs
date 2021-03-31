using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    public Transform head;
    public Transform GrabFollow;
    public Transform GrabGimbal;
    public float lerpSpeed = 30;

    public List<Transform> attachedObjects;
    private List<Transform> attachedObjectsTransformHelpers;

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
        attachedObjectsTransformHelpers = new List<Transform>();
        foreach(Transform attachedObject in attachedObjects){
            GameObject attachedObjectTransformHelper = new GameObject(attachedObject.name + "TransformHelper");
            attachedObjectTransformHelper.transform.position = attachedObject.position;
            attachedObjectTransformHelper.transform.rotation = attachedObject.rotation;
            attachedObjectTransformHelper.transform.parent = GrabGimbal;
            attachedObjectsTransformHelpers.Add(attachedObjectTransformHelper.transform);
        }
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

        for (int i = 0; i < attachedObjects.Count; i++)
        {
            attachedObjects[i].position = attachedObjectsTransformHelpers[i].position;
            attachedObjects[i].rotation = attachedObjectsTransformHelpers[i].rotation;
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
        Vector3 position = rigidBody.position;
        position.y = head.position.y;
        Vector3 forward = position - head.position;
        targetRotation = Quaternion.LookRotation(forward, Vector3.up);
    }

}