using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;

public class PinchToScale : MonoBehaviour
{

    [SerializeField]
    Transform[] Pinchballs;

    [SerializeField]
    private ToggleSwitchButton toggleSwitchButton;

    private float minScale = 0.2f;
    private float maxScale = 2f;

    bool bothHandsPinching = false;
    bool start = true;

    float initialDistance;
    Vector3 initialScale;

    void FixedUpdate()
    {
        if (toggleSwitchButton.On)
        {
            if (Hands.Right == null || Hands.Left == null)
            {
                return;
            }
            bothHandsPinching = Hands.Right.IsPinching() && Hands.Left.IsPinching();

            if (bothHandsPinching && start)
            {
                initialDistance = Vector3.Distance(Pinchballs[0].position, Pinchballs[1].position);
                initialScale = transform.localScale;
                start = false;
            } else if (bothHandsPinching && !start)
            {
                Scale();

            } else if (!bothHandsPinching && !start)
            {
                start = true;
            }
        }
    }

    void Scale()
    {
        Vector3 newScale = initialScale * Vector3.Distance(Pinchballs[0].position, Pinchballs[1].position) / initialDistance;

        if (newScale.x < maxScale && newScale.x > minScale)
        {
            transform.localScale = newScale;
        }
    }
}
