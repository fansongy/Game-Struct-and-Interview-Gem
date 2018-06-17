using UnityEngine;
using System.Collections;

public class CustomGlow : MonoBehaviour
{
	#region Public Attributes
	public Shader m_renderGlowShader;
	public Material m_blurMaterial;
	public Material m_mixMaterial;
	#endregion

	#region Private Attributes
	private RenderTexture m_rt;
	#endregion

	#region Unity Messages

	void Start()
	{
		var mainCam = GetComponent<Camera>();

		m_rt = new RenderTexture(Screen.width, Screen.height,(int)mainCam.depth);

		GameObject obj = new GameObject("CameraRT");
		obj.transform.SetParent(transform,false);

		var cam = obj.AddComponent<Camera>();
		cam.enabled = false; 
		cam.clearFlags = CameraClearFlags.SolidColor;
		cam.backgroundColor = Color.black;
		cam.orthographic = mainCam.orthographic;
		cam.orthographicSize = mainCam.orthographicSize;
		cam.nearClipPlane = mainCam.nearClipPlane;
		cam.farClipPlane = mainCam.farClipPlane;
		cam.fieldOfView = mainCam.fieldOfView;
		cam.targetTexture = m_rt;

		var bloomTex = obj.AddComponent<RenderPostEffect>();
		bloomTex.replaceShader = m_renderGlowShader;
		bloomTex.renderMaterial = m_blurMaterial;


		m_mixMaterial.SetTexture("_BlurTex", m_rt);

	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		Graphics.Blit(sourceTexture, destTexture, m_mixMaterial);
	}

	#endregion
}