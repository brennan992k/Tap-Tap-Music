using System;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class ColorManager : MonoBehaviorHelper
	{
		public Image imgBg;

		public Material platFormMeshRender;

		public Sprite[] imgSprite;

		public Texture[] platFormMaterial;

		private ColorStatus colorStatus;

		private int defaultColor = -1;

		private void OnEnable()
		{
			EventManager.OnChangePlatformColor += new EventManager.PlatformColor(this.OnChangePlatformColor);
			EventManager.OnUpSpeed += new EventManager.UpSpeed(this.OnUpSpeed);
			EventManager.DOPlatformColor(0);
		}

		private void OnDisable()
		{
			EventManager.OnChangePlatformColor -= new EventManager.PlatformColor(this.OnChangePlatformColor);
			EventManager.OnUpSpeed -= new EventManager.UpSpeed(this.OnUpSpeed);
		}

		private void OnUpSpeed(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				this.defaultColor = -1;
				EventManager.DOPlatformColor(0);
			}
		}

		private void OnChangePlatformColor(int status)
		{
			this.colorStatus = (ColorStatus)status;
			if (status == 1)
			{
				this.imgBg.sprite = this.imgSprite[0];
				this.platFormMeshRender.mainTexture = this.platFormMaterial[0];
			}
			else
			{
				this.imgBg.sprite = this.imgSprite[1];
				if (this.defaultColor > 0)
				{
					this.platFormMeshRender.mainTexture = this.platFormMaterial[this.defaultColor];
				}
				else
				{
					this.defaultColor = UnityEngine.Random.Range(1, 6);
					this.platFormMeshRender.mainTexture = this.platFormMaterial[this.defaultColor];
				}
			}
		}
	}
}
