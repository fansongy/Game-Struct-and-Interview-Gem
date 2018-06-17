using UnityEngine;
using System.Collections;

public class RayPick : MonoBehaviour 
{
	public Vector3 m_normal = Vector3.up;
	public float m_d = 1;

	void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				//Debug.Log("Pick " + hit.collider.gameObject.name);
				if (Vector3.Dot(ray.direction.normalized,m_normal) == 0)
				{
					return;
				}
				hit.collider.transform.position = calculatePos(Camera.main.transform.position,ray.direction.normalized,m_normal,m_d);
			}
		}
	}

	Vector3 calculatePos(Vector3 p0,Vector3 direction,Vector3 normal,float d)
	{
		float t = (d - Vector3.Dot(p0, normal)) / Vector3.Dot(direction , normal);
		return p0 + t * direction;
	}
}