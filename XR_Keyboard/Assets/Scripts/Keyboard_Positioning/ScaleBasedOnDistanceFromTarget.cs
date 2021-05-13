using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Leap.Unity.Interaction;
using Leap.Unity;

public class ScaleBasedOnDistanceFromTarget : MonoBehaviour
{
    public InteractionBehaviour behaviour;

    [MinMaxSlider(0, 10)] public Vector2 minMaxScale = new Vector2(0.1f, 1);
    [MinMaxSlider(0, 1)] public Vector2 minMaxDistance = new Vector2(0.1f, 0.5f);
    public AnimationCurve scaleAnimationCurve;
    public float lerpSpeed;
    private Vector3 targetScale;
    private Vector3 targetPosition;
    
    // Update is called once per frame
    void Update()
    {
        float closestDistance = Mathf.Infinity;

        if (behaviour != null && behaviour.primaryHoveringFinger != null)
        {
            targetPosition = behaviour.primaryHoveringFinger.TipPosition.ToVector3();
        
            float distance = Vector3.Distance(transform.position, targetPosition);
            float clampedDistance = Mathf.Clamp(Vector3.Distance(transform.position, targetPosition), minMaxDistance.x, minMaxDistance.y);

            if (clampedDistance < closestDistance)
            {
                closestDistance = clampedDistance;
            }
        }
        targetScale = CalculateScale(closestDistance);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
    }

    private Vector3 CalculateScale(float _distance)
    {
        float t = Mathf.InverseLerp(minMaxDistance.x, minMaxDistance.y, _distance);
        float scale = Mathf.Lerp(minMaxScale.x, minMaxScale.y, Mathf.Abs(1 - t));
        return Vector3.one * scaleAnimationCurve.Evaluate(scale);
    }
}
