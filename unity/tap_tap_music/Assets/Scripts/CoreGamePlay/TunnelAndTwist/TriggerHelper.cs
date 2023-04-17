using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class TriggerHelper : MonoBehaviorHelper
	{
		private sealed class _CoUpdate_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal TriggerHelper _this;

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
				this._this.CheckCustom();
				this._this.Check();
				this._current = null;
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

		private Transform camTransform;

		protected bool isGamePlaying;

		public virtual void OnCollisionWithPlayer()
		{
		}

		public virtual void OnCenterCollisionWithPlayer()
		{
		}

		public virtual void IsOutOfScreen()
		{
		}

		public virtual void CheckCustom()
		{
		}

		private void Awake()
		{
			this.camTransform = Camera.main.transform;
		}

		protected void OnEnable()
		{
			EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnGamePlay);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			if (this.isGamePlaying)
			{
				base.StartCoroutine(this.CoUpdate());
			}
		}

		protected void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnGamePlay);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			base.StopAllCoroutines();
		}

		private IEnumerator CoUpdate()
		{
			TriggerHelper._CoUpdate_c__Iterator0 _CoUpdate_c__Iterator = new TriggerHelper._CoUpdate_c__Iterator0();
			_CoUpdate_c__Iterator._this = this;
			return _CoUpdate_c__Iterator;
		}

		protected void Check()
		{
			float num = Vector3.Distance(base.transform.position, base.playerTransform.position);
			float num2 = base.transform.position.z - base.playerTransform.position.z;
			float num3 = base.transform.position.y - base.playerTransform.position.y;
			if (num < 1.2f)
			{
				this.OnCenterCollisionWithPlayer();
			}
			if ((double)num < 2.5)
			{
				this.OnCollisionWithPlayer();
			}
			if (this.IsBehind())
			{
				this.IsOutOfScreen();
			}
		}

		protected bool IsBehind()
		{
			if (this.camTransform == null)
			{
				this.camTransform = Camera.main.transform;
			}
			Vector3 lhs = base.transform.TransformDirection(Vector3.forward);
			Vector3 rhs = this.camTransform.position - base.transform.position;
			float num = Vector3.Dot(lhs, rhs);
			return num > base.transform.localScale.z * 2f;
		}

		private void OnGamePlay()
		{
			this.isGamePlaying = true;
			base.StartCoroutine(this.CoUpdate());
		}

		private void OnAnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.start)
			{
				this.isGamePlaying = false;
				base.StopAllCoroutines();
			}
		}
	}
}
