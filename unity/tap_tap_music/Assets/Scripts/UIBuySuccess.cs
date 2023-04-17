using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBuySuccess : MonoBehaviour
{
	public Text notice;

	public void ShowNotice(string str)
	{
		this.notice.text = str;
	}
}
