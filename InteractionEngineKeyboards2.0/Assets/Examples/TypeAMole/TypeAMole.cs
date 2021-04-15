using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Leap.Unity.Interaction;
using static SimpleInteractionGlowImage;

public class TypeAMole : MonoBehaviour
{

    /*
        Guides the user through typing a sequence of characters
        Assumes buttons gameobject names are the same as their label - e.g. a keyboard
    */

    [Header("Setup")]
    public TextFieldTimer textFieldTimer;
    public GameObject keyboardParent;

    [Header("Sequence")]
    [OnValueChanged("SetSequence")] public string nonRandomSequence = "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG";
    [OnValueChanged("SetSequence")] public bool randomiseSequence = false;
    [OnValueChanged("SetSequence")] public int randomSequenceLength = 15;

    [Header("Active")]
    public InteractionBehaviourColours activeColors;
    public Color ActiveTextColour;
    [Header("Default")]
    public InteractionBehaviourColours defaultColors;
    public Color DefaultTextColour;

    [Header("Audio")]
    public AudioSource successBeep;
    public AudioSource failureBeep;

    private Dictionary<string, InteractionButton> buttonDictionary;
    private int sequenceIndex = -1;
    private string activeButton;
    private bool finished = false;
    private string sequence;
    private List<InteractionButton> allButtons;

    // Start is called before the first frame update
    void Start()
    {
        buttonDictionary = new Dictionary<string, InteractionButton>();

        allButtons = keyboardParent.GetComponentsInChildren<InteractionButton>(false).ToList();

        foreach (InteractionButton button in allButtons)
        {
            string key = button.GetComponent<TextInputButton>().Key;
            button.GetComponentInChildren<TextMeshProUGUI>().color = DefaultTextColour;
            button.GetComponent<SimpleInteractionGlowImage>().colors = defaultColors;

            if (KeyboardCollections.AlphabetKeys.Contains(key) || KeyboardCollections.NumericKeys.Contains(key) || key == "space")
            {
                if (KeyboardCollections.NonStandardKeyToDisplayString.TryGetValue(key, out string newKey))
                {
                    key = newKey;
                }

                buttonDictionary.Add(key, button);
                buttonDictionary[key].OnPress += () => OnPressButton(key);
                buttonDictionary[key].OnUnpress += () => OnUnpressButton(key);
            }
        }

        SetSequence();
        activeButton = sequence[0].ToString();

        HighlightNextButton();
    }

    private void OnPressButton(string key)
    {
        if (finished)
        {
            return;
        }
        if (KeyboardCollections.NonStandardKeyToDisplayString.TryGetValue(key, out string newKey))
        {
            key = newKey;
        }

        if (key == activeButton)
        {
            if (successBeep != null) successBeep.Play();
        }
        else
        {
            if (failureBeep != null) failureBeep.Play();
        }
    }

    public void OnUnpressButton(string key)
    {
        if (finished)
        {
            return;
        }

        if (KeyboardCollections.NonStandardKeyToDisplayString.TryGetValue(key, out string newKey))
        {
            key = newKey;
        }

        if (key == activeButton)
        {
            HighlightNextButton();
        }
        else
        {
            textFieldTimer.IncrementButtonErrors();
            textFieldTimer.RemoveLastChar();
        }
    }

    public void HighlightNextButton()
    {
        if (sequenceIndex + 1 == sequence.Length)
        {
            textFieldTimer.AddCharacter(sequence[sequenceIndex]);

            Finished();
            return;
        }

        if (sequenceIndex >= 0)
        {
            textFieldTimer.AddCharacter(sequence[sequenceIndex]);
        }
        sequenceIndex++;


        string nextButton = sequence[sequenceIndex].ToString();

        buttonDictionary[activeButton].GetComponent<SimpleInteractionGlowImage>().colors = defaultColors;
        buttonDictionary[activeButton].GetComponentInChildren<TextMeshProUGUI>().color = DefaultTextColour;

        activeButton = nextButton;
        buttonDictionary[activeButton].GetComponent<SimpleInteractionGlowImage>().colors = activeColors;
        buttonDictionary[activeButton].GetComponentInChildren<TextMeshProUGUI>().color = ActiveTextColour;
    }

    private void Finished()
    {
        finished = true;
        foreach (InteractionButton button in allButtons)
        {
            button.GetComponent<SimpleInteractionGlowImage>().colors = activeColors;
            button.GetComponentInChildren<TextMeshProUGUI>().color = ActiveTextColour;
        }
    }

    public void Restart()
    {
        foreach (InteractionButton button in allButtons)
        {
            button.GetComponent<SimpleInteractionGlowImage>().colors = defaultColors;
            button.GetComponentInChildren<TextMeshProUGUI>().color = DefaultTextColour;
        }
        textFieldTimer.Clear();
        finished = false;
        sequenceIndex = -1;
        SetSequence();
        HighlightNextButton();
    }

    private void SetSequence()
    {
        if (randomiseSequence)
        {
            if (buttonDictionary == null)
            {
                return;
            }
            sequence = "";
            for (int i = 0; i < randomSequenceLength; i++)
            {
                int randomButton = Random.Range(0, buttonDictionary.Count - 1);
                string key = buttonDictionary.ElementAt(randomButton).Key;
                sequence += key;
            }
        }
        else
        {
            sequence = nonRandomSequence;
        }
        sequence = sequence.ToLower();
        textFieldTimer.Sequence = sequence;
    }
}