using System.Collections;
using UnityEngine;

[RequireComponent(typeof(KeyMap))]
[RequireComponent(typeof(KeyMapGenerator))]
[RequireComponent(typeof(UIKeyboardResizer))]
public class KeyboardPanel : MonoBehaviour
{
    [HideInInspector] public KeyMap keyMap;
    [HideInInspector] public KeyMapGenerator keyMapGenerator;
    [HideInInspector] public UIKeyboardResizer keyboardResizer;
    
    [Tooltip("When enabled, the keyboard will regenerate from the key map on start")]
    public bool regenKeyboardOnStart;

    // Start is called before the first frame update
    void Start()
    {
        keyMap = GetComponent<KeyMap>();
        keyMapGenerator = GetComponent<KeyMapGenerator>();
        keyboardResizer = GetComponent<UIKeyboardResizer>();

        if (regenKeyboardOnStart) keyMapGenerator.RegenerateKeyboard(keyMap, keyboardResizer);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        StartCoroutine(HidePanelAfter(0.5f));
    }

    public IEnumerator HidePanelAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}

