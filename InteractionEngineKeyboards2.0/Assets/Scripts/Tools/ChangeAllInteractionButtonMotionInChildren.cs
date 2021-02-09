using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using NaughtyAttributes;
using UnityEngine;


public class ChangeAllInteractionButtonMotionInChildren : MonoBehaviour
{
    [OnValueChanged("SetAllButtons")] public Vector2 minMaxHeight = new Vector2(0, 0.03f);
    [OnValueChanged("SetAllButtons")] [Range(0f, 1f)] public float restingHeight = 0.5f;
    [OnValueChanged("SetAllButtons")] [Range(0f, 1f)] public float springForce = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        SetAllButtons();
    }

    private void SetAllButtons()
    {
        InteractionButton[] interactionButtons = gameObject.GetComponentsInChildren<InteractionButton>();
        foreach (InteractionButton ib in interactionButtons)
        {
            ib.minMaxHeight = minMaxHeight;
            ib.restingHeight = restingHeight;
            ib.springForce = springForce;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
