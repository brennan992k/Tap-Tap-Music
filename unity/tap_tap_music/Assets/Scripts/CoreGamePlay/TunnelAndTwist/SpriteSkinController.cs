using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class SpriteSkinController : MonoBehaviour
	{
		public SpriteRenderer[] spriteRenderer;

		public Sprite[] normalSkinSprite;

		public Sprite[] highSkinSprite;

		private void OnEnable()
		{
			EventManager.OnChangePlatformColor += new EventManager.PlatformColor(this.ChangeColor);
		}

		private void OnDisable()
		{
			EventManager.OnChangePlatformColor -= new EventManager.PlatformColor(this.ChangeColor);
		}

		private void ChangeColor(int skinId)
		{
			if (skinId == 0)
			{
				for (int i = 0; i < this.spriteRenderer.Length; i++)
				{
					this.spriteRenderer[i].sprite = this.normalSkinSprite[i];
				}
			}
			else if (skinId == 1)
			{
				for (int j = 0; j < this.spriteRenderer.Length; j++)
				{
					this.spriteRenderer[j].sprite = this.highSkinSprite[j];
				}
			}
		}
	}
}
