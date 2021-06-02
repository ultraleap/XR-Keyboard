using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMap : MonoBehaviour
{
    [System.Serializable]
    public struct KeyboardKey
    {
        public string keyCode;
        public float widthScale;
    } 

    [System.Serializable]
    public struct KeyRow
    {
        public List<KeyboardKey> row;
    }

    public string description;
        
    /// <Summary>
    /// Each dictionary entry contains a reference to the transform 
    /// for a row of keys and a list of keys to be present in the row
    ///</Summary>
    public List<KeyRow> keyMap;

    public KeyMap()
    {
        keyMap = new List<KeyRow>();
    }

    public virtual List<KeyRow> GetKeyMap()
    {
        return keyMap;
    }

    public bool ValidateKeyMap()
    {
        if (keyMap.Count != 5)
        {
            throw new System.Exception("Keymap is invalid. Ensure it contains 5 rows, each containing a list of KeyboardKeys");
        }

        return true;
    }
}
