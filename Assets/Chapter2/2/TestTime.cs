using UnityEngine;
using System.Collections;

public class TestTime : MonoBehaviour 
{
	
	void Update() 
	{
		Debug.Log(" TimeScale:"+Time.timeScale+"\n"+
			"DeltaTime"+Time.deltaTime+"\n"+
			"Unscaled DeltaTime"+Time.unscaledDeltaTime+"\n"+
			"Smooth DeltaTime"+Time.smoothDeltaTime);

	}

	void OnGUI()
	{
		GUILayout.Label("TIme Scale:"+Time.timeScale);
		Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale,0,2,GUILayout.Width(200));
		GUILayout.Space(20);

		GUILayout.Label("RealTime: "+Time.realtimeSinceStartup);
		GUILayout.Label("GameTime: "+Time.time);
		GUILayout.Space(20);

	}
}
