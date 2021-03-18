using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class AccentOverlayPanel : MonoBehaviour
{
    public GameObject keyPrefab, shadowPrefab;
    public Transform panel, shadowRow, keyRow;

    public List<KeyMap.KeyboardKey> keys;

    public AudioClip showSound, hideSound;
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

    // Update is called once per frame
    void Update()
    {
        
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

        foreach (var key in keys)
        {
            GameObject shadow = Instantiate(shadowPrefab, shadowRow);
            GameObject newKey = Instantiate(keyPrefab, keyRow);
            TextInputButton button = newKey.GetComponentInChildren<TextInputButton>();
            button.NeutralKey = key.neutralKey;
            button.Symbols1Key = key.symbols1Key;
            button.Symbols2Key = key.symbols2Key;
            button.UpdateActiveKey(button.NeutralKey, KeyboardManager.KeyboardMode.NEUTRAL);
            newKey.name = button.NeutralKey.ToString();
        }
        
        GetComponent<UIKeyboardResizer>().ResizeKeyboard();
    }
}
