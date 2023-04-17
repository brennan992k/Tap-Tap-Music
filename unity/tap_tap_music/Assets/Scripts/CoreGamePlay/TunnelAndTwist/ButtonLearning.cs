using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonLearning : MonoBehaviour
	{
		public void OnClickedPlaying()
		{
			EventManager.DoAnimLearningEvent(AnimIntro.end);
		}
	}
}
