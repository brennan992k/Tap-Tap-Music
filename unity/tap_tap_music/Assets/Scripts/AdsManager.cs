/*
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class AdsManager
{
	private static AdsManager instance;

	private bool hasShowMg;

	private bool removeAd;

	private Action<bool> _success;

	private Action<bool> _intAdsuccess;

	public static AdsManager Instance
	{
		get
		{
			if (AdsManager.instance == null)
			{
				AdsManager.instance = new AdsManager();
				AdsManager.instance.Init();
			}
			return AdsManager.instance;
		}
		set
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void _showAdBanner();

	[DllImport("__Internal")]
	private static extern void _hideBanner();

	[DllImport("__Internal")]
	private static extern void _showAdInt();

	[DllImport("__Internal")]
	private static extern bool _isAdIntEnable();

	[DllImport("__Internal")]
	private static extern void _praise();

	[DllImport("__Internal")]
	private static extern void _showTop();

	[DllImport("__Internal")]
	private static extern void _uploadScore(int index, int score);

	[DllImport("__Internal")]
	private static extern void _showMoreGame();

	[DllImport("__Internal")]
	private static extern void _showAdVideo();

	[DllImport("__Internal")]
	private static extern bool _isAdVideoEnable();

	[DllImport("__Internal")]
	private static extern bool _isBannerEnable();

	[DllImport("__Internal")]
	private static extern void _copyTextToClipboard(string input);

	public void Init()
	{
		this.removeAd = (PlayerPrefs.GetInt("bc.removeAd", 0) == 1);
	}

	public void ShowBanner()
	{
		if (!this.removeAd)
		{
			new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("ShowBanner", new object[0]);
		}
	}

	public void HideBanner()
	{
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("HideBanner", new object[0]);
	}

	public bool IsReadyRewardedVideo()
	{
		return new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic<bool>("IsAdVideoEnable", new object[0]);
	}

	public bool IsBannerEnable()
	{
		return new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic<bool>("IsBannerEnable", new object[0]);
	}

	public void ShowInterstitial(Action<bool> success = null)
	{
		this._intAdsuccess = success;
		if (!this.removeAd)
		{
			new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("ShowAdInt", new object[0]);
		}
		else if (this._intAdsuccess != null)
		{
			this._intAdsuccess(true);
		}
	}

	public void InterstitialCallBackSucc()
	{
		if (this._intAdsuccess != null)
		{
			this._intAdsuccess(true);
			this._intAdsuccess = null;
		}
	}

	public void InterstitialCallBackFail()
	{
		if (this._intAdsuccess != null)
		{
			this._intAdsuccess(false);
			this._intAdsuccess = null;
		}
	}

	public void ShowRewardedVideo(Action<bool> success)
	{
		this._success = success;
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("ShowAdVideo", new object[0]);
	}

	public void RewardedVideoCallBackSucc()
	{
		if (this._success != null)
		{
			this._success(true);
		}
	}

	public void RewardedVideoCallBackFail()
	{
		if (this._success != null)
		{
			this._success(false);
		}
	}

	public void ShowMoreGame()
	{
		this.hasShowMg = true;
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("ShowMoreGame", new object[0]);
	}

	public bool HasShowMoreGame()
	{
		return this.hasShowMg;
	}

	public void Rate()
	{
		this.hasShowMg = true;
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("ShowRate", new object[0]);
	}

	public void ShowAppRate()
	{
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("ShowAppRate", new object[0]);
	}

	public void BuyRemoveAd()
	{
		this.removeAd = true;
		PlayerPrefs.SetInt("bc.removeAd", 1);
		PlayerPrefs.Save();
		this.HideBanner();
	}

	public bool HasRemoveAd()
	{
		return this.removeAd;
	}

	public void ShowLeaderboard()
	{
	}

	public void UploadScore(int index, int score)
	{
	}

	public void OpenFacebook()
	{
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("OpenFacebook", new object[0]);
	}

	public void ReportEvent(string key, string type, string remark = "")
	{
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("ReportEvent", new object[]
		{
			key,
			type,
			remark
		});
	}

	public void UmEvent(string key, string lable)
	{
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("UmEvent", new object[]
		{
			key,
			lable
		});
	}

	public void CopyToClipboard(string input)
	{
		new AndroidJavaClass("com.abcgame.utils.AdManager").CallStatic("CopyTextToClipboard", new object[]
		{
			input
		});
	}

	public void FaceBookCoin(bool isGet, int num, int curCoins, string channel)
	{
		new AndroidJavaClass("com.abcgame.utils.FaceBookStatistic").CallStatic("LogGameCoinEevent", new object[]
		{
			isGet,
			num,
			curCoins,
			channel
		});
	}

	public void FaceBookAchievedLevel(int level)
	{
		new AndroidJavaClass("com.abcgame.utils.FaceBookStatistic").CallStatic("LogAchievedLevelEvent", new object[]
		{
			level
		});
	}

	public void FaceBookLevelEntry(int level)
	{
		new AndroidJavaClass("com.abcgame.utils.FaceBookStatistic").CallStatic("LogLevelEntryEvent", new object[]
		{
			level
		});
	}

	public void FaceBookLevelCompleted(int level, int speed, int reborns)
	{
		new AndroidJavaClass("com.abcgame.utils.FaceBookStatistic").CallStatic("LogLevelCompletedEvent", new object[]
		{
			level,
			speed,
			reborns
		});
	}

	public void FaceBookBasicInfo()
	{
		new AndroidJavaClass("com.abcgame.utils.FaceBookStatistic").CallStatic("LogBasicEvent", new object[0]);
	}

	public int FaceBookGetVersionCode()
	{
		return new AndroidJavaClass("com.abcgame.utils.FaceBookStatistic").CallStatic<int>("GetVersionColde", new object[0]);
	}

	public void AppsFlyStartPlayGame()
	{
		new AndroidJavaClass("com.com.dugames.ads.GameApplication").CallStatic("startPlayGame", new object[0]);
	}

	public void AppsFlyRebornByWatchVideo()
	{
		new AndroidJavaClass("com.com.dugames.ads.GameApplication").CallStatic("rebornByWatchVideo", new object[0]);
	}

	public void AppsFlyLevelAchieved(int level)
	{
		new AndroidJavaClass("com.com.dugames.ads.GameApplication").CallStatic("levelAchieved", new object[]
		{
			level
		});
	}

	public void AppsFlyGameUpdate()
	{
		new AndroidJavaClass("com.com.dugames.ads.GameApplication").CallStatic("gameUpdate", new object[0]);
	}
}
*/