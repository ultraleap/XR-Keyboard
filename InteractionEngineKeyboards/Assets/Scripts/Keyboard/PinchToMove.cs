using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity;

public class PinchToMove : MonoBehaviour
{
    [SerializeField]
    private GameObject rightHandModel, leftHandModel, target;

    private GameObject pinchingHand;

    [SerializeField]
    private ToggleSwitchButton toggleSwitchButton;

    bool isRightPinching = false;
    bool isLeftPinching = false;
    bool singleHandPinching = false;

    GameObject emptyChild;

    private void Start()
    {
        emptyChild = new GameObject("EmptyChild");
        emptyChild.transform.position = target.transform.position;
        emptyChild.transform.rotation = target.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggleSwitchButton.On)
        {
            if (Hands.Right == null)
            {
                isRightPinching = false;
            }
            else
            {
                isRightPinching = Hands.Right.IsPinching();
            }

            if (Hands.Left == null)
            {
                isLeftPinching = false;
            }
            else
            {
                isLeftPinching = Hands.Left.IsPinching();
            }

            singleHandPinching = isRightPinching ^ isLeftPinching;

            if (singleHandPinching)
            {
                if (isLeftPinching)
                {
                    pinchingHand = leftHandModel;
                }
                else if (isRightPinching)
                {
                    pinchingHand = rightHandModel;
                }
            }

            if (!singleHandPinching && emptyChild.transform.parent == null)
            {
                return;
            }
            else if (!singleHandPinching && emptyChild.transform.parent != null)
            {
                emptyChild.transform.SetParent(null);
            }
            else if (singleHandPinching && emptyChild.transform.parent == null)
            {
                emptyChild.transform.position = target.transform.position;
                emptyChild.transform.rotation = target.transform.rotation;
                emptyChild.transform.SetParent(pinchingHand.transform);
            }
            else if (singleHandPinching)
            {
                target.transform.position = emptyChild.transform.position;
                target.transform.rotation = emptyChild.transform.rotation;
            }
        }
    }
}
