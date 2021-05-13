using System.Collections.Generic;
using System.Collections;
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
    public AudioClip showSound, hideSound;
    public Vector3 anchorOffset = new Vector3(0, 250, 250);
    public Vector3 horizontalOffset = new Vector3(200, 0, 0);
    public float timeout = 10;
    public float accentPanelHideDelay = 0.25f;

    [HideInInspector] public List<string> SpecialChars;
    [HideInInspector] public Transform AccentKeyAnchor;

    private AudioSource audioSource;
    private bool makeNoise = false;
    private Coroutine hidePanelRoutine;
    private Coroutine timeoutPanelRoutine;


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

    public void ShowAccentPanel(List<string> specialChars)
    {
        transform.position = AccentKeyAnchor.position;
        transform.rotation = AccentKeyAnchor.rotation;
        transform.localPosition += anchorOffset + HorizontalOffset(AccentKeyAnchor, specialChars.Count);

        this.SpecialChars = specialChars;
        panel.gameObject.SetActive(true);

        ClearPanel();
        GeneratePanel();

        if (Application.isPlaying && makeNoise)
        {
            audioSource.PlayOneShot(showSound);
        }

        if (timeoutPanelRoutine != null)
        {
            StopCoroutine(timeoutPanelRoutine);
        }
        timeoutPanelRoutine = StartCoroutine(TimeOutPanel(timeout));
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
        for (int i = keyRow.childCount - 1; i >= 0; i--)
        {
            if (keyRow.GetChild(i).GetComponentInChildren<TextInputButton>())
            {
                DestroyImmediate(keyRow.GetChild(i).gameObject);
            }
        }
        for (int i = shadowRow.childCount - 1; i >= 0; i--)
        {
            if (shadowRow.GetChild(i).GetComponentInChildren<TextInputButton>())
            {
                DestroyImmediate(shadowRow.GetChild(i).gameObject);
            }
        }
    }

    public void GeneratePanel()
    {
        foreach (var special in SpecialChars)
        {
            GameObject shadow = Instantiate(shadowPrefab, shadowRow);
            GameObject newKey = Instantiate(keyPrefab, keyRow);
            TextInputButton button = newKey.GetComponentInChildren<TextInputButton>();
            button.Key = special;

            button.UpdateActiveKey(button.Key, KeyboardManager.Instance.ActiveKeyboard().ActivekeyboardMode);
            newKey.name = button.Key;
        }
        Canvas.ForceUpdateCanvases();

        UIKeyboardResizer.ResizeKeyboardLayoutObject(accentPanelLayoutObject);
    }

    public Vector3 HorizontalOffset(Transform _keyTransform, int keyCount)
    {
        float midPoint = _keyTransform.parent.parent.childCount / 2f;
        float dir = -Mathf.Clamp(_keyTransform.parent.GetSiblingIndex() - midPoint, -1, 1);

        return horizontalOffset * dir * (keyCount / 2);
    }

    public void DisableInput()
    {
        InteractionButton[] interactionButtons = keyRow.GetComponentsInChildren<InteractionButton>();

        foreach (InteractionButton interactionButton in interactionButtons)
        {
            interactionButton.controlEnabled = false;
        }
    }

    public IEnumerator HidePanelAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideAccentPanel();
    }

    public IEnumerator TimeOutPanel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideAccentPanel();
    }

    public void DismissAccentPanel()
    {
        DisableInput();
        hidePanelRoutine = StartCoroutine(HidePanelAfter(accentPanelHideDelay));
    }
}
