using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonWarningScale : MonoBehaviour
{
	private sealed class _DisActive_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ButtonWarningScale _this;

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

		public _DisActive_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(10f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.scaleSequence.Kill(false);
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

	private float scaleMax = 1.2f;

	private float scaleMin = 1f;

	private float scaleBigTime = 0.3f;

	private float scaleSmallTime = 0.04f;

	private Vector3 maxScale;

	private Vector3 minScale;

	private Sequence scaleSequence;

	private void Start()
	{
		this.maxScale = new Vector3(this.scaleMax, this.scaleMax, this.scaleMax);
		this.minScale = new Vector3(this.scaleMin, this.scaleMin, this.scaleMin);
		this.scaleSequence = DOTween.Sequence();
		this.scaleSequence.Append(base.transform.DOScale(this.maxScale, this.scaleBigTime));
		this.scaleSequence.Append(base.transform.DOScale(this.minScale, this.scaleSmallTime));
		this.scaleSequence.SetLoops(-1);
		base.StartCoroutine(this.DisActive());
	}

	private IEnumerator DisActive()
	{
		ButtonWarningScale._DisActive_c__Iterator0 _DisActive_c__Iterator = new ButtonWarningScale._DisActive_c__Iterator0();
		_DisActive_c__Iterator._this = this;
		return _DisActive_c__Iterator;
	}
}
