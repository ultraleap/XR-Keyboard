using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMap : MonoBehaviour
{
    public struct KeyboardKey
    {
        public int position;
        public KeyCode neutralKey;
        public KeyCode symbols1Key;
        public KeyCode symbols2Key;
    } 

    /// <Summary>
    /// Each dictionary entry contains a reference to the transform 
    /// for a row of keys and a list of keys to be present in the row
    ///</Summary>
    public List<List<KeyboardKey>> keyMap;
    
    public KeyMap()
    {
        keyMap = new List<List<KeyboardKey>>();
    }

    public virtual List<List<KeyboardKey>> GetKeyMap()
    {
        return keyMap;
    }
}
