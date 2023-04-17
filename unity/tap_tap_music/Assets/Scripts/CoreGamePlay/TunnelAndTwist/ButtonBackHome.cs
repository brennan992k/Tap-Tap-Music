using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonBackHome : MonoBehaviour
	{
		public void OnClickedBackHome()
		{
			EventManager.DOBackHomeEvent();
		}
	}
}
