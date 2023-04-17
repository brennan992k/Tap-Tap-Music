using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public sealed class ObjectPooling : MonoBehaviorHelper
	{
		public ObjectPoolClass[] poolingObjects;

		public List<GameObject>[] pooledObjects;

		private int defaultPoolAmount = 10;

		private void Awake()
		{
			this.pooledObjects = new List<GameObject>[this.poolingObjects.Length];
			for (int i = 0; i < this.poolingObjects.Length; i++)
			{
				this.pooledObjects[i] = new List<GameObject>();
				int numberToPreSpawnAtStart;
				if (this.poolingObjects[i].numberToPreSpawnAtStart > 0)
				{
					numberToPreSpawnAtStart = this.poolingObjects[i].numberToPreSpawnAtStart;
				}
				else
				{
					numberToPreSpawnAtStart = this.defaultPoolAmount;
				}
				for (int j = 0; j < numberToPreSpawnAtStart; j++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.poolingObjects[i].prefab);
					gameObject.SetActive(false);
					this.pooledObjects[i].Add(gameObject);
					gameObject.transform.parent = base.transform;
				}
			}
		}

		private void OnEnable()
		{
			EventManager.OnReloadSceneEvent += new EventManager.ReloadSceneEvent(this.OnReloadSceneEvent);
		}

		private void OnDisable()
		{
			EventManager.OnReloadSceneEvent -= new EventManager.ReloadSceneEvent(this.OnReloadSceneEvent);
		}

		private void OnReloadSceneEvent()
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public void DespawnAll()
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					transform.gameObject.SetActive(false);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public static void Despawn(GameObject myObject)
		{
			myObject.SetActive(false);
		}

		public GameObject Spawn(string itemType)
		{
			GameObject pooledItem = this.GetPooledItem(itemType);
			if (pooledItem != null)
			{
				pooledItem.SetActive(true);
				return pooledItem;
			}
			UnityEngine.Debug.Log("Warning: Pool is out of objects.\nTry enabling 'Pool Expand' option.");
			return null;
		}

		public GameObject Spawn(string itemType, Vector3 itemPosition, Quaternion itemRotation)
		{
			GameObject pooledItem = this.GetPooledItem(itemType);
			if (pooledItem != null)
			{
				pooledItem.transform.position = itemPosition;
				pooledItem.transform.rotation = itemRotation;
				pooledItem.SetActive(true);
				return pooledItem;
			}
			UnityEngine.Debug.Log("Warning: Pool is out of objects.\nTry enabling 'Pool Expand' option.");
			return null;
		}

		public GameObject Spawn(string itemType, Vector3 itemPosition, Quaternion itemRotation, GameObject myParent)
		{
			GameObject pooledItem = this.GetPooledItem(itemType);
			if (pooledItem != null)
			{
				pooledItem.transform.position = itemPosition;
				pooledItem.transform.rotation = itemRotation;
				pooledItem.transform.parent = myParent.transform;
				pooledItem.SetActive(true);
				return pooledItem;
			}
			UnityEngine.Debug.Log("Warning: Pool is out of objects.\nTry enabling 'Pool Expand' option.");
			return null;
		}

		public static void PlayEffect(GameObject particleEffect, int particlesAmount)
		{
			if (particleEffect.GetComponent<ParticleSystem>())
			{
				particleEffect.GetComponent<ParticleSystem>().Emit(particlesAmount);
			}
		}

		public static void PlaySound(GameObject soundSource)
		{
			if (soundSource.GetComponent<AudioSource>())
			{
				soundSource.GetComponent<AudioSource>().PlayOneShot(soundSource.GetComponent<AudioSource>().GetComponent<AudioSource>().clip);
			}
		}

		public GameObject GetPooledItem(string itemType)
		{
			for (int i = 0; i < this.poolingObjects.Length; i++)
			{
				if (this.poolingObjects[i].prefab.name == itemType)
				{
					for (int j = 0; j < this.pooledObjects[i].Count; j++)
					{
						if (!this.pooledObjects[i][j].activeInHierarchy)
						{
							return this.pooledObjects[i][j];
						}
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.poolingObjects[i].prefab);
					gameObject.SetActive(false);
					this.pooledObjects[i].Add(gameObject);
					gameObject.transform.parent = base.transform;
					return gameObject;
				}
			}
			return null;
		}
	}
}
