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
	public class TimeCountDown : MonoBehaviour
	{
		private sealed class _CountDown_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal TimeCountDown _this;

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

			public _CountDown_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(this._this.delayTime);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.Active();
					this._current = new WaitForSeconds(this._this.nothanksWaitTime);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				case 2u:
					this._this.nothanks.DOFade(1f, 0.2f).OnComplete(delegate
					{
						this._this.btnThanks.enabled = true;
					});
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
				this._this.btnThanks.enabled = true;
			}
		}

		private sealed class _OnClickedFreeVedio_c__AnonStorey1
		{
			//internal AdsManager adsManager;

			internal void __m__0(bool state)
			{
				if (state)
				{
					EventManager.DOAnimReviveEvent(AnimStatus.start);
					//this.adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.ReviveAdsSucc.ToString());
				}
			}
		}

		public Image circleProgress;

		public Button btnThanks;

		public Text nothanks;

		public float reviveTime = 5f;

		public float delayTime = 0.5f;

		public float nothanksWaitTime = 3f;

		private float stillTime;

		private bool isCountDown;

		private void Awake()
		{
			this.stillTime = this.reviveTime;
			this.isCountDown = false;
		}

		private void OnEnable()
		{
			this.isCountDown = false;
			this.stillTime = this.reviveTime;
			this.circleProgress.fillAmount = 1f;
			this.nothanks.color = new Color(this.nothanks.color.r, this.nothanks.color.r, this.nothanks.color.r, 0f);
			this.btnThanks.enabled = false;
			base.StartCoroutine(this.CountDown());
		}

		private void OnDisable()
		{
			this.DisActive();
		}

		private IEnumerator CountDown()
		{
			TimeCountDown._CountDown_c__Iterator0 _CountDown_c__Iterator = new TimeCountDown._CountDown_c__Iterator0();
			_CountDown_c__Iterator._this = this;
			return _CountDown_c__Iterator;
		}

		public void Active()
		{
			this.isCountDown = true;
		}

		public void DisActive()
		{
			this.isCountDown = false;
		}

		private void Update()
		{
			if (!this.isCountDown)
			{
				return;
			}
			if (this.stillTime > 0f && this.circleProgress != null)
			{
				this.stillTime -= Time.deltaTime;
				this.circleProgress.fillAmount = this.stillTime / this.reviveTime;
			}
			else
			{
				this.isCountDown = false;
				EventManager.DoAnimGameEndEvent();
			}
		}

		public void OnClickedFreeVedio()
		{
			//AdsManager adsManager = AdsManager.Instance;
			//adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.ReviveAds.ToString());
			if (AdsControl.Instance.GetRewardAvailable())
			{
				this.DisActive();
                /*
				adsManager.ShowRewardedVideo(delegate(bool state)
				{
					if (state)
					{
						EventManager.DOAnimReviveEvent(AnimStatus.start);
						adsManager.UmEvent(UMConstants.umAds, UMConstants.UMAdsConstantsId.ReviveAdsSucc.ToString());
					}
				});
				*/
                AdsControl.Instance.PlayDelegateRewardVideo(delegate
                {

                    EventManager.DOAnimReviveEvent(AnimStatus.start);
                
                });
            }
			else
			{
				EventManager.DoShowNotice("No Ads");
			}
		}
	}
}
