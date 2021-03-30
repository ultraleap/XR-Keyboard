using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationForm : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField mobileField;
    public TMP_InputField passwordField;
    public TMP_Text warningText;

    private Coroutine reset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);

        emailField.text = "";
        emailField.GetComponent<TMPInputFieldTextReceiver>().Clear();
        mobileField.text = "";
        mobileField.GetComponent<TMPInputFieldTextReceiver>().Clear();
        passwordField.text = "";
        passwordField.GetComponent<TMPInputFieldTextReceiver>().Clear();
        warningText.text = "";
    }

    public IEnumerator ClearWarning()
    {
        yield return new WaitForSeconds(2);
        warningText.text = "";
    }

    public void Confirm()
    {
        if (emailField.text == "" | mobileField.text == "" | passwordField.text == "")
        {
            warningText.text = "Give me more data!";
            StartCoroutine("Reset");
            return;
        }

        warningText.text = "Registered for identity theft, thanks!";

        if (reset != null) StopCoroutine(reset);
        reset = StartCoroutine("Reset");
    }

}

