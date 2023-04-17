using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class UIGameManager : MonoBehaviour
	{
		private sealed class _HideUpgradeSpeed_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal UIGameManager _this;

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

			public _HideUpgradeSpeed_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(2f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.panelUpSpeed.SetActive(false);
					this._this.gameIntro.DisAllGuidText();
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

		private sealed class _RefreshStar_c__AnonStorey1
		{
			internal int i;

			internal UIGameManager _this;

			internal void __m__0()
			{
				this._this.imgStarsLight[this.i].gameObject.SetActive(false);
			}
		}

		private sealed class _DoubleReward_c__AnonStorey2
		{
			//internal AdsManager adsManager;

			internal UIGameManager _this;

			internal void __m__0(bool state)
			{
				if (state)
				{
					this._this.DoubleRewardSuccess();
					//this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.OverAdsDoubleSucc.ToString());
				}
			}
		}

		private sealed class _AdsReward_c__AnonStorey3
		{
			//internal AdsManager adsManager;

			internal UIGameManager _this;

			internal void __m__0(bool state)
			{
				if (state)
				{
					this._this.AdsRewardSucc();
					///this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.OverAdsGemSucc.ToString());
				}
			}
		}

		public GameManager gameManager;

		public GameIntro gameIntro;

		[Header("游戏")]
		public GameObject panelGame;

		public GameObject panelWin;

		public GameObject panelRevive;

		public GameObject panelGameOver;

		public GameObject panelUpSpeed;

		public GameObject panelPerfect;

		public GameObject panelLearning;

		public GameObject panelGem;

		public GameObject panelTapTostart;

		public GameObject buttonSkipGuid;

		[Header("弹出界面")]
		public GameObject panelNewSong;

		public UISecretSong panelSecretSong;

		public UIVedioBuyGem panelBuyGem;

		public Button btnGem;

		[Header("速度提升")]
		public Text textUpspeed;

		public ArtNumText textUpSpeedLv;

		[Header("combo count")]
		public Text perfectCount;

		public Animator perfectAnimation;

		[Header("星星")]
		public Image[] imgStars;

		public Image[] imgStarsLight;

		public Image imgSpeedLv;

		public ArtNumText textSpeedLv;

		public Sprite[] spriteSpeedScale;

		public ArtNumText textScore;

		[Header("货币")]
		public Text txtGold;

		private bool isSwitchScene;

		private Vector3 btnGemPosition;

		private void Start()
		{
			this.btnGemPosition = this.btnGem.transform.localPosition;
			this.panelUpSpeed.SetActive(false);
			this.HideAllPanel();
			this.HideAllStars();
			this.ShowGame();
			this.disTapToStart();
			this.RefreshGold();
		}

		private void HideAllPanel()
		{
			this.panelGame.gameObject.SetActive(false);
			this.panelWin.gameObject.SetActive(false);
			this.panelRevive.gameObject.SetActive(false);
			this.panelGameOver.gameObject.SetActive(false);
			this.panelNewSong.gameObject.SetActive(false);
			this.panelGem.gameObject.SetActive(false);
			this.buttonSkipGuid.gameObject.SetActive(false);
			this.gameIntro.DisAllGuidText();
		}

		public void ShowGame()
		{
			if (this.panelGame.activeSelf)
			{
				return;
			}
			this.HideAllPanel();
			this.panelPerfect.gameObject.SetActive(false);
			this.panelGame.SetActive(true);
            AdsControl.Instance.ShowBanner();
		}

		public void showTapToStart()
		{
			this.panelTapTostart.SetActive(true);
			if (this.gameManager.IsGuid())
			{
				this.gameIntro.ShowStart();
			}
		}

		public void disTapToStart()
		{
			this.panelTapTostart.SetActive(false);
			this.gameIntro.DisAllGuidText();
		}

		public void ShowGameOver()
		{
			if (this.panelGameOver.activeSelf)
			{
				return;
			}
			this.HideAllPanel();
			this.panelGameOver.SetActive(true);
			this.panelGem.gameObject.SetActive(true);
		}

		public void ShowGameWin()
		{
			if (this.panelWin.activeSelf)
			{
				return;
			}
			this.HideAllPanel();
			this.panelWin.SetActive(true);
			this.panelGem.gameObject.SetActive(true);
		}

		public void ShowRevive()
		{
			if (this.panelRevive.activeSelf)
			{
				return;
			}
			this.HideAllPanel();
			this.panelRevive.SetActive(true);
		}

		public void ShowUpSpeed()
		{
			if (this.panelUpSpeed.activeSelf)
			{
				return;
			}
			this.panelUpSpeed.SetActive(true);
			this.NextSpeedLv();
		}

		private void OnEnable()
		{
			EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimWinEvent += new EventManager.AnimWinEvent(this.OnAnimWinEvent);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnBackHomeEvent += new EventManager.BackHomeEvent(this.OnBackHomeEvent);
			EventManager.OnAnimGameOverEvent += new EventManager.AnimGameOverEvent(this.OnAnimGameOverEvent);
			EventManager.OnAnimReviveEvent += new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
			EventManager.OnMusicSpeedEvent += new EventManager.MusicSpeedEvent(this.OnMusicSpeedEvent);
			EventManager.OnUpSpeed += new EventManager.UpSpeed(this.OnUpSpeed);
			EventManager.OnSetComboCount += new EventManager.SetComboCount(this.OnSetComboCount);
			EventManager.OnUpgradeEvent += new EventManager.UpgradeEvent(this.OnUpgradeEvent);
			EventManager.OnAnimLearningEvent += new EventManager.AnimLearningEvent(this.OnAnimLearningEvent);
			EventManager.OnRefreshUIStatus += new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
			EventManager.OnGoldNotEnough += new EventManager.GoldNotEnough(this.OnGoldNotEnough);
			EventManager.OnSelectSongEvent += new EventManager.SelectSongEvent(this.OnSelectSong);
		}

		private void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimWinEvent -= new EventManager.AnimWinEvent(this.OnAnimWinEvent);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnBackHomeEvent -= new EventManager.BackHomeEvent(this.OnBackHomeEvent);
			EventManager.OnAnimGameOverEvent -= new EventManager.AnimGameOverEvent(this.OnAnimGameOverEvent);
			EventManager.OnAnimReviveEvent -= new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
			EventManager.OnMusicSpeedEvent -= new EventManager.MusicSpeedEvent(this.OnMusicSpeedEvent);
			EventManager.OnUpSpeed -= new EventManager.UpSpeed(this.OnUpSpeed);
			EventManager.OnSetComboCount -= new EventManager.SetComboCount(this.OnSetComboCount);
			EventManager.OnUpgradeEvent -= new EventManager.UpgradeEvent(this.OnUpgradeEvent);
			EventManager.OnAnimLearningEvent -= new EventManager.AnimLearningEvent(this.OnAnimLearningEvent);
			EventManager.OnRefreshUIStatus -= new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
			EventManager.OnGoldNotEnough -= new EventManager.GoldNotEnough(this.OnGoldNotEnough);
			EventManager.OnSelectSongEvent -= new EventManager.SelectSongEvent(this.OnSelectSong);
		}

		private void OnPlayerStartEvent()
		{
			this.ShowGame();
			this.disTapToStart();
		}

		private void OnAnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.end)
			{
				this.ShowRevive();
				//AdsManager.Instance.UmEvent(UMConstants.umFail, PlayerPrefsManager.GetGameInfo());
				//AdsManager.Instance.UmEvent(UMConstants.umFailInfo, PlayerPrefsManager.GetGameFailInfo());
			}
		}

		private void OnAnimReviveEvent(AnimStatus status)
		{
			if (status == AnimStatus.start)
			{
				this.ShowGame();
				EventManager.DOAnimReviveEvent(AnimStatus.end);
			//AdsManager.Instance.UmEvent(UMConstants.umRevive, PlayerPrefsManager.GetGameInfo());
			}
		}

		private void OnAnimWinEvent(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				this.ShowGameWin();
			//AdsManager.Instance.UmEvent(UMConstants.umWin, PlayerPrefsManager.GetGameInfo());
			}
		}

		private void OnAnimGameOverEvent()
		{
			this.ShowGameOver();
		}

		private void OnUpgradeEvent()
		{
			this.panelNewSong.SetActive(true);
			this.panelGem.gameObject.SetActive(true);
		}

		private void OnBackHomeEvent()
		{
			if (this.isSwitchScene)
			{
				return;
			}
			this.isSwitchScene = true;
			this.SwitchScene();
		}

		private void SwitchScene()
		{
			DOTween.KillAll(false);
			SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
			GC.Collect();
		}

		private void OnUpSpeed(AnimStatus animStatus)
		{
			if (animStatus == AnimStatus.start)
			{
				this.ShowUpSpeed();
			}
		}

		public void OnMusicSpeedEvent(float value)
		{
			int num = (int)((value + 0.001f - 1f) * 100f);
			this.textUpspeed.text = I18NManager.Instance.GetValue("Speedup") + num + I18NManager.Instance.GetValue("Continue");
		}

		public void NextSpeedLv()
		{
			base.StartCoroutine(this.HideUpgradeSpeed());
		}

		private IEnumerator HideUpgradeSpeed()
		{
			UIGameManager._HideUpgradeSpeed_c__Iterator0 _HideUpgradeSpeed_c__Iterator = new UIGameManager._HideUpgradeSpeed_c__Iterator0();
			_HideUpgradeSpeed_c__Iterator._this = this;
			return _HideUpgradeSpeed_c__Iterator;
		}

		public void EndGame()
		{
			if (PlayerPrefsManager.PassInterstitialTime >= 3)
			{
                this.panelUpSpeed.SetActive(false);
                EventManager.DOAnimWinEvent(AnimStatus.start);
                AdsControl.Instance.showAds();
                /*
				AdsManager.Instance.ShowInterstitial(delegate(bool state)
				{
					this.panelUpSpeed.SetActive(false);
					EventManager.DOAnimWinEvent(AnimStatus.start);
				});
				*/
			}
			else
			{
				PlayerPrefsManager.PassInterstitialTime++;
				this.panelUpSpeed.SetActive(false);
				EventManager.DOAnimWinEvent(AnimStatus.start);
			}
		}

		private void OnSetComboCount(int count)
		{
			this.panelPerfect.SetActive(true);
			this.perfectCount.text = count.ToString();
			this.perfectAnimation.Play(0);
		}

		private void OnAnimLearningEvent(AnimIntro intro)
		{
			if (intro == AnimIntro.start)
			{
				this.panelLearning.SetActive(true);
			}
			else
			{
				this.panelLearning.SetActive(false);
			}
		}

		public void RefreshScore(int score)
		{
			this.textScore.text = score.ToString();
		}

		public void RefreshStar(int star)
		{
			int i;
			for (i = 0; i < star; i++)
			{
				if (!this.imgStars[i].gameObject.activeSelf)
				{
					this.imgStars[i].gameObject.SetActive(true);
					this.imgStarsLight[i].gameObject.SetActive(true);
					this.imgStarsLight[i].transform.localScale = Vector3.zero;
					Sequence s = DOTween.Sequence();
					s.Append(this.imgStarsLight[i].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f));
					s.Append(this.imgStarsLight[i].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
					s.Append(this.imgStarsLight[i].DOFade(0f, 0.1f).OnComplete(delegate
					{
						this.imgStarsLight[i].gameObject.SetActive(false);
					}));
				}
			}
		}

		public void RefreshSpeedLv(int hard)
		{
			if (hard == 0)
			{
				this.imgSpeedLv.sprite = this.spriteSpeedScale[0];
				this.textSpeedLv.gameObject.SetActive(false);
			}
			else
			{
				this.imgSpeedLv.sprite = this.spriteSpeedScale[1];
				this.textSpeedLv.gameObject.SetActive(true);
				this.textSpeedLv.text = hard.ToString();
			}
			this.textUpSpeedLv.text = (hard + 1).ToString();
			Sequence s = DOTween.Sequence();
			s.Append(this.imgSpeedLv.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.3f));
			s.Append(this.imgSpeedLv.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		}

		public void HideAllStars()
		{
			for (int i = 0; i < this.imgStars.Length; i++)
			{
				this.imgStars[i].gameObject.SetActive(false);
				this.imgStarsLight[i].gameObject.SetActive(false);
			}
		}

		public void DoubleReward()
		{
		//	AdsManager adsManager = AdsManager.Instance;
		//	adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.OverAdsDouble.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						this.DoubleRewardSuccess();
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.OverAdsDoubleSucc.ToString());
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
			int num = PlayerPrefsManager.GetCurExp();
			int num2 = PlayerPrefsManager.GetCurGem();
			if (num2 < PlayerPrefsManager.VEDIO_REWARD_MIN)
			{
				num2 = PlayerPrefsManager.VEDIO_REWARD_MIN;
			}
			if (num < PlayerPrefsManager.VEDIO_REWARD_MIN)
			{
				num = PlayerPrefsManager.VEDIO_REWARD_MIN;
			}
			PlayerPrefsManager.SetGold(PlayerPrefsManager.GetGold() + num2);
			PlayerPrefsManager.SetExp(PlayerPrefsManager.GetExp() + num);
			PlayerPrefsManager.SetCurGem(PlayerPrefsManager.GetCurGem() + num2);
			PlayerPrefsManager.SetCurExp(PlayerPrefsManager.GetCurExp() + num);
			EventManager.DoRefreshGemEvent(EventManager.RewardType.doubleGem);
			this.OnRefreshUIStatus();
			//AdsManager.Instance.FaceBookCoin(true, num2, PlayerPrefsManager.GetGold(), "double reward (game complete)");
		}

		public void AdsReward()
		{
			//AdsManager adsManager = AdsManager.Instance;
			//adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.OverAdsGem.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						this.AdsRewardSucc();
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.OverAdsGemSucc.ToString());
					}
				});
				*/
                AdsControl.Instance.PlayDelegateRewardVideo(delegate
                {

                    this.AdsRewardSucc();

                });
            }
			else
			{
				EventManager.DoShowNotice("No Ads");
			}
		}

		private void AdsRewardSucc()
		{
			int aDS_REWARD_GEM = PlayerPrefsManager.ADS_REWARD_GEM;
			PlayerPrefsManager.SetGold(PlayerPrefsManager.GetGold() + aDS_REWARD_GEM);
			PlayerPrefsManager.SetCurGem(PlayerPrefsManager.GetCurGem() + aDS_REWARD_GEM);
			EventManager.DoRefreshGemEvent(EventManager.RewardType.addGem);
			this.OnRefreshUIStatus();
		//AdsManager.Instance.FaceBookCoin(true, aDS_REWARD_GEM, PlayerPrefsManager.GetGold(), "ads reward (complete game)");
		}

		private void OnRefreshUIStatus()
		{
			this.RefreshGold();
		}

		private void RefreshGold()
		{
			this.txtGold.text = PlayerPrefsManager.GetGold().ToString();
		}

		public void OnEndGuid()
		{
			this.buttonSkipGuid.SetActive(false);
			this.imgSpeedLv.gameObject.SetActive(true);
		}

		public void ShowBuyGem()
		{
			this.panelBuyGem.gameObject.SetActive(true);
		}

		private void ShowSecretSong(int songId)
		{
			this.panelSecretSong.songId = songId;
			this.panelSecretSong.gameObject.SetActive(true);
		}

		public void OnNextLevel()
		{
			//AdsManager.Instance.UmEvent(UMConstants.umNextLevel, PlayerPrefsManager.GetSelectedSongId().ToString());
			int num = PlayerPrefsManager.GetSelectedSongId() + 1;
			int songStatus = PlayerPrefsManager.GetSongStatus(num);
			if (songStatus == 2)
			{
				PlayerPrefsManager.SetSelectedSongId(num);
				EventManager.DOReloadSceneEvent();
				return;
			}
			if (songStatus == 1 || songStatus < 0)
			{
				this.ShowSecretSong(num);
			}
			else
			{
				EventManager.DOReloadSceneEvent();
			}
		}

		private void OnGoldNotEnough()
		{
			this.btnGem.DOKill(false);
			this.btnGem.transform.localPosition = this.btnGemPosition;
			this.btnGem.transform.DOShakePosition(0.5f, 30f, 30, 90f, false).OnComplete(delegate
			{
				this.btnGem.transform.localPosition = this.btnGemPosition;
			});
		}

		public void OnSelectSong(int index)
		{
			PlayerPrefsManager.SetSelectedSongId(index);
			if (this.isSwitchScene)
			{
				return;
			}
			this.isSwitchScene = true;
			//AdsManager.Instance.UmEvent(UMConstants.umMusic, index.ToString());
			EventManager.DOReloadSceneEvent();
		}
	}
}
