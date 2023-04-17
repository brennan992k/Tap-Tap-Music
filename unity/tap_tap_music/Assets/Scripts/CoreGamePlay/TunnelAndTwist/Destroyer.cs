using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class Destroyer : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			UnityEngine.Object.Destroy(other.transform.parent.gameObject);
		}
	}
}
