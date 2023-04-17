using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChinarRotateObjectSelf : MonoBehaviour
{
	private sealed class _OnMouseDown_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Vector3 _offset___1;

		internal ChinarRotateObjectSelf _this;

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

		public _OnMouseDown_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.mousePos = UnityEngine.Input.mousePosition;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (Input.GetMouseButton(0))
			{
				this._offset___1 = this._this.mousePos - UnityEngine.Input.mousePosition;
				this._this.transform.Rotate(Vector3.up * this._offset___1.x, Space.World);
				this._this.transform.Rotate(Vector3.left * this._offset___1.y, Space.World);
				this._this.mousePos = UnityEngine.Input.mousePosition;
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
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

	public Vector3 mousePos;

	private IEnumerator OnMouseDown()
	{
		ChinarRotateObjectSelf._OnMouseDown_c__Iterator0 _OnMouseDown_c__Iterator = new ChinarRotateObjectSelf._OnMouseDown_c__Iterator0();
		_OnMouseDown_c__Iterator._this = this;
		return _OnMouseDown_c__Iterator;
	}
}
