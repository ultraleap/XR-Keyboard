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
    public Dictionary<Transform, List<KeyboardKey> > keyMap;

    public Transform[] keyboardRows;
    
    public KeyMap()
    {
        keyMap = new Dictionary<Transform, List<KeyboardKey>>();
    }

    public virtual Dictionary<Transform, List<KeyboardKey> > GetKeyMap()
    {
        return keyMap;
    }


    public virtual void SetKeyRows(Transform[] rows)
    {
        keyboardRows = rows;
    }
}
