using System;
using UnityEngine;
using UnityEngine.UI;

public class TextureRotate : MonoBehaviour
{
	private Transform m_Horse;

	private RawImage m_rawImage;

	public CameraManger cameraManger;

	private int m_key = -1;

	private int modelIndex = -1;

	private bool rotating = true;

	public void SetModelId(int index)
	{
		this.modelIndex = index;
	}

	public void Show3DModel(int index)
	{
		if (index < 0)
		{
			return;
		}
		this.modelIndex = index;
		string path = "GameBalls/Ball" + index;
		this.m_key = this.cameraManger.Add3DModel(path);
		this.m_Horse = this.cameraManger.Get3DModel(this.m_key);
		this.m_rawImage = base.transform.GetComponent<RawImage>();
		float num = (float)this.cameraManger.numberWidth;
		float num2 = (float)this.cameraManger.numberHeight;
		float y = (float)((int)((float)this.m_key / num)) / num2;
		float x = (float)this.m_key % num / num;
		Rect uvRect = new Rect(x, y, 1f / num, 1f / num2);
		this.m_rawImage.uvRect = uvRect;
		this.m_rawImage.SetNativeSize();
	}

	private void OnEnable()
	{
		if (this.m_key == -1 && this.modelIndex >= 0)
		{
			this.Show3DModel(this.modelIndex);
		}
	}

	private void OnDisable()
	{
		if (this.cameraManger != null && this.m_key != -1)
		{
			this.cameraManger.Remove3DModel(this.m_key);
			this.m_Horse = null;
			this.m_key = -1;
		}
	}

	public void SetRotating(bool rotate)
	{
		this.rotating = rotate;
	}

	private void Update()
	{
		if (this.m_Horse != null && this.rotating)
		{
			this.m_Horse.Rotate(1f, 0f, 0f);
		}
	}
}
