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

    bool isPinching;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggleSwitchButton.On)
        {
            if (Hands.Right == null)
            {
                return;
            }
            isPinching = Hands.Right.IsPinching();


            if (isPinching && transform.parent == null)
            {
                transform.SetParent(rightHandModel.transform);
            }
            else if (!isPinching && transform.parent != null)
            {
                transform.SetParent(null);
            }
        }
    }
}
