using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class MoveForward : MonoBehaviorHelper
	{
		private sealed class _CoUpdate_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal MoveForward _this;

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

			public _CoUpdate_c__Iterator0()
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
				default:
					return false;
				}
				if (!this._this.pasue)
				{
					this._this.transform.Translate(Vector3.forward * this._this.pointSpeed * Time.deltaTime);
				}
				this._current = 0;
				if (!this._disposing)
				{
					this._PC = 1;
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

		private float pointSpeed;

		private bool pasue;

		private void OnEnable()
		{
			EventManager.OnSetSpeed += new EventManager.SetSpeed(this.OnSetSpeed);
			EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnAnimReviveEvent += new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
		}

		private void OnDisable()
		{
			EventManager.OnSetSpeed -= new EventManager.SetSpeed(this.OnSetSpeed);
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnAnimReviveEvent -= new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
		}

		private void OnPlayerStartEvent()
		{
			this.pasue = false;
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			base.StartCoroutine("CoUpdate");
		}

		private void OnAnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.start)
			{
				this.pasue = true;
				base.StopCoroutine("CoUpdate");
			}
		}

		private void OnAnimReviveEvent(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			}
		}

		private void OnSetSpeed(float speed)
		{
			this.pointSpeed = speed;
		}

		private void OnPauseForGuid(AnimStatus status)
		{
			if (status == AnimStatus.start)
			{
				this.pasue = true;
			}
			else
			{
				this.pasue = false;
			}
		}

		private IEnumerator CoUpdate()
		{
			MoveForward._CoUpdate_c__Iterator0 _CoUpdate_c__Iterator = new MoveForward._CoUpdate_c__Iterator0();
			_CoUpdate_c__Iterator._this = this;
			return _CoUpdate_c__Iterator;
		}
	}
}
