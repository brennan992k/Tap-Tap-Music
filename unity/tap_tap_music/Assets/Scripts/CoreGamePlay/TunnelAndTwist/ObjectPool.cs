using System;
using System.Collections.Generic;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class ObjectPool
	{
		public List<GameObject> pooledObjects;

		private GameObject pooledObj;

		private int maxPoolSize;

		public ObjectPool(GameObject obj, int initialPoolSize, int maxPoolSize)
		{
			this.pooledObjects = new List<GameObject>();
			for (int i = 0; i < initialPoolSize; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(obj, Vector3.zero, Quaternion.identity);
				gameObject.SetActive(false);
				this.pooledObjects.Add(gameObject);
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			this.maxPoolSize = maxPoolSize;
			this.pooledObj = obj;
		}

		public GameObject GetObject()
		{
			for (int i = 0; i < this.pooledObjects.Count; i++)
			{
				if (!this.pooledObjects[i].activeSelf)
				{
					this.pooledObjects[i].SetActive(true);
					return this.pooledObjects[i];
				}
			}
			if (this.maxPoolSize > this.pooledObjects.Count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObj, Vector3.zero, Quaternion.identity);
				gameObject.SetActive(true);
				this.pooledObjects.Add(gameObject);
				return gameObject;
			}
			return null;
		}
	}
}
