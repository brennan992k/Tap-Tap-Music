using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class UINewSong : MonoBehaviour
	{
		private sealed class _DoubleReward_c__AnonStorey0
		{
			//internal AdsManager adsManager;

			internal UINewSong _this;

			internal void __m__0(bool state)
			{
				if (state)
				{
					this._this.DoubleRewardSuccess();
				//this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.UpgradeAdsDoubleSucc.ToString());
				}
			}
		}

		public MaterialManager materialManager;

		public Text[] textNames;

		public Text[] textIds;

		public Text textLv;

		public Text textGem;

		public Text textExp;

		public Button btnDouble;

		public Button btnCloseShow;

		public Button btnCloseHide;

		private void OnEnable()
		{
			int musicOpenLv = PlayerPrefsManager.GetMusicOpenLv();
			int num = musicOpenLv * 4;
			int num2 = num + 3;
			if (musicOpenLv > 0)
			{
				PlayerPrefsManager.SetNewMusicLv(0);
				int i = num;
				int num3 = 0;
				while (i < num2)
				{
					MusicInfo value = MusicList.Instance.GetValue(i);
					if (value != null)
					{
						this.textNames[num3].text = value.MusicName;
						this.textIds[num3].text = i.ToString();
					}
					i++;
					num3++;
				}
				this.textLv.text = (musicOpenLv + 1).ToString();
				this.textGem.text = PlayerPrefsManager.GetUpgradeGem().ToString();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
			this.btnCloseShow.gameObject.SetActive(false);
			this.btnCloseHide.gameObject.SetActive(true);
		}

		public void DoubleReward()
		{
		//AdsManager adsManager = AdsManager.Instance;
		//	adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.UpgradeAdsDouble.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						this.DoubleRewardSuccess();
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.UpgradeAdsDoubleSucc.ToString());
					}
				});
				*/
                AdsControl.Instance.PlayDelegateRewardVideo(delegate
                {

                    this.DoubleRewardSuccess();

                });
            }
			else
			{
				EventManager.DoShowNotice("No Ads");
			}
		}

		private void DoubleRewardSuccess()
		{
			int upgradeGem = PlayerPrefsManager.GetUpgradeGem();
			PlayerPrefsManager.SetGold(PlayerPrefsManager.GetGold() + upgradeGem);
			//AdsManager.Instance.FaceBookCoin(true, upgradeGem, PlayerPrefsManager.GetGold(), "double reward (new song)");
			this.textGem.text = (upgradeGem * 2).ToString();
			this.btnDouble.image.material = this.materialManager.uiGrayMaterial;
			this.btnDouble.enabled = false;
			EventManager.DoRefreshUIStatus();
			this.btnCloseShow.gameObject.SetActive(true);
			this.btnCloseHide.gameObject.SetActive(false);
		}

		public void AdsClose()
		{
			this.AdsCloseSuccess();
		}

		private void AdsCloseSuccess()
		{
			base.gameObject.SetActive(false);
		}
	}
}
