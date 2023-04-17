using System;
using UnityEngine;

public class SelectMusicSelectedSong : MonoBehaviour
{
	private Vector3 rotate;

	private void Awake()
	{
		this.rotate = new Vector3(0f, 0f, 8f);
	}

	private void Update()
	{
		base.transform.Rotate(this.rotate);
	}
}
