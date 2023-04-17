using System;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class SelectBallItem : MonoBehaviour
	{
		private enum BallStatus
		{
			selected,
			idle,
			colsed
		}

		public TextureRotate textureRotate;

		public GameObject imgSelected;

		public GameObject[] buttons;

		public Text textCost;

		private int ballId;

		public void SetBallIndex(int index)
		{
			this.ballId = index;
			this.textureRotate.SetModelId(this.ballId);
		}

		private void RefreshItem()
		{
			int num = this.ballId;
			this.imgSelected.SetActive(false);
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].SetActive(false);
			}
			int selectedBallId = PlayerPrefsManager.GetSelectedBallId();
			bool flag = 1 == PlayerPrefsManager.GetIsGotBall(num);
			if (num == selectedBallId)
			{
				this.imgSelected.SetActive(true);
				this.buttons[0].SetActive(true);
			}
			else if (flag)
			{
				this.buttons[1].SetActive(true);
			}
			else
			{
				this.buttons[2].SetActive(true);
			}
			int ballCost = this.GetBallCost();
			this.textCost.text = ballCost.ToString();
		}

		public int GetBallCost()
		{
			int num = this.ballId / 5;
			int num2 = PlayerPrefsManager.BUY_NEW_BALL_COST.Length;
			return PlayerPrefsManager.BUY_NEW_BALL_COST[(num >= num2) ? (num2 - 1) : num];
		}

		public void BuyNewBall()
		{
			int gold = PlayerPrefsManager.GetGold();
			int ballCost = this.GetBallCost();
			if (gold >= ballCost)
			{
				int num = gold - ballCost;
				PlayerPrefsManager.SetIsGotBall(this.ballId);
				PlayerPrefsManager.SetGold(num);
			//AdsManager instance = AdsManager.Instance;
			//	instance.FaceBookCoin(false, ballCost, num, "buy new ball");
				EventManager.DoRefreshUIStatus();
				this.SelectedBall();
			//	instance.UmEvent(UMConstants.umGem, UMConstants.UMGemConstantsId.ListBuyBall.ToString());
			}
			else
			{
				EventManager.DoShowShop();
			}
		}

		private void OnSelectBallEvent(int ballId)
		{
			if (PlayerPrefsManager.GetIsGotBall(this.ballId) == 1)
			{
				this.buttons[0].SetActive(false);
				this.buttons[1].SetActive(true);
				this.buttons[2].SetActive(false);
				this.imgSelected.SetActive(false);
			}
		}

		public void SelectedBall()
		{
			EventManager.DOSelectBallEvent(this.ballId);
			this.buttons[0].SetActive(true);
			this.buttons[1].SetActive(false);
			this.imgSelected.SetActive(true);
		}

		private void OnEnable()
		{
			EventManager.OnSelectBallEvent += new EventManager.SelectBallEvent(this.OnSelectBallEvent);
			this.RefreshItem();
		}

		private void OnDisable()
		{
			EventManager.OnSelectBallEvent -= new EventManager.SelectBallEvent(this.OnSelectBallEvent);
		}
	}
}
