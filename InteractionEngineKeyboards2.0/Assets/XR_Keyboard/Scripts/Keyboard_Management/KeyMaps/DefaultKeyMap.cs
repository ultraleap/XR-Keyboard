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
        int i = 0;
        List<KeyboardKey> numberRow = new  List<KeyboardKey>();
        numberRow.Add(NewKey(i++, KeyCode.Keypad1, KeyCode.Keypad1, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad2, KeyCode.Keypad2, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad3, KeyCode.Keypad3, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad4, KeyCode.Keypad4, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad5, KeyCode.Keypad5, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad6, KeyCode.Keypad6, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad7, KeyCode.Keypad7, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad8, KeyCode.Keypad8, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad9, KeyCode.Keypad9, Vector2.one, 0));
        numberRow.Add(NewKey(i++, KeyCode.Keypad0, KeyCode.Keypad0, Vector2.one, 0));
    
        return numberRow;
    }

    private List<KeyboardKey> TopAlphaRow()
    {
        int i = 0;
        List<KeyboardKey> topAlphaRow = new  List<KeyboardKey>();
        topAlphaRow.Add(NewKey(i++, KeyCode.Q,          KeyCode.Keypad1,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.W,          KeyCode.Keypad2,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.E,          KeyCode.Keypad3,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.R,          KeyCode.Keypad4,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.T,          KeyCode.Keypad5,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.Y,          KeyCode.Keypad6,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.U,          KeyCode.Keypad7,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.I,          KeyCode.Keypad8,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.O,          KeyCode.Keypad9,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.P,          KeyCode.Keypad0,    Vector2.one, 0));
        topAlphaRow.Add(NewKey(i++, KeyCode.Backspace,  KeyCode.Backspace,  new Vector2(2,2), 0));
    
        return topAlphaRow;
    }

    private List<KeyboardKey> MiddleAlphaRow()
    {
        int i = 0;
        List<KeyboardKey> middleAlphaRow = new  List<KeyboardKey>();
        middleAlphaRow.Add(NewKey(i++, KeyCode.A,       KeyCode.At,         Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.S,       KeyCode.Hash,       Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.D,       KeyCode.Alpha1,     Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.F,       KeyCode.Underscore, Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.G,       KeyCode.Ampersand,  Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.H,       KeyCode.Minus,      Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.J,       KeyCode.Plus,       Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.K,       KeyCode.LeftParen,  Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.L,       KeyCode.RightParen, Vector2.one, 0));
        middleAlphaRow.Add(NewKey(i++, KeyCode.Return,  KeyCode.Return,     new Vector2(2,2), 0));
    
        return middleAlphaRow;
    }

    private List<KeyboardKey> BottomAlphaRow()
    {
        int i = 0;
        List<KeyboardKey> bottomAlphaRow = new List<KeyboardKey>();
        bottomAlphaRow.Add(NewKey(i++, KeyCode.LeftShift,   KeyCode.Backslash,      new Vector2(1.5f, 1), 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.Z,           KeyCode.Asterisk,       Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.X,           KeyCode.DoubleQuote,    Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.C,           KeyCode.Quote,          Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.V,           KeyCode.Colon,          Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.B,           KeyCode.Semicolon,      Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.N,           KeyCode.Exclaim,        Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.M,           KeyCode.Question,       Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.Comma,       KeyCode.Comma,          Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.Period,      KeyCode.Period,         Vector2.one, 0));
        bottomAlphaRow.Add(NewKey(i++, KeyCode.RightShift,  KeyCode.Slash,          new Vector2(1.5f, 1), 0));

        return bottomAlphaRow;
    }

    private List<KeyboardKey> SpaceRow()
    {
        int i = 0;
        List<KeyboardKey> spaceRow = new List<KeyboardKey>();
        spaceRow.Add(NewKey(i++, KeyCode.LeftAlt,   KeyCode.Alpha0, new Vector2(2,2), 0));
        spaceRow.Add(NewKey(i++, KeyCode.Space,     KeyCode.Space,  new Vector2(8,8), 0));
        spaceRow.Add(NewKey(i++, KeyCode.Return,    KeyCode.Return, new Vector2(2,2), 0));
    
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

    private KeyboardKey NewKey(int position, KeyCode neutral, KeyCode symbols, Vector2 scale, float padding)
    {
        return new KeyboardKey() {
            position = position,
            neutralKey = neutral,
            symbolsKey = symbols,
            keyScale = scale,
            keyPadding = padding
        };
    }
}
 