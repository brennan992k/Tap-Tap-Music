using System;
using System.Collections.Generic;
using UnityEngine;

public class I18NManager
{
	private static I18NManager instance;

	private const string chinese = "Chinese";

	private const string english = "English";

	private const string chineseTW = "ChineseTW";

	private const string DE = "DE";

	private const string ES = "ES";

	private const string FR = "FR";

	private const string JA = "JA";

	private const string KO = "KO";

	private const string PT = "PT";

	private const string RU = "RU";

	private string language = "Chinese";

	private bool isChinese;

	private Dictionary<string, string> dic = new Dictionary<string, string>();

	public static I18NManager Instance
	{
		get
		{
			if (I18NManager.instance == null)
			{
				I18NManager.instance = new I18NManager();
			}
			return I18NManager.instance;
		}
		set
		{
		}
	}

	public I18NManager()
	{
		if (Application.systemLanguage == SystemLanguage.ChineseSimplified)
		{
			this.language = "Chinese";
			this.isChinese = true;
		}
		else if (Application.systemLanguage == SystemLanguage.ChineseTraditional || Application.systemLanguage == SystemLanguage.Chinese)
		{
			this.language = "ChineseTW";
			this.isChinese = true;
		}
		else if (Application.systemLanguage == SystemLanguage.German)
		{
			this.language = "DE";
			this.isChinese = false;
		}
		else if (Application.systemLanguage == SystemLanguage.Spanish)
		{
			this.language = "ES";
			this.isChinese = false;
		}
		else if (Application.systemLanguage == SystemLanguage.French)
		{
			this.language = "FR";
			this.isChinese = false;
		}
		else if (Application.systemLanguage == SystemLanguage.Japanese)
		{
			this.language = "JA";
			this.isChinese = false;
		}
		else if (Application.systemLanguage == SystemLanguage.Korean)
		{
			this.language = "KO";
			this.isChinese = false;
		}
		else if (Application.systemLanguage == SystemLanguage.Portuguese)
		{
			this.language = "PT";
			this.isChinese = false;
		}
		else if (Application.systemLanguage == SystemLanguage.Russian)
		{
			this.language = "RU";
			this.isChinese = false;
		}
		else
		{
			this.language = "English";
			this.isChinese = false;
		}
		TextAsset textAsset = Resources.Load<TextAsset>(this.language);
		string text = textAsset.text;
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text2 = array2[i];
			if (text2 != null)
			{
				string[] array3 = text2.Split(new char[]
				{
					'@'
				});
				if (array3.Length == 2)
				{
					this.dic.Add(array3[0], array3[1]);
				}
			}
		}
	}

	public string GetValue(string key)
	{
		if (!this.dic.ContainsKey(key))
		{
			return key;
		}
		string result = null;
		this.dic.TryGetValue(key, out result);
		return result;
	}

	public bool IsChinese()
	{
		return this.isChinese;
	}
}
