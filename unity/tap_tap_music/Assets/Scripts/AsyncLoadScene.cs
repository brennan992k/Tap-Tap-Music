using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadScene : MonoBehaviour
{
	private sealed class _AsyncLoading_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal AsyncLoadScene _this;

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

		public _AsyncLoading_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.operation = SceneManager.LoadSceneAsync(Globe.nextSceneName);
				this._this.operation.allowSceneActivation = false;
				this._this.isLoading = true;
				this._current = this._this.operation;
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
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

	public Slider loadingSlider;

	public Text loadingText;

	public bool autoSwitch;

	private float loadingSpeed = 1f;

	private float targetValue;

	private AsyncOperation operation;

	private bool isLoading;

	public Transform imgMic;

	private Vector3 micInitScale;

	private void Start()
	{
		if (this.loadingSlider != null)
		{
			this.loadingSlider.value = 0f;
		}
		if (SceneManager.GetActiveScene().name == Globe.currentSceneName)
		{
			base.StartCoroutine(this.AsyncLoading());
		}
		if (this.imgMic != null)
		{
			this.micInitScale = this.imgMic.transform.localScale;
			Vector3 endValue = this.micInitScale * 1.3f;
			float duration = 0.3f;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.imgMic.DOScale(endValue, duration));
			sequence.Append(this.imgMic.DOScale(this.micInitScale, duration));
			sequence.SetLoops(-1);
		}
	}

	private IEnumerator AsyncLoading()
	{
		AsyncLoadScene._AsyncLoading_c__Iterator0 _AsyncLoading_c__Iterator = new AsyncLoadScene._AsyncLoading_c__Iterator0();
		_AsyncLoading_c__Iterator._this = this;
		return _AsyncLoading_c__Iterator;
	}

	private void FixedUpdate()
	{
		if (!this.autoSwitch)
		{
			return;
		}
		if (!this.isLoading)
		{
			return;
		}
		this.targetValue = this.operation.progress;
		if (this.targetValue >= 0.9f)
		{
			this.loadingSlider.value = Mathf.Lerp(this.loadingSlider.value, 1f, Time.deltaTime * this.loadingSpeed);
		}
		else
		{
			this.loadingSlider.value = Mathf.Lerp(this.loadingSlider.value, this.targetValue, Time.deltaTime * this.loadingSpeed);
		}
		this.loadingText.text = ((int)(this.loadingSlider.value * 100f)).ToString() + "%";
		if (this.loadingSlider.value >= 0.9f)
		{
			this.operation.allowSceneActivation = true;
		}
	}

	public void AutoSwitch()
	{
		this.autoSwitch = true;
	}

	public void MenuSwitch()
	{
		this.operation.allowSceneActivation = true;
	}
}
