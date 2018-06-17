using UnityEngine;
using System.Collections;

public class RenderPostEffect : MonoBehaviour
{
	#region Public Attributes
	public bool isDepthEnable = false;
	public Material renderMaterial;
	public Shader replaceShader;
	#endregion

	#region Unity Messages
	void Start()
	{
		if(GetComponent<Camera>() != null )
		{
			if(isDepthEnable)
			{
				GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
			}
		}
	}

	void LateUpdate()
	{
		if(GetComponent<Camera>() != null && replaceShader != null)
		{
			GetComponent<Camera>().RenderWithShader(replaceShader,"RenderType");
		}
	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(renderMaterial != null)
		{
			Graphics.Blit(sourceTexture, destTexture,renderMaterial);
		}
		else
		{
			Graphics.Blit(sourceTexture,destTexture);
		}
	}
	#endregion
}
