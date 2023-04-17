using DG.Tweening;
using System;

namespace AppAdvisory.TunnelAndTwist
{
	public class SpeedText : UIHelper
	{
		private float newSpeed;

		private void Start()
		{
			this.OnSetSpeed(0f);
		}

		public void OnEnable()
		{
			EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnSetSpeed += new EventManager.SetSpeed(this.OnSetSpeed);
		}

		private void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnSetSpeed -= new EventManager.SetSpeed(this.OnSetSpeed);
		}

		public void OnSetSpeed(float speed)
		{
			DOVirtual.Float(this.newSpeed, speed, 0.3f, new TweenCallback<float>(this.SetSpeedText));
			this.newSpeed = speed;
		}

		private void SetSpeedText(float speed)
		{
			this.text.text = speed.ToString("F1");
		}

		public void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			this.OnSetSpeed(0f);
			base.ActivateText(true);
		}
	}
}
