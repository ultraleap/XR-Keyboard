using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;

public class DefaultKeyMap : KeyMap
{
    public void OnValidate()
    {
        description = "Default QWERTY";
        InitialiseKeyboardMap();
    }

    public override List<KeyRow> GetKeyMap()
    {
        if (keyMap.Count == 0)
        {
            InitialiseKeyboardMap();
        }
        return keyMap;
    }

    public void InitialiseKeyboardMap()
    {
        List<KeyboardKey>[] keyRows = new List<KeyboardKey>[] { NumberRow(), TopAlphaRow(), MiddleAlphaRow(), BottomAlphaRow(), SpaceRow() };
        keyMap = new List<KeyRow>();
        for(int i = 0; i < keyRows.Length; i++)
        {
            keyMap.Add(new KeyRow { row = keyRows[i]} );
        }
        ValidateKeyMap();
    }

    private List<KeyboardKey> NumberRow()
    {
        List<KeyboardKey> numberRow = new  List<KeyboardKey>();
        numberRow.Add(NewKey(KeyCode.Keypad1, 1));
        numberRow.Add(NewKey(KeyCode.Keypad2, 1));
        numberRow.Add(NewKey(KeyCode.Keypad3, 1));
        numberRow.Add(NewKey(KeyCode.Keypad4, 1));
        numberRow.Add(NewKey(KeyCode.Keypad5, 1));
        numberRow.Add(NewKey(KeyCode.Keypad6, 1));
        numberRow.Add(NewKey(KeyCode.Keypad7, 1));
        numberRow.Add(NewKey(KeyCode.Keypad8, 1));
        numberRow.Add(NewKey(KeyCode.Keypad9, 1));
        numberRow.Add(NewKey(KeyCode.Keypad0, 1));
    
        return numberRow;
    }

    private List<KeyboardKey> TopAlphaRow()
    {
        List<KeyboardKey> topAlphaRow = new  List<KeyboardKey>();
        topAlphaRow.Add(NewKey(KeyCode.Q,          1));
        topAlphaRow.Add(NewKey(KeyCode.W,          1));
        topAlphaRow.Add(NewKey(KeyCode.E,          1));
        topAlphaRow.Add(NewKey(KeyCode.R,          1));
        topAlphaRow.Add(NewKey(KeyCode.T,          1));
        topAlphaRow.Add(NewKey(KeyCode.Y,          1));
        topAlphaRow.Add(NewKey(KeyCode.U,          1));
        topAlphaRow.Add(NewKey(KeyCode.I,          1));
        topAlphaRow.Add(NewKey(KeyCode.O,          1));
        topAlphaRow.Add(NewKey(KeyCode.P,          1));
        topAlphaRow.Add(NewKey(KeyCode.Backspace,  2));
    
        return topAlphaRow;
    }

    private List<KeyboardKey> MiddleAlphaRow()
    {
        List<KeyboardKey> middleAlphaRow = new  List<KeyboardKey>();
        middleAlphaRow.Add(NewKey(KeyCode.A,       1));
        middleAlphaRow.Add(NewKey(KeyCode.S,       1));
        middleAlphaRow.Add(NewKey(KeyCode.D,       1));
        middleAlphaRow.Add(NewKey(KeyCode.F,       1));
        middleAlphaRow.Add(NewKey(KeyCode.G,       1));
        middleAlphaRow.Add(NewKey(KeyCode.H,       1));
        middleAlphaRow.Add(NewKey(KeyCode.J,       1));
        middleAlphaRow.Add(NewKey(KeyCode.K,       1));
        middleAlphaRow.Add(NewKey(KeyCode.L,       1));
        middleAlphaRow.Add(NewKey(KeyCode.Return,  2));
    
        return middleAlphaRow;
    }

    private List<KeyboardKey> BottomAlphaRow()
    {
        List<KeyboardKey> bottomAlphaRow = new List<KeyboardKey>();
        bottomAlphaRow.Add(NewKey(KeyCode.LeftShift,   1.5f));
        bottomAlphaRow.Add(NewKey(KeyCode.Z,           1));
        bottomAlphaRow.Add(NewKey(KeyCode.X,           1));
        bottomAlphaRow.Add(NewKey(KeyCode.C,           1));
        bottomAlphaRow.Add(NewKey(KeyCode.V,           1));
        bottomAlphaRow.Add(NewKey(KeyCode.B,           1));
        bottomAlphaRow.Add(NewKey(KeyCode.N,           1));
        bottomAlphaRow.Add(NewKey(KeyCode.M,           1));
        bottomAlphaRow.Add(NewKey(KeyCode.Comma,       1));
        bottomAlphaRow.Add(NewKey(KeyCode.Period,      1));
        bottomAlphaRow.Add(NewKey(KeyCode.RightShift,  1.5f));

        return bottomAlphaRow;
    }

    private List<KeyboardKey> SpaceRow()
    {
        List<KeyboardKey> spaceRow = new List<KeyboardKey>();
        spaceRow.Add(NewKey(KeyCode.LeftAlt, 2));
        spaceRow.Add(NewKey(KeyCode.Space,   8));
        spaceRow.Add(NewKey(KeyCode.Return,  2));
    
        return spaceRow;
    }

    [Button]
    public void WriteToJSON()
    {
        if (keyMap.Count == 0 || keyMap[0].row.Count == 0) 
        {
            InitialiseKeyboardMap();
        }
        
        string jsonMap = JsonUtility.ToJson(this, true);
        
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath + "/XR_Keyboard/KeyMaps", description + ".json"), jsonMap);
        Debug.Log(jsonMap.ToString());
    }

    private KeyboardKey NewKey(KeyCode neutral, float keyWidthScale)
    {
        return new KeyboardKey() {
            keyCode = neutral,
            widthScale = keyWidthScale
        };
    }
}
 