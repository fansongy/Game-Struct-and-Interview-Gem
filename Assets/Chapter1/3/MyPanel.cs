using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyPanel : MonoBehaviour 
{
	
	void Awake()
	{
		var btns = GetComponentsInChildren<Button>();
		for(int i = 0;i<btns.Length;++i)
		{
			int index = i;
			btns[i].onClick.AddListener(()=>{
				OnBtnClick(index);
			});
		}
	}

	public void OnBtnClick(int index)
	{
		Debug.Log("btn click:"+index);
	}
}
