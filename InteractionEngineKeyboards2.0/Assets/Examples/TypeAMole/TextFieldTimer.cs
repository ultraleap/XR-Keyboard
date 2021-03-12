using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class TextFieldTimer : MonoBehaviour
{
    [Header("Setup")]
    public TMP_InputField targetInputField;
    public TextMeshProUGUI timer;
    [HideInInspector] public string TestNotes;
    [HideInInspector] public string Sequence;
    private float quickTime;
    private float startTime = 0;
    private bool timing = false;
    private int buttonErrors = 0;
    private int totalClicks = 0;

    public void IncrementButtonErrors()
    {
        buttonErrors++;
    }

    public void AddCharacter(char _character)
    {

        CheckTimerStart();
        CheckTimerEnd();
    }

    public void Clear()
    {
        targetInputField.text = "";
        timer.text = "";
        startTime = 0;
        timing = false;
        buttonErrors = 0;
        totalClicks = 0;
    }

    public void CheckTimerStart()
    {
        if (timing == false && startTime == 0)
        {
            if (targetInputField.text == Sequence[0].ToString())
            {
                timing = true;
                startTime = Time.time;
            }
        }
    }

    public void CheckTimerEnd()
    {
        if (targetInputField.text.Contains(Sequence) && timing)
        {
            quickTime = Time.time - startTime;
            timer.text = $"Time: {quickTime.ToString("F2")}s, Button Errors: {buttonErrors}";
            timing = false;
            startTime = 0;
            SaveToCsv();
        }
    }

    public void RemoveLastChar()
    {
        if (targetInputField.text.Length > 0)
        {
            targetInputField.text = targetInputField.text.Substring(0, targetInputField.text.Length - 1);
        }
    }

    private void SaveToCsv()
    {
        string filePath = Application.persistentDataPath + @"\TextFieldTimerResults.csv";
        string delimiter = ",";
        string[] results = new string[]
        {
            DateTime.Now.ToString(), quickTime.ToString("F2"),
            buttonErrors.ToString(), totalClicks.ToString(), TestNotes
        };

        bool fileExists = File.Exists(filePath);
        StringBuilder sb = new StringBuilder();
        using (StreamWriter sw = File.AppendText(filePath))
        {
            if (!fileExists)
            {
                sw.WriteLine("Timestamp,Time Taken,Deadspace Errors,Button Errors,Total Clicks,TestNotes");
                fileExists = true;
            }
            sw.WriteLine(string.Join(delimiter, results));
        }
        //On windows, saves to C:\Users\<user>\AppData\LocalLow\Ultraleap\<ProjectName>\TextFieldTimerResults.csv
    }
}