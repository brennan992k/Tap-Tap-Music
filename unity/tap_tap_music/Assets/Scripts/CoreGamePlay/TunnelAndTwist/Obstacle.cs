using System;
using System.Collections.Generic;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	[Serializable]
	public class Obstacle
	{
		[SerializeField]
		private List<bool> cubes;

		[SerializeField]
		private int activeIndex;

		private int platformId;

		public Obstacle(int num, int assignIndex)
		{
			this.platformId = num;
			this.cubes = new List<bool>(new bool[8]);
			this.Activate(assignIndex);
		}

		public Obstacle(Obstacle lastObstacle, int num, int exceptIndex)
		{
			this.platformId = num;
			this.cubes = new List<bool>(new bool[8]);
			if (lastObstacle == null)
			{
				return;
			}
			for (int i = 0; i < this.cubes.Count; i++)
			{
				if (lastObstacle.IsActivate(i))
				{
					int num2 = i + 1;
					if (num2 >= this.GetSize())
					{
						num2 = 0;
					}
					int num3 = i - 1;
					if (num3 < 0)
					{
						num3 = this.GetSize() - 1;
					}
					int num4 = Utils.RandomRange(0, 1000);
					if (num4 < 500)
					{
						if (num3 != exceptIndex)
						{
							this.Activate(num3);
						}
						else
						{
							this.Activate(num2);
						}
					}
					else if (num2 != exceptIndex)
					{
						this.Activate(num2);
					}
					else
					{
						this.Activate(num3);
					}
				}
			}
		}

		private void DisableSome(int num)
		{
			List<int> list = new List<int>(new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7
			});
			list.Shuffle<int>();
			for (int i = 0; i < 8; i++)
			{
				this.cubes[list[i]] = false;
			}
		}

		public List<bool> GetList()
		{
			return this.cubes;
		}

		public int GetSize()
		{
			return this.cubes.Count;
		}

		public void Activate(int n)
		{
			this.cubes[n] = true;
			this.activeIndex = n;
		}

		public void Desactivate(int n)
		{
			this.cubes[n] = false;
		}

		public bool IsActivate(int n)
		{
			return this.cubes[n];
		}

		public int getActiveIndex()
		{
			return this.activeIndex;
		}

		public int GetPlatformId()
		{
			return this.platformId;
		}

		public static implicit operator Obstacle(List<GameObject> v)
		{
			throw new NotImplementedException();
		}
	}
}
