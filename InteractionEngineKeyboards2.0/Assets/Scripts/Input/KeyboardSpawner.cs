using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardSpawner : MonoBehaviour
{
    public enum RelativeTo
    {
        HEAD,
        TEXT_FIELD
    }
    public Transform head;
    public Transform GrabBall;
    public GrabGimbal GrabGimbal;
    public Transform KeyboardCentre;
    public Vector3 DistanceFromHead;
    public RelativeTo RotationRelativeTo;
    private bool keyboardActive = false;
    private GameObject currentlySelected;
    public Vector3 currentDistance;

    // Update is called once per frame
    void Update()
    {
        currentDistance = KeyboardCentre.position - head.position;
        currentlySelected = EventSystem.current.currentSelectedGameObject;
        if (currentlySelected == null)
        {
            if (keyboardActive)
            {
                DespawnKeyboard();
            }
            return;
        }
        if (currentlySelected.GetComponent<TMP_InputField>() != null
        || currentlySelected.GetComponent<InputField>() != null)
        {
            if (!keyboardActive)
            {
                SpawnKeyboard();
            }
        }
        else
        {
            if (keyboardActive)
            {
                DespawnKeyboard();
            }
        }
    }
    private void SpawnKeyboard()
    {

        keyboardActive = true;
        GrabBall.parent.gameObject.SetActive(keyboardActive);
        //As the grab ball's pivot point isn't in the centre of the keyboard, 
        // we want to work out how far we'd need to move the keyboard to be offset from the head
        // and then apply that offset to the grab ball
        Vector3 offset = (head.position + DistanceFromHead) - KeyboardCentre.position;
        GrabBall.position += offset;
        if (RotationRelativeTo == RelativeTo.HEAD)
        {
            GrabGimbal.UpdateTargetRotation();
        } else {
            GrabGimbal.targetRotation = currentlySelected.transform.rotation;
        }
    }
    private void DespawnKeyboard()
    {
        keyboardActive = false;
        GrabBall.parent.gameObject.SetActive(keyboardActive);
    }


}
