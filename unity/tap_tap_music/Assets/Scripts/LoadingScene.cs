using System;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
	private void Awake()
	{
		PlayerPrefsManager.LoginTime++;
		PlayerPrefsManager.LoadIn = 1;
		PlayerPrefsManager.WarningStartGame = 1;
		if (PlayerPrefsManager.FirstGame == 1)
		{
			PlayerPrefsManager.FirstGame = 0;
			for (int i = 0; i < 2; i++)
			{
				PlayerPrefsManager.SetIsGotBall(i);
			}
			for (int j = 0; j < 3; j++)
			{
				PlayerPrefsManager.SetSongStatus(j, 2);
			}
			PlayerPrefsManager.SetSongStatus(3, 0);
			PlayerPrefsManager.SetMusicOpenLv(0);
			PlayerPrefsManager.SetSelectedBallId(0);
			PlayerPrefsManager.SetSelectedSongId(0);
			PlayerPrefsManager.SetGold(PlayerPrefsManager.INIT_GOLD);
		//AdsManager.Instance.FaceBookCoin(true, PlayerPrefsManager.INIT_GOLD, PlayerPrefsManager.GetGold(), "first load");
		}
        /*
		AdsManager instance = AdsManager.Instance;
		int num = instance.FaceBookGetVersionCode();
		if (num != 0 && num != PlayerPrefsManager.VersionCode)
		{
			PlayerPrefsManager.VersionCode = num;
			instance.FaceBookBasicInfo();
			instance.AppsFlyGameUpdate();
		}
		*/
	}
}
