using System;
using UnityEngine;

public class SpectrumPartStartColor : MonoBehaviour
{
	public ParticleSystem partEmitter;

	public int audioChannel = 3;

	public float audioSensibility = 0.1f;

	public Color beatColor = new Color(1f, 0f, 0f);

	public Color normalColor = new Color(0.5f, 0.5f, 0.5f);

	private void Update()
	{
		ParticleSystem.MainModule main = this.partEmitter.main;
		if (SpectrumKernel.spects[this.audioChannel] * SpectrumKernel.threshold >= this.audioSensibility)
		{
			main.startColor = this.beatColor;
			main.startSize = 2f;
		}
		else
		{
			main.startColor = this.normalColor;
			main.startSize = 1f;
		}
	}
}
