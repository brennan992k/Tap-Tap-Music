using System;
using UnityEngine;

public class SpectrumLight : MonoBehaviour
{
	public int audioChannel = 4;

	public float audioSensibility = 0.15f;

	public float intensity = 3f;

	public float lerpTime = 2f;

	private Light lt;

	private float oldIntensity;

	private void Start()
	{
		this.lt = base.GetComponent<Light>();
		this.oldIntensity = this.lt.intensity;
	}

	private void Update()
	{
		if (SpectrumKernel.spects[this.audioChannel] * SpectrumKernel.threshold >= this.audioSensibility)
		{
			this.lt.intensity = SpectrumKernel.spects[this.audioChannel] * (this.intensity * SpectrumKernel.threshold);
		}
		else
		{
			this.oldIntensity = Mathf.Lerp(this.lt.intensity, 1f, this.lerpTime * Time.deltaTime);
			this.lt.intensity = this.oldIntensity;
		}
	}
}
