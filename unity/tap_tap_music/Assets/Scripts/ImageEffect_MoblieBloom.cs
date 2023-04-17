using System;
using UnityEngine;

[AddComponentMenu("guangyun/MobileBloom"), ExecuteInEditMode]
public class ImageEffect_MoblieBloom : MonoBehaviour
{
	public Shader BloomShader;

	private Material BloomMaterial;

	private RenderTextureFormat rtFormat = RenderTextureFormat.Default;

	public Color colorMix = new Color(1f, 1f, 1f, 1f);

	[Range(0f, 1f)]
	public float threshold = 0.25f;

	[Range(0f, 2.5f)]
	public float intensity = 0.75f;

	[Range(0.2f, 1f)]
	public float BlurSize = 1f;

	private void Start()
	{
		this.FindShaders();
		this.CheckSupport();
		this.CreateMaterials();
	}

	private void FindShaders()
	{
		if (!this.BloomShader)
		{
			this.BloomShader = Shader.Find("guangyun/MobileBloom");
		}
	}

	private void CreateMaterials()
	{
		if (!this.BloomMaterial)
		{
			this.BloomMaterial = new Material(this.BloomShader);
			this.BloomMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	private bool Supported()
	{
		return SystemInfo.supportsImageEffects && SystemInfo.supportsRenderTextures && this.BloomShader.isSupported;
	}

	private bool CheckSupport()
	{
		if (!this.Supported())
		{
			base.enabled = false;
			return false;
		}
		this.rtFormat = ((!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGB565)) ? RenderTextureFormat.Default : RenderTextureFormat.RGB565);
		return true;
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.threshold != 0f && this.intensity != 0f)
		{
			int width = sourceTexture.width / 4;
			int height = sourceTexture.height / 4;
			this.BloomMaterial.SetColor("_ColorMix", this.colorMix);
			this.BloomMaterial.SetVector("_Parameter", new Vector4(this.BlurSize * 1.5f, 0f, this.intensity, 0.8f - this.threshold));
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, this.rtFormat);
			temporary.filterMode = FilterMode.Bilinear;
			RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0, this.rtFormat);
			temporary.filterMode = FilterMode.Bilinear;
			Graphics.Blit(sourceTexture, temporary, this.BloomMaterial, 0);
			Graphics.Blit(temporary, temporary2, this.BloomMaterial, 1);
			RenderTexture.ReleaseTemporary(temporary);
			temporary = RenderTexture.GetTemporary(width, height, 0, this.rtFormat);
			temporary2.filterMode = FilterMode.Bilinear;
			Graphics.Blit(temporary2, temporary, this.BloomMaterial, 2);
			this.BloomMaterial.SetTexture("_Bloom", temporary);
			Graphics.Blit(sourceTexture, destTexture, this.BloomMaterial, 3);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	public void OnDisable()
	{
		if (this.BloomMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.BloomMaterial);
		}
	}
}
