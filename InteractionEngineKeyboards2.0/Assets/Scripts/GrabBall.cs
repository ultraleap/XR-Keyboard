using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class GrabBall : MonoBehaviour
{

    public TargetTransformManager TargetTransformManager;
    public InteractionBehaviour interactionBehaviour;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = TargetTransformManager.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionBehaviour.isGrasped){
            TargetTransformManager.TargetPosition = transform.position + offset;
        }
        if(!interactionBehaviour.isGrasped && TargetTransformManager.transform.position - transform.position != offset){
            transform.position = TargetTransformManager.transform.position - offset;
        }
    }
}
