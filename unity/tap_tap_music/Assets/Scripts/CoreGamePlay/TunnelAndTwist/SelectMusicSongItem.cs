using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class SelectMusicSongItem : MonoBehaviour
	{
		private sealed class _AdsOpenMusic_c__AnonStorey0
		{
//			internal AdsManager adsManager;

			internal SelectMusicSongItem _this;

			internal void __m__0(bool state)
			{
				if (state)
				{
					//this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.ListAdsBuySongSucc.ToString());
					this._this.AdsOpenMusicSuccess();
				}
			}
		}

		public MaterialManager materialManager;

		public Image imgBgIcon;

		public Image imgIcon;

		public ArtNumText txtDiffLv;

		public Image imgIconSelected;

		public GameObject panelGift;

		public GameObject panelStar;

		public Text textName;

		public Text textScore;

		public ArtNumText textId;

		public Text textSecretNeedStar;

		public Text textSecretCurStar;

		public Text textCost;

		public Image secretSlider;

		public Image[] imgStars;

		public Image[] imgGift;

		public Button btnListen;

		public Button btnPlay;

		public Button btnBuy;

		public Button btnClose;

		public Button btnAds;

		public Image imgAdsNum;

		public Font[] fonts;

		public Image[] allImages;

		public Text[] allTexts;

		private int songId;

		private void OnEnable()
		{
			EventManager.OnListeningTestEvent += new EventManager.ListeningTestEvent(this.OnListeningTestEvent);
			EventManager.OnRefreshUIStatus += new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
		}

		private void OnDisable()
		{
			EventManager.OnListeningTestEvent -= new EventManager.ListeningTestEvent(this.OnListeningTestEvent);
			EventManager.OnRefreshUIStatus -= new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
		}

		private void OnRefreshUIStatus()
		{
			this.CheckButtons();
		}

		public void CheckButtons()
		{
			this.btnAds.gameObject.SetActive(false);
			this.btnListen.gameObject.SetActive(true);
			int songStatus = PlayerPrefsManager.GetSongStatus(this.songId);
			if (songStatus == 0)
			{
				this.btnClose.gameObject.SetActive(true);
				this.btnPlay.gameObject.SetActive(false);
				this.btnBuy.gameObject.SetActive(false);
				this.btnListen.gameObject.SetActive(false);
			}
			else if (songStatus == 1)
			{
				this.btnClose.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(false);
				this.btnBuy.gameObject.SetActive(true);
			}
			else if (songStatus == 2)
			{
				this.btnClose.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(true);
				this.btnBuy.gameObject.SetActive(false);
			}
			else if (songStatus < 0)
			{
				this.btnClose.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(false);
				this.btnBuy.gameObject.SetActive(false);
				this.btnAds.gameObject.SetActive(true);
				this.imgAdsNum.sprite = this.materialManager.uiAdsNum[Mathf.Abs(songStatus) - 1];
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

		public void RefreshItem(int index)
		{
			this.songId = index;
			this.btnAds.gameObject.SetActive(false);
			MusicInfo value = MusicList.Instance.GetValue(index);
			int num = 0;
			if (value != null)
			{
				this.textCost.text = this.GetSongCost().ToString();
				this.textName.text = value.MusicName;
				this.textScore.text = value.Score.ToString();
				num = value.PassLv;
				for (int i = 0; i < this.imgStars.Length; i++)
				{
					this.imgStars[i].gameObject.SetActive(value.StarLv > i);
				}
				this.CheckButtons();
			}
			if (num > 0)
			{
				this.textId.gameObject.SetActive(false);
				this.imgIcon.gameObject.SetActive(true);
				this.txtDiffLv.text = num.ToString();
			}
			else
			{
				this.imgIcon.gameObject.SetActive(false);
				this.textId.gameObject.SetActive(true);
				this.textId.text = (this.songId + 1).ToString();
			}
			if (PlayerPrefsManager.GetSelectedSongId() == this.songId)
			{
				this.imgBgIcon.sprite = this.materialManager.musicListBg[1];
				this.imgIconSelected.gameObject.SetActive(true);
				this.textId.font = this.fonts[1];
				if (PlayerPrefsManager.WarningStartGame == 1)
				{
					ButtonWarningScale component = this.btnPlay.GetComponent<ButtonWarningScale>();
					component.enabled = true;
				}
			}
			else
			{
				this.imgBgIcon.sprite = this.materialManager.musicListBg[0];
				this.imgIconSelected.gameObject.SetActive(false);
				this.textId.font = this.fonts[0];
			}
			bool flag = (this.songId + 1) % 4 == 0;
			this.panelGift.SetActive(flag);
			this.panelStar.SetActive(!flag);
			if (value != null && flag)
			{
				int upgradeNeedStar = this.GetUpgradeNeedStar();
				this.textSecretNeedStar.text = "/ " + upgradeNeedStar.ToString();
				int num2 = 0;
				for (int j = this.songId - 3; j < this.songId; j++)
				{
					num2 += PlayerPrefsManager.GetStar(j);
				}
				this.textSecretCurStar.text = num2.ToString();
				float num3 = (float)num2 * 1f / (float)upgradeNeedStar;
				this.secretSlider.fillAmount = ((num3 <= 1f) ? num3 : 1f);
				this.imgGift[1].gameObject.SetActive(num3 >= 1f);
				this.imgGift[0].gameObject.SetActive(num3 < 1f);
				int songStatus = PlayerPrefsManager.GetSongStatus(this.songId);
				if (songStatus == 2 || songStatus < 0)
				{
					this.panelGift.SetActive(false);
					this.panelStar.SetActive(true);
				}
				else
				{
					this.textName.text = "??????????????";
				}
			}
			int musicOpenLv = PlayerPrefsManager.GetMusicOpenLv();
			if (this.songId >= (musicOpenLv + 1) * 4)
			{
				this.textScore.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(false);
				this.btnBuy.gameObject.SetActive(false);
				this.btnClose.gameObject.SetActive(true);
				for (int k = 0; k < this.imgStars.Length; k++)
				{
					this.imgStars[k].gameObject.SetActive(true);
				}
				this.SetGray();
			}
		}

		public int GetUpgradeNeedStar()
		{
			int num = this.songId / 4;
			int num2 = PlayerPrefsManager.UpgradeStar.Length;
			return PlayerPrefsManager.UpgradeStar[(num >= num2) ? (num2 - 1) : num];
		}

		private void OnListeningTestEvent(int index)
		{
			this.imgIconSelected.gameObject.SetActive(false);
			this.imgBgIcon.sprite = this.materialManager.musicListBg[0];
			this.textId.font = this.fonts[0];
		}

		public void SelectMusic()
		{
			this.ListeningTest();
			EventManager.DOSelectSongEvent(this.songId);
		}

		public void ListeningTest()
		{
			EventManager.DOListeningTestEvent(this.songId);
			PlayerPrefsManager.SetSelectedSongId(this.songId);
			this.imgIconSelected.gameObject.SetActive(true);
			this.imgBgIcon.sprite = this.materialManager.musicListBg[1];
			this.textId.font = this.fonts[1];
		}

		private void SetGray()
		{
			for (int i = 0; i < this.allImages.Length; i++)
			{
				this.allImages[i].material = this.materialManager.uiGrayMaterial;
			}
			for (int j = 0; j < this.allTexts.Length; j++)
			{
				this.allTexts[j].color = this.materialManager.textGrayColor;
			}
		}

		public void ShowSecretSong()
		{
			bool flag = (this.songId + 1) % 4 == 0;
			this.panelGift.SetActive(flag);
			this.panelStar.SetActive(!flag);
		}

		public void BtnClosedShake(GameObject obj)
		{
			obj.transform.DOKill(false);
			Sequence s = DOTween.Sequence();
			s.Append(obj.transform.DORotate(new Vector3(0f, 0f, -10f), 0.05f, RotateMode.Fast));
			s.Append(obj.transform.DORotate(new Vector3(0f, 0f, 10f), 0.1f, RotateMode.Fast));
			s.Append(obj.transform.DORotate(new Vector3(0f, 0f, -10f), 0.1f, RotateMode.Fast));
			s.Append(obj.transform.DORotate(new Vector3(0f, 0f, 0f), 0.05f, RotateMode.Fast));
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
			int songCost = this.GetSongCost();
			if (gold >= songCost)
			{
				int num = gold - songCost;
				PlayerPrefsManager.SetSongStatus(this.songId, 2);
				PlayerPrefsManager.SetGold(num);
				//AdsManager instance = AdsManager.Instance;
				//instance.FaceBookCoin(false, songCost, num, "buy new song");
				EventManager.DoRefreshUIStatus();
				this.btnBuy.gameObject.SetActive(false);
				this.btnPlay.gameObject.SetActive(true);
			//instance.UmEvent(UMConstants.umGem, UMConstants.UMGemConstantsId.ListBuySong.ToString());
			}
			else
			{
				EventManager.DoShowShop();
			}
		}

		public void AdsOpenMusic()
		{
			//AdsManager adsManager = AdsManager.Instance;
			//adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.ListAdsBuySong.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.ListAdsBuySongSucc.ToString());
						this.AdsOpenMusicSuccess();
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
				}
				else
				{
					PlayerPrefsManager.SetSongStatus(this.songId, num);
				}
			}
			this.CheckButtons();
		}
	}
}
