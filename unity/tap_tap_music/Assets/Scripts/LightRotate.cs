using System;
using UnityEngine;

public class LightRotate : MonoBehaviour
{
	public float speed = 200f;

	private void Update()
	{
		base.transform.Rotate(new Vector3(0f, 0f, this.speed * Time.deltaTime));
	}
}
