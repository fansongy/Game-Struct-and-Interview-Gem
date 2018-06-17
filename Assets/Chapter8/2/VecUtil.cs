using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class Vec
{
	public Vector3 offset = Vector3.zero;
	public Vector3 vec = Vector3.right;
	public Color lineColor = Color.cyan;
}

[ExecuteInEditMode]
public class VecUtil : MonoBehaviour 
{
	public List<Vec> vecList = new List<Vec>();

	void Update()
	{
		for (int i = 0; i < vecList.Count; ++i)
		{
			ForDebug(transform.position+vecList[i].offset,vecList[i].vec,vecList[i].lineColor);
		}
	}

	public void ForDebug(Vector3 pos, Vector3 direction,Color lineColor)
	{
		float arrowHeadLength = 0.25f;
		float arrowHeadAngle = 20.0f;
		Debug.DrawRay(pos, direction, lineColor);

		Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
		Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
		Debug.DrawRay(pos + direction, right * arrowHeadLength, lineColor);
		Debug.DrawRay(pos + direction, left * arrowHeadLength, lineColor);
	}

	void Start()
	{
		//计算叉乘值
		Vector3 a = new Vector3(1,0.5f,1);
		Vector3 b = new Vector3(0,0,1);
		Debug.Log(Vector3.Cross(b,a));
	}
}