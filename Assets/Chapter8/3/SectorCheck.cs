using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class SectorCheck : MonoBehaviour 
{
	#region Public Attributes
	public float m_radius;
	public float m_angle;
	public List<GameObject> m_list = new List<GameObject>();
	#endregion

	void Update()
	{
		for (int i = 0; i < m_list.Count; ++i)
		{
			if (checkInArea(m_list[i].transform.position,m_angle, m_radius))
			{
				m_list[i].SetActive(true);
			}
			else
			{
				m_list[i].SetActive(false);
			}
		}
	}

	bool checkInArea(Vector3 tarPos, float angle, float radius)
	{
		var cosAngle = Mathf.Cos(Mathf.Deg2Rad * angle * 0.5f);
		Vector3 circleCenter = transform.position;
		Vector3 disV = tarPos - circleCenter;
		float dis2 = disV.sqrMagnitude;
		if (dis2 < radius * radius)
		{
			disV.y = 0.0f;
			disV = disV.normalized;
			float cos = Vector3.Dot(transform.forward, disV);
			if (cos - cosAngle >= 1e-5 )
			{
				return true;
			}
		}
		return false;
	}

	[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
	static void DrawGiz(SectorCheck scr, GizmoType gizType)
	{
		Handles.color = new Color(1, 1, 1, 0.2f);
		var newStart = Quaternion.Euler(new Vector3(0, -scr.m_angle / 2, 0)) * scr.transform.forward;
		Handles.DrawSolidArc(scr.transform.position, scr.transform.up, newStart, scr.m_angle, scr.m_radius);
	}
}