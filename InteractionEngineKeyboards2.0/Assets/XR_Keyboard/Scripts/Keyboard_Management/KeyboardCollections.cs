
using System.Collections.Generic;
using KeyCode = UnityEngine.KeyCode;

public static class KeyboardCollections
{



    public static Dictionary<KeyCode, string> NonAlphaNumericNameToCharString = new Dictionary<KeyCode, string>(){

      {KeyCode.Backspace,         "\u0008"},
      {KeyCode.Return,            "\n"},
      {KeyCode.Tab,               "\t"},
      {KeyCode.Percent,           "%"},
      {KeyCode.LeftControl,   "=\\<"},
      {KeyCode.RightControl,  "=\\<"},
      {KeyCode.LeftShift,     "SHIFT"},
      {KeyCode.RightShift,    "SHIFT"},
      {KeyCode.LeftAlt,       "#+="},
      {KeyCode.RightAlt,      "#+="},

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

      {KeyCode.LeftControl,   "=\\<"},
      {KeyCode.RightControl,  "=\\<"},
      {KeyCode.LeftShift,     "SHIFT"},
      {KeyCode.RightShift,    "SHIFT"},
      {KeyCode.LeftAlt,       "#+="},
      {KeyCode.RightAlt,      "#+="},
      {KeyCode.Alpha0,        "ABC"},
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


      //-------------------------KEYCODES with NO CHARACTER KEY-------------------------

      //-----KeyCodes without Logical Mappings
      //-Anything above "KeyCode.Space" in Unity's Documentation (9 KeyCodes)
      //-Anything between "KeyCode.UpArrow" and "KeyCode.F15" in Unity's Documentation (24 KeyCodes)
      //-Anything Below "KeyCode.Numlock" in Unity's Documentation [(28 KeyCodes) + (9 * 20 = 180 JoyStickCodes) = 208 KeyCodes]

      //-------------------------other-------------------------

      //-----KeyCodes that are inaccesible for some reason

    };

    public static Dictionary<KeyCodeSpecialChar, string> KeyCodeSpecialCharToString = new Dictionary<KeyCodeSpecialChar, string>()
    {

      {KeyCodeSpecialChar.AE_LIGATURE, "æ"},
      {KeyCodeSpecialChar.A_TILDE, "ã"},
      {KeyCodeSpecialChar.A_NORDIC_RING, "å"},
      {KeyCodeSpecialChar.A_MACRON, "ā"},
      {KeyCodeSpecialChar.A_GRAVE, "à"},
      {KeyCodeSpecialChar.A_ACUTE, "á"},
      {KeyCodeSpecialChar.A_CIRCUMFLEX, "â"},
      {KeyCodeSpecialChar.A_UMLAUT, "ä"},
      {KeyCodeSpecialChar.C_CEDILLE, "ç"},
      {KeyCodeSpecialChar.E_MACRON, "ē"},
      {KeyCodeSpecialChar.E_GRAVE, "è"},
      {KeyCodeSpecialChar.E_ACUTE, "é"},
      {KeyCodeSpecialChar.E_CIRCUMFLEX, "ê"},
      {KeyCodeSpecialChar.E_UMLAUT, "ë"},
      {KeyCodeSpecialChar.I_MACRON, "ī"},
      {KeyCodeSpecialChar.I_GRAVE, "ì"},
      {KeyCodeSpecialChar.I_ACUTE, "í"},
      {KeyCodeSpecialChar.I_CIRCUMFLEX, "î"},
      {KeyCodeSpecialChar.I_UMLAUT, "ï"},
      {KeyCodeSpecialChar.NONE, ""},
      {KeyCodeSpecialChar.N_TILDE, "ñ"},
      {KeyCodeSpecialChar.OE_LIGATURE, "œ"},
      {KeyCodeSpecialChar.O_TILDE, "õ"},
      {KeyCodeSpecialChar.O_GRAVE, "ø"},
      {KeyCodeSpecialChar.O_ACUTE, "о̄"},
      {KeyCodeSpecialChar.O_CIRCUMFLEX, "ò"},
      {KeyCodeSpecialChar.O_NORDIC_SLASH, "ó"},
      {KeyCodeSpecialChar.O_MACRON, "ô"},
      {KeyCodeSpecialChar.O_UMLAUT, "ö"},
      {KeyCodeSpecialChar.ESZETT, "ß"},
      {KeyCodeSpecialChar.U_MACRON, "ū"},
      {KeyCodeSpecialChar.U_GRAVE, "ù"},
      {KeyCodeSpecialChar.U_ACUTE, "ú"},
      {KeyCodeSpecialChar.U_CIRCUMFLEX, "û"},
      {KeyCodeSpecialChar.U_UMLAUT, "ü"},
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

public enum KeyCodeSpecialChar
{
    AE_LIGATURE,
    A_TILDE,
    A_GRAVE,
    A_ACUTE,
    A_CIRCUMFLEX,
    A_NORDIC_RING,
    A_MACRON,
    A_UMLAUT,
    C_CEDILLE,
    E_MACRON,
    E_GRAVE,
    E_ACUTE,
    E_CIRCUMFLEX,
    E_UMLAUT,
    I_MACRON,
    I_GRAVE,
    I_ACUTE,
    I_CIRCUMFLEX,
    I_UMLAUT,
    N_TILDE,
    NONE,
    OE_LIGATURE,
    O_TILDE,
    O_GRAVE,
    O_ACUTE,
    O_CIRCUMFLEX,
    O_NORDIC_SLASH,
    O_MACRON,
    O_UMLAUT,
    ESZETT,
    U_MACRON,
    U_GRAVE,
    U_ACUTE,
    U_CIRCUMFLEX,
    U_UMLAUT
}