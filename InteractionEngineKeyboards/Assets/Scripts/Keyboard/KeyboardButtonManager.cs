using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public enum KeyboardType
{
    PINCH, PUSH
}

public class KeyboardButtonManager : MonoBehaviour
{
    [SerializeProperty("KeyType")] // pass the name of the property as a parameter
    public KeyboardType keyType;

    public KeyboardType KeyType
    {
        get
        {
            return keyType;
        }
        set
        {
            keyType = value;
            UpdateButtonType();
        }
    }
    [SerializeField]
    private Transform[] KeyboardRows;
    [SerializeField]
    private GameObject[] pinchBalls;

    void UpdateButtonType()
    {
        List<GameObject> keys = new List<GameObject>();
        foreach (Transform row in KeyboardRows)
        {
            foreach (Transform keyBase in row)
            {
                keys.Add(keyBase.GetChild(0).gameObject);
            }
        }
        if (KeyType == KeyboardType.PINCH)
        {
            foreach (GameObject key in keys)
            {
                if (key.GetComponent<TouchInputButton>())
                {
                    DestroyImmediate(key.GetComponent<TouchInputButton>());
                }
                PinchInputButton p = key.AddComponent<PinchInputButton>();
                p.ScaleModification = true;
                p.ScaleModifier = new Vector3(0.9f, 0.9f, 1);

                InteractionButton interactionButton = key.GetComponent<InteractionButton>();
                interactionButton.minMaxHeight = Vector2.zero;
                interactionButton.restingHeight = 0;
                interactionButton.springForce = 0;
            }
            foreach (GameObject pb in pinchBalls)
            {
                pb.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
        else if (KeyType == KeyboardType.PUSH)
        {
            foreach (GameObject pb in pinchBalls)
            {
                pb.GetComponent<CapsuleCollider>().enabled = false;
            }

            foreach (GameObject key in keys)
            {
                if (key.GetComponent<PinchInputButton>())
                {
                    DestroyImmediate(key.GetComponent<PinchInputButton>());
                }
                TouchInputButton p = key.AddComponent<TouchInputButton>();
                p.ScaleModification = false;
                p.ScaleModifier = Vector3.one;

                InteractionButton interactionButton = key.GetComponent<InteractionButton>();
                interactionButton.minMaxHeight = new Vector2(0, 0.03f);
                interactionButton.restingHeight = 0.5f;
                interactionButton.springForce = 0.1f;
            }
        }
    }
}
