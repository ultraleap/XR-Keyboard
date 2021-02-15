using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtonSounds : MonoBehaviour
{
    public Transform UIParent;
    public AudioClip hoverSound;
    public AudioClip downSound;
    public AudioClip upSound;

    // Start is called before the first frame update
    void Start()
    {
        var selectables = UIParent.transform.GetComponentsInChildren<Selectable>(true);
        foreach (Selectable s in selectables)
        {
            if (s.gameObject.GetComponent<AudioSource>() == null)
            {
                s.gameObject.AddComponent<AudioSource>();
            }

            if (s.gameObject.GetComponent<UIButtonSounds>() == null)
            {
                UIButtonSounds bSounds = s.gameObject.AddComponent<UIButtonSounds>();
                if (hoverSound != null) { bSounds.hoverSound = hoverSound; }
                if (downSound != null) { bSounds.downSound = downSound; }
                if (upSound != null) { bSounds.upSound = upSound; }
            }
        }
    }
}
