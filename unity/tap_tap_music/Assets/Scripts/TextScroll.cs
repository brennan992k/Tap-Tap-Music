using System;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{
	public Text text;

	private ScrollRect rect;

	private float contentWidth;

	private void Start()
	{
		this.rect = base.GetComponent<ScrollRect>();
		Image component = base.GetComponent<Image>();
		this.contentWidth = component.rectTransform.rect.width;
	}

	private void Update()
	{
		this.ScrollValue();
	}

	private void ScrollValue()
	{
		if (this.text.preferredWidth > this.contentWidth)
		{
			if (this.rect.horizontalNormalizedPosition > this.text.preferredWidth / 2f / this.text.rectTransform.rect.width)
			{
				this.rect.horizontalNormalizedPosition = 0f;
			}
			this.rect.horizontalNormalizedPosition = this.rect.horizontalNormalizedPosition + 0.05f * Time.deltaTime;
		}
	}
}
