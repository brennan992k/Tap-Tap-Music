using System;
using System.Collections.Generic;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class SelectBallList : MonoBehaviour
	{
		public Transform scrollContent;

		public Transform ballItem;

		public int ballCount = 15;

		private List<SelectBallItem> ballItems;

		private void Awake()
		{
			this.ballItems = new List<SelectBallItem>();
			SelectBallItem component = this.ballItem.GetComponent<SelectBallItem>();
			this.ballItems.Add(component);
			component.SetBallIndex(0);
			this.ballItem.gameObject.SetActive(false);
		}

		private void Start()
		{
			for (int i = 1; i < this.ballCount; i++)
			{
				Transform transform = UnityEngine.Object.Instantiate<Transform>(this.ballItem.transform, this.scrollContent);
				SelectBallItem component = transform.GetComponent<SelectBallItem>();
				component.SetBallIndex(i);
				component.gameObject.SetActive(true);
				this.ballItems.Add(component);
			}
			this.ballItem.gameObject.SetActive(true);
		}
	}
}
