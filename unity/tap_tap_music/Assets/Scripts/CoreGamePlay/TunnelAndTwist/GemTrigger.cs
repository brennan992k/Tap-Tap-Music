using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class GemTrigger : TriggerHelper
	{
		private bool isCollision;

		private void Awake()
		{
			this.isCollision = false;
			this.isGamePlaying = true;
		}

		private new void OnEnable()
		{
			base.OnEnable();
			this.isCollision = false;
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
			if (Mathf.Abs(f) < 1.2f && Mathf.Abs(f2) < 1.5f)
			{
				this.isCollision = true;
				base.player.DoExplosionParticle();
				EventManager.DoAddGem();
				base.gameObject.SetActive(false);
			}
		}

		public override void IsOutOfScreen()
		{
			EventManager.DODesactivatePointTrigger(base.gameObject);
		}
	}
}
