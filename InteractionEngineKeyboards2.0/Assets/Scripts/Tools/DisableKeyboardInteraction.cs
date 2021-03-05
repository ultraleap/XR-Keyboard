using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class DisableKeyboardInteraction : MonoBehaviour
{
    private InteractionButton[] keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = transform.GetComponentsInChildren<InteractionButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableKeys()
    {
        foreach(var key in keys)
        {
            key.controlEnabled = false;
        }
    }

    public void EnableKeys()
    {
        foreach(var key in keys)
        {
            key.controlEnabled = true;
        }
    }
}
