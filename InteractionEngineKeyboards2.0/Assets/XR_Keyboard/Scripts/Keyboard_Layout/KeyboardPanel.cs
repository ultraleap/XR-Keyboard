using System.Collections;
using UnityEngine;
using Leap.Unity.Interaction;

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

    private float timeOut = 0.1f;


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
        EnableInput();
        gameObject.SetActive(true);
        StopAllCoroutines();
    }

    public void HidePanel()
    {
        DisableInput();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(HidePanelAfter(timeOut));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator HidePanelAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }

    public void DisableInput()
    {
        InteractionButton[] interactionButtons = GetComponentsInChildren<InteractionButton>();

        foreach (InteractionButton interactionButton in interactionButtons)
        {
            interactionButton.controlEnabled = false;
        }
    }

    public void EnableInput()
    {
        InteractionButton[] interactionButtons = GetComponentsInChildren<InteractionButton>();

        foreach (InteractionButton interactionButton in interactionButtons)
        {
            if (interactionButton.GetComponent<TextInputButton>().Key != "")
            {
                interactionButton.controlEnabled = true;
            }
        }
    }
}

