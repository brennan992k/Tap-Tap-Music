using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BallRotate : MonoBehaviour
{
	private sealed class _ColorEnumerator_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Color temColor;

		internal BallRotate _this;

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

		public _ColorEnumerator_c__Iterator0()
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
			this._this.deMaterial.color = Color.Lerp(this._this.deMaterial.color, this.temColor, this._this.speed * Time.deltaTime);
			this._current = 10;
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

	private Material deMaterial;

	public float speed = 10f;

	public float rotateSpeed = 360f;

	private void Start()
	{
		this.deMaterial = base.GetComponent<MeshRenderer>().material;
		base.InvokeRepeating("ChangeColor", 1f, 1f);
	}

	private void Update()
	{
		base.transform.Rotate(Vector3.up, this.rotateSpeed * Time.deltaTime);
	}

	private Color RandomColor()
	{
		float r = UnityEngine.Random.Range(0f, 1f);
		float g = UnityEngine.Random.Range(0f, 1f);
		float b = UnityEngine.Random.Range(0f, 1f);
		Color result = new Color(r, g, b);
		return result;
	}

	private void ChangeColor()
	{
		base.StopAllCoroutines();
		Color temColor = this.RandomColor();
		base.StartCoroutine(this.ColorEnumerator(temColor));
	}

	private IEnumerator ColorEnumerator(Color temColor)
	{
		BallRotate._ColorEnumerator_c__Iterator0 _ColorEnumerator_c__Iterator = new BallRotate._ColorEnumerator_c__Iterator0();
		_ColorEnumerator_c__Iterator.temColor = temColor;
		_ColorEnumerator_c__Iterator._this = this;
		return _ColorEnumerator_c__Iterator;
	}
}
