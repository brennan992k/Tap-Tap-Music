using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class UIWin : MonoBehaviour
	{
		private sealed class _StartShow_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector3 _srcscale___0;

			internal Vector3 _dstscale___0;

			internal int _star___0;

			internal Image _imgStar___1;

			internal UIWin _this;

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

			public _StartShow_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(0.1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.StartCoroutine(this._this.StartGemNumRoll());
					this._srcscale___0 = new Vector3(1f, 1f, 1f);
					this._dstscale___0 = new Vector3(2f, 2f, 2f);
					this._star___0 = PlayerPrefsManager.GetCurStar();
					for (int i = 0; i < this._star___0; i++)
					{
						this._imgStar___1 = this._this.imgStars[i];
						this._imgStar___1.gameObject.SetActive(true);
						this._imgStar___1.transform.localScale = Vector3.zero;
						Sequence sequence = DOTween.Sequence();
						sequence.AppendInterval(0.1f + (float)i * 0.2f);
						sequence.Append(this._imgStar___1.transform.DOScale(this._dstscale___0, 0.2f).SetEase(Ease.OutSine));
						sequence.Append(this._imgStar___1.transform.DOScale(this._srcscale___0, 0.2f).SetEase(Ease.OutSine));
						if (i == this._star___0 - 1)
						{
							sequence.OnStart(delegate
							{
								this._this.StartCoroutine(this._this.StartGamePercentNumRoll());
								this._this.StartCoroutine(this._this.StartScoreNumRoll());
							});
						}
					}
					if (this._star___0 == 0)
					{
						this._this.StartCoroutine(this._this.StartGamePercentNumRoll());
						this._this.StartCoroutine(this._this.StartScoreNumRoll());
					}
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

			internal void __m__0()
			{
				this._this.StartCoroutine(this._this.StartGamePercentNumRoll());
				this._this.StartCoroutine(this._this.StartScoreNumRoll());
			}
		}

		private sealed class _StartScoreNumRoll_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _ad___0;

			internal int _curScore___0;

			internal int _dstScore___0;

			internal int _adMax___0;

			internal UIWin _this;

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

			public _StartScoreNumRoll_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(0.1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._ad___0 = 1;
					this._curScore___0 = this._ad___0;
					this._dstScore___0 = PlayerPrefsManager.GetCurScore();
					this._adMax___0 = 11111;
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if (this._curScore___0 < this._dstScore___0)
				{
					this._this.textScore.text = this._curScore___0.ToString();
					this._curScore___0 += this._ad___0;
					this._ad___0 = this._curScore___0 / 10 + 1;
					if (this._ad___0 > this._adMax___0)
					{
						this._ad___0 = this._adMax___0;
					}
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				this._this.scoreRollFinished = true;
				this._this.textScore.text = this._dstScore___0.ToString();
				this._this.StartMoveAdsPanel();
				this._PC = -1;
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

		private sealed class _StartGamePercentNumRoll_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _curPercent___0;

			internal float _dstPercent___0;

			internal int _ad___0;

			internal float _rotateZ___0;

			internal UIWin _this;

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

			public _StartGamePercentNumRoll_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._curPercent___0 = 1;
					this._dstPercent___0 = (float)PlayerPrefsManager.GetCurGamePercent();
					this._ad___0 = 1;
					this._rotateZ___0 = 0f;
					this._this.objDot.gameObject.SetActive(true);
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if ((float)this._curPercent___0 < this._dstPercent___0)
				{
					this._this.textGamePercent.text = this._curPercent___0.ToString() + "%";
					this._this.circleProgress.fillAmount = (float)this._curPercent___0 * 1f / 100f;
					this._rotateZ___0 = 360f * this._this.circleProgress.fillAmount;
					this._this.objDot.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -this._rotateZ___0));
					this._curPercent___0 += this._ad___0;
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.textGamePercent.text = this._dstPercent___0.ToString() + "%";
				this._this.circleProgress.fillAmount = this._dstPercent___0 / 100f;
				this._PC = -1;
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

		private sealed class _StartGemNumRoll_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _dstGem___0;

			internal UIWin _this;

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

			public _StartGemNumRoll_c__Iterator3()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._dstGem___0 = PlayerPrefsManager.GetCurGem();
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._this.curGem < this._dstGem___0)
				{
					this._this.curGem++;
					this._this.texGemCount.text = "+" + this._this.curGem.ToString();
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.curGem = this._dstGem___0;
				this._this.texGemCount.text = "+" + this._dstGem___0.ToString();
				this._PC = -1;
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

		private sealed class _StartShowButton_c__Iterator4 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal UIWin _this;

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

			public _StartShowButton_c__Iterator4()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					break;
				case 1u:
					break;
				case 2u:
					this._this.ShowButtons();
					this._PC = -1;
					return false;
				default:
					return false;
				}
				if (this._this.scoreRollFinished)
				{
					this._current = new WaitForSeconds(0.2f);
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

		public UIGameManager uIGameManager;

		public Image[] imgStars;

		public Text musicName;

		public Text texGemCount;

		public Text textScore;

		public Text textGamePercent;

		public GameObject buttonsPanel;

		public GameObject goldPanel;

		public GameObject btnDoubleGem;

		public Button btnNextLevel;

		public GridLayoutGroup buttonGird;

		public Image circleProgress;

		public Transform objDot;

		private bool scoreRollFinished;

		private int curGem;

		private Vector2 bigSpace;

		private Vector2 smallSpace;

		private static TweenCallback __f__am_cache0;

		private void Awake()
		{
			this.smallSpace = this.buttonGird.spacing;
			this.bigSpace = this.buttonGird.spacing + new Vector2(this.buttonGird.spacing.x * 1.5f, 0f);
		}

		private void OnEnable()
		{
			this.textScore.text = string.Empty;
			this.texGemCount.text = string.Empty;
			this.textGamePercent.text = string.Empty;
			this.circleProgress.fillAmount = 0f;
			this.objDot.localRotation = Quaternion.Euler(Vector3.zero);
			this.objDot.gameObject.SetActive(false);
			this.goldPanel.transform.localPosition = new Vector3(100f, 0f, 0f);
			this.scoreRollFinished = false;
			this.musicName.text = MusicList.Instance.GetValue(PlayerPrefsManager.GetSelectedSongId()).MusicName;
			this.buttonsPanel.SetActive(false);
			this.btnDoubleGem.SetActive(false);
			this.curGem = 0;
			base.StartCoroutine(this.StartShow());
			int songStatus = PlayerPrefsManager.GetSongStatus(PlayerPrefsManager.GetSelectedSongId() + 1);
			if (songStatus == 0 || PlayerPrefsManager.GetCurGamePercent() < 100)
			{
				this.btnNextLevel.gameObject.SetActive(false);
				this.buttonGird.spacing = this.bigSpace;
			}
			else
			{
				this.btnNextLevel.gameObject.SetActive(true);
				this.buttonGird.spacing = this.smallSpace;
			}
			EventManager.OnRefreshGemEvent += new EventManager.RefreshGemEvent(this.OnRefreshGemEvent);
		}

		private void OnDisable()
		{
			EventManager.OnRefreshGemEvent -= new EventManager.RefreshGemEvent(this.OnRefreshGemEvent);
		}

		private void OnRefreshGemEvent(EventManager.RewardType type)
		{
			base.StartCoroutine(this.StartGemNumRoll());
			if (type == EventManager.RewardType.doubleGem)
			{
				this.goldPanel.transform.DOLocalMoveX(100f, 0.2f, false);
				this.btnDoubleGem.gameObject.SetActive(false);
			}
		}

		private IEnumerator StartShow()
		{
			UIWin._StartShow_c__Iterator0 _StartShow_c__Iterator = new UIWin._StartShow_c__Iterator0();
			_StartShow_c__Iterator._this = this;
			return _StartShow_c__Iterator;
		}

		private IEnumerator StartScoreNumRoll()
		{
			UIWin._StartScoreNumRoll_c__Iterator1 _StartScoreNumRoll_c__Iterator = new UIWin._StartScoreNumRoll_c__Iterator1();
			_StartScoreNumRoll_c__Iterator._this = this;
			return _StartScoreNumRoll_c__Iterator;
		}

		private IEnumerator StartGamePercentNumRoll()
		{
			UIWin._StartGamePercentNumRoll_c__Iterator2 _StartGamePercentNumRoll_c__Iterator = new UIWin._StartGamePercentNumRoll_c__Iterator2();
			_StartGamePercentNumRoll_c__Iterator._this = this;
			return _StartGamePercentNumRoll_c__Iterator;
		}

		private IEnumerator StartGemNumRoll()
		{
			UIWin._StartGemNumRoll_c__Iterator3 _StartGemNumRoll_c__Iterator = new UIWin._StartGemNumRoll_c__Iterator3();
			_StartGemNumRoll_c__Iterator._this = this;
			return _StartGemNumRoll_c__Iterator;
		}

		private void StartMoveAdsPanel()
		{
			Vector3 localPosition = this.btnDoubleGem.transform.localPosition;
			this.btnDoubleGem.SetActive(true);
			this.btnDoubleGem.transform.localPosition += new Vector3(100f, 0f, 0f);
			this.btnDoubleGem.transform.DOLocalMoveX(localPosition.x, 0.2f, false);
			this.goldPanel.transform.DOLocalMoveX(0f, 0.2f, false);
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(0.1f);
			sequence.OnComplete(delegate
			{
				base.StartCoroutine(this.StartShowButton());
			});
		}

		private IEnumerator StartShowButton()
		{
			UIWin._StartShowButton_c__Iterator4 _StartShowButton_c__Iterator = new UIWin._StartShowButton_c__Iterator4();
			_StartShowButton_c__Iterator._this = this;
			return _StartShowButton_c__Iterator;
		}

		private void ShowButtons()
		{
			if (this.buttonsPanel.activeSelf)
			{
				return;
			}
			this.buttonsPanel.transform.DOKill(false);
			Vector3 localPosition = this.buttonsPanel.transform.localPosition;
			this.buttonsPanel.SetActive(true);
			this.buttonsPanel.transform.localPosition += new Vector3(800f, 0f, 0f);
			Sequence s = DOTween.Sequence();
			s.Append(this.buttonsPanel.transform.DOLocalMoveX(localPosition.x, 0.3f, false));
			s.AppendInterval(0.5f).OnComplete(delegate
			{
				if (PlayerPrefsManager.GetNewMusicLv() > 0)
				{
					EventManager.DOUpgradeEvent();
				}
			});
		}
	}
}
