using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonSounds : MonoBehaviour, 
                            IPointerClickHandler, 
                            IPointerDownHandler, 
                            IPointerEnterHandler, 
                            IPointerExitHandler
{
    public AudioClip hoverSound;
    public AudioClip downSound;
    public AudioClip upSound;

    public AudioSource source;

    private bool over = false;
    private bool click = false;

    // Start is called before the first frame update
    void Start()
    {
        if (source == null) source = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (hoverSound != null && !over) source.PlayOneShot(hoverSound);
        over = true;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (downSound != null) source.PlayOneShot(downSound);
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (upSound != null) source.PlayOneShot(upSound);
        click = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!click) 
        {
            over = false;
        }
        click = false;
    }
}
