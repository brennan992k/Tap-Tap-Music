using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class GameIntroTrigger : TriggerHelper
	{
		private bool isCollision;

		public bool isLeft;

		private void Awake()
		{
			this.isCollision = false;
			this.isGamePlaying = true;
		}

		private new void OnEnable()
		{
			base.OnEnable();
			this.isCollision = false;
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(true);
		}

		private new void OnDisable()
		{
			base.OnDisable();
			this.isCollision = false;
		}

		public override void OnCollisionWithPlayer()
		{
			if (this.isCollision)
			{
				return;
			}
			float f = base.transform.position.z - base.playerTransform.position.z;
			float f2 = base.transform.position.y - base.playerTransform.position.y;
			if (Mathf.Abs(f) < 0.8f && Mathf.Abs(f2) < 1.5f)
			{
				this.isCollision = true;
				EventManager.DoPauseForGuid(AnimStatus.start);
				base.transform.GetChild(0).gameObject.SetActive(this.isLeft);
				base.transform.GetChild(1).gameObject.SetActive(!this.isLeft);
				base.transform.GetChild(2).gameObject.SetActive(false);
			}
		}

		public override void IsOutOfScreen()
		{
			EventManager.DODesactivatePointTrigger(base.gameObject);
		}
	}
}
