using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
[RequireComponent( typeof( MeshRenderer ) )]
[RequireComponent( typeof( MeshFilter ) )]
public class RangeShower : MonoBehaviour
{
	public enum ShapeType
	{
		none,
		sector,
		obb,
		ring,
	}
	#region Public Attributes
	public ShapeType m_type = ShapeType.none;
	public float m_degree = 120;
	public float m_intervalDegree = 10;
	//	public float beginOffsetDegree = 0;
	public float m_radius = 5;
	public Material m_circleIndicator;
	public Material m_RectIndicator;
	public float m_InnerOff= 2;
	#endregion

	#region Private Attributes
	Mesh mesh = null;
	MeshFilter meshFilter = null;
	MeshRenderer meshRender = null;

	Vector3[] vertices;
	int[] triangles;
	Vector2[] uvs;

	int lastCount ;


	#endregion

	#region Unity Messages
//	void Awake()
//	{
//
//	}
//	void OnEnable()
//  {
//
//  }
//
//	void Start() 
//	{
//	
//	}
//	
#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying)
        {
            updateMesh(m_type, m_degree, m_radius, m_InnerOff);
        }
    }
#endif
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
	public void updateMesh(ShapeType shape, float degree,float radius,float innerOff=2) {
        if (shape == ShapeType.sector)
        {
            if (mesh == null)
            {
                mesh = new Mesh();
            }
            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();

            int i;
            float beginDegree;
            float endDegree;
            float beginRadian;
            float endRadian;
            float uvRadius = 0.5f;
            Vector2 uvCenter = new Vector2(0.5f, 0.5f);
            float currentIntervalDegree = 0;
            float limitDegree;
            int count;

            float beginCos;
            float beginSin;
            float endCos;
            float endSin;

            int beginNumber;
            int endNumber;
            int triangleNumber;

            currentIntervalDegree = Mathf.Abs(m_intervalDegree);

            count = (int)(Mathf.Abs(degree) / currentIntervalDegree);
            if (degree % m_intervalDegree != 0)
            {
                ++count;
            }
            if (degree < 0)
            {
                currentIntervalDegree = -currentIntervalDegree;
            }

            if (lastCount != count || shape != m_type)
            {
                mesh.Clear();
                vertices = new Vector3[count * 2 + 1];
                triangles = new int[count * 3];
                uvs = new Vector2[count * 2 + 1];
                vertices[0] = Vector3.zero;
                uvs[0] = uvCenter;
                lastCount = count;
            }

            i = 0;
            //		beginDegree = beginOffsetDegree;
            //		limitDegree = degree + beginOffsetDegree;
            beginDegree = 90 - degree / 2;
            limitDegree = degree + beginDegree;

            while (i < count)
            {
                endDegree = beginDegree + currentIntervalDegree;

                if (degree > 0)
                {
                    if (endDegree > limitDegree)
                    {
                        endDegree = limitDegree;
                    }
                }
                else
                {
                    if (endDegree < limitDegree)
                    {
                        endDegree = limitDegree;
                    }
                }

                beginRadian = Mathf.Deg2Rad * beginDegree;
                endRadian = Mathf.Deg2Rad * endDegree;

                beginCos = Mathf.Cos(beginRadian);
                beginSin = Mathf.Sin(beginRadian);
                endCos = Mathf.Cos(endRadian);
                endSin = Mathf.Sin(endRadian);

                beginNumber = i * 2 + 1;
                endNumber = i * 2 + 2;
                triangleNumber = i * 3;

                vertices[beginNumber].x = beginCos * radius;
                vertices[beginNumber].y = 0;
                vertices[beginNumber].z = beginSin * radius;
                vertices[endNumber].x = endCos * radius;
                vertices[endNumber].y = 0;
                vertices[endNumber].z = endSin * radius;

                triangles[triangleNumber] = 0;
                if (degree > 0)
                {
                    triangles[triangleNumber + 1] = endNumber;
                    triangles[triangleNumber + 2] = beginNumber;
                }
                else
                {
                    triangles[triangleNumber + 1] = beginNumber;
                    triangles[triangleNumber + 2] = endNumber;
                }

                if (radius > 0)
                {
                    uvs[beginNumber].x = beginCos * uvRadius + uvCenter.x;
                    uvs[beginNumber].y = beginSin * uvRadius + uvCenter.y;
                    uvs[endNumber].x = endCos * uvRadius + uvCenter.x;
                    uvs[endNumber].y = endSin * uvRadius + uvCenter.y;
                }
                else
                {
                    uvs[beginNumber].x = -beginCos * uvRadius + uvCenter.x;
                    uvs[beginNumber].y = -beginSin * uvRadius + uvCenter.y;
                    uvs[endNumber].x = -endCos * uvRadius + uvCenter.x;
                    uvs[endNumber].y = -endSin * uvRadius + uvCenter.y;
                }

                beginDegree += currentIntervalDegree;
                ++i;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshFilter.sharedMesh = mesh;
            meshFilter.sharedMesh.name = "CircularSectorMesh";

            m_degree = degree;
            m_radius = radius;

            meshRender.sharedMaterial = m_circleIndicator;

        }
        else if (shape == ShapeType.obb)
        {
            if (mesh == null)
            {
                mesh = new Mesh();
            }
            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();

            Vector2 uvCenter = new Vector2(0.5f, 0.5f);
            mesh.Clear();
            vertices = new Vector3[4];
            triangles = new int[6];
            uvs = new Vector2[4];
            vertices[0] = Vector3.zero;
            uvs[0] = uvCenter;

            vertices[0] = new Vector3(degree, 0, 0); //1,0
            vertices[1] = new Vector3(degree, 0, radius); //1,1
            vertices[2] = new Vector3(-degree, 0, radius); //-1,-1
            vertices[3] = new Vector3(-degree, 0, 0); //-1,0

            triangles[0] = 3;
            triangles[1] = 2;
            triangles[2] = 0;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 1;

            uvs[0] = new Vector2(0, 1);
            uvs[1] = new Vector2(0, 0);
            uvs[2] = new Vector2(1, 0);
            uvs[3] = new Vector2(1, 1);

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            meshFilter.sharedMesh = mesh;
            meshFilter.sharedMesh.name = "CircularSectorMesh"; ;
            meshRender.sharedMaterial = m_RectIndicator;

            m_degree = degree;
            m_radius = radius;
        }
        else if (shape == ShapeType.ring)
        {
            if (mesh == null)
            {
                mesh = new Mesh();
            }
            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();

            int i;
            float beginDegree;
            float endDegree;
            float beginRadian;
            float endRadian;
            float uvRadius = 0.5f;
            Vector2 uvCenter = new Vector2(0.5f, 0.5f);
            float currentIntervalDegree = 0;
            float limitDegree;
            int count;

            float beginCos;
            float beginSin;
            float endCos;
            float endSin;

            int beginNumber;
            int beginMiddleNumber;
            int endNumber;
            int endMiddleNumber;
            int triangleNumber;
            int triangleAddition;

            currentIntervalDegree = Mathf.Abs(m_intervalDegree);

            count = (int)(Mathf.Abs(degree) / currentIntervalDegree);
            if (degree % m_intervalDegree != 0)
            {
                ++count;
            }
            if (degree < 0)
            {
                currentIntervalDegree = -currentIntervalDegree;
            }

            if (lastCount != count || m_type != shape)
            {
                mesh.Clear();
                vertices = new Vector3[count * 2 * 2 + 1];
                triangles = new int[count * 3 * 2];
                uvs = new Vector2[count * 2 * 2 + 1];
                vertices[0] = Vector3.zero;
                uvs[0] = uvCenter;
                lastCount = count;
            }

            i = 0;
            //		beginDegree = beginOffsetDegree;
            //		limitDegree = degree + beginOffsetDegree;
            beginDegree = 90 - degree / 2;
            limitDegree = degree + beginDegree;

            while (i < count)
            {
                endDegree = beginDegree + currentIntervalDegree;

                if (degree > 0)
                {
                    if (endDegree > limitDegree)
                    {
                        endDegree = limitDegree;
                    }
                }
                else
                {
                    if (endDegree < limitDegree)
                    {
                        endDegree = limitDegree;
                    }
                }

                beginRadian = Mathf.Deg2Rad * beginDegree;
                endRadian = Mathf.Deg2Rad * endDegree;

                beginCos = Mathf.Cos(beginRadian);
                beginSin = Mathf.Sin(beginRadian);
                endCos = Mathf.Cos(endRadian);
                endSin = Mathf.Sin(endRadian);

                beginMiddleNumber = i * 2 * 2 + 1;
                beginNumber = i * 2 * 2 + 1 + 1;
                endNumber = i * 2 * 2 + 2 + 1;
                endMiddleNumber = i * 2 * 2 + 3 + 1;
                triangleNumber = i * 3 * 2;
                triangleAddition = i * 3 * 2 + 3;

                vertices[beginMiddleNumber].x = beginCos * innerOff;
                vertices[beginMiddleNumber].y = 0;
                vertices[beginMiddleNumber].z = beginSin * innerOff;

                vertices[beginNumber].x = beginCos * radius;
                vertices[beginNumber].y = 0;
                vertices[beginNumber].z = beginSin * radius;

                vertices[endNumber].x = endCos * radius;
                vertices[endNumber].y = 0;
                vertices[endNumber].z = endSin * radius;

                vertices[endMiddleNumber].x = endCos * innerOff;
                vertices[endMiddleNumber].y = 0;
                vertices[endMiddleNumber].z = endSin * innerOff;

                triangles[triangleNumber] = beginMiddleNumber;
                if (degree > 0)
                {
                    triangles[triangleNumber + 1] = endNumber;
                    triangles[triangleNumber + 2] = beginNumber;
                }
                else
                {
                    triangles[triangleNumber + 1] = beginNumber;
                    triangles[triangleNumber + 2] = endNumber;
                }

                triangles[triangleAddition] = endMiddleNumber;
                if (degree > 0)
                {
                    triangles[triangleAddition + 1] = endNumber;
                    triangles[triangleAddition + 2] = beginMiddleNumber;
                }
                else
                {
                    triangles[triangleAddition + 1] = beginMiddleNumber;
                    triangles[triangleAddition + 2] = endNumber;
                }

                if (radius > 0)
                {
                    uvs[beginNumber].x = beginCos * uvRadius + uvCenter.x;
                    uvs[beginNumber].y = beginSin * uvRadius + uvCenter.y;

                    uvs[beginMiddleNumber].x = beginCos * uvRadius * innerOff / radius + uvCenter.x;
                    uvs[beginMiddleNumber].y = beginSin * uvRadius * innerOff / radius + uvCenter.y;

                    uvs[endNumber].x = endCos * uvRadius + uvCenter.x;
                    uvs[endNumber].y = endSin * uvRadius + uvCenter.y;

                    uvs[endMiddleNumber].x = endCos * uvRadius * innerOff / radius + uvCenter.x;
                    uvs[endMiddleNumber].y = endSin * uvRadius * innerOff / radius + uvCenter.y;
                }
                else
                {
                    uvs[beginNumber].x = -beginCos * uvRadius + uvCenter.x;
                    uvs[beginNumber].y = -beginSin * uvRadius + uvCenter.y;

                    uvs[beginMiddleNumber].x = -beginCos * uvRadius * innerOff / radius + uvCenter.x;
                    uvs[beginMiddleNumber].y = -beginSin * uvRadius * innerOff / radius + uvCenter.y;

                    uvs[endNumber].x = -endCos * uvRadius + uvCenter.x;
                    uvs[endNumber].y = -endSin * uvRadius + uvCenter.y;

                    uvs[endMiddleNumber].x = -endCos * uvRadius * innerOff / radius + uvCenter.x;
                    uvs[endMiddleNumber].y = -endSin * uvRadius * innerOff / radius + uvCenter.y;
                }

                beginDegree += currentIntervalDegree;
                ++i;
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;

            //			mesh.RecalculateNormals();
            //			mesh.RecalculateBounds();

            meshFilter.sharedMesh = mesh;
            meshFilter.sharedMesh.name = "CircularSectorMesh";

            m_degree = degree;
            m_radius = radius;

            meshRender.sharedMaterial = m_circleIndicator;
        }
		m_type = shape;
	}

	#endregion

	#region Override Methods
	#endregion

	#region Private Methods
	#endregion
}


