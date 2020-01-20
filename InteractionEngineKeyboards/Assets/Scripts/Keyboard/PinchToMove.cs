using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity;

public class PinchToMove : MonoBehaviour
{
    [SerializeField]
    private GameObject rightHandModel;

    [SerializeField]
    private ToggleSwitchButton toggleSwitchButton;

    bool isRightPinching = false;
    bool bothHandsPinching = false;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggleSwitchButton.On)
        {
            if (Hands.Right == null)
            {
                isRightPinching = false;
                return;
            }
            else
            {
                isRightPinching = Hands.Right.IsPinching();
            }

            if (Hands.Left == null)
            {
                bothHandsPinching = false;
            }
            else
            {
                bothHandsPinching = Hands.Right.IsPinching() && Hands.Left.IsPinching();
            }

            if (bothHandsPinching && transform.parent == null)
            {
                return;
            }
            else if (bothHandsPinching && transform.parent != null)
            {
                transform.SetParent(null);
            }
            else if (isRightPinching && !bothHandsPinching && transform.parent == null)
            {
                transform.SetParent(rightHandModel.transform);
            }
            else if (!isRightPinching && transform.parent != null)
            {
                transform.SetParent(null);
            }
        }
    }
}
