using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
	[Tooltip("横向模型显示数量")]
	public int numberWidth = 4;

	[Tooltip("纵向模型显示数量")]
	public int numberHeight = 4;

	private static GameObject gameRuning;

	private Dictionary<int, Vector3> m_Vector3List = new Dictionary<int, Vector3>();

	private Dictionary<int, bool> m_Pos = new Dictionary<int, bool>();

	private Dictionary<int, Transform> m_Trans = new Dictionary<int, Transform>();

	public static CameraManger Instance
	{
		get
		{
			if (CameraManger.gameRuning == null)
			{
				return null;
			}
			return CameraManger.gameRuning.GetComponent<CameraManger>();
		}
	}

	private void Awake()
	{
		CameraManger.gameRuning = base.gameObject;
		this.SavePosition();
	}

	private void SavePosition()
	{
		float num = (float)this.numberHeight;
		for (int i = 0; i < this.numberWidth; i++)
		{
			for (int j = 0; j < this.numberHeight; j++)
			{
				float x = -num + num * 2f / (float)this.numberWidth * (float)j + 1f;
				float y = -num + num * 2f / (float)this.numberHeight * (float)i + 1f;
				Vector3 value = new Vector3(x, y, 8.5f);
				this.m_Vector3List.Add(i * this.numberWidth + j, value);
				this.m_Pos.Add(i * this.numberWidth + j, true);
			}
		}
	}

	public int Add3DModel(string path)
	{
		if (this.m_Pos == null || this.m_Pos.Count == 0)
		{
			return 0;
		}
		foreach (KeyValuePair<int, bool> current in this.m_Pos)
		{
			if (current.Value)
			{
				Vector3 localPosition = this.m_Vector3List[current.Key];
				this.m_Pos[current.Key] = false;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load(path) as GameObject);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(base.transform);
				gameObject.transform.localPosition = localPosition;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				this.m_Trans.Add(current.Key, gameObject.transform);
				gameObject.name = current.Key.ToString();
				gameObject.layer = 14;
				return current.Key;
			}
		}
		return 0;
	}

	public Transform Get3DModel(int key)
	{
		return this.m_Trans[key];
	}

	public void Remove3DModel(int key)
	{
		Transform transform = this.m_Trans[key];
		this.m_Trans.Remove(key);
		this.m_Pos[key] = true;
		UnityEngine.Object.Destroy(transform.gameObject);
	}
}
