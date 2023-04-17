using DG.Tweening;
using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class SpeedAddText : UIHelper
	{
		public void OnEnable()
		{
			EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.AnimFailEvent);
			EventManager.OnSetAddSpeed += new EventManager.SetAddSpeed(this.OnSetAddSpeed);
		}

		private void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.AnimFailEvent);
			EventManager.OnSetAddSpeed -= new EventManager.SetAddSpeed(this.OnSetAddSpeed);
		}

		private void Start()
		{
			this.Reset();
		}

		public void OnSetAddSpeed(AddSpeed speed)
		{
			string str = "+";
			if (speed.sign == -1)
			{
				str = "-";
			}
			this.text.text = str + speed.decrease.ToString("F1");
			this.Reset();
			this.text.DOFade(1f, 0.05f).OnComplete(delegate
			{
				this.text.rectTransform.DOScale(Vector2.one * 5f, 0.05f).OnComplete(delegate
				{
					this.text.rectTransform.DOScale(Vector2.one, 0.3f).SetDelay(0.1f);
				});
				this.text.rectTransform.DOLocalMoveZ(-20f, 0.05f, false).OnComplete(delegate
				{
					this.text.rectTransform.DOLocalMoveZ(0f, 0.3f, false).SetDelay(0.1f).OnComplete(delegate
					{
						this.text.DOFade(0f, 0.1f).SetDelay(0.3f).OnComplete(new TweenCallback(this.Reset));
					});
				});
			});
		}

		private void Reset()
		{
			this.text.DOKill(false);
			this.text.rectTransform.DOKill(false);
			this.text.rectTransform.localScale = Vector3.one;
			RectTransform rectTransform = this.text.rectTransform;
			Vector3 localPosition = rectTransform.localPosition;
			localPosition.z = 0f;
			this.text.rectTransform.localPosition = localPosition;
			Color color = this.text.color;
			color.a = 0f;
			this.text.color = color;
		}

		public void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			base.ActivateText(true);
			Color color = this.text.color;
			color.a = 0f;
			this.text.color = color;
		}

		public void AnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.end)
			{
				EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.AnimFailEvent);
				base.ActivateText(false);
			}
		}
	}
}
