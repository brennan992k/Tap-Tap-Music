using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class UIVedioBuyGem : MonoBehaviour
	{
		private sealed class _GetVedioGem_c__AnonStorey0
		{
			//internal AdsManager adsManager;

			internal UIVedioBuyGem _this;

			internal void __m__0(bool state)
			{
				if (state)
				{
					this._this.GetVedioGemSuccess();
					//this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuyGemSucc.ToString());
				}
			}
		}

		public Text textVedioBuyGem;

		public Button adsBtn;

		public UIBuySuccess panelBuySuccess;

		private void OnEnable()
		{
			int showVedioTime = this.GetShowVedioTime();
			this.textVedioBuyGem.text = "×" + PlayerPrefsManager.VEDIO_REWARD_GEM[showVedioTime];
		}

		public void GetVedioGem()
		{
			this.panelBuySuccess.gameObject.SetActive(false);
		//AdsManager adsManager = AdsManager.Instance;
		//	adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuyGem.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						this.GetVedioGemSuccess();
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuyGemSucc.ToString());
					}
				});
				*/
                AdsControl.Instance.PlayDelegateRewardVideo(delegate
                {

                    this.GetVedioGemSuccess();

                });
            }
			else
			{
				EventManager.DoShowNotice("No Ads");
			}
		}

		private void GetVedioGemSuccess()
		{
			int showVedioTime = this.GetShowVedioTime();
			int num = PlayerPrefsManager.VEDIO_REWARD_GEM[showVedioTime];
			PlayerPrefsManager.SetGold(num + PlayerPrefsManager.GetGold());
		//AdsManager.Instance.FaceBookCoin(true, num, PlayerPrefsManager.GetGold(), "free vedio");
			PlayerPrefsManager.SetShowVedioTime(showVedioTime + 1);
			showVedioTime = this.GetShowVedioTime();
			this.textVedioBuyGem.text = "×" + PlayerPrefsManager.VEDIO_REWARD_GEM[showVedioTime];
			this.panelBuySuccess.ShowNotice(I18NManager.Instance.GetValue("PurchSucc"));
			this.panelBuySuccess.gameObject.SetActive(true);
			EventManager.DoRefreshUIStatus();
		}

		private int GetShowVedioTime()
		{
			int num = PlayerPrefsManager.GetShowVedioTime();
			if (num >= PlayerPrefsManager.VEDIO_REWARD_GEM.Length)
			{
				num = 0;
				PlayerPrefsManager.SetShowVedioTime(num);
			}
			return num;
		}

		public void ButtonClose()
		{
			base.gameObject.SetActive(false);
			this.panelBuySuccess.gameObject.SetActive(false);
		}
	}
}
