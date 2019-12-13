using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCorrectWord : MonoBehaviour
{
	private AutocompleteWordPicker wordPicker;
    bool enter = false;

	void Start()
	{
		wordPicker = gameObject.GetComponentInParent<AutocompleteWordPicker>();
	}

	public void WordChosen()
	{
		wordPicker.ReplaceWord(gameObject.GetComponentInChildren<Text>().text);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!enter)
        {

        WordChosen();
            enter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        enter = false;
    }
}