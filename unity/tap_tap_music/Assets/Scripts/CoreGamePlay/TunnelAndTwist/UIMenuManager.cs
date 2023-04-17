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
	public class UIMenuManager : MonoBehaviour
	{
		private sealed class _SwitchScene_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal ResourceRequest _r___0;

			internal AsyncOperation _operation___0;

			internal UIMenuManager _this;

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

			public _SwitchScene_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._r___0 = this._this.soundManager.AsyncLoadMusic(PlayerPrefsManager.GetSelectedSongId());
					break;
				case 1u:
					break;
				case 2u:
					this._operation___0.allowSceneActivation = true;
					this._PC = -1;
					return false;
				default:
					return false;
				}
				if (this._r___0.isDone)
				{
					DOTween.KillAll(false);
					GC.Collect();
					this._operation___0 = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
					this._operation___0.allowSceneActivation = false;
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
					}
				}
				else
				{
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
				}
				return true;
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

		[Header("主菜单")]
		public GameObject panelSelectSong;

		public GameObject panelSelectBall;

		public GameObject panelMenu;

		public GameObject panelSetting;

		public UISecretSong panelSecretSong;

		public UISecretBall panelSecretBall;

		public UIVedioBuyGem panelBuyGem;

		public GameObject panelMusicLoading;

		[Header("设置界面")]
		public Button btnMusicOpen;

		public Button btnMusicClose;

		[Header("普通背景")]
		public GameObject imageCommonBg;

		[Header("音乐背景")]
		public GameObject imageMusicBg;

		[Header("选歌界面粒子")]
		public GameObject selectSongParticle;

		[Header("选歌界面光圈")]
		public GameObject selectSongLight;

		[Header("货币")]
		public Button btnGem;

		public Text txtGold;

		public MusicManager soundManager;

		private bool isSwitchScene;

		private Vector3 btnGemPosition;

		private void OnEnable()
		{
			EventManager.OnSelectSongEvent += new EventManager.SelectSongEvent(this.OnSelectSong);
			EventManager.OnSelectBallEvent += new EventManager.SelectBallEvent(this.OnSelectBall);
			EventManager.OnRefreshUIStatus += new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
			EventManager.OnGoldNotEnough += new EventManager.GoldNotEnough(this.OnGoldNotEnough);
			EventManager.OnShowShop += new EventManager.ShowShop(this.ShowBuyGem);
			this.CheckSecretSong();
		}

		private void OnDisable()
		{
			EventManager.OnSelectSongEvent -= new EventManager.SelectSongEvent(this.OnSelectSong);
			EventManager.OnSelectBallEvent -= new EventManager.SelectBallEvent(this.OnSelectBall);
			EventManager.OnRefreshUIStatus -= new EventManager.RefreshUIStatus(this.OnRefreshUIStatus);
			EventManager.OnGoldNotEnough -= new EventManager.GoldNotEnough(this.OnGoldNotEnough);
			EventManager.OnShowShop -= new EventManager.ShowShop(this.ShowBuyGem);
		}

		private void Awake()
		{
            Application.targetFrameRate = 60;
            MusicList.Instance.RefreshMusicData();
			bool flag = PlayerPrefsManager.GetSoundOpen() == 1;
			this.btnMusicOpen.gameObject.SetActive(flag);
			this.btnMusicClose.gameObject.SetActive(!flag);
			this.RefreshGold();
			this.panelSecretSong.gameObject.SetActive(false);
			this.panelSecretBall.gameObject.SetActive(false);
			this.panelBuyGem.gameObject.SetActive(false);
			this.panelMusicLoading.SetActive(false);
		}

		private void Start()
		{
			this.btnGemPosition = this.btnGem.transform.localPosition;
			this.HideAllPanel();
			this.ShowSelectMusic();
		}

		private void OnRefreshUIStatus()
		{
			this.RefreshGold();
		}

		private void RefreshGold()
		{
			this.txtGold.text = PlayerPrefsManager.GetGold().ToString();
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

		private void CheckSecretSong()
		{
			int num = 0;
			int num2 = 0;
			int num3 = PlayerPrefsManager.UpgradeStar.Length;
			int num4 = MusicList.Instance.GetMusicCount() / 4;
			for (int i = 0; i < num4; i++)
			{
				int num5 = i * 4 + 3;
				int songStatus = PlayerPrefsManager.GetSongStatus(num5);
				if (songStatus != 2)
				{
					int num6 = PlayerPrefsManager.UpgradeStar[(i >= num3) ? (num3 - 1) : i];
					int num7 = i * 4;
					int num8 = num7 + 3;
					for (int j = num7; j < num8; j++)
					{
						num2 += PlayerPrefsManager.GetStar(j);
					}
					if (num2 >= num6)
					{
						num = num5;
						if (songStatus == 1 || songStatus == 0)
						{
							PlayerPrefsManager.SetSongStatus(num5, 2);
						}
						EventManager.DoRefreshUIStatus();
						break;
					}
				}
			}
			if (PlayerPrefsManager.LoadIn == 1)
			{
				PlayerPrefsManager.LoadIn = 0;
				//AdsManager.Instance.UmEvent(UMConstants.umEnterGame, 1.ToString());
				return;
			}
			bool flag = AdsControl.Instance.GetRewardAvailable();
			if (num > 0 && flag)
			{
				this.ShowSecretSong(num);
			}
			else
			{
				List<int> list = new List<int>();
				int num9 = (PlayerPrefsManager.GetMusicOpenLv() + 1) * 4;
				for (int k = 4; k < num9; k++)
				{
					if ((k + 1) % 4 != 0)
					{
						if (PlayerPrefsManager.GetSongStatus(k) == 1)
						{
							list.Add(k);
						}
					}
				}
				if (list.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, list.Count);
					this.ShowBuySong(list[index]);
				}
				else if (UnityEngine.Random.Range(0, 2) == 0)
				{
					list.Clear();
					for (int l = 1; l < 15; l++)
					{
						if (PlayerPrefsManager.GetIsGotBall(l) == 0)
						{
							list.Add(l);
						}
					}
					if (list.Count > 0)
					{
						int index2 = UnityEngine.Random.Range(0, list.Count);
						this.ShowBuyBall(list[index2]);
					}
				}
				else if (flag)
				{
					this.ShowBuyGem();
				}
			}
		}

		private void ShowSecretSong(int songId)
		{
			this.panelSecretSong.songId = songId;
			this.panelSecretSong.gameObject.SetActive(true);
		}

		private void ShowBuySong(int songId)
		{
			this.panelSecretSong.songId = songId;
			this.panelSecretSong.gameObject.SetActive(true);
		}

		private void ShowBuyBall(int ballId)
		{
			this.panelSecretBall.ballId = ballId;
			this.panelSecretBall.gameObject.SetActive(true);
		}

		public void ShowBuyGem()
		{
			this.panelBuyGem.gameObject.SetActive(true);
		}

		private void HideAllPanel()
		{
			this.panelSelectSong.gameObject.SetActive(false);
			this.panelSelectBall.gameObject.SetActive(false);
			this.panelSetting.gameObject.SetActive(false);
			this.panelMenu.gameObject.SetActive(false);
			this.imageCommonBg.gameObject.SetActive(false);
			this.imageMusicBg.gameObject.SetActive(false);
			this.selectSongLight.gameObject.SetActive(false);
			this.selectSongParticle.gameObject.SetActive(false);
			this.panelMusicLoading.SetActive(false);
		}

		public void ShowSelectMusic()
		{
			if (this.panelSelectSong.activeSelf)
			{
				return;
			}
			this.HideAllPanel();
			this.panelSelectSong.gameObject.SetActive(true);
			this.panelMenu.gameObject.SetActive(true);
			this.imageMusicBg.gameObject.SetActive(true);
			this.selectSongLight.gameObject.SetActive(true);
			this.selectSongParticle.gameObject.SetActive(true);
            AdsControl.Instance.ShowBanner();
		}

		public void ShowSelectBall()
		{
			if (this.panelSelectBall.activeSelf)
			{
				return;
			}
			this.HideAllPanel();
			this.panelSelectBall.gameObject.SetActive(true);
			this.panelMenu.gameObject.SetActive(true);
			this.imageCommonBg.gameObject.SetActive(true);
		}

		public void ShowSetting()
		{
			if (this.panelSetting.activeSelf)
			{
				return;
			}
			this.HideAllPanel();
			this.panelSetting.gameObject.SetActive(true);
			this.panelMenu.gameObject.SetActive(true);
			this.imageCommonBg.gameObject.SetActive(true);
		}

		public void OnSelectSong(int index)
		{
			PlayerPrefsManager.SetSelectedSongId(index);
			if (this.isSwitchScene)
			{
				return;
			}
			this.isSwitchScene = true;
			this.panelMusicLoading.SetActive(true);
		//AdsManager.Instance.UmEvent(UMConstants.umMusic, index.ToString());
			if (PlayerPrefsManager.WarningStartGame == 1)
			{
				PlayerPrefsManager.WarningStartGame = 0;
			//AdsManager.Instance.AppsFlyStartPlayGame();
			}
			base.StartCoroutine(this.SwitchScene());
		}

		public void OnPlay()
		{
			int selectedSongId = PlayerPrefsManager.GetSelectedSongId();
			this.OnSelectSong(selectedSongId);
		}

		private IEnumerator SwitchScene()
		{
			UIMenuManager._SwitchScene_c__Iterator0 _SwitchScene_c__Iterator = new UIMenuManager._SwitchScene_c__Iterator0();
			_SwitchScene_c__Iterator._this = this;
			return _SwitchScene_c__Iterator;
		}

		public void OnSelectBall(int index)
		{
			PlayerPrefsManager.SetSelectedBallId(index);
		//AdsManager.Instance.UmEvent(UMConstants.umBall, index.ToString());
		}

		public void CloseMusic()
		{
			this.btnMusicOpen.gameObject.SetActive(false);
			this.btnMusicClose.gameObject.SetActive(true);
			PlayerPrefsManager.SetSoundOpen(0);
			this.soundManager.SetMusicOpen(false);
		}

		public void OpenMusic()
		{
			this.btnMusicOpen.gameObject.SetActive(true);
			this.btnMusicClose.gameObject.SetActive(false);
			PlayerPrefsManager.SetSoundOpen(1);
			this.soundManager.SetMusicOpen(true);
		}

		public void ButtonRateClicked()
		{
            //AdsManager.Instance.Rate();
            Application.OpenURL("https://www.facebook.com/FoxGameStudio1989/");
		}

		public void ButtonFBClicked()
		{
            //	AdsManager.Instance.OpenFacebook();
            Application.OpenURL("https://www.facebook.com/FoxGameStudio1989/");
        }
	}
}
