using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class DisableKeyboardInteraction : MonoBehaviour
{
    public void DisableKeys()
    {
        InteractionButton[] keys = transform.GetComponentsInChildren<InteractionButton>();
        foreach(var key in keys)
        {
            key.controlEnabled = false;
        }
    }

    public void EnableKeys()
    {
        InteractionButton[] keys = transform.GetComponentsInChildren<InteractionButton>();
        foreach(var key in keys)
        {
            key.controlEnabled = true;
        }
    }
}
