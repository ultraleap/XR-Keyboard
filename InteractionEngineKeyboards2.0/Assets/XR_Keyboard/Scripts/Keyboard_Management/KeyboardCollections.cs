
using System.Collections.Generic;
using KeyCode = UnityEngine.KeyCode;

public static class KeyboardCollections
{
    public static Dictionary<string, string> NonCharIdentifierToStringChar = new Dictionary<string, string>() {
        { "space", " "},
        { "backspace", "\u0008"},
        { "return", "\n"},
        { "tab", "\t"},
    };

    public static Dictionary<string, string> NonStandardKeyToDisplayString = new Dictionary<string, string>(){
        { "space", " "},
        { "backspace", ""},
        { "return", ""},
        { "shift_neutral", ""},
        { "shift_shift", ""},
        { "shift_caps", ""},
        { "accentPanelDismiss", " "},
        { "switch_symbols", "#+="},
        { "switch_letters", "ABC"},

    };

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
      {KeyCode.Backspace,         "backspace"},
      {KeyCode.Space,             "space"},
      {KeyCode.Return,            "return"},
      {KeyCode.Tab,               "tab"},
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

      {KeyCode.LeftControl,   "switch_symbols"},
      {KeyCode.RightControl,  "switch_symbols"},
      {KeyCode.LeftShift,     "shift"},
      {KeyCode.RightShift,    "shift"},
      {KeyCode.LeftAlt,       "switch_symbols"},
      {KeyCode.RightAlt,      "switch_symbols"},
      {KeyCode.Alpha0,        "switch_letters"},
      {KeyCode.Alpha1,        "£"},
      {KeyCode.Alpha2,        "€"},
      {KeyCode.Alpha3,        "¥"},
      {KeyCode.Alpha4, ""},
      {KeyCode.Alpha5, ""},
      {KeyCode.Alpha6, ""},
      {KeyCode.Alpha7, ""},
      {KeyCode.Alpha8, ""},
      {KeyCode.Alpha9, ""},
      {KeyCode.None, ""},
      {KeyCode.Escape, "accentPanelDismiss"}


      //-------------------------KEYCODES with NO CHARACTER KEY-------------------------

      //-----KeyCodes without Logical Mappings
      //-Anything above "KeyCode.Space" in Unity's Documentation (9 KeyCodes)
      //-Anything between "KeyCode.UpArrow" and "KeyCode.F15" in Unity's Documentation (24 KeyCodes)
      //-Anything Below "KeyCode.Numlock" in Unity's Documentation [(28 KeyCodes) + (9 * 20 = 180 JoyStickCodes) = 208 KeyCodes]

      //-------------------------other-------------------------

      //-----KeyCodes that are inaccesible for some reason

    };



    public static List<string> AlphabetKeys = new List<string>()
    {
      "a",
      "b",
      "c",
      "d",
      "e",
      "f",
      "g",
      "h",
      "i",
      "J",
      "k",
      "l",
      "m",
      "n",
      "o",
      "p",
      "q",
      "r",
      "s",
      "t",
      "u",
      "v",
      "w",
      "x",
      "y",
      "z"
    };

    public static List<string> NumericKeys = new List<string>(){
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      "0"
    };

    public static List<string> ModeShifters = new List<string>(){
      "shift",
      "switch_symbols",
      "switch_letters",
    };

    public static Dictionary<string, List<string>> CharacterToAccentedChars = new Dictionary<string, List<string>>(){
      {
        "a", new List<string>()
        {
          "æ",
          "ã",
          "å",
          "ā",
          "à",
          "á",
          "â",
          "ä"
        }
      },
      {
        "c", new List<string>()
        {
          "ç",
        }
      },
      {
        "e", new List<string>()
        {
          "ē",
          "è",
          "é",
          "ê",
          "ë",
        }
      },
      {
        "i", new List<string>()
        {
          "ī",
          "ì",
          "í",
          "î",
          "ï"
        }
      },
      {
        "n", new List<string>()
        {
          "ñ"
        }
      },
      {
        "o", new List<string>()
        {
          "œ",
          "õ",
          "ø",
          "о̄",
          "ò",
          "ó",
          "ô",
          "ö"
        }
      },
      {
        "s", new List<string>()
        {
          "ß"
        }
      },
      {
        "u", new List<string>()
        {
          "ū",
          "ù",
          "ú",
          "û",
          "ü",
        }
      }
    };
}
