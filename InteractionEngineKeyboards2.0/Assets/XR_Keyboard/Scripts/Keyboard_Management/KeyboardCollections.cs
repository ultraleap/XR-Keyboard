using System.Collections.Generic;

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
        { "backspace", "\uf55a"},
        { "return", "\uf3be"},
        { "shift_neutral", "\uf0d8"},
        { "shift_shift", "\uf0d8"},
        { "shift_caps", "\uf151"},
        { "accentPanelDismiss", " "},
        { "switch_symbols", "#+="},
        { "switch_letters", "ABC"},

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
      "j",
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
