using DG.Tweening;
using System;
using UnityEngine;

public class ImageScale : MonoBehaviour
{
	private float scaleMax = 1.5f;

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
		this.Active();
	}

	private void Active()
	{
		this.scaleSequence = DOTween.Sequence();
		this.scaleSequence.Append(base.transform.DOScale(this.maxScale, this.scaleBigTime));
		this.scaleSequence.Append(base.transform.DOScale(this.minScale, this.scaleSmallTime));
		this.scaleSequence.Append(base.transform.DOScale(this.maxScale, this.scaleBigTime));
		this.scaleSequence.Append(base.transform.DOScale(this.minScale, this.scaleSmallTime));
		this.scaleSequence.Append(base.transform.DOScale(this.maxScale, this.scaleBigTime));
		this.scaleSequence.Append(base.transform.DOScale(this.minScale, this.scaleSmallTime));
		this.scaleSequence.AppendInterval(5f);
		this.scaleSequence.SetLoops(-1);
	}
}
