using System;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour
{
	public string key = " ";

	private void Start()
	{
		base.GetComponent<Text>().text = I18NManager.Instance.GetValue(this.key);
	}
}
