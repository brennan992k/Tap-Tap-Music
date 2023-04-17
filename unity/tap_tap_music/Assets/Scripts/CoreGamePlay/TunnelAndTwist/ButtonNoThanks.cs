using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonNoThanks : MonoBehaviour
	{
		private static Action<bool> __f__am_cache0;

		public void OnClickedGameOver()
		{
			if (PlayerPrefsManager.PassInterstitialTime >= 3)
			{
                EventManager.DoAnimGameEndEvent();
                AdsControl.Instance.showAds();
				
			}
			else
			{
				PlayerPrefsManager.PassInterstitialTime++;
				EventManager.DoAnimGameEndEvent();
			}
		}
	}
}
