using DG.Tweening;
using System;
using UnityEngine;

public class RingScale : MonoBehaviour
{
	private float scaleMax = 3f;

	private float scaleMin = 2f;

	private float scaleBigTime = 0.3f;

	private float scaleSmallTime = 0.04f;

	private float sleepTime = 0.04f;

	private Vector3 maxScale;

	private Vector3 minScale;

	private void Start()
	{
		this.maxScale = new Vector3(this.scaleMax, this.scaleMax, this.scaleMax);
		this.minScale = new Vector3(this.scaleMin, this.scaleMin, this.scaleMin);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(base.transform.DOScale(this.maxScale, this.scaleBigTime).SetEase(Ease.OutSine));
		sequence.AppendInterval(this.sleepTime);
		sequence.Append(base.transform.DOScale(this.minScale, this.scaleSmallTime).SetEase(Ease.InSine));
		sequence.SetLoops(-1);
	}
}
