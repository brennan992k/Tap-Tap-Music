using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectMusicCircleLight : MonoBehaviour
{
	public Image[] circles;

	public Color[] colors;

	private float scaleMin = 0.2f;

	private float scaleMax = 1.1f;

	private float scaleTime = 2f;

	private float scaleSpeed;

	private Vector3 maxScale;

	private Vector3 minScale;

	private void Start()
	{
		this.scaleSpeed = (this.scaleMax - this.scaleMin) / this.scaleTime;
		this.maxScale = new Vector3(this.scaleMax, this.scaleMax, this.scaleMax);
		this.minScale = new Vector3(this.scaleMin, this.scaleMin, this.scaleMin);
	}

	private void Update()
	{
		float num = this.scaleSpeed * Time.deltaTime;
		for (int i = 0; i < this.circles.Length; i++)
		{
			Color color = this.circles[i].color;
			Vector3 localScale = this.circles[i].transform.localScale;
			if (localScale.x > 0.9f)
			{
				Color color2 = this.colors[2];
				float a = (this.scaleMax - localScale.x) / (this.scaleMax - 0.9f);
				this.circles[i].color = new Color(color2.r, color2.g, color2.b, a);
			}
			else if (localScale.x > 0.5f)
			{
				float d = (localScale.x - 0.5f) / 0.399999976f;
				Color color3 = this.colors[1];
				Color color4 = this.colors[2];
				Vector3 vector = new Vector3(color4.r - color3.r, color4.g - color3.g, color4.b - color3.b) * d;
				this.circles[i].color = new Color(color3.r + vector.x, color3.g + vector.y, color3.b + vector.z, 1f);
			}
			else if (localScale.x > 0.3f)
			{
				float d2 = (localScale.x - 0.3f) / 0.199999988f;
				Color color5 = this.colors[0];
				Color color6 = this.colors[1];
				Vector3 vector2 = new Vector3(color6.r - color5.r, color6.g - color5.g, color6.b - color5.b) * d2;
				this.circles[i].color = new Color(color5.r + vector2.x, color5.g + vector2.y, color5.b + vector2.z, 1f);
			}
			else
			{
				this.circles[i].color = this.colors[0];
			}
			if (localScale.x >= this.scaleMax)
			{
				this.circles[i].transform.localScale = this.minScale;
				this.circles[i].color = this.colors[0];
			}
			else if (localScale.x < this.scaleMax)
			{
				this.circles[i].transform.localScale = new Vector3(localScale.x + num, localScale.y + num, localScale.z);
			}
		}
	}
}
