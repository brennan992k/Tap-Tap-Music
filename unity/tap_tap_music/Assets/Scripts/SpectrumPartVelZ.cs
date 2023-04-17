using System;
using UnityEngine;

public class SpectrumPartVelZ : MonoBehaviour
{
	public ParticleSystem partEmitter;

	public int audioChannel = 3;

	public float audioSensibility = 0.1f;

	public float particleVelocity = 50f;

	private void Update()
	{
		ParticleSystem.MainModule main = this.partEmitter.main;
		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = this.partEmitter.velocityOverLifetime;
		if (SpectrumKernel.spects != null && this.audioChannel < SpectrumKernel.spects.Length && SpectrumKernel.spects[this.audioChannel] * SpectrumKernel.threshold >= this.audioSensibility)
		{
			velocityOverLifetime.zMultiplier = this.particleVelocity;
		}
		else
		{
			velocityOverLifetime.zMultiplier = 0f;
		}
	}
}
