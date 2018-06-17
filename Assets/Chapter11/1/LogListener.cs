using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LogListener : MonoBehaviour 
{
    public Text logText;
    public Button btn;

	void Start () 
	{

        btn.onClick.AddListener(() =>
        {
            List<int> a = new List<int>();
            a[0].ToString();
        });

		Application.logMessageReceived += logMessageReceivedFunc;
	}

	void OnDestroy()
	{
		Application.logMessageReceived -= logMessageReceivedFunc;
	}

    public void logMessageReceivedFunc(string logValue, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            Time.timeScale = 0f; //pause the game
            logText.text +="<color=red>"+ logValue + "\n" + stackTrace+"</color>";
        }
        else
        {
            logText.text +=  logValue + "\n" + stackTrace;
        }
    }
}
