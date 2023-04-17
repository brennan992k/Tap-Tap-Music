using System;
using UnityEngine;

public class ButtonMessageYes : MonoBehaviour
{
	public GameObject messageBox;

	public void OnClickedClose()
	{
		if (this.messageBox != null)
		{
			this.messageBox.SetActive(false);
		}
	}
}
