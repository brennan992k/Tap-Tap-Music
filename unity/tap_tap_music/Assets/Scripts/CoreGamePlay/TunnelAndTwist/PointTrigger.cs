using DG.Tweening;
using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class PointTrigger : TriggerHelper
	{
		public float defaultScale = 0.6f;

		public Vector3 dstScale = new Vector3(2.5f, 2.5f, 2.5f);

		public SpriteRenderer circleSprite;

		public SpriteRenderer dotSprite;

		private Transform circle;

		private Transform dot;

		private Vector3 myOriginalScale;

		private Vector3 myDstScale;

		private Color circleOriginalColor;

		private bool isCollision;

		private bool isCenterCollision;

		private bool isActive;

		private Sequence sequence;

		private void Awake()
		{
			this.isGamePlaying = true;
			this.isActive = true;
			this.isCollision = false;
			this.isCenterCollision = false;
			this.circle = this.circleSprite.transform;
			this.dot = this.dotSprite.transform;
			this.circleOriginalColor = this.circleSprite.color;
			this.myOriginalScale = new Vector3(this.defaultScale, this.defaultScale, this.defaultScale);
			this.myDstScale = this.dstScale;
		}

		private new void OnEnable()
		{
			base.OnEnable();
			if (this.sequence != null)
			{
				this.sequence.Kill(false);
				this.sequence = null;
			}
			this.isActive = true;
			this.isCollision = false;
			this.isCenterCollision = false;
			this.circle.localScale = this.myOriginalScale;
			this.circleSprite.color = this.circleOriginalColor;
		}

		private new void OnDisable()
		{
			base.OnDisable();
		}

		public override void CheckCustom()
		{
			if (this.isActive && (this.isCollision || this.isCenterCollision) && base.playerTransform.transform.position.z + 1f > base.transform.position.z)
			{
				this.isActive = false;
				EventManager.DoCheckPerfectJump(AnimStatus.end);
			}
		}

		public override void OnCenterCollisionWithPlayer()
		{
			if (this.isCenterCollision)
			{
				return;
			}
			this.isCenterCollision = true;
			EventManager.DOAddOnePerfect(base.gameObject);
			this.sequence = DOTween.Sequence();
			this.sequence.Append(this.circle.DOScale(this.myDstScale, 0.5f));
			this.sequence.Insert(0f, this.circleSprite.DOFade(0f, 0.5f));
		}

		public override void OnCollisionWithPlayer()
		{
			if (this.isCollision)
			{
				return;
			}
			this.isCollision = true;
			EventManager.DOAddOnePoint(base.gameObject);
		}

		public override void IsOutOfScreen()
		{
			EventManager.DODesactivatePointTrigger(base.gameObject);
		}
	}
}
