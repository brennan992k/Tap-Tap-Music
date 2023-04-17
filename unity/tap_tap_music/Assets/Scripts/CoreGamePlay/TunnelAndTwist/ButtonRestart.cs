using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonRestart : MonoBehaviour
	{
		private bool isSwitchScene;

		private void OnEnable()
		{
			this.isSwitchScene = false;
		}

		public void OnClickedRestart()
		{
			if (!this.isSwitchScene)
			{
				this.isSwitchScene = true;
				EventManager.DOReloadSceneEvent();
			}
		}
	}
}
