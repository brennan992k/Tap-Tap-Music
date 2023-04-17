using System;
using UnityEngine;

public class UMConstants : MonoBehaviour
{
	public enum UMAdsConstantsId
	{
		BoxAdsBuyGem = 1,
		BoxAdsBuySong,
		BoxAdsBuyBall,
		ListAdsBuySong,
		UpgradeAdsDouble,
		ReviveAds,
		OverAdsDouble,
		OverAdsGem,
		BoxAdsBuyGemSucc,
		BoxAdsBuySongSucc,
		BoxAdsBuyBallSucc,
		ListAdsBuySongSucc,
		UpgradeAdsDoubleSucc,
		ReviveAdsSucc,
		OverAdsDoubleSucc,
		OverAdsGemSucc
	}

	public enum UMGemConstantsId
	{
		BoxBuySong = 1,
		BoxBuyBall,
		ListBuySong,
		ListBuyBall
	}

	public static string umAds = "umAds";

	public static string umGem = "umGem";

	public static string umMusic = "umMusic";

	public static string umBall = "umBall";

	public static string umGameMusic = "umGameMusic";

	public static string umFail = "umFail";

	public static string umWin = "umWin";

	public static string umRevive = "umReviv";

	public static string umEnterGame = "umEnterGame";

	public static string umFailInfo = "umFailInfo";

	public static string umGuidFail = "umGuidFail";

	public static string umNextLevel = "umNextLevel";
}
