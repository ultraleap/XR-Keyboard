using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;
using Leap.Unity.Interaction;

public class AddInteractionButtonSounds : MonoBehaviour
{
    public Transform ButtonParent;
    public AudioClip hoverSound;
    public AudioClip downSound;
    public AudioClip upSound;

    // Start is called before the first frame update
    void Start()
    {
        InteractionButton[] interactionButtons = ButtonParent.transform.GetComponentsInChildren<InteractionButton>(true);
        foreach (InteractionButton interactionButton in interactionButtons)
        {
            if (interactionButton.gameObject.GetComponent<AudioSource>() == null)
            {
                interactionButton.gameObject.AddComponent<AudioSource>();
            }

            if (interactionButton.gameObject.GetComponent<ButtonSounds>() == null)
            {
                ButtonSounds bSounds = interactionButton.gameObject.AddComponent<ButtonSounds>();
                if (hoverSound != null) { bSounds.hoverSound = hoverSound; }
                if (downSound != null) { bSounds.downSound = downSound; }
                if (upSound != null) { bSounds.upSound = upSound; }
            }
        }
    }
}
