using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class SetUIElementPosition : MonoBehaviour
	{
		public UIPosition position;

		public Camera UICam;

		public RectTransform rectTransform;

		private void Awake()
		{
			this.UICam = base.GetComponentInParent<Camera>();
			this.rectTransform = base.GetComponent<RectTransform>();
		}

		private void Start()
		{
			if (this.position == UIPosition.topLeft)
			{
				Vector3 a = this.UICam.ViewportToWorldPoint(new Vector3(0f, 1f, base.transform.position.z));
				base.transform.position = a + new Vector3(0.4f, -0.8f, 0f);
			}
			if (this.position == UIPosition.topRight)
			{
				Vector3 a2 = this.UICam.ViewportToWorldPoint(new Vector3(1f, 1f, base.transform.position.z));
				base.transform.position = a2 + new Vector3(-0.4f, -0.8f, 0f);
			}
		}

		private void DOIt()
		{
			Vector2 v = base.gameObject.transform.position;
			Vector2 vector = Camera.main.WorldToViewportPoint(v);
			this.rectTransform.anchorMin = vector;
			this.rectTransform.anchorMax = vector;
		}
	}
}
