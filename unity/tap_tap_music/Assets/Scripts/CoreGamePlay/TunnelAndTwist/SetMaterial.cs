using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class SetMaterial : MonoBehaviour
	{
		public Material m;

		private void Start()
		{
			base.GetComponent<Renderer>().material = this.m;
		}
	}
}
