using System;
using UnityEngine;

public class OcMessage : MonoBehaviour
{
	private void videoCallBack()
	{
		//AdsManager.Instance.RewardedVideoCallBackSucc();
	}

	private void videoCallBackFail()
	{
	//AdsManager.Instance.RewardedVideoCallBackFail();
	}

	private void interstitialAdCallBack()
	{
	//AdsManager.Instance.InterstitialCallBackSucc();
	}

	private void interstitialAdBackFail()
	{
		//AdsManager.Instance.InterstitialCallBackFail();
	}
}
