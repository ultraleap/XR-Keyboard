using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public static KeyboardManager Instance;

    public List<Keyboard> keyboards;
    private Keyboard defaultKeyboard;
    public KeyboardSpawner keyboardSpawner;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        if (keyboards.Count == 0)
        {
            Debug.LogWarning("Detecting keyboards in the scene. If you require access to a specific keyboard assign them manually in the inspector.");
            keyboards = new List<Keyboard>();
            keyboards.AddRange(FindObjectsOfType<Keyboard>());
        }

        if (keyboards.Count == 0)
        {
            Debug.LogWarning("No Keyboards Found. Make sure there is an object with a keyboard component in the scene.");
        }
        else
        {
            defaultKeyboard = keyboards[0];
        }
        if (keyboardSpawner != null)
        {
            keyboardSpawner.KeyboardStart();
        }
    }

    // This currently only supports spawning of one keyboard, but this could pick from
    // a collection of keyboards and return an appropriate one.
    public Keyboard SpawnKeyboard(Transform currentlySelected)
    {
        if (keyboardSpawner != null)
        {
            keyboardSpawner.SpawnKeyboard(currentlySelected);
        }

        return defaultKeyboard;
    }

    public void DespawnKeyboard()
    {
        if (keyboardSpawner != null)
        {
            keyboardSpawner.DespawnKeyboard();
        }
    }

    public Keyboard ActiveKeyboard()
    {
        return defaultKeyboard;
    }
}