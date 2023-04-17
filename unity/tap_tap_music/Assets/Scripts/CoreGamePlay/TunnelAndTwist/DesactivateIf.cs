using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class DesactivateIf : MonoBehaviour
	{
		public bool activate;

		private void Update()
		{
			if (!this.activate)
			{
				return;
			}
			base.gameObject.SetActive(false);
		}
	}
}
