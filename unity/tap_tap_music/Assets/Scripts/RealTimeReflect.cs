using System;
using UnityEngine;

public class RealTimeReflect : MonoBehaviour
{
	private ReflectionProbe probe;

	private void Awake()
	{
		this.probe = base.GetComponent<ReflectionProbe>();
	}

	private void Update()
	{
		this.probe.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y * -1f, Camera.main.transform.position.z);
		this.probe.RenderProbe();
	}
}
