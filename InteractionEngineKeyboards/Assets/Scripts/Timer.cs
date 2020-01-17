using System.Diagnostics;
using Leap.Unity.Interaction;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField]
    TMPro.TextMeshPro _textMeshOutput;
    TextInputReceiver _inputReceiver;
    [SerializeField]
    string targetString;
    [SerializeField]
    InteractionButton _resetButton;
    Stopwatch stopwatch;


    // Start is called before the first frame update
    void Awake()
    {
        stopwatch = new Stopwatch();
        targetString = targetString.ToLower();
        _resetButton.OnPress += ResetTimer;
        _inputReceiver = FindObjectOfType<TextInputReceiver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatch.IsRunning == false && _inputReceiver.text.Length == 1)
        {
            stopwatch.Start();
        }

        if(_inputReceiver.text.ToLower() == targetString)
        {
            stopwatch.Stop();
        }
        _textMeshOutput.text = stopwatch.Elapsed.ToString();
    }

    public void ResetTimer()
    {
        stopwatch.Reset();
        _inputReceiver.text = string.Empty;
    }
}
