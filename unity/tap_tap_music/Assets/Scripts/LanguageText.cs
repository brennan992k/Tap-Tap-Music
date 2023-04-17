using System;
using UnityEngine;
using UnityEngine.UI;

public class LanguageText : MonoBehaviour
{
	public Text txt;

	public string key;

	protected void Start()
	{
		this.txt.text = I18NManager.Instance.GetValue(this.key);
	}
}
