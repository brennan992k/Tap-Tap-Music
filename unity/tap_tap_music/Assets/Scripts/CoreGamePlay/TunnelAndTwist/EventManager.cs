using DG.Tweening;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AppAdvisory.TunnelAndTwist
{
	public class EventManager : MonoBehaviour
	{
		public enum RewardType
		{
			doubleGem,
			addGem
		}

		public delegate void RefreshGemEvent(EventManager.RewardType type);

		public delegate void AnimLearningEvent(AnimIntro g);

		public delegate void AnimIntroEvent(AnimIntro g);

		public delegate void AnimFailEvent(AnimFail g);

		public delegate void AnimReviveEvent(AnimStatus status);

		public delegate void AnimWinEvent(AnimStatus status);

		public delegate void AnimGameOverEvent();

		public delegate void AnimGameEndEvent();

		public delegate void UpgradeEvent();

		public delegate void PlayerStartEvent();

		public delegate void PlayerJumpEvent();

		public delegate void GetPointEvent();

		public delegate void ReloadSceneEvent();

		public delegate void DespawnPlatformEvent(PlatformParent platformcs);

		public delegate void OnCurve(TCurve c);

		public delegate void PlatformColor(int status);

		public delegate void PointColor(Color c);

		public delegate void PointScale(float scale);

		public delegate void MotionBlur(float f, bool enabled);

		public delegate void SetSpeed(float speed);

		public delegate void UpSpeed(AnimStatus animStatus);

		public delegate void SetAddSpeed(AddSpeed addSpeed);

		public delegate void AddOnePerfect(GameObject pointTrigger);

		public delegate void AddOnePoint(GameObject pointTrigger);

		public delegate void AddGem();

		public delegate void DesactivatePointTrigger(GameObject pointTrigger);

		public delegate void SetComboCount(int comboCount);

		public delegate void CheckPerfectJump(AnimStatus status);

		public delegate void BackHomeEvent();

		public delegate void ListeningTestEvent(int singid);

		public delegate void SelectBallEvent(int id);

		public delegate void SelectSongEvent(int id);

		public delegate void MusicSpeedEvent(float value);

		public delegate void CollidePlatForm();

		public delegate void RefreshUIStatus();

		public delegate void GoldNotEnough();

		public delegate void ShowShop();

		public delegate void ShowNotice(string notice);

		public delegate void PauseForGuid(AnimStatus status);

		public delegate void SkipGuid();

















































































		public static event EventManager.RefreshGemEvent OnRefreshGemEvent;

		public static event EventManager.AnimLearningEvent OnAnimLearningEvent;

		public static event EventManager.AnimIntroEvent OnAnimIntroEvent;

		public static event EventManager.AnimFailEvent OnAnimFailEvent;

		public static event EventManager.AnimReviveEvent OnAnimReviveEvent;

		public static event EventManager.AnimWinEvent OnAnimWinEvent;

		public static event EventManager.AnimGameOverEvent OnAnimGameOverEvent;

		public static event EventManager.AnimGameEndEvent OnAnimGameEndEvent;

		public static event EventManager.UpgradeEvent OnUpgradeEvent;

		public static event EventManager.PlayerStartEvent OnPlayerStartEvent;

		public static event EventManager.PlayerJumpEvent OnPlayerJumpEvent;

		public static event EventManager.GetPointEvent OnGetPointEvent;

		public static event EventManager.ReloadSceneEvent OnReloadSceneEvent;

		public static event EventManager.DespawnPlatformEvent OnDespawnPlatformEvent;

		public static event EventManager.OnCurve OnChangeCurve;

		public static event EventManager.PlatformColor OnChangePlatformColor;

		public static event EventManager.PointColor OnChangePointColor;

		public static event EventManager.PointScale OnPointScale;

		public static event EventManager.MotionBlur OnMotionBlur;

		public static event EventManager.SetSpeed OnSetSpeed;

		public static event EventManager.UpSpeed OnUpSpeed;

		public static event EventManager.SetAddSpeed OnSetAddSpeed;

		public static event EventManager.AddOnePerfect OnAddOnePerfect;

		public static event EventManager.AddOnePoint OnAddOnePoint;

		public static event EventManager.AddGem OnAddGem;

		public static event EventManager.DesactivatePointTrigger OnDesactivatePointTrigger;

		public static event EventManager.SetComboCount OnSetComboCount;

		public static event EventManager.CheckPerfectJump OnCheckPerfectJump;

		public static event EventManager.BackHomeEvent OnBackHomeEvent;

		public static event EventManager.ListeningTestEvent OnListeningTestEvent;

		public static event EventManager.SelectBallEvent OnSelectBallEvent;

		public static event EventManager.SelectSongEvent OnSelectSongEvent;

		public static event EventManager.MusicSpeedEvent OnMusicSpeedEvent;

		public static event EventManager.CollidePlatForm OnCollidePlatForm;

		public static event EventManager.RefreshUIStatus OnRefreshUIStatus;

		public static event EventManager.GoldNotEnough OnGoldNotEnough;

		public static event EventManager.ShowShop OnShowShop;

		public static event EventManager.ShowNotice OnShowNotice;

		public static event EventManager.PauseForGuid OnPauseForGuid;

		public static event EventManager.SkipGuid OnSkipGuid;

		public static void DoRefreshGemEvent(EventManager.RewardType type)
		{
			if (EventManager.OnRefreshGemEvent != null)
			{
				EventManager.OnRefreshGemEvent(type);
			}
		}

		public static void DoAnimLearningEvent(AnimIntro g)
		{
			if (EventManager.OnAnimLearningEvent != null)
			{
				EventManager.OnAnimLearningEvent(g);
			}
		}

		public static void DOAnimIntroEvent(AnimIntro g)
		{
			if (EventManager.OnAnimIntroEvent != null)
			{
				EventManager.OnAnimIntroEvent(g);
			}
		}

		public static void DOAnimFailEvent(AnimFail g)
		{
			if (EventManager.OnAnimFailEvent != null)
			{
				EventManager.OnAnimFailEvent(g);
			}
		}

		public static void DOAnimReviveEvent(AnimStatus status)
		{
			if (EventManager.OnAnimReviveEvent != null)
			{
				EventManager.OnAnimReviveEvent(status);
			}
		}

		public static void DOAnimWinEvent(AnimStatus status)
		{
			if (EventManager.OnAnimWinEvent != null)
			{
				EventManager.OnAnimWinEvent(status);
			}
		}

		public static void DOAnimGameOverEvent()
		{
			if (EventManager.OnAnimGameOverEvent != null)
			{
				EventManager.OnAnimGameOverEvent();
			}
		}

		public static void DoAnimGameEndEvent()
		{
			if (EventManager.OnAnimGameEndEvent != null)
			{
				EventManager.OnAnimGameEndEvent();
			}
		}

		public static void DOUpgradeEvent()
		{
			if (EventManager.OnUpgradeEvent != null)
			{
				EventManager.OnUpgradeEvent();
			}
		}

		public static void DOPlayerStartEvent()
		{
			if (EventManager.OnPlayerStartEvent != null)
			{
				EventManager.OnPlayerStartEvent();
			}
		}

		public static void DOPlayerJumpEvent()
		{
			if (EventManager.OnPlayerJumpEvent != null)
			{
				EventManager.OnPlayerJumpEvent();
			}
		}

		public static void DOGetPointEvent()
		{
			if (EventManager.OnGetPointEvent != null)
			{
				EventManager.OnGetPointEvent();
			}
		}

		public static void DOReloadSceneEvent()
		{
			if (EventManager.OnReloadSceneEvent != null)
			{
				EventManager.OnReloadSceneEvent();
			}
			DOTween.KillAll(false);
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
			Resources.UnloadUnusedAssets();
		}

		public static void DODespawnPlatformEvent(PlatformParent platformcs)
		{
			if (EventManager.OnDespawnPlatformEvent != null)
			{
				EventManager.OnDespawnPlatformEvent(platformcs);
			}
		}

		public static void DOCurve(TCurve c)
		{
			if (EventManager.OnChangeCurve != null)
			{
				EventManager.OnChangeCurve(c);
			}
		}

		public static void DOPlatformColor(int status)
		{
			if (EventManager.OnChangePlatformColor != null)
			{
				EventManager.OnChangePlatformColor(status);
			}
		}

		public static void DOPointColor(Color c)
		{
			if (EventManager.OnChangePointColor != null)
			{
				EventManager.OnChangePointColor(c);
			}
		}

		public static void DOPointScale(float scale)
		{
			if (EventManager.OnPointScale != null)
			{
				EventManager.OnPointScale(scale);
			}
		}

		public static void DOMotionBlur(float f, bool enabled)
		{
			if (EventManager.OnMotionBlur != null)
			{
				EventManager.OnMotionBlur(f, enabled);
			}
		}

		public static void DOSetSpeed(float speed)
		{
			if (EventManager.OnSetSpeed != null)
			{
				EventManager.OnSetSpeed(speed);
			}
		}

		public static void DoUpSpeed(AnimStatus animStatus)
		{
			if (EventManager.OnUpSpeed != null)
			{
				EventManager.OnUpSpeed(animStatus);
			}
		}

		public static void DOSetAddSpeed(AddSpeed addSpeed)
		{
			if (EventManager.OnSetAddSpeed != null)
			{
				EventManager.OnSetAddSpeed(addSpeed);
			}
		}

		public static void DOAddOnePerfect(GameObject pointTrigger)
		{
			if (EventManager.OnAddOnePerfect != null)
			{
				EventManager.OnAddOnePerfect(pointTrigger);
			}
		}

		public static void DOAddOnePoint(GameObject pointTrigger)
		{
			if (EventManager.OnAddOnePoint != null)
			{
				EventManager.OnAddOnePoint(pointTrigger);
			}
		}

		public static void DoAddGem()
		{
			if (EventManager.OnAddGem != null)
			{
				EventManager.OnAddGem();
			}
		}

		public static void DODesactivatePointTrigger(GameObject pointTrigger)
		{
			if (EventManager.OnDesactivatePointTrigger != null)
			{
				EventManager.OnDesactivatePointTrigger(pointTrigger);
			}
		}

		public static void DOSetComboCount(int comboCount)
		{
			if (EventManager.OnSetComboCount != null)
			{
				EventManager.OnSetComboCount(comboCount);
			}
		}

		public static void DoCheckPerfectJump(AnimStatus status)
		{
			if (EventManager.OnCheckPerfectJump != null)
			{
				EventManager.OnCheckPerfectJump(status);
			}
		}

		public static void DOBackHomeEvent()
		{
			if (EventManager.OnBackHomeEvent != null)
			{
				EventManager.OnBackHomeEvent();
			}
		}

		public static void DOListeningTestEvent(int singid)
		{
			if (EventManager.OnListeningTestEvent != null)
			{
				EventManager.OnListeningTestEvent(singid);
			}
		}

		public static void DOSelectBallEvent(int ballId)
		{
			if (EventManager.OnSelectBallEvent != null)
			{
				EventManager.OnSelectBallEvent(ballId);
			}
		}

		public static void DOSelectSongEvent(int songId)
		{
			if (EventManager.OnSelectSongEvent != null)
			{
				EventManager.OnSelectSongEvent(songId);
			}
		}

		public static void DOMusicSpeedEvent(float value)
		{
			if (EventManager.OnMusicSpeedEvent != null)
			{
				EventManager.OnMusicSpeedEvent(value);
			}
		}

		public static void DOCollidePlatForm()
		{
			if (EventManager.OnCollidePlatForm != null)
			{
				EventManager.OnCollidePlatForm();
			}
		}

		public static void DoRefreshUIStatus()
		{
			if (EventManager.OnRefreshUIStatus != null)
			{
				EventManager.OnRefreshUIStatus();
			}
		}

		public static void DoGoldNotEnough()
		{
			if (EventManager.OnGoldNotEnough != null)
			{
				EventManager.OnGoldNotEnough();
			}
		}

		public static void DoShowShop()
		{
			if (EventManager.OnShowShop != null)
			{
				EventManager.OnShowShop();
			}
		}

		public static void DoShowNotice(string notice)
		{
			if (EventManager.OnShowNotice != null)
			{
				EventManager.OnShowNotice(notice);
			}
		}

		public static void DoPauseForGuid(AnimStatus status)
		{
			if (EventManager.OnPauseForGuid != null)
			{
				EventManager.OnPauseForGuid(status);
			}
		}

		public static void DoSkipGuid()
		{
			if (EventManager.OnSkipGuid != null)
			{
				EventManager.OnSkipGuid();
			}
		}
	}
}
