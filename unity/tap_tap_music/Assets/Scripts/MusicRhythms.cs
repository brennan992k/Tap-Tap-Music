using System;
using UnityEngine;

public class MusicRhythms
{
	private string musicName;

	private string fileName;

	private float speed;

	private int rhythmCount;

	private int rhythmsIndex;

	private int validRhythmCount;

	public char[] rhythms;

	private static MusicRhythms instance;

	public string MusicName
	{
		get
		{
			return this.musicName;
		}
	}

	public float Speed
	{
		get
		{
			return this.speed;
		}
	}

	public int RhythmCount
	{
		get
		{
			return this.rhythmCount;
		}
	}

	public int ValidRhythmCount
	{
		get
		{
			return this.validRhythmCount;
		}
	}

	public int RhythmsIndex
	{
		get
		{
			return this.rhythmsIndex;
		}
		set
		{
			this.rhythmsIndex = value;
		}
	}

	public static MusicRhythms Instance
	{
		get
		{
			return MusicRhythms.instance;
		}
	}

	public static MusicRhythms Create(int level)
	{
		MusicRhythms musicRhythms = new MusicRhythms();
		musicRhythms.ReadFile(level);
		MusicRhythms.instance = musicRhythms;
		return musicRhythms;
	}

	private void ReadFile(int level)
	{
		MusicInfo value = MusicList.Instance.GetValue(level);
		this.fileName = value.TextFile;
		this.musicName = value.MusicName;
		this.speed = (float)value.Rhythm;
		TextAsset textAsset = Resources.Load<TextAsset>(this.fileName);
		string text = textAsset.text;
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		if (array.Length <= 0)
		{
			UnityEngine.Debug.LogError(string.Format("illegal file fileName={0}  line={1}", this.fileName, array.Length));
			return;
		}
		this.rhythms = array[0].ToCharArray();
		this.rhythmCount = this.rhythms.Length;
		int num = 0;
		for (int i = 0; i < this.rhythmCount; i++)
		{
			if (this.rhythms[i] == ',')
			{
				num++;
			}
			else if (this.rhythms[i] == '1')
			{
				this.validRhythmCount++;
			}
		}
		this.rhythmCount -= num;
		if (this.rhythmCount <= 0)
		{
			UnityEngine.Debug.LogError(string.Format("illegal file fileName={0}  rhythmCount={1}", this.fileName, this.rhythmCount));
			return;
		}
		this.rhythmsIndex = 0;
	}

	public char NextRhythms()
	{
		if (this.rhythmsIndex < this.rhythms.Length)
		{
			return this.rhythms[this.rhythmsIndex++];
		}
		return '@';
	}

	public void JumpToCurrentRhythms()
	{
		float value = (float)this.rhythmsIndex * 1f / (float)this.rhythmCount;
		SpectrumKernel.JumpAudio(value);
	}

	public void JumpToCurrentRhythms(int index)
	{
		float value = (float)index * 1f / (float)this.rhythmCount;
		SpectrumKernel.JumpAudio(value);
	}

	public bool IsMusicComplete()
	{
		return !SpectrumKernel.IsAudioPlaying();
	}
}
