using UnityEngine;

public class KeyboardSpawner : MonoBehaviour
{
    public GameObject KeyboardPrefabRoot;
    public bool keyboardEnabledOnStart = false;
    private bool keyboardActive = false;

    // Start is called before the first frame update
    public virtual void KeyboardStart()
    {
        keyboardActive = KeyboardPrefabRoot.activeInHierarchy;

        if (keyboardEnabledOnStart)
        {
            SpawnKeyboard();
        }
        else
        {
            DespawnKeyboard();
        }
    }

    public virtual void SpawnKeyboard()
    {
        if (keyboardActive)
        {
            return;
        }
        else
        {
            keyboardActive = true;
        }
        KeyboardPrefabRoot.SetActive(keyboardActive);
    }

    public virtual void SpawnKeyboard(Transform currentlySelected)
    {
        SpawnKeyboard();
    }

    public virtual void DespawnKeyboard()
    {
        if (keyboardActive)
        {
            keyboardActive = false;
            KeyboardPrefabRoot.gameObject.SetActive(keyboardActive);
        }
    }
}
