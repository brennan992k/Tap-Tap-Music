using DG.Tweening;
using System;
using UnityEngine;

public class LoadingScale : MonoBehaviour
{
	public float scaleMax = 1.5f;

	public float scaleMin = 1f;

	public float scaleBigTime = 0.3f;

	public float scaleSmallTime = 0.04f;

	private Vector3 maxScale;

	private Vector3 minScale;

	private void Start()
	{
		this.maxScale = new Vector3(this.scaleMax, this.scaleMax, this.scaleMax);
		this.minScale = new Vector3(this.scaleMin, this.scaleMin, this.scaleMin);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(base.transform.DOScale(this.maxScale, this.scaleBigTime));
		sequence.Append(base.transform.DOScale(this.minScale, this.scaleSmallTime));
		sequence.SetLoops(-1);
	}
}
