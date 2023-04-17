using System;
using UnityEngine;

public class Turn_Move : MonoBehaviour
{
	public float TurnX;

	public float TurnY;

	public float TurnZ;

	public float MoveX;

	public float MoveY;

	public float MoveZ;

	public bool World;

	private void Start()
	{
	}

	private void Update()
	{
		if (this.World)
		{
			base.transform.Rotate(this.TurnX * Time.deltaTime, this.TurnY * Time.deltaTime, this.TurnZ * Time.deltaTime, Space.World);
			base.transform.Translate(this.MoveX * Time.deltaTime, this.MoveY * Time.deltaTime, this.MoveZ * Time.deltaTime, Space.World);
		}
		else
		{
			base.transform.Rotate(this.TurnX * Time.deltaTime, this.TurnY * Time.deltaTime, this.TurnZ * Time.deltaTime, Space.Self);
			base.transform.Translate(this.MoveX * Time.deltaTime, this.MoveY * Time.deltaTime, this.MoveZ * Time.deltaTime, Space.Self);
		}
	}
}
