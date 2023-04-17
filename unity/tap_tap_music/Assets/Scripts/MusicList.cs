using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicList
{
	private static MusicList _instance;

	private string filePath = "music/musicList";

	private Dictionary<int, MusicInfo> dic = new Dictionary<int, MusicInfo>();

	public static MusicList Instance
	{
		get
		{
			if (MusicList._instance == null)
			{
				MusicList._instance = new MusicList();
			}
			return MusicList._instance;
		}
	}

	private MusicList()
	{
		TextAsset textAsset = Resources.Load<TextAsset>(this.filePath);
		string text = textAsset.text;
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		int num = 0;
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text2 = array2[i];
			if (text2 != null)
			{
				string[] array3 = text2.Split(new char[]
				{
					','
				});
				if (array3.Length == 4)
				{
					MusicInfo musicInfo = new MusicInfo(num, array3[0], int.Parse(array3[1]), array3[2], array3[3]);
					musicInfo.Score = PlayerPrefsManager.GetBestScore(num);
					musicInfo.StarLv = PlayerPrefsManager.GetStar(num);
					musicInfo.PassLv = PlayerPrefsManager.GetPassDifficulty(num);
					this.dic.Add(num, musicInfo);
					num++;
				}
			}
		}
	}

	public MusicInfo GetValue(int key)
	{
		if (!this.dic.ContainsKey(key))
		{
			return null;
		}
		MusicInfo result = null;
		this.dic.TryGetValue(key, out result);
		return result;
	}

	public int GetMusicCount()
	{
		return this.dic.Count;
	}

	public void RefreshMusicData()
	{
		for (int i = 0; i < this.dic.Count; i++)
		{
			this.dic[i].RefreshData();
		}
	}
}
