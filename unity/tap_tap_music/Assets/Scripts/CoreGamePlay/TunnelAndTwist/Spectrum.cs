using System;
using System.Collections.Generic;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class Spectrum : MonoBehaviour
	{
		public AudioSource asource;

		public int channel;

		private float[] spectrum;

		private float[] volume;

		public int numSamples;

		public GameObject enem;

		public Transform ball;

		private List<GameObject> list = new List<GameObject>();

		public int freq;

		private void Awake()
		{
			this.volume = new float[this.numSamples];
			this.spectrum = new float[this.numSamples];
			this.list = new List<GameObject>();
			for (int i = 1; i < 255; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enem, new Vector3((float)i, 0f, 0f), base.transform.rotation);
				gameObject.name = "sp" + i;
				this.list.Add(gameObject);
			}
		}

		private void Update()
		{
			this.asource.GetOutputData(this.volume, this.channel);
			this.asource.GetSpectrumData(this.spectrum, this.channel, FFTWindow.BlackmanHarris);
			for (int i = 0; i < this.list.Count; i++)
			{
				GameObject gameObject = this.list[i];
				gameObject.transform.localScale = new Vector3(1f, 200f * this.spectrum[i], 1f);
				gameObject.GetComponent<MeshRenderer>().material.color = new Color(100f * this.spectrum[i], 1f / (20f * this.spectrum[i]), 1f / (20f * this.spectrum[i]), 1f);
			}
			MonoBehaviour.print(string.Concat(new object[]
			{
				"freq ",
				this.freq,
				" = ",
				this.spectrum[this.freq]
			}));
		}
	}
}
