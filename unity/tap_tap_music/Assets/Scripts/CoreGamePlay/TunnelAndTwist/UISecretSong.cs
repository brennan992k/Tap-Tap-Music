using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class UISecretSong : MonoBehaviour
	{
		private sealed class _AdsOpenMusic_c__AnonStorey0
		{
		//internal AdsManager adsManager;

			internal UISecretSong _this;

			internal void __m__0(bool state)
			{
				if (state)
				{
					this._this.AdsOpenMusicSuccess();
					//this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuySongSucc.ToString());
				}
			}
		}

		public int songId;

		public Text textName;

		public Image imgBg;

		public Text textAdsNum;

		public Button btnAds;

		public Button btnBuy;

		public Button btnPlay;

		public Sprite[] spBg;

		public UIBuySuccess panelBuySuccess;

		public Text newSongCost;

		public Text textTitle;

		public Color textColor;

		private int price;

		private void OnRefreshUIStatus()
		{
			int gold = PlayerPrefsManager.GetGold();
			if (gold < this.price)
			{
				this.newSongCost.color = Color.red;
			}
			else
			{
				this.newSongCost.color = this.textColor;
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
			if (this.songId != 0)
			{
				MusicInfo value = MusicList.Instance.GetValue(this.songId);
				if (value != null)
				{
					this.textName.text = value.MusicName;
				}
				int num = this.songId / 4;
				if (num >= this.spBg.Length)
				{
					num = this.spBg.Length - 1;
				}
				this.imgBg.sprite = this.spBg[num];
				int songStatus = PlayerPrefsManager.GetSongStatus(this.songId);
				if (songStatus == 2)
				{
					this.btnPlay.gameObject.SetActive(true);
					this.btnAds.gameObject.SetActive(false);
					this.btnBuy.gameObject.SetActive(false);
				}
				else
				{
					this.btnPlay.gameObject.SetActive(false);
					this.btnAds.gameObject.SetActive(false);
					this.btnBuy.gameObject.SetActive(false);
					bool flag = (this.songId + 1) % 4 == 0;
					int songCost = this.GetSongCost();
					if (flag)
					{
						this.textTitle.text = I18NManager.Instance.GetValue("HiddenTracks");
						this.price = songCost * 2;
						this.btnAds.gameObject.SetActive(true);
						this.textAdsNum.text = Mathf.Abs(songStatus) + "/3";
					}
					else
					{
						this.textTitle.text = I18NManager.Instance.GetValue("NewTracks");
						this.price = songCost;
						this.btnBuy.gameObject.SetActive(true);
					}
					this.newSongCost.text = this.price.ToString();
					this.OnRefreshUIStatus();
				}
			}
			else
			{
				this.btnPlay.gameObject.SetActive(true);
				this.btnAds.gameObject.SetActive(false);
				this.btnBuy.gameObject.SetActive(false);
			}
		}

		public int GetSongCost()
		{
			int num = this.songId / 4;
			int num2 = PlayerPrefsManager.BUY_NEW_SONG_COST.Length;
			if (num >= num2)
			{
				num = num2 - 1;
			}
			return PlayerPrefsManager.BUY_NEW_SONG_COST[num];
		}

		public void ButtonClose()
		{
			base.gameObject.SetActive(false);
			this.panelBuySuccess.gameObject.SetActive(false);
		}

		public void SelectMusic()
		{
			EventManager.DOSelectSongEvent(this.songId);
		}

		public void AdsOpenMusic()
		{
			//AdsManager adsManager = AdsManager.Instance;
		//adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuySong.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						this.AdsOpenMusicSuccess();
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.BoxAdsBuySongSucc.ToString());
					}
				});
				*/
                AdsControl.Instance.PlayDelegateRewardVideo(delegate
                {

                    this.AdsOpenMusicSuccess();

                });

            }
			else
			{
				EventManager.DoShowNotice("No Ads");
			}
		}

		private void AdsOpenMusicSuccess()
		{
			int num = PlayerPrefsManager.GetSongStatus(this.songId);
			if (num < 0)
			{
				num++;
				if (num == 0)
				{
					PlayerPrefsManager.SetSongStatus(this.songId, 2);
					this.btnAds.gameObject.SetActive(false);
					this.btnPlay.gameObject.SetActive(true);
					this.panelBuySuccess.ShowNotice(I18NManager.Instance.GetValue("UnlockSucc"));
					this.panelBuySuccess.gameObject.SetActive(true);
				}
				else
				{
					PlayerPrefsManager.SetSongStatus(this.songId, num);
					this.textAdsNum.text = Mathf.Abs(num) + "/3";
				}
				EventManager.DoRefreshUIStatus();
			}
			if (PlayerPrefsManager.GetSongStatus(this.songId) == 2 && SceneManager.GetActiveScene().name == "Game")
			{
				this.SelectMusic();
			}
		}

		public void BuyNewSong()
		{
			if (PlayerPrefsManager.GetSongStatus(this.songId) == 2)
			{
				this.btnBuy.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(true);
				return;
			}
			int gold = PlayerPrefsManager.GetGold();
			if (gold >= this.price)
			{
				PlayerPrefsManager.SetSongStatus(this.songId, 2);
				PlayerPrefsManager.SetGold(gold - this.price);
				//AdsManager.Instance.FaceBookCoin(false, this.price, PlayerPrefsManager.GetGold(), "buy new song");
				EventManager.DoRefreshUIStatus();
				this.btnBuy.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(true);
				this.panelBuySuccess.ShowNotice(I18NManager.Instance.GetValue("PurchSucc"));
				this.panelBuySuccess.gameObject.SetActive(true);
				//AdsManager.Instance.UmEvent(UMConstants.umGem, UMConstants.UMGemConstantsId.BoxBuySong.ToString());
				if (PlayerPrefsManager.GetSongStatus(this.songId) == 2 && SceneManager.GetActiveScene().name == "Game")
				{
					this.SelectMusic();
				}
			}
			else
			{
				EventManager.DoGoldNotEnough();
			}
		}
	}
}
