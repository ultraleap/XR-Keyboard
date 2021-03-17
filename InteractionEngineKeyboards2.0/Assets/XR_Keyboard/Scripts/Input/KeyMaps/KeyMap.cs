using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMap : MonoBehaviour
{
    [System.Serializable]
    public struct KeyboardKey
    {
        public int position;
        public KeyCode neutralKey;
        public KeyCode symbols1Key;
        public KeyCode symbols2Key;
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
}
