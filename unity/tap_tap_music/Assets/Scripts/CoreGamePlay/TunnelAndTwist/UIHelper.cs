using System;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class UIHelper : MonoBehaviour
	{
		public Text text;

		public void Awake()
		{
			if (this.text == null)
			{
				this.text = base.GetComponent<Text>();
			}
		}

		public void ActivateText(bool activate)
		{
			if (this.text)
			{
				this.text.gameObject.SetActive(activate);
			}
		}
	}
}
