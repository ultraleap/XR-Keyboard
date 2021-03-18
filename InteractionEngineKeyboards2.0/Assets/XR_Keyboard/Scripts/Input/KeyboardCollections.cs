
using System.Collections.Generic;
using KeyCode = UnityEngine.KeyCode;

public static class KeyboardCollections
{

    //CREDIT: https://gist.github.com/b-cancel/c516990b8b304d47188a7fa8be9a1ad9#file-unity3d_character_to_keycode-cs

    //NOTE: This is only a DICTIONARY with MOST character to keycode bindings... it is NOT a working cs file
    //ITS USEFUL: when you are reading in your control scheme from a file

    //NOTE: some characters SHOULD map to multiple keycodes (but this is impossible)
    //since this is a dictionary, only 1 character is bound to 1 keycode
    //EX: * from the keyboard will be read the same as * from the keypad... because they produce the same character in a text file

    public static Dictionary<KeyCode, string> KeyCodeToString = new Dictionary<KeyCode, string>()
    {
      //-------------------------LOGICAL mappings-------------------------

      //Lower Case Letters
      {KeyCode.A, "a"},
      {KeyCode.B, "b"},
      {KeyCode.C, "c"},
      {KeyCode.D, "d"},
      {KeyCode.E, "e"},
      {KeyCode.F, "f"},
      {KeyCode.G, "g"},
      {KeyCode.H, "h"},
      {KeyCode.I, "i"},
      {KeyCode.J, "j"},
      {KeyCode.K, "k"},
      {KeyCode.L, "l"},
      {KeyCode.M, "m"},
      {KeyCode.N, "n"},
      {KeyCode.O, "o"},
      {KeyCode.P, "p"},
      {KeyCode.Q, "q"},
      {KeyCode.R, "r"},
      {KeyCode.S, "s"},
      {KeyCode.T, "t"},
      {KeyCode.U, "u"},
      {KeyCode.V, "v"},
      {KeyCode.W, "w"},
      {KeyCode.X, "x"},
      {KeyCode.Y, "y"},
      {KeyCode.Z, "z"},

      //KeyPad Numbers
      {KeyCode.Keypad1, "1"},
      {KeyCode.Keypad2, "2"},
      {KeyCode.Keypad3, "3"},
      {KeyCode.Keypad4, "4"},
      {KeyCode.Keypad5, "5"},
      {KeyCode.Keypad6, "6"},
      {KeyCode.Keypad7, "7"},
      {KeyCode.Keypad8, "8"},
      {KeyCode.Keypad9, "9"},
      {KeyCode.Keypad0, "0"},

      //Other Symbols
      {KeyCode.Exclaim,           "!"}, //1
      {KeyCode.DoubleQuote,       "\""},
      {KeyCode.Hash,              "#"}, //3
      {KeyCode.Dollar,            "$"}, //4
      {KeyCode.Ampersand,         "&"}, //7
      {KeyCode.Quote,             "\'"},
      {KeyCode.LeftParen,         "("}, //9
      {KeyCode.RightParen,        ")"}, //0
      {KeyCode.Asterisk,          "*"}, //8
      {KeyCode.Plus,              "+"},
      {KeyCode.Comma,             ","},
      {KeyCode.Minus,             "-"},
      {KeyCode.Period,            "."},
      {KeyCode.Slash,             "/"},
      {KeyCode.Colon,             ":"},
      {KeyCode.Semicolon,         ";"},
      {KeyCode.Less,              "<"},
      {KeyCode.Equals,            "="},
      {KeyCode.Greater,           ">"},
      {KeyCode.Question,          "?"},
      {KeyCode.At,                "@"}, //2
      {KeyCode.LeftBracket,       "["},
      {KeyCode.Backslash,         "\\"},
      {KeyCode.RightBracket,      "]"},
      {KeyCode.Caret,             "^"}, //6
      {KeyCode.Underscore,        "_"},
      {KeyCode.BackQuote,         "`"},
      {KeyCode.Backspace,         "\u0008"},
      {KeyCode.Space,             " "},
      {KeyCode.Return,            "\n"},
      {KeyCode.Tab,               "\t"},
      {KeyCode.KeypadPeriod,      "."},
      {KeyCode.KeypadDivide,      "/"},
      {KeyCode.KeypadMultiply,    "*"},
      {KeyCode.KeypadMinus,       "-"},
      {KeyCode.KeypadPlus,        "+"},
      {KeyCode.KeypadEquals,      "="},
      {KeyCode.LeftArrow,         "<"},
      {KeyCode.RightArrow,        ">"},
      {KeyCode.UpArrow,           "^"},
      {KeyCode.Tilde,             "~"},
      {KeyCode.Pipe,              "|"},
      {KeyCode.LeftCurlyBracket,  "{"},
      {KeyCode.RightCurlyBracket, "}"},
      {KeyCode.Percent,           "%"},

      //-------------------------NON-LOGICAL mappings-------------------------

      //NOTE: all of these can easily be remapped to something that perhaps you find more useful

      //---Mappings where the logical keycode was taken up by its counter part in either (the regular keybaord) or the (keypad)

      {KeyCode.Alpha0,        "ABC"},
      {KeyCode.Alpha1,        "£"},
      {KeyCode.LeftControl,   "=\\<"},
      {KeyCode.RightControl,  "=\\<"},
      {KeyCode.LeftShift,     "SHIFT"},
      {KeyCode.RightShift,    "SHIFT"},
      {KeyCode.LeftAlt,       "&123"},
      {KeyCode.RightAlt,      "&123"},
      {KeyCode.Alpha2, "W"},
      {KeyCode.Alpha3, "E"},
      {KeyCode.Alpha4, "R"},
      {KeyCode.Alpha5, "T"},
      {KeyCode.Alpha6, "Y"},
      {KeyCode.Alpha7, "U"},
      {KeyCode.Alpha8, "I"},
      {KeyCode.Alpha9, "O"},
      {KeyCode.None, ""},

      //INACTIVE since I am using these characters else where


      //-------------------------CHARACTER KEYS with NO KEYCODE-------------------------

      //NOTE: you can map these to any of the OPEN KEYCODES below

      /*
      //Upper Case Letters (16)
      {-,"H"},
      {-,"J"},
      {-,"K"},
      {-,"L"},
      {-,"M"},
      {-,"N"},
      {-,"S"},
      {-,"V"},
      {-,"X"},
      {-,"Z"}
      */

      //-------------------------KEYCODES with NO CHARACER KEY-------------------------

      //-----KeyCodes without Logical Mappings
      //-Anything above "KeyCode.Space" in Unity's Documentation (9 KeyCodes)
      //-Anything between "KeyCode.UpArrow" and "KeyCode.F15" in Unity's Documentation (24 KeyCodes)
      //-Anything Below "KeyCode.Numlock" in Unity's Documentation [(28 KeyCodes) + (9 * 20 = 180 JoyStickCodes) = 208 KeyCodes]

      //-------------------------other-------------------------

      //-----KeyCodes that are inaccesible for some reason

    };

    public static List<KeyCode> AlphabetKeyCodes = new List<KeyCode>()
    {
      KeyCode.A,
      KeyCode.B,
      KeyCode.C,
      KeyCode.D,
      KeyCode.E,
      KeyCode.F,
      KeyCode.G,
      KeyCode.H,
      KeyCode.I,
      KeyCode.J,
      KeyCode.K,
      KeyCode.L,
      KeyCode.M,
      KeyCode.N,
      KeyCode.O,
      KeyCode.P,
      KeyCode.Q,
      KeyCode.R,
      KeyCode.S,
      KeyCode.T,
      KeyCode.U,
      KeyCode.V,
      KeyCode.W,
      KeyCode.X,
      KeyCode.Y,
      KeyCode.Z,
    };

    public static List<KeyCode> NumericKeyCodes = new List<KeyCode>(){
      KeyCode.Keypad1,
      KeyCode.Keypad2,
      KeyCode.Keypad3,
      KeyCode.Keypad4,
      KeyCode.Keypad5,
      KeyCode.Keypad6,
      KeyCode.Keypad7,
      KeyCode.Keypad8,
      KeyCode.Keypad9,
      KeyCode.Keypad0
    };

    public static List<KeyCode> ModeShifters = new List<KeyCode>(){
      KeyCode.LeftShift,
      KeyCode.RightShift,
      KeyCode.LeftAlt,
      KeyCode.RightAlt,
      KeyCode.LeftControl,
      KeyCode.RightControl,
      KeyCode.Alpha0,
    };
  
    public static Dictionary<KeyCode, List<string>> CharacterToSpecialChars = new Dictionary<KeyCode, List<string>>(){
      {KeyCode.A, new List<string>(){"æ","ã","å","ā","à","á","â","ä"}},
      {KeyCode.E, new List<string>(){"ē","è","é","ê","ë"}},
      {KeyCode.I, new List<string>(){"ī","ì","í","î","ï"}},
      {KeyCode.N, new List<string>(){"ñ"}},
      {KeyCode.O, new List<string>(){"œ","õ","ø","о̄","ò","ó","ô","ö"}},
      {KeyCode.S, new List<string>(){"ß"}},
      {KeyCode.U, new List<string>(){"ū","ù","ú","û","ü"}},
    };

}
