using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class GameIntro : MonoBehaviour
	{
		public GameObject uipanelFinger;

		public GameObject panelGuidText;

		public GameObject startGuid;

		public GameObject endGuid;

		public GameObject revive;

		public bool isRevive;

		private void OnEnable()
		{
			EventManager.OnPauseForGuid += new EventManager.PauseForGuid(this.OnPauseForGuid);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			this.isRevive = false;
		}

		private void OnDisable()
		{
			EventManager.OnPauseForGuid -= new EventManager.PauseForGuid(this.OnPauseForGuid);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
		}

		private void OnAnimFailEvent(AnimFail status)
		{
			if (status == AnimFail.start)
			{
				this.uipanelFinger.gameObject.SetActive(false);
			}
		}

		private void OnPauseForGuid(AnimStatus status)
		{
			if (status == AnimStatus.start)
			{
				this.uipanelFinger.gameObject.SetActive(true);
			}
			else
			{
				this.uipanelFinger.gameObject.SetActive(false);
			}
		}

		public void DisAllGuidText()
		{
			this.startGuid.gameObject.SetActive(false);
			this.endGuid.gameObject.SetActive(false);
			this.revive.gameObject.SetActive(false);
		}

		public void ShowStart()
		{
			this.DisAllGuidText();
			if (this.isRevive)
			{
				this.isRevive = false;
				this.revive.gameObject.SetActive(true);
			}
			else
			{
				this.startGuid.gameObject.SetActive(true);
			}
		}

		public void ShowEnd()
		{
			this.DisAllGuidText();
			this.endGuid.gameObject.SetActive(true);
		}
	}
}
