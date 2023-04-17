using System;
using UnityEngine;

public class SpectrumVis : MonoBehaviour
{
	public enum axisStrech
	{
		dx,
		dy,
		dz,
		dyAndDz,
		all
	}

	public enum channelColour
	{
		red,
		green,
		blue,
		all
	}

	public GameObject[] cubes;

	public Color barColor;

	public float sizePower = 20f;

	public SpectrumVis.axisStrech stretchAxis = SpectrumVis.axisStrech.dy;

	public SpectrumVis.channelColour currentChannel;

	private float currentRed;

	private float currentGreen;

	private float currentBlue;

	public float colorPower = 12f;

	private void Start()
	{
		this.currentRed = this.barColor.r;
		this.currentGreen = this.barColor.g;
		this.currentBlue = this.barColor.b;
	}

	private void Update()
	{
		for (int i = 0; i < this.cubes.Length; i++)
		{
			Vector3 localScale = this.cubes[i].transform.localScale;
			float b = SpectrumKernel.spects[i] * this.sizePower;
			if (this.stretchAxis == SpectrumVis.axisStrech.dx)
			{
				localScale.x = Mathf.Lerp(localScale.x, b, Time.deltaTime * this.sizePower);
			}
			if (this.stretchAxis == SpectrumVis.axisStrech.dy)
			{
				localScale.y = Mathf.Lerp(localScale.y, b, Time.deltaTime * this.sizePower);
			}
			if (this.stretchAxis == SpectrumVis.axisStrech.dz)
			{
				localScale.z = Mathf.Lerp(localScale.z, b, Time.deltaTime * this.sizePower);
			}
			if (this.stretchAxis == SpectrumVis.axisStrech.dyAndDz)
			{
				localScale.y = Mathf.Lerp(localScale.y, b, Time.deltaTime * this.sizePower);
				localScale.z = Mathf.Lerp(localScale.z, b, Time.deltaTime * this.sizePower);
			}
			if (this.stretchAxis == SpectrumVis.axisStrech.all)
			{
				localScale.x = Mathf.Lerp(localScale.x, b, Time.deltaTime * this.sizePower);
				localScale.y = Mathf.Lerp(localScale.y, b, Time.deltaTime * this.sizePower);
				localScale.z = Mathf.Lerp(localScale.z, b, Time.deltaTime * this.sizePower);
			}
			this.cubes[i].transform.localScale = localScale;
			float num = SpectrumKernel.spects[i] * this.colorPower;
			if (this.currentChannel == SpectrumVis.channelColour.red)
			{
				this.barColor.r = this.currentRed + num;
			}
			if (this.currentChannel == SpectrumVis.channelColour.green)
			{
				this.barColor.g = this.currentGreen + num;
			}
			if (this.currentChannel == SpectrumVis.channelColour.blue)
			{
				this.barColor.b = this.currentBlue + num;
			}
			if (this.currentChannel == SpectrumVis.channelColour.all)
			{
				this.barColor.b = this.currentBlue + num;
				this.barColor.g = this.currentGreen + num;
				this.barColor.r = this.currentRed + num;
			}
			this.cubes[i].GetComponent<Renderer>().material.color = this.barColor;
			this.cubes[i].GetComponent<Renderer>().material.SetColor("_TintColor", this.barColor);
		}
	}
}
