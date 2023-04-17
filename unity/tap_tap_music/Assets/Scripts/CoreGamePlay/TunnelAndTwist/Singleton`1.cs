using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		private static object _lock = new object();

		private static bool applicationIsQuitting = false;

		public static T Instance
		{
			get
			{
				if (Singleton<T>.applicationIsQuitting)
				{
					return (T)((object)null);
				}
				object @lock = Singleton<T>._lock;
				T instance;
				lock (@lock)
				{
					if (Singleton<T>._instance == null)
					{
						Singleton<T>._instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
						if (UnityEngine.Object.FindObjectsOfType(typeof(T)).Length > 1)
						{
							instance = Singleton<T>._instance;
							return instance;
						}
						if (Singleton<T>._instance == null)
						{
							GameObject gameObject = new GameObject();
							Singleton<T>._instance = gameObject.AddComponent<T>();
							gameObject.name = "(singleton) " + typeof(T).ToString();
							UnityEngine.Object.DontDestroyOnLoad(gameObject);
						}
					}
					instance = Singleton<T>._instance;
				}
				return instance;
			}
		}

		public void OnDestroy()
		{
			Singleton<T>.applicationIsQuitting = true;
		}
	}
}
