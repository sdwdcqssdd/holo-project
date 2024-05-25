
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;
using System.Text;
using TMPro;

public class DictionManager : MonoBehaviour
{

    [Tooltip("A text area for the recognizer to display the recognized strings.")]
    public TextMeshProUGUI DictationDisplay;
    private DictationRecognizer dictationRecognizer;
    public ConnectionManager connectionManager;

    // Use this for initialization
    void Start()
    {
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;
        dictationRecognizer.InitialSilenceTimeoutSeconds = 3f;
        dictationRecognizer.AutoSilenceTimeoutSeconds = 2f;

        dictationRecognizer.Start();
    }

    private void DictationRecognizer_DictationError(string error, int hresult)
    {
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        if (cause == DictationCompletionCause.TimeoutExceeded)
        {
            if (DictationDisplay.text != "")
            { 
                connectionManager.sendData(DictationDisplay.text);
                DictationDisplay.text = "";
                connectionManager.recieveData();
                dictationRecognizer.Stop();
                dictationRecognizer.Start();
            }
        }
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        DictationDisplay.text += text;
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
    }

    void Update()
    {

    }

    void OnDestroy()
    {
        dictationRecognizer.Stop();
        dictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete -= DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError -= DictationRecognizer_DictationError;
        dictationRecognizer.Dispose();
    }
}
