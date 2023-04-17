using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	[Serializable]
	public struct TUNNELColor
	{
		public float h;

		public float s;

		public float l;

		public float a;

		public TUNNELColor(float h, float s, float l, float a = 1f)
		{
			this.h = h;
			this.s = s;
			this.l = l;
			this.a = a;
		}

		public TUNNELColor(Color c)
		{
			TUNNELColor tUNNELColor = Utils.FromColor(c);
			this.h = tUNNELColor.h;
			this.s = tUNNELColor.s;
			this.l = tUNNELColor.l;
			this.a = tUNNELColor.a;
		}

		public static implicit operator TUNNELColor(Color src)
		{
			return Utils.FromColor(src);
		}

		public static implicit operator Color(TUNNELColor src)
		{
			return Utils.ToColor(src);
		}
	}
}
