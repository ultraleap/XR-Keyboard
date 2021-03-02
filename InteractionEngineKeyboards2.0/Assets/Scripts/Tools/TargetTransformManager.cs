using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTransformManager : MonoBehaviour
{

    public Transform head;
    [HideInInspector] public Vector3 TargetPosition;
    public float lerpSpeed = 30;
    [HideInInspector] public float TargetXAngle;
    private Quaternion targetRotation;

    private void Start()
    {
        TargetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != TargetPosition || transform.rotation != targetRotation)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * lerpSpeed);
            UpdateTargetRotation();
        }
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        }
    }

    private void UpdateTargetRotation()
    {
        Quaternion rotation = transform.rotation;

        //Get lookat angle
        transform.LookAt(head);
        targetRotation = transform.rotation;

        //Change xAngle to be the one we want
        Vector3 euler = targetRotation.eulerAngles;
        euler.x = TargetXAngle;
        
        //orient the right way round
        euler.y +=180f;

        //set target rotation
        targetRotation = Quaternion.Euler(euler);
        transform.rotation = rotation;
    }
}
