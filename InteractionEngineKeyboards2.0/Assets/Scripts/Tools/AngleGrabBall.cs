using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;


[RequireComponent(typeof(InteractionBehaviour))]
[RequireComponent(typeof(LineRenderer))]
public class AngleGrabBall : MonoBehaviour
{
    public Transform targetObject;
    public Transform grabGimbal;
    public float lerpSpeed = 20;

    public float offsetDistance = 0.25f;

    public float startAngle = 35;

    public Vector3 localOffset;

    private LineRenderer rotationGizmo;
    private InteractionBehaviour behaviour;

    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<InteractionBehaviour>();
        rotationGizmo = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!behaviour.isGrasped)
        {
            Vector3 targetPosition = targetObject.TransformPoint(localOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            HideRotationGizmo();
        }
        else
        {
            DrawRotationGizmo();
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

    private void HideRotationGizmo()
    {
        if ( rotationGizmo.positionCount > 0)
        rotationGizmo.positionCount--;
    }

    private void DrawRotationGizmo()
    {
        rotationGizmo.enabled = true;
        rotationGizmo.positionCount = 64;

        Keyframe[] keys = new Keyframe[rotationGizmo.positionCount];
        float currentRotationTime = Vector3.SignedAngle(Vector3.up, targetObject.up, targetObject.right) / 360;
        float dir = Mathf.Sign(currentRotationTime);
        
        currentRotationTime = Mathf.Abs(currentRotationTime);

        startAngle = currentRotationTime;
        for (int i = 0; i < rotationGizmo.positionCount; i++)
        {
            float time = (i / (float)rotationGizmo.positionCount) ;
            Vector3 position = new Vector3() {
                x = 0,
                y = Mathf.Cos(time * 2 * Mathf.PI) * localOffset.magnitude,
                z = -dir * Mathf.Sin(time * 2 * Mathf.PI) * localOffset.magnitude
            };
            
            rotationGizmo.SetPosition(i, targetObject.TransformPoint(position));
            
            bool innerAngle = time > currentRotationTime;
            float width = innerAngle ? Mathf.Lerp(1, 0.1f,(time - currentRotationTime) * 10f) : 1;

            keys[i] = new Keyframe(time, width);
        }
        rotationGizmo.widthCurve = new AnimationCurve(keys);

    }
}
