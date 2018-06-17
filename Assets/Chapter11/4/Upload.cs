using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Upload : MonoBehaviour 
{
    public Text logText;
    public Button btn;

    private readonly string ErrorTag = "LogErrorTag";

    void Start()
    {
        string str = PlayerPrefs.GetString(ErrorTag);
        logText.text = str;
        if (PlayerPrefs.HasKey(ErrorTag))
        {
            PlayerPrefs.DeleteKey(ErrorTag);
        }

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
            string text = "<color=red>" + logValue + "\n" + stackTrace + "</color>";
            PlayerPrefs.SetString(ErrorTag, text);
        }
    }

}
