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
        numberRow.Add(NewKey("1", 1));
        numberRow.Add(NewKey("2", 1));
        numberRow.Add(NewKey("3", 1));
        numberRow.Add(NewKey("4", 1));
        numberRow.Add(NewKey("5", 1));
        numberRow.Add(NewKey("6", 1));
        numberRow.Add(NewKey("7", 1));
        numberRow.Add(NewKey("8", 1));
        numberRow.Add(NewKey("9", 1));
        numberRow.Add(NewKey("0", 1));
    
        return numberRow;
    }

    private List<KeyboardKey> TopAlphaRow()
    {
        List<KeyboardKey> topAlphaRow = new  List<KeyboardKey>();
        topAlphaRow.Add(NewKey("q",          1));
        topAlphaRow.Add(NewKey("w",          1));
        topAlphaRow.Add(NewKey("e",          1));
        topAlphaRow.Add(NewKey("r",          1));
        topAlphaRow.Add(NewKey("t",          1));
        topAlphaRow.Add(NewKey("y",          1));
        topAlphaRow.Add(NewKey("u",          1));
        topAlphaRow.Add(NewKey("i",          1));
        topAlphaRow.Add(NewKey("o",          1));
        topAlphaRow.Add(NewKey("p",          1));
        topAlphaRow.Add(NewKey("backspace",  2));
    
        return topAlphaRow;
    }

    private List<KeyboardKey> MiddleAlphaRow()
    {
        List<KeyboardKey> middleAlphaRow = new  List<KeyboardKey>();
        middleAlphaRow.Add(NewKey("a",       1));
        middleAlphaRow.Add(NewKey("s",       1));
        middleAlphaRow.Add(NewKey("d",       1));
        middleAlphaRow.Add(NewKey("f",       1));
        middleAlphaRow.Add(NewKey("g",       1));
        middleAlphaRow.Add(NewKey("h",       1));
        middleAlphaRow.Add(NewKey("j",       1));
        middleAlphaRow.Add(NewKey("k",       1));
        middleAlphaRow.Add(NewKey("l",       1));
        middleAlphaRow.Add(NewKey("return",  2));
    
        return middleAlphaRow;
    }

    private List<KeyboardKey> BottomAlphaRow()
    {
        List<KeyboardKey> bottomAlphaRow = new List<KeyboardKey>();
        bottomAlphaRow.Add(NewKey("shift",   1.5f));
        bottomAlphaRow.Add(NewKey("z",           1));
        bottomAlphaRow.Add(NewKey("x",           1));
        bottomAlphaRow.Add(NewKey("c",           1));
        bottomAlphaRow.Add(NewKey("v",           1));
        bottomAlphaRow.Add(NewKey("b",           1));
        bottomAlphaRow.Add(NewKey("n",           1));
        bottomAlphaRow.Add(NewKey("m",           1));
        bottomAlphaRow.Add(NewKey(",",       1));
        bottomAlphaRow.Add(NewKey(".",      1));
        bottomAlphaRow.Add(NewKey("shift",  1.5f));

        return bottomAlphaRow;
    }

    private List<KeyboardKey> SpaceRow()
    {
        List<KeyboardKey> spaceRow = new List<KeyboardKey>();
        spaceRow.Add(NewKey("switch_symbols", 2));
        spaceRow.Add(NewKey("space",   8));
        spaceRow.Add(NewKey("return",  2));
    
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

    private KeyboardKey NewKey(string neutral, float keyWidthScale)
    {
        return new KeyboardKey() {
            keyCode = neutral,
            widthScale = keyWidthScale
        };
    }
}
 