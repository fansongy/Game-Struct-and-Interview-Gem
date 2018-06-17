using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class DrawCustLine : MonoBehaviour
{
	#region Public Attributes
	public Transform target;

	public float Height = 1;
	public float Length = 2;
	public float Offset = 0;
	public float TotalLength = 8;

	[Range(0.1f,1)]
	public float delat = 0.1f;
	#endregion

	#region Unity Messages
	void Update()
	{
		makeSin(target);
	}
	#endregion

	#region Public Methods
	public void makeSin(Transform tarTrans)
	{
		if(tarTrans== null)
		{
			return;
		}
		Debug.DrawLine(tarTrans.position, tarTrans.TransformPoint(Vector3.forward * TotalLength));

		Vector3 PerPos = tarTrans.position;
		for (float i = 0; i < TotalLength; i = i + delat)
		{
			float h = Mathf.Sin(i * Length + Offset) * Height;
			Vector3 localPos = new Vector3(0, h, i);
			Vector3 worldPos = tarTrans.TransformPoint(localPos);
			Debug.DrawLine(PerPos, worldPos, Color.green);
			PerPos = worldPos;
		}
	}
	#endregion
}