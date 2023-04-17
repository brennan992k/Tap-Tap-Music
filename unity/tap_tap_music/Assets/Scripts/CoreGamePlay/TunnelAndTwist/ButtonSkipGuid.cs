using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonSkipGuid : MonoBehaviour
	{
		public void OnClickSkip()
		{
			EventManager.DoSkipGuid();
		}
	}
}
