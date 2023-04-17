using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public static class ObjectPoolingExtensions
	{
		public static void Despawn(this GameObject myobject)
		{
			ObjectPooling.Despawn(myobject);
		}

		public static void PlayEffect(this GameObject particleEffect, int particlesAmount)
		{
			ObjectPooling.PlayEffect(particleEffect, particlesAmount);
		}

		public static void PlaySound(this GameObject soundSource)
		{
			ObjectPooling.PlaySound(soundSource);
		}
	}
}
