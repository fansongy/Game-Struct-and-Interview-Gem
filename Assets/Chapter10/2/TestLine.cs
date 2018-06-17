using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class LineParam
{
	public float Height = 1;
	public float Length = 2;
	public float Offset = 0;
	public float TotalLength = 8;
}

[ExecuteInEditMode]
public class TestLine : MonoBehaviour
{
	#region Public Attributes
	public Transform target;
	public LineParam lineParam1;

	public Transform target2;
	public LineParam lineParam2;

	[Range(0.1f, 1)]
	public float delat = 0.1f;
	#endregion

	#region Private Attributes

	#endregion

	#region Unity Messages

	void Update()
	{
		//makeSin(target);

		makeLine(target, lineParam1, (float x, LineParam param) =>
			{
				float h = Mathf.Sin(param.Length * x + param.Offset) * param.Height;
				Vector3 localPos = new Vector3(0, h, x);
				return localPos;
			});

		makeLine(target2, lineParam2, (float x, LineParam param) =>
			{
				if (((int)x) % 2 == 0)
				{
					Vector3 localPos = new Vector3(0, -param.Height, x + param.Offset);
					return localPos;
				}
				else
				{
					Vector3 localPos = new Vector3(0, param.Height, x + param.Offset);
					return localPos;

				}
			});
	}

	#endregion

	#region Public Methods

	public void makeLine(Transform tarTrans, LineParam param, Func<float, LineParam, Vector3> calcPos)
	{
		if (tarTrans == null)
		{
			return;
		}
		Debug.DrawLine(tarTrans.position, tarTrans.TransformPoint(Vector3.forward * param.TotalLength));

		Vector3 PerPos = tarTrans.position;
		for (float i = 0; i < param.TotalLength; i = i + delat)
		{
			Vector3 localPos = calcPos(i, param);
			Vector3 worldPos = tarTrans.TransformPoint(localPos);
			if (i > 0)
			{
				Debug.DrawLine(PerPos, worldPos, Color.green);
			}
			PerPos = worldPos;
		}
	}
	#endregion
}