using System;
using UnityEngine;

public class SpectrumKernel : MonoBehaviour
{
	private static AudioSource audioSource;

	public static float[] spects;

	public static float threshold = 3f;

	private void Awake()
	{
		SpectrumKernel.spects = new float[1024];
		SpectrumKernel.audioSource = base.GetComponent<AudioSource>();
	}

	private void Update()
	{
		AudioListener.GetSpectrumData(SpectrumKernel.spects, 0, FFTWindow.BlackmanHarris);
	}

	public static void JumpAudio(float value)
	{
		SpectrumKernel.audioSource.time = value * SpectrumKernel.audioSource.clip.length;
	}

	public static float GetAudioProgress()
	{
		return SpectrumKernel.audioSource.time * 100f / SpectrumKernel.audioSource.clip.length;
	}

	public static bool IsAudioPlaying()
	{
		return SpectrumKernel.audioSource.isPlaying;
	}
}
