using System;
using UnityEngine;

public class ChinarRotateObject : MonoBehaviour
{
	public Transform obj;

	public float speed = 10f;

	private bool _mouseDown;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this._mouseDown = true;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this._mouseDown = false;
		}
		if (!this._mouseDown)
		{
			return;
		}
		float axis = UnityEngine.Input.GetAxis("Mouse X");
		float axis2 = UnityEngine.Input.GetAxis("Mouse Y");
		this.obj.Rotate(Vector3.up, -axis * this.speed, Space.World);
		this.obj.Rotate(Vector3.right, axis2 * this.speed, Space.World);
	}
}
