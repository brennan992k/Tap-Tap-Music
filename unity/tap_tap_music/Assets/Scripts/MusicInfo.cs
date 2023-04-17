using System;

public class MusicInfo
{
	private int _songId;

	private string _musicName;

	private string _txtFile;

	private string _mp3File;

	private int _rhythm;

	private int _score;

	private int _passLv;

	private int _starLv;

	public string MusicName
	{
		get
		{
			return this._musicName;
		}
	}

	public string TextFile
	{
		get
		{
			return this._txtFile;
		}
	}

	public string Mp3File
	{
		get
		{
			return this._mp3File;
		}
	}

	public int Rhythm
	{
		get
		{
			return this._rhythm;
		}
	}

	public int Score
	{
		get
		{
			return this._score;
		}
		set
		{
			this._score = value;
		}
	}

	public int PassLv
	{
		get
		{
			return this._passLv;
		}
		set
		{
			this._passLv = value;
		}
	}

	public int StarLv
	{
		get
		{
			return this._starLv;
		}
		set
		{
			this._starLv = value;
		}
	}

	public MusicInfo(int songId, string musicName, int rhythm, string txtFile, string mp3File)
	{
		this._songId = songId;
		this._musicName = musicName;
		this._txtFile = "music/" + txtFile;
		this._mp3File = "music/" + mp3File;
		this._rhythm = rhythm;
	}

	public void RefreshData()
	{
		this._passLv = PlayerPrefsManager.GetPassDifficulty(this._songId);
		this._starLv = PlayerPrefsManager.GetStar(this._songId);
		this._score = PlayerPrefsManager.GetBestScore(this._songId);
	}
}
