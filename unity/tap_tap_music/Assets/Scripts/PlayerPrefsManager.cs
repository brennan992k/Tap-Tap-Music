using System;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
	private static string LV = "lv";

	private static string SCORE = "bestscore";

	private static string STAR = "star";

	private static string DIFFICULTY = "passlv";

	private static string SELECTED_SONG_ID = "selectedSongId";

	private static string SELECTED_BALL_ID = "selectedBallId";

	private static string IS_GOT_BALL = "isGotBall";

	private static string IS_GOT_SONG = "isGotSong";

	private static string GOLD = "gold";

	private static string EXP = "exp";

	private static string MUSIC_OPEN_LV = "musicOpenLv";

	private static string FIRST_GAME = "firstGame";

	private static string LOGIN_TIME = "logintime";

	private static string LOADIN = "loadin";

	private static string LEARNING = "learning";

	private static string NEW_MUSIC_LV = "newmusiclv";

	private static string SOUND_OPEN = "soundOpen";

	private static string WARNING_PLAY = "warningPlay";

	private static string VEDIO_SHOW_ITME = "showVedioAdsTime";

	private static string MAX_LEVEL = "maxlevel";

	public static readonly int[] UpgradeExp = new int[]
	{
		400,
		400,
		400,
		400
	};

	public static readonly int[] UpgradeStar = new int[]
	{
		6,
		7,
		8,
		9
	};

	public static readonly int[] BUY_NEW_BALL_COST = new int[]
	{
		100,
		200,
		300
	};

	public static readonly int[] BUY_NEW_SONG_COST = new int[]
	{
		100,
		200,
		300,
		400
	};

	public static readonly int[] UPGRADE_REWARD_GEM = new int[]
	{
		100,
		200,
		300,
		400
	};

	public static readonly int INIT_GOLD = 0;

	public static readonly int ADS_REWARD_GEM = 30;

	public static readonly int[] VEDIO_REWARD_GEM = new int[]
	{
		20,
		40,
		60,
		80,
		100
	};

	public static readonly int VEDIO_REWARD_MIN = 10;

	private static string CUR_EXP = "curexp";

	private static string CUR_PERFECT = "curperfect";

	private static string CUR_GAME_PERCENT = "curgamepercent";

	private static string CUR_GEM = "curgem";

	private static string CUR_STAR = "curstar";

	private static string CUR_SPEED_LV = "curspeedlv";

	private static string CUR_SOCRE = "curscore";

	private static string UPGRADE_REWARD = "upgradeGem";

	private static string Version_Code = "VersionCode";

	private static string GAME_INFO = "gameInfo";

	private static string GAME_FAIL_INFO = "gameFailInfo";

	private static string GAME_GUIDE = "gameguide";

	public static int FirstGame
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.FIRST_GAME, 1);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.FIRST_GAME, value);
			PlayerPrefs.Save();
		}
	}

	public static int LoginTime
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.LOGIN_TIME, 0);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.LOGIN_TIME, value);
			PlayerPrefs.Save();
		}
	}

	public static int LoadIn
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.LOADIN, 0);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.LOADIN, value);
			PlayerPrefs.Save();
		}
	}

	public static int WarningStartGame
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.WARNING_PLAY, 0);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.WARNING_PLAY, value);
			PlayerPrefs.Save();
		}
	}

	public static int Learning
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.LEARNING, 0);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.LEARNING, value);
			PlayerPrefs.Save();
		}
	}

	public static int MaxLevel
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.MAX_LEVEL, -1);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.MAX_LEVEL, value);
			PlayerPrefs.Save();
		}
	}

	public static int VersionCode
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.Version_Code, 0);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.Version_Code, value);
			PlayerPrefs.Save();
		}
	}

	public static int GameGuide
	{
		get
		{
			return PlayerPrefs.GetInt(PlayerPrefsManager.GAME_GUIDE, 1);
		}
		set
		{
			PlayerPrefs.SetInt(PlayerPrefsManager.GAME_GUIDE, value);
			PlayerPrefs.Save();
		}
	}

	public static int PassInterstitialTime
	{
		get
		{
			return PlayerPrefs.GetInt("PassInterstitialTime", 0);
		}
		set
		{
			PlayerPrefs.SetInt("PassInterstitialTime", value);
			PlayerPrefs.Save();
		}
	}

	public static void SetSoundOpen(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.SOUND_OPEN, value);
		PlayerPrefs.Save();
	}

	public static int GetSoundOpen()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.SOUND_OPEN, 1);
	}

	public static void SetBestScore(int songId, int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.LV + songId + PlayerPrefsManager.SCORE, value);
		PlayerPrefs.Save();
	}

	public static int GetBestScore(int songId)
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.LV + songId + PlayerPrefsManager.SCORE, 0);
	}

	public static void SetStar(int songId, int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.LV + songId + PlayerPrefsManager.STAR, value);
		PlayerPrefs.Save();
	}

	public static int GetStar(int songId)
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.LV + songId + PlayerPrefsManager.STAR, 0);
	}

	public static void SetPassDifficulty(int songId, int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.LV + songId + PlayerPrefsManager.DIFFICULTY, value);
		PlayerPrefs.Save();
	}

	public static int GetPassDifficulty(int songId)
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.LV + songId + PlayerPrefsManager.DIFFICULTY, 0);
	}

	public static void SetSelectedSongId(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.SELECTED_SONG_ID, value);
		PlayerPrefs.Save();
	}

	public static int GetSelectedSongId()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.SELECTED_SONG_ID, 0);
	}

	public static void SetSelectedBallId(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.SELECTED_BALL_ID, value);
		PlayerPrefs.Save();
	}

	public static int GetSelectedBallId()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.SELECTED_BALL_ID, 0);
	}

	public static void SetIsGotBall(int ballId)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.IS_GOT_BALL + ballId, 1);
		PlayerPrefs.Save();
	}

	public static int GetIsGotBall(int ballId)
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.IS_GOT_BALL + ballId, 0);
	}

	public static void SetSongStatus(int songId, int status)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.IS_GOT_SONG + songId, status);
		PlayerPrefs.Save();
	}

	public static int GetSongStatus(int songId)
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.IS_GOT_SONG + songId, 0);
	}

	public static void SetGold(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.GOLD, value);
		PlayerPrefs.Save();
	}

	public static int GetGold()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.GOLD, 0);
	}

	public static void SetExp(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.EXP, value);
		PlayerPrefs.Save();
	}

	public static int GetExp()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.EXP, 0);
	}

	public static void SetMusicOpenLv(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.MUSIC_OPEN_LV, value);
		PlayerPrefs.Save();
	}

	public static int GetMusicOpenLv()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.MUSIC_OPEN_LV, 0);
	}

	public static void SetNewMusicLv(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.NEW_MUSIC_LV, value);
		PlayerPrefs.Save();
	}

	public static int GetNewMusicLv()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.NEW_MUSIC_LV, 0);
	}

	public static void SetShowVedioTime(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.VEDIO_SHOW_ITME, value);
		PlayerPrefs.Save();
	}

	public static int GetShowVedioTime()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.VEDIO_SHOW_ITME, 0);
	}

	public static void SetCurScore(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.CUR_SOCRE, value);
		PlayerPrefs.Save();
	}

	public static int GetCurScore()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.CUR_SOCRE, 0);
	}

	public static void SetCurExp(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.CUR_EXP, value);
		PlayerPrefs.Save();
	}

	public static int GetCurExp()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.CUR_EXP, 0);
	}

	public static void SetCurPerfect(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.CUR_PERFECT, value);
		PlayerPrefs.Save();
	}

	public static int GetCurPerfect()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.CUR_PERFECT, 0);
	}

	public static void SetCurGamePercent(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.CUR_GAME_PERCENT, value);
		PlayerPrefs.Save();
	}

	public static int GetCurGamePercent()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.CUR_GAME_PERCENT, 0);
	}

	public static void SetCurSpeedLv(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.CUR_SPEED_LV, value);
		PlayerPrefs.Save();
	}

	public static int GetCurSpeedLv()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.CUR_SPEED_LV, 0);
	}

	public static void SetCurGem(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.CUR_GEM, value);
		PlayerPrefs.Save();
	}

	public static int GetCurGem()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.CUR_GEM, 0);
	}

	public static void SetCurStar(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.CUR_STAR, value);
		PlayerPrefs.Save();
	}

	public static int GetCurStar()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.CUR_STAR, 0);
	}

	public static void SetUpgradeGem(int value)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.UPGRADE_REWARD, value);
		PlayerPrefs.Save();
	}

	public static int GetUpgradeGem()
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.UPGRADE_REWARD, 0);
	}

	public static void SetGameInfo(int songId, int speedScale)
	{
		PlayerPrefs.SetString(PlayerPrefsManager.GAME_INFO, songId.ToString() + "," + speedScale.ToString());
	}

	public static string GetGameInfo()
	{
		return PlayerPrefs.GetString(PlayerPrefsManager.GAME_INFO);
	}

	public static void SetGameFailInfo(int songId, int speedScale, int swapIndex)
	{
		PlayerPrefs.SetString(PlayerPrefsManager.GAME_FAIL_INFO, string.Concat(new object[]
		{
			songId.ToString(),
			",",
			speedScale.ToString(),
			",",
			swapIndex
		}));
	}

	public static string GetGameFailInfo()
	{
		return PlayerPrefs.GetString(PlayerPrefsManager.GAME_FAIL_INFO);
	}

	public static void SetGameGuideOver(int songid)
	{
		PlayerPrefs.SetInt(PlayerPrefsManager.GAME_GUIDE + songid, 0);
		PlayerPrefs.Save();
	}

	public static int GetGameGuide(int songid)
	{
		return PlayerPrefs.GetInt(PlayerPrefsManager.GAME_GUIDE + songid, 1);
	}
}
