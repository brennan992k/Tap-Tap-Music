using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class DontRotate : MonoBehaviour
	{
		private void Update()
		{
			base.transform.rotation = Quaternion.identity;
		}
	}
}
