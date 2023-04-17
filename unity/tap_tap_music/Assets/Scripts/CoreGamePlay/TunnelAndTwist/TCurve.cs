using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class TCurve
	{
		public float x;

		public float y;

		public TCurve(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector4 GetVector()
		{
			return new Vector4(this.x, this.y, 0f, 0f);
		}
	}
}
