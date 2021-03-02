using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTransformManager : MonoBehaviour
{
    [HideInInspector] public Vector3 TargetPosition;
    [HideInInspector] public Quaternion TargetRotation;
    public float lerpSpeed = 30;

    private void Start() {
        TargetPosition = transform.position;
        TargetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != TargetPosition || transform.rotation != TargetRotation)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, Time.deltaTime * lerpSpeed);
        }
    }
}
