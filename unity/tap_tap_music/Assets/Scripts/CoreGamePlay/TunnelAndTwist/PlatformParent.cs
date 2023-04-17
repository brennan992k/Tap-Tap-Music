using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class PlatformParent : MonoBehaviorHelper
	{
		public Obstacle obstacle;

		public CubePlatform[] CubePlatforms;

		private void Awake()
		{
		}

		private void DoDestroy()
		{
			CubePlatform cubePlatform = this.CubePlatforms[this.obstacle.getActiveIndex()];
			cubePlatform.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			this.DisActivePoint(cubePlatform);
			this.DisActiveGem(cubePlatform);
			this.DisActiveGameIntro(cubePlatform);
			EventManager.DODespawnPlatformEvent(this);
			base.gameObject.Despawn();
		}

		private void Update()
		{
			if (this.IsBehind())
			{
				this.DoDestroy();
			}
		}

		private bool IsBehind()
		{
			Vector3 lhs = base.transform.TransformDirection(Vector3.forward);
			Vector3 rhs = base.playerTransform.position - base.transform.position;
			return Vector3.Dot(lhs, rhs) > base.transform.localScale.z * 2f;
		}

		private void OnEnable()
		{
			base.transform.localScale = new Vector3(1f, 1f, Utils.GetZScaleCube(base.gameManager.pointSpeed));
		}

		private void OnDisable()
		{
			CubePlatform o = this.CubePlatforms[this.obstacle.getActiveIndex()];
			this.DisActivePoint(o);
			this.DisActiveGem(o);
			this.DisActiveGameIntro(o);
			base.StopAllCoroutines();
		}

		public void Set(Obstacle obstacle)
		{
			this.obstacle = obstacle;
			for (int i = 0; i < obstacle.GetSize(); i++)
			{
				if (!obstacle.IsActivate(i))
				{
					this.CubePlatforms[i].DesactivateRendererAndCollider();
				}
				else
				{
					this.CubePlatforms[i].ActivatedRendererAndCollider();
				}
			}
			this.DoActivePoint(this.CubePlatforms[this.obstacle.getActiveIndex()]);
		}

		private void DoActivePoint(CubePlatform o)
		{
			if (o != null && o.IsActive())
			{
				o.ActivatePoint();
			}
		}

		private void DisActivePoint(CubePlatform o)
		{
			if (o != null && o.IsActive())
			{
				o.DisActivatePoint();
			}
		}

		private void DoActiveGem(CubePlatform o)
		{
			if (o != null && o.IsActive())
			{
				o.ActivateGem();
			}
		}

		public void DoActiveGem()
		{
			this.DoActiveGem(this.CubePlatforms[this.obstacle.getActiveIndex()]);
		}

		private void DisActiveGem(CubePlatform o)
		{
			if (o != null && o.IsActive())
			{
				o.DisGemPoint();
			}
		}

		public void ActivePlatformRotate45()
		{
			this.CubePlatforms[this.obstacle.getActiveIndex()].Rotate45();
		}

		public int NormalizePosition(int pos)
		{
			if (pos >= 8)
			{
				pos -= 8;
			}
			else if (pos <= -1)
			{
				pos = 8 + pos;
			}
			return pos;
		}

		public int GetActiveCubeIndex()
		{
			if (this.obstacle != null)
			{
				return this.obstacle.getActiveIndex();
			}
			return -1;
		}

		private void DoActiveGameIntro(CubePlatform o, bool isLeft)
		{
			if (o != null && o.IsActive())
			{
				o.ActivateGameIntro(isLeft);
			}
		}

		public void DoActiveGameIntro(bool isLeft)
		{
			this.DoActiveGameIntro(this.CubePlatforms[this.obstacle.getActiveIndex()], isLeft);
		}

		public void DisActiveGameIntro()
		{
			this.DisActiveGameIntro(this.CubePlatforms[this.obstacle.getActiveIndex()]);
		}

		private void DisActiveGameIntro(CubePlatform o)
		{
			if (o != null && o.IsActive())
			{
				o.DisGameIntro();
			}
		}

		public void SetGameIntroActive(bool active)
		{
			CubePlatform cubePlatform = this.CubePlatforms[this.obstacle.getActiveIndex()];
			if (cubePlatform != null && cubePlatform.IsActive())
			{
				cubePlatform.SetGameIntroActive(active);
			}
		}
	}
}
