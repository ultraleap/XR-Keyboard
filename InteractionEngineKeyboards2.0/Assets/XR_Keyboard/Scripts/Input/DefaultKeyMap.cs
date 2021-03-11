using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultKeyMap : KeyMap
{
    public override Dictionary<Transform, List<KeyboardKey>> GetKeyMap()
    {
        if (keyMap.Count == 0)
        {
            InitialiseKeyboardMap();
        }
        return keyMap;
    }

    private void InitialiseKeyboardMap()
    {
        List<KeyboardKey>[] keyRows = new List<KeyboardKey>[] { NumberRow(), TopAlphaRow(), MiddleAlphaRow(), BottomAlphaRow(), SpaceRow() };
        for(int i = 0; i < 5; i++)
        {
            keyMap.Add(keyboardRows[i], keyRows[i]);
        }
    }

    private List<KeyboardKey> NumberRow()
    {
        int i = 0;
        List<KeyboardKey> numberRow = new  List<KeyboardKey>();
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad1, symbols1Key = KeyCode.Keypad1, symbols2Key = KeyCode.Keypad1});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad2, symbols1Key = KeyCode.Keypad2, symbols2Key = KeyCode.Keypad2});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad3, symbols1Key = KeyCode.Keypad3, symbols2Key = KeyCode.Keypad3});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad4, symbols1Key = KeyCode.Keypad4, symbols2Key = KeyCode.Keypad4});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad5, symbols1Key = KeyCode.Keypad5, symbols2Key = KeyCode.Keypad5});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad6, symbols1Key = KeyCode.Keypad6, symbols2Key = KeyCode.Keypad6});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad7, symbols1Key = KeyCode.Keypad7, symbols2Key = KeyCode.Keypad7});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad8, symbols1Key = KeyCode.Keypad8, symbols2Key = KeyCode.Keypad8});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad9, symbols1Key = KeyCode.Keypad9, symbols2Key = KeyCode.Keypad9});
        numberRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Keypad0, symbols1Key = KeyCode.Keypad0, symbols2Key = KeyCode.Keypad0});
    
        return numberRow;
    }

    private List<KeyboardKey> TopAlphaRow()
    {
        int i = 0;
        List<KeyboardKey> topAlphaRow = new  List<KeyboardKey>();
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Q, symbols1Key = KeyCode.Keypad1, symbols2Key = KeyCode.Tilde});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.W, symbols1Key = KeyCode.Keypad2, symbols2Key = KeyCode.BackQuote});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.E, symbols1Key = KeyCode.Keypad3, symbols2Key = KeyCode.Pipe});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.R, symbols1Key = KeyCode.Keypad4, symbols2Key = KeyCode.None});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.T, symbols1Key = KeyCode.Keypad5, symbols2Key = KeyCode.None});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Y, symbols1Key = KeyCode.Keypad6, symbols2Key = KeyCode.None});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.U, symbols1Key = KeyCode.Keypad7, symbols2Key = KeyCode.None});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.I, symbols1Key = KeyCode.Keypad8, symbols2Key = KeyCode.None});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.O, symbols1Key = KeyCode.Keypad9, symbols2Key = KeyCode.None});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.P, symbols1Key = KeyCode.Keypad0, symbols2Key = KeyCode.None});
        topAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Backspace, symbols1Key = KeyCode.Backspace, symbols2Key = KeyCode.Backspace});
    
        return topAlphaRow;
    }

    private List<KeyboardKey> MiddleAlphaRow()
    {
        int i = 0;
        List<KeyboardKey> middleAlphaRow = new  List<KeyboardKey>();
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.A, symbols1Key = KeyCode.At, symbols2Key = KeyCode.None});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.S, symbols1Key = KeyCode.Hash, symbols2Key = KeyCode.None});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.D, symbols1Key = KeyCode.Alpha1, symbols2Key = KeyCode.None});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.F, symbols1Key = KeyCode.Underscore, symbols2Key = KeyCode.None});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.G, symbols1Key = KeyCode.Ampersand, symbols2Key = KeyCode.UpArrow});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.H, symbols1Key = KeyCode.Minus, symbols2Key = KeyCode.None});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.J, symbols1Key = KeyCode.Plus, symbols2Key = KeyCode.Equals});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.K, symbols1Key = KeyCode.LeftParen, symbols2Key = KeyCode.LeftCurlyBracket});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.L, symbols1Key = KeyCode.RightParen, symbols2Key = KeyCode.RightCurlyBracket});
        middleAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Return, symbols1Key = KeyCode.Return, symbols2Key = KeyCode.Return});
    
        return middleAlphaRow;
    }

    private List<KeyboardKey> BottomAlphaRow()
    {
        int i = 0;
        List<KeyboardKey> bottomAlphaRow = new List<KeyboardKey>();
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.LeftShift, symbols1Key = KeyCode.LeftControl, symbols2Key = KeyCode.LeftAlt});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Z, symbols1Key = KeyCode.Asterisk, symbols2Key = KeyCode.Percent});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.X, symbols1Key = KeyCode.DoubleQuote, symbols2Key = KeyCode.None});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.C, symbols1Key = KeyCode.Quote, symbols2Key = KeyCode.None});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.V, symbols1Key = KeyCode.Colon, symbols2Key = KeyCode.None});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.B, symbols1Key = KeyCode.Semicolon, symbols2Key = KeyCode.None});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.N, symbols1Key = KeyCode.Exclaim, symbols2Key = KeyCode.LeftBracket});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.M, symbols1Key = KeyCode.Question, symbols2Key = KeyCode.RightBracket});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Comma, symbols1Key = KeyCode.Comma, symbols2Key = KeyCode.LeftArrow});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Period, symbols1Key = KeyCode.Period, symbols2Key = KeyCode.RightArrow});
        bottomAlphaRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.RightShift, symbols1Key = KeyCode.RightControl, symbols2Key = KeyCode.RightAlt});

        return bottomAlphaRow;
    }

    private List<KeyboardKey> SpaceRow()
    {
        int i = 0;
        List<KeyboardKey> spaceRow = new List<KeyboardKey>();
        spaceRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.LeftAlt, symbols1Key = KeyCode.Alpha0, symbols2Key = KeyCode.Alpha0});
        spaceRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.Space, symbols1Key = KeyCode.Space, symbols2Key = KeyCode.Space});
        spaceRow.Add(new KeyboardKey() {position = i++, neutralKey = KeyCode.RightAlt, symbols1Key = KeyCode.Alpha0, symbols2Key = KeyCode.Alpha0});
    
        return spaceRow;
    }
}
