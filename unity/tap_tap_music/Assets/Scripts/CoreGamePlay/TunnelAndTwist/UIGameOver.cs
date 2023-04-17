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
	public class UIGameOver : MonoBehaviour
	{
		private sealed class _StartShow_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector3 _srcscale___0;

			internal Vector3 _dstscale___0;

			internal int _star___0;

			internal Image _imgStar___1;

			internal UIGameOver _this;

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
							sequence.OnComplete(delegate
							{
								this._this.textPerfect.gameObject.SetActive(true);
								this._this.textPerfectPercent.gameObject.SetActive(true);
								this._this.StartCoroutine(this._this.StartPerfectPercentNumRoll());
								this._this.StartCoroutine(this._this.StartScoreNumRoll());
							});
						}
					}
					if (this._star___0 == 0)
					{
						this._this.textPerfect.gameObject.SetActive(true);
						this._this.textPerfectPercent.gameObject.SetActive(true);
						this._this.StartCoroutine(this._this.StartPerfectPercentNumRoll());
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
				this._this.textPerfect.gameObject.SetActive(true);
				this._this.textPerfectPercent.gameObject.SetActive(true);
				this._this.StartCoroutine(this._this.StartPerfectPercentNumRoll());
				this._this.StartCoroutine(this._this.StartScoreNumRoll());
			}
		}

		private sealed class _StartScoreNumRoll_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _ad___0;

			internal int _curScore___0;

			internal int _dstScore___0;

			internal int _adMax___0;

			internal UIGameOver _this;

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
					this._adMax___0 = ((this._dstScore___0 <= 200000) ? 1111 : 11111);
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
				this._this.ShowGemAndExpImage();
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

		private sealed class _StartPerfectPercentNumRoll_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _curPercent___0;

			internal int _dstPercent___0;

			internal int _ad___0;

			internal int _adMax___0;

			internal UIGameOver _this;

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

			public _StartPerfectPercentNumRoll_c__Iterator2()
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
					this._dstPercent___0 = PlayerPrefsManager.GetCurPerfect();
					this._ad___0 = 1;
					this._adMax___0 = 11;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._curPercent___0 < this._dstPercent___0)
				{
					this._this.textPerfectPercent.text = this._curPercent___0.ToString() + "%";
					this._curPercent___0 += this._ad___0;
					this._ad___0 = this._curPercent___0 / 10 + 1;
					if (this._ad___0 > this._adMax___0)
					{
						this._ad___0 = this._adMax___0;
					}
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.textPerfectPercent.text = this._dstPercent___0.ToString() + "%";
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

			internal UIGameOver _this;

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
					this._this.texGemCount.text = this._this.curGem.ToString();
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.curGem = this._dstGem___0;
				this._this.texGemCount.text = this._dstGem___0.ToString();
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

		private sealed class _StartExpNumRoll_c__Iterator4 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _dstExp___0;

			internal int _ad___0;

			internal int _adMax___0;

			internal UIGameOver _this;

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

			public _StartExpNumRoll_c__Iterator4()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._dstExp___0 = PlayerPrefsManager.GetCurExp();
					this._ad___0 = 1;
					this._adMax___0 = 11;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._this.curExp < this._dstExp___0)
				{
					this._this.textExp.text = this._this.curExp.ToString();
					this._this.curExp += this._ad___0;
					this._ad___0 = this._this.curExp / 10 + 1;
					if (this._ad___0 > this._adMax___0)
					{
						this._ad___0 = this._adMax___0;
					}
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.curExp = this._dstExp___0;
				this._this.textExp.text = this._dstExp___0.ToString();
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

		private sealed class _Upgrade_c__Iterator5 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal UIGameOver _this;

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

			public _Upgrade_c__Iterator5()
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

		public MaterialManager materialManager;

		public UIGameManager uIGameManager;

		public Image[] imgStars;

		public Text musicId;

		public Text musicName;

		public Text textPerfect;

		public Text textPerfectPercent;

		public Text texGemCount;

		public Text textExp;

		public Text textAdsRewardGem;

		public Text textAdsRewardExp;

		public Text textScore;

		public GameObject buttonsPanel;

		public GameObject Ads1Panel;

		public Image imgGem;

		public Image imgExp;

		public Button btnDoubleGem;

		private bool scoreRollFinished;

		private int curGem;

		private int curExp;

		public Button btnFullScreen;

		private int butFullScreenClickTime;

		private void OnEnable()
		{
			this.textPerfectPercent.text = string.Empty;
			this.textExp.text = string.Empty;
			this.textScore.text = string.Empty;
			this.texGemCount.text = string.Empty;
			int vEDIO_REWARD_MIN = PlayerPrefsManager.GetCurExp();
			int vEDIO_REWARD_MIN2 = PlayerPrefsManager.GetCurGem();
			if (vEDIO_REWARD_MIN2 < PlayerPrefsManager.VEDIO_REWARD_MIN)
			{
				vEDIO_REWARD_MIN2 = PlayerPrefsManager.VEDIO_REWARD_MIN;
			}
			if (vEDIO_REWARD_MIN < PlayerPrefsManager.VEDIO_REWARD_MIN)
			{
				vEDIO_REWARD_MIN = PlayerPrefsManager.VEDIO_REWARD_MIN;
			}
			this.textAdsRewardGem.text = "+" + vEDIO_REWARD_MIN2.ToString();
			this.textAdsRewardExp.text = "+" + vEDIO_REWARD_MIN.ToString();
			int selectedSongId = PlayerPrefsManager.GetSelectedSongId();
			this.musicName.text = MusicList.Instance.GetValue(selectedSongId).MusicName;
			this.musicId.text = (selectedSongId + 1).ToString();
			this.buttonsPanel.SetActive(false);
			this.Ads1Panel.SetActive(false);
			this.imgGem.transform.localScale = Vector3.zero;
			this.imgExp.transform.localScale = Vector3.zero;
			this.curGem = 0;
			this.curExp = 0;
			this.btnFullScreen.enabled = true;
			base.StartCoroutine(this.StartShow());
			EventManager.OnRefreshGemEvent += new EventManager.RefreshGemEvent(this.OnRefreshGemEvent);
		}

		private void OnDisable()
		{
			EventManager.OnRefreshGemEvent -= new EventManager.RefreshGemEvent(this.OnRefreshGemEvent);
		}

		private void OnRefreshGemEvent(EventManager.RewardType type)
		{
			base.StartCoroutine(this.StartExpNumRoll());
			base.StartCoroutine(this.StartGemNumRoll());
			if (type == EventManager.RewardType.doubleGem)
			{
				this.btnDoubleGem.gameObject.SetActive(false);
			}
		}

		private IEnumerator StartShow()
		{
			UIGameOver._StartShow_c__Iterator0 _StartShow_c__Iterator = new UIGameOver._StartShow_c__Iterator0();
			_StartShow_c__Iterator._this = this;
			return _StartShow_c__Iterator;
		}

		private IEnumerator StartScoreNumRoll()
		{
			UIGameOver._StartScoreNumRoll_c__Iterator1 _StartScoreNumRoll_c__Iterator = new UIGameOver._StartScoreNumRoll_c__Iterator1();
			_StartScoreNumRoll_c__Iterator._this = this;
			return _StartScoreNumRoll_c__Iterator;
		}

		private void ShowGemAndExpImage()
		{
			Vector3 endValue = new Vector3(1f, 1f, 1f);
			Vector3 endValue2 = new Vector3(2f, 2f, 2f);
			Sequence s = DOTween.Sequence();
			s.Append(this.imgGem.transform.DOScale(endValue2, 0.2f).SetEase(Ease.OutSine));
			s.Append(this.imgGem.transform.DOScale(endValue, 0.2f).SetEase(Ease.OutSine)).OnComplete(delegate
			{
				this.StartMoveAdsPanel();
				base.StartCoroutine(this.StartGemNumRoll());
			});
			Sequence s2 = DOTween.Sequence();
			s2.Append(this.imgExp.transform.DOScale(endValue2, 0.2f).SetEase(Ease.OutSine));
			s2.Append(this.imgExp.transform.DOScale(endValue, 0.2f).SetEase(Ease.OutSine)).OnComplete(delegate
			{
				base.StartCoroutine(this.StartExpNumRoll());
			});
		}

		private IEnumerator StartPerfectPercentNumRoll()
		{
			UIGameOver._StartPerfectPercentNumRoll_c__Iterator2 _StartPerfectPercentNumRoll_c__Iterator = new UIGameOver._StartPerfectPercentNumRoll_c__Iterator2();
			_StartPerfectPercentNumRoll_c__Iterator._this = this;
			return _StartPerfectPercentNumRoll_c__Iterator;
		}

		private IEnumerator StartGemNumRoll()
		{
			UIGameOver._StartGemNumRoll_c__Iterator3 _StartGemNumRoll_c__Iterator = new UIGameOver._StartGemNumRoll_c__Iterator3();
			_StartGemNumRoll_c__Iterator._this = this;
			return _StartGemNumRoll_c__Iterator;
		}

		private IEnumerator StartExpNumRoll()
		{
			UIGameOver._StartExpNumRoll_c__Iterator4 _StartExpNumRoll_c__Iterator = new UIGameOver._StartExpNumRoll_c__Iterator4();
			_StartExpNumRoll_c__Iterator._this = this;
			return _StartExpNumRoll_c__Iterator;
		}

		private void StartMoveAdsPanel()
		{
			Vector3 localPosition = this.Ads1Panel.transform.localPosition;
			this.Ads1Panel.SetActive(true);
			this.Ads1Panel.transform.localPosition += new Vector3(800f, 0f, 0f);
			this.Ads1Panel.transform.DOLocalMoveX(localPosition.x, 0.3f, false);
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(0.1f);
			sequence.OnComplete(delegate
			{
				base.StartCoroutine(this.Upgrade());
			});
		}

		private IEnumerator Upgrade()
		{
			UIGameOver._Upgrade_c__Iterator5 _Upgrade_c__Iterator = new UIGameOver._Upgrade_c__Iterator5();
			_Upgrade_c__Iterator._this = this;
			return _Upgrade_c__Iterator;
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
				this.btnFullScreen.enabled = false;
			});
		}

		public void BtnFullScreenClicked()
		{
			this.butFullScreenClickTime++;
			if (this.butFullScreenClickTime > 2 && !this.buttonsPanel.gameObject.activeSelf)
			{
				this.ShowButtons();
			}
		}
	}
}
