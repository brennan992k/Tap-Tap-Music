using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonTransitionAnimation : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		public UnityEvent Onclicked;

		private bool isClicked;

		private float sizeDefault;

		private float sizeUp
		{
			get
			{
				return this.sizeDefault * 1.2f;
			}
		}

		private float sizeDown
		{
			get
			{
				return this.sizeDefault * 0.8f;
			}
		}

		private void Awake()
		{
			this.sizeDefault = base.transform.localScale.x;
		}

		private void OnEnable()
		{
			this.isClicked = false;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.isClicked)
			{
				return;
			}
			this.isClicked = true;
			base.transform.DOKill(false);
			base.transform.DOScale(Vector3.one * this.sizeDown, 0.1f).OnComplete(delegate
			{
				base.transform.DOScale(Vector3.one * this.sizeDefault, 0.1f).OnComplete(delegate
				{
					if (this.Onclicked != null)
					{
						this.Onclicked.Invoke();
					}
					this.isClicked = false;
				});
			});
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.isClicked)
			{
				return;
			}
			base.transform.DOKill(false);
			base.transform.DOScale(Vector3.one * this.sizeUp, 0.3f);
			this.isClicked = false;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.isClicked)
			{
				return;
			}
			base.transform.DOKill(false);
			base.transform.DOScale(Vector3.one * this.sizeDefault, 0.3f);
			this.isClicked = false;
		}
	}
}
