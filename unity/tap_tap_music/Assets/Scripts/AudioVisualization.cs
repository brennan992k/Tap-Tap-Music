using System;
using UnityEngine;

public class AudioVisualization : MonoBehaviour
{
	public AudioSource audioSource;

	private float[] samples = new float[64];

	public GameObject cube;

	public float posX = 0.38f;

	public float posY = -8.7f;

	public float bannerPosY = -5.5f;

	private int minIndex = 10;

	private int maxIndex = 40;

	private Transform[] cubeTransform;

	private Vector3 cubePos;

	private void Start()
	{
		this.cubeTransform = new Transform[this.samples.Length];
		Vector3 localScale = this.cube.transform.localScale;
		for (int i = this.minIndex; i < this.maxIndex; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cube, new Vector3(base.transform.position.x + (float)i * 0.65f, base.transform.position.y, base.transform.position.z), Quaternion.identity);
			gameObject.name = "sprite" + i.ToString();
			gameObject.transform.parent = base.transform;
			gameObject.transform.localScale = localScale;
			this.cubeTransform[i] = gameObject.transform;
		}
		if (AdsControl.Instance.GetBannerAvailable())
		{
			base.transform.position = new Vector3((float)(-(float)this.samples.Length) * this.posX, this.bannerPosY, base.transform.position.z);
		}
		else
		{
			base.transform.position = new Vector3((float)(-(float)this.samples.Length) * this.posX, this.posY, base.transform.position.z);
		}
	}

	private void FixedUpdate()
	{
		this.audioSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
		int num = 12;
		for (int i = this.minIndex; i < this.maxIndex; i++)
		{
			Transform child = this.cubeTransform[i].GetChild(3);
			Transform child2 = this.cubeTransform[i].GetChild(4);
			Transform child3 = this.cubeTransform[i].GetChild(5);
			this.cubePos.Set(child2.localScale.x, Mathf.Clamp(this.samples[i] * ((float)(i * i) * 0.2f), 0f, (float)num), child2.localScale.z);
			if (child.localScale.y + child2.localScale.y + child3.localScale.y < this.cubePos.y)
			{
				if (this.cubePos.y < 2f)
				{
					child.localScale = new Vector3(this.cubePos.x, this.cubePos.y, this.cubePos.z);
					child2.localScale = new Vector3(this.cubePos.x, 0f, this.cubePos.z);
					child3.localScale = new Vector3(this.cubePos.x, 0f, this.cubePos.z);
				}
				else if (this.cubePos.y < 8f)
				{
					child.localScale = new Vector3(this.cubePos.x, 0f, this.cubePos.z);
					child2.localScale = new Vector3(this.cubePos.x, this.cubePos.y / 4f, this.cubePos.z);
					child3.localScale = new Vector3(this.cubePos.x, 0f, this.cubePos.z);
				}
				else
				{
					child.localScale = new Vector3(this.cubePos.x, 0f, this.cubePos.z);
					child2.localScale = new Vector3(this.cubePos.x, 0f, this.cubePos.z);
					child3.localScale = new Vector3(this.cubePos.x, this.cubePos.y / 12f, this.cubePos.z);
				}
			}
			else if (child3.localScale.y > 0f)
			{
				child3.localScale -= new Vector3(0f, 0.1f, 0f);
			}
			else if (child2.localScale.y > 0f)
			{
				child2.localScale -= new Vector3(0f, 0.1f, 0f);
			}
			else if (child.localScale.y > 0f)
			{
				child.localScale -= new Vector3(0f, 0.1f, 0f);
			}
		}
	}
}
