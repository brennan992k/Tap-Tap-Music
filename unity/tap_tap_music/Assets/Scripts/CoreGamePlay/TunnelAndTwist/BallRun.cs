using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class BallRun : MonoBehaviour
	{
		private float speed;

		private bool rotating;

		private void OnEnable()
		{
			EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnMusicSpeedEvent += new EventManager.MusicSpeedEvent(this.OnMusicSpeedEvent);
		}

		private void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnMusicSpeedEvent += new EventManager.MusicSpeedEvent(this.OnMusicSpeedEvent);
		}

		private void OnPlayerStartEvent()
		{
			this.rotating = true;
		}

		private void OnAnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.start)
			{
				this.rotating = false;
			}
		}

		private void OnMusicSpeedEvent(float value)
		{
			this.speed = 916.7324f * value;
		}

		private void FixedUpdate()
		{
			if (this.rotating)
			{
				base.transform.Rotate(Vector3.right * this.speed * Time.fixedDeltaTime);
			}
		}
	}
}
