using DG.Tweening;
using System;
using UnityEngine;

public class SelectMusicMicScale : MonoBehaviour
{
	public GameObject mic;

	private float scaleMax = 1.5f;

	private float scaleMin = 1f;

	private float scaleBigTime = 0.15f;

	private float scaleSmallTime = 0.02f;

	private float sleepTime = 0.02f;

	private Vector3 maxScale;

	private Vector3 minScale;

	private void Start()
	{
		this.maxScale = new Vector3(this.scaleMax, this.scaleMax, this.scaleMax);
		this.minScale = new Vector3(this.scaleMin, this.scaleMin, this.scaleMin);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(this.mic.transform.DOScale(this.maxScale, this.scaleBigTime).SetEase(Ease.OutSine));
		sequence.AppendInterval(this.sleepTime);
		sequence.Append(this.mic.transform.DOScale(this.minScale, this.scaleSmallTime).SetEase(Ease.InSine));
		sequence.SetLoops(-1);
	}
}
