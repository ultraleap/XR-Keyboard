using UnityEngine;

public class KeyMapGenerator : MonoBehaviour
{
    public GameObject keyPrefab;
    public KeyMap keyboardMap;

    public Transform[] keyboardRows = new Transform[5];

    public GameObject shadowPrefab;

    public Transform[] shadowRows = new Transform[5];
    public UIKeyboardResizer keyboardResizer;
    public bool overWritePrefab = false;


    // Start is called before the first frame update
    void Awake()
    {
        // If the keyboard is empty of keys them generate a new one
        if (keyboardRows[0].GetComponentsInChildren<TextInputButton>().Length == 0)
        {
            RegenerateKeyboard();
        }
    }

    public void RegenerateKeyboard()
    {
        if (keyboardMap == null)
        {
            keyboardMap = GetComponent<KeyMap>();
            if (keyboardMap == null)
            {
                keyboardMap = gameObject.AddComponent<DefaultKeyMap>();
            }
        }

        if (!keyPrefab.transform.GetComponentInChildren<TextInputButton>())
        {
            throw new System.Exception("Ensure prefab contains an object with the TextInputButton component");
        }

        ClearKeys();
        ClearShadows();

        GenerateKeyboard();
    }

    public void GenerateKeyboard()
    {
        var keyMap = keyboardMap.GetKeyMap();
        for (int i = 0; i < keyboardRows.Length; i++)
        {
            foreach (var key in keyMap[i].row)
            {
                GameObject shadow = Instantiate(shadowPrefab, shadowRows[i]);
                GameObject newKey = Instantiate(keyPrefab, keyboardRows[i]);
                TextInputButton button = newKey.GetComponentInChildren<TextInputButton>();
                button.NeutralKey = key.neutralKey;
                button.Symbols1Key = key.symbols1Key;
                button.Symbols2Key = key.symbols2Key;
                button.UpdateActiveKey(button.NeutralKey, Keyboard.KeyboardMode.NEUTRAL);
                newKey.name = button.NeutralKey.ToString();
            }
        }
        keyboardResizer.ResizeKeyboard();
    }

    private void ClearKeys()
    {
        foreach (Transform row in keyboardRows)
        {
            for (int i = row.childCount - 1; i >= 0; i--)
            {
                if (row.GetChild(i).GetComponentInChildren<TextInputButton>())
                {
                    DestroyImmediate(row.GetChild(i).gameObject);
                }
            }
        }
    }

    private void ClearShadows()
    {
        foreach (Transform row in shadowRows)
        {
            for (int i = row.childCount - 1; i >= 0; i--)
            {
                if (row.GetChild(i).GetComponentInChildren<TextInputButton>())
                {
                    DestroyImmediate(row.GetChild(i).gameObject);
                }
            }
        }
    }


    public void SetNewKeyPrefab(GameObject newPrefab)
    {
        keyPrefab = newPrefab;

        if (Application.isPlaying)
        {
            RegenerateKeyboard();
        }
    }

    public void SetNewKeyMap(KeyMap newMap)
    {
        keyboardMap = newMap;

        if (Application.isPlaying)
        {
            RegenerateKeyboard();
        }
    }
}
