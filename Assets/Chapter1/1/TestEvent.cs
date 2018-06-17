using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestEvent : MonoBehaviour 
{
	#region Public Attributes
	public Button m_btn;
	public Toggle m_toggle;
	public Slider m_slider;
	#endregion

	#region Private Attributes

	#endregion

	#region Unity Messages
	void Start() 
	{
//		m_btn.onClick.AddListener(OnBtnClick);
//		m_toggle.onValueChanged.AddListener(OnToggleChange);
//		m_slider.onValueChanged.AddListener(OnSliderChange);
	}
//	
//	void Update() 
//	{
//	
//	}
//
//	void OnDisable()
//	{
//
//	}
//
//	void OnDestroy()
//	{
//
//	}

	#endregion

	#region Public Methods
	public void OnBtnClick()
	{
			Debug.Log("Btn Click");
	}
	public void OnToggleChange(bool isOn)
	{
			Debug.Log("Toggle Is "+isOn);
	}
	public void OnSliderChange(float rate)
	{
			Debug.Log("Slider cur is "+rate);
	}
	#endregion

	#region Override Methods

	#endregion

	#region Private Methods

	#endregion

	#region Inner

	#endregion
}
