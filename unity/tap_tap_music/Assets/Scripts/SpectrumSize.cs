using System;
using UnityEngine;

public class SpectrumSize : MonoBehaviour
{
	public GameObject theObject;

	public int audioChannel = 4;

	public float audioSensibility = 0.15f;

	public float scaleFactorX = 4f;

	public float scaleFactorY = 4f;

	public float scaleFactorZ = 4f;

	public float lerpTime = 2f;

	private Vector3 oldLocalScale;

	private void Start()
	{
		this.oldLocalScale = this.theObject.transform.localScale;
	}

	private void Update()
	{
		if (SpectrumKernel.spects[this.audioChannel] * SpectrumKernel.threshold >= this.audioSensibility)
		{
			this.theObject.transform.localScale = new Vector3(this.scaleFactorX, this.scaleFactorY, this.scaleFactorZ);
		}
		else
		{
			this.theObject.transform.localScale = Vector3.Lerp(this.theObject.transform.localScale, this.oldLocalScale, this.lerpTime * Time.deltaTime);
		}
	}
}
