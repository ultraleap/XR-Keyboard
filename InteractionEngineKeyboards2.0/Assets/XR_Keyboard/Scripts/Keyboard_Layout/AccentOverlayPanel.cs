﻿using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using Leap.Unity.Interaction;

public class AccentOverlayPanel : MonoBehaviour
{
    public UIKeyboardResizer.KeyboardLayoutObjects accentPanelLayoutObject;
    public UIKeyboardResizer UIKeyboardResizer;
    public GameObject keyPrefab, shadowPrefab;
    public Transform panel, shadowRow, keyRow, background;

    public List<string> specialChars;

    public AudioClip showSound, hideSound;

    public Vector3 anchorOffset = Vector3.zero;
    public Vector3 horizontalOffset = new Vector3(150, 0, 0);
    public Color overlayColour, inlineColour;

    [Header("Dismissal")] public float timeout = 5;

    private AudioSource audioSource;
    private bool makeNoise = false;


    // Start is called before the first frame update
    void Start()
    {
        if (keyPrefab == null) keyPrefab = transform.GetComponentInParent<KeyMapGenerator>().keyPrefab;
        if (shadowPrefab == null) shadowPrefab = transform.GetComponentInParent<KeyMapGenerator>().shadowPrefab;
        if (panel == null) panel = transform.GetChild(0);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = showSound;
        makeNoise = showSound != null && hideSound != null;

    }

    [Button]
    public void TogglePanelVisibility()
    {
        if (panel.gameObject.activeSelf == false)
        {
            ClearPanel();
            GeneratePanel();
        }

        panel.gameObject.SetActive(!panel.gameObject.activeSelf);

        if (Application.isPlaying && makeNoise)
        {
            audioSource.PlayOneShot(panel.gameObject.activeSelf ? showSound : hideSound);
        }
    }

    public void ShowAccentPanel(List<string> specialChars, Transform _keyTransform, bool offsetAnchor = false)
    {
        transform.position = _keyTransform.position;
        transform.rotation = _keyTransform.rotation;

        if (offsetAnchor)
        {
            transform.localPosition += anchorOffset + HorizontalOffset(_keyTransform, specialChars.Count);
        }

        this.specialChars = specialChars;
        panel.gameObject.SetActive(true);

        ClearPanel();
        GeneratePanel();

        if (Application.isPlaying && makeNoise)
        {
            audioSource.PlayOneShot(showSound);
        }

    }

    public void HideAccentPanel()
    {
        if (panel.gameObject.activeSelf == true)
        {
            panel.gameObject.SetActive(false);

            if (Application.isPlaying && makeNoise)
            {
                audioSource.PlayOneShot(hideSound);
            }
        }
    }

    public void ClearPanel()
    {
        for(int i = keyRow.childCount - 1; i >= 0; i--)
        {
            if (keyRow.GetChild(i).GetComponentInChildren<TextInputButton>())
            {
                DestroyImmediate(keyRow.GetChild(i).gameObject);
            }
        }
        for(int i = shadowRow.childCount - 1; i >= 0; i--)
        {
            if (shadowRow.GetChild(i).GetComponentInChildren<TextInputButton>())
            {
                DestroyImmediate(shadowRow.GetChild(i).gameObject);
            }
        }
    }

    public void GeneratePanel()
    {
        foreach (var special in specialChars)
        {
            GameObject shadow = Instantiate(shadowPrefab, shadowRow);
            GameObject newKey = Instantiate(keyPrefab, keyRow);
            TextInputButton button = newKey.GetComponentInChildren<TextInputButton>();
            button.Key = special;
            
            button.UpdateActiveKey(button.Key, Keyboard.KeyboardMode.NEUTRAL);
            newKey.name = button.Key;
        }
        Canvas.ForceUpdateCanvases();
        
        UIKeyboardResizer.ResizeKeyboardLayoutObject(accentPanelLayoutObject);
    }

    public void SetOverlayColour(){
        background.GetComponent<Image>().color = overlayColour;
    }

    public void SetInlineColour(){
        background.GetComponent<Image>().color = inlineColour;
    }

    public Vector3 HorizontalOffset(Transform _keyTransform, int keyCount)
    {
        float midPoint = _keyTransform.parent.parent.childCount / 2f; 
        float dir = -Mathf.Clamp(_keyTransform.parent.GetSiblingIndex() - midPoint, -1, 1);

        return horizontalOffset * dir * (keyCount / 2);
    }
    
    public void DisableInput(){
        InteractionButton[] interactionButtons = keyRow.GetComponentsInChildren<InteractionButton>(); 

        foreach(InteractionButton interactionButton in interactionButtons){
            interactionButton.controlEnabled = false;
        }
    }
}
