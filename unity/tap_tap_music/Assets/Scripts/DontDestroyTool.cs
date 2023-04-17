using System;
using UnityEngine;

public class DontDestroyTool : MonoBehaviour
{
	public new bool DontDestroyOnLoad = true;

	public bool DontCreateNewWhenBackToThisScene = true;

	public static DontDestroyTool Instance;

	private void Awake()
	{
		if (DontDestroyTool.Instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		DontDestroyTool.Instance = this;
		if (this.DontDestroyOnLoad)
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
	}
}
