using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class CameraLogic : MonoBehaviorHelper
	{
		private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal CameraLogic _this;

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

			public _Start_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = 0;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.SetAnimationIsStarted();
					DOTween.Sequence().AppendInterval(1f).OnComplete(delegate
					{
						this._this.DOLookAt();
						this._this.SetAnimationIsEnded();
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
				this._this.DOLookAt();
				this._this.SetAnimationIsEnded();
			}
		}

		public Vector3 originalPosition1;

		public Vector3 originalRotation1;

		public float defaultVelocityScale = 0.375f;

		public bool AnimationIsEnded;

		private InputTouch inputTouch;

		private void Awake()
		{
			this.AnimationIsEnded = false;
			this.inputTouch = UnityEngine.Object.FindObjectOfType<InputTouch>();
		}

		private void OnEnable()
		{
			EventManager.OnAnimReviveEvent += new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
		}

		private void OnDisable()
		{
			EventManager.OnAnimReviveEvent -= new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
			DOTween.KillAll(false);
		}

		private void OnAnimReviveEvent(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				base.StartCoroutine(this.Start());
			}
		}

		private IEnumerator Start()
		{
			CameraLogic._Start_c__Iterator0 _Start_c__Iterator = new CameraLogic._Start_c__Iterator0();
			_Start_c__Iterator._this = this;
			return _Start_c__Iterator;
		}

		private void DOLookAt()
		{
			Vector3 worldPosition = new Vector3(0f, 0f, -0.96f);
			base.transform.LookAt(worldPosition);
		}

		public void SetAnimationIsStarted()
		{
			this.AnimationIsEnded = false;
			EventManager.DOAnimIntroEvent(AnimIntro.start);
		}

		public void SetAnimationIsEnded()
		{
			Time.timeScale = 1f;
			this.AnimationIsEnded = true;
			base.transform.localPosition = this.originalPosition1;
			base.transform.eulerAngles = this.originalRotation1;
			EventManager.DOAnimIntroEvent(AnimIntro.end);
			UnityEngine.Object.FindObjectOfType<InputTouch>().enabled = true;
		}
	}
}
