using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class UISecretBall : MonoBehaviour
	{
		private sealed class _Show3DModel_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal UISecretBall _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _Show3DModel_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(0.001f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.textureRotate.Show3DModel(this._this.ballId);
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _AdsOpenBall_c__AnonStorey1
		{
		//	internal AdsManager adsManager;

			internal UISecretBall _this;

			internal void __m__0(bool state)
			{
				if (state)
				{
					this._this.AdsOpenBallSuccess();
					//this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuyBallSucc.ToString());
				}
			}
		}

		public TextureRotate textureRotate;

		public int ballId;

		public Button btnAds;

		public Button btnBuy;

		public Button btnPlay;

		public UIBuySuccess panelBuySuccess;

		public Text textTitle;

		public Text textPrice;

		public Text textOriginalPrice;

		public Color textColor;

		private int price;

		public int GetBallCost()
		{
			int num = this.ballId / 5;
			int num2 = PlayerPrefsManager.BUY_NEW_BALL_COST.Length;
			return PlayerPrefsManager.BUY_NEW_BALL_COST[(num >= num2) ? (num2 - 1) : num];
		}

		private void OnRefreshUIStatus()
		{
			int gold = PlayerPrefsManager.GetGold();
			if (gold < this.price)
			{
				this.textPrice.color = Color.red;
			}
			else
			{
				this.textPrice.color = this.textColor;
			}
		}

		private void OnDisable()
		{
			EventManager.OnRefreshUIStatus -= new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
		}

		private void OnEnable()
		{
			EventManager.OnRefreshUIStatus += new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
			this.panelBuySuccess.gameObject.SetActive(false);
			base.StartCoroutine(this.Show3DModel());
			if (this.ballId != 0)
			{
				this.price = this.GetBallCost();
				this.textOriginalPrice.text = I18NManager.Instance.GetValue("OriginalPrice") + this.price.ToString();
				this.price = (int)((float)this.price * 0.8f);
				this.textPrice.text = this.price.ToString();
				this.OnRefreshUIStatus();
				if (PlayerPrefsManager.GetIsGotBall(this.ballId) == 1)
				{
					this.btnPlay.gameObject.SetActive(false);
					this.btnAds.gameObject.SetActive(false);
					this.btnBuy.gameObject.SetActive(false);
				}
				else
				{
					this.btnPlay.gameObject.SetActive(false);
					this.btnAds.gameObject.SetActive(false);
					this.btnBuy.gameObject.SetActive(false);
					if (UnityEngine.Random.Range(0, 2) == 0 && AdsControl.Instance.GetRewardAvailable())
					{
						this.textTitle.text = I18NManager.Instance.GetValue("UnlockNewBall");
						this.btnAds.gameObject.SetActive(true);
						this.textOriginalPrice.gameObject.SetActive(false);
					}
					else
					{
						this.textTitle.text = I18NManager.Instance.GetValue("Discount");
						this.btnBuy.gameObject.SetActive(true);
						this.textOriginalPrice.gameObject.SetActive(true);
					}
				}
			}
			else
			{
				this.btnPlay.gameObject.SetActive(true);
				this.btnAds.gameObject.SetActive(false);
				this.btnBuy.gameObject.SetActive(false);
			}
		}

		private IEnumerator Show3DModel()
		{
			UISecretBall._Show3DModel_c__Iterator0 _Show3DModel_c__Iterator = new UISecretBall._Show3DModel_c__Iterator0();
			_Show3DModel_c__Iterator._this = this;
			return _Show3DModel_c__Iterator;
		}

		public void ButtonClose()
		{
			base.gameObject.SetActive(false);
			this.panelBuySuccess.gameObject.SetActive(false);
		}

		public void SelectBall()
		{
			EventManager.DOSelectBallEvent(this.ballId);
			this.ButtonClose();
		}

		public void AdsOpenBall()
		{
		//AdsManager adsManager = AdsManager.Instance;
		//	adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuyBall.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
                AdsControl.Instance.PlayDelegateRewardVideo(delegate
                {


                    this.AdsOpenBallSuccess();

                });
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						this.AdsOpenBallSuccess();
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuyBallSucc.ToString());
					}
				});
				*/
            }
			else
			{
				EventManager.DoShowNotice("No Ads");
			}
		}

		private void AdsOpenBallSuccess()
		{
			PlayerPrefsManager.SetIsGotBall(this.ballId);
			this.btnAds.gameObject.SetActive(false);
			this.btnPlay.gameObject.SetActive(true);
			EventManager.DoRefreshUIStatus();
			this.panelBuySuccess.ShowNotice(I18NManager.Instance.GetValue("UnlockSucc"));
			this.panelBuySuccess.gameObject.SetActive(true);
		}

		public void BuyNewBall()
		{
			int gold = PlayerPrefsManager.GetGold();
			if (gold >= this.price)
			{
				PlayerPrefsManager.SetIsGotBall(this.ballId);
				PlayerPrefsManager.SetGold(gold - this.price);
				//AdsManager.Instance.FaceBookCoin(false, this.price, PlayerPrefsManager.GetGold(), "buy new ball");
				EventManager.DoRefreshUIStatus();
				this.btnBuy.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(true);
				this.panelBuySuccess.ShowNotice(I18NManager.Instance.GetValue("PurchSucc"));
				this.panelBuySuccess.gameObject.SetActive(true);
				//AdsManager.Instance.UmEvent(UMConstants.umGem, UMConstants.UMGemConstantsId.BoxBuyBall.ToString());
			}
			else
			{
				EventManager.DoGoldNotEnough();
			}
		}
	}
}
