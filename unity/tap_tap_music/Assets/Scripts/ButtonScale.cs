using DG.Tweening;
using System;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
	public void ButtonTouchedDown()
	{
		base.transform.DOScale(1.5f, 0.2f).SetEase(Ease.InSine);
	}

	public void ButtonTouchedUp()
	{
		this.DOKill(false);
		base.transform.DOScale(1f, 0.2f).SetEase(Ease.OutSine);
	}
}
