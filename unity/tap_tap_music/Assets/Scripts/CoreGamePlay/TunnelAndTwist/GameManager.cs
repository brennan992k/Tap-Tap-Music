using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class GameManager : MonoBehaviorHelper
	{
		private class MusicRevivePoint
		{
			private int platfromIndex;

			private int rhythmsIndex;

			public int RhythmsIndex
			{
				get
				{
					return this.rhythmsIndex;
				}
			}

			public int PlatfromIndex
			{
				get
				{
					return this.platfromIndex;
				}
			}

			public MusicRevivePoint(int platForm, int rhythms)
			{
				this.platfromIndex = platForm;
				this.rhythmsIndex = rhythms;
			}
		}

		private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _selecteSongId___0;

			internal int _i___1;

			internal GameManager _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _Start_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._this.gemCount = 0;
					this._this.curStar = 0;
					this._this.point = 0;
					this._this.bestPerfects = 0;
					this._this.perfectCount = 0;
					this._this.isPerfect = false;
					this._this.forcePlatformIndex = false;
					this._this.totalScore = 0;
					this._this.spawnCount = 0;
					this._this.reviveCount = 0;
					Time.timeScale = 1f;
					GC.Collect();
					this._this.musicRevivePoints.Clear();
					this._this.changeColorDot.Clear();
					this._this.platformScore.Clear();
					this._this.platformPerfect.Clear();
					GameManager.gemRate.Clear();
					this._this.platformParents.Clear();
					Application.targetFrameRate = 60;
					Physics.gravity = Vector3.zero;
					this._selecteSongId___0 = PlayerPrefsManager.GetSelectedSongId();
					this._this.musicData = MusicRhythms.Create(this._selecteSongId___0);
					this._this.StartCoroutine(this._this.ResetGemRate());
					this._this.curSongId = this._selecteSongId___0;
					this._this.bestScore = PlayerPrefsManager.GetBestScore(this._this.curSongId);
					this._this.isGameGuid = (this._this.curSongId < 3 && PlayerPrefsManager.GetGameGuide(this._this.curSongId) == 1);
					this._this.FirstPlatForm(false);
					this._i___1 = 0;
					break;
				case 1u:
					this._this.SpawnPlatform();
					this._i___1++;
					break;
				default:
					return false;
				}
				if (this._i___1 < this._this.numPlatformAtStart)
				{
					this._current = new WaitForSeconds(0.1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.StartGuid(0, 1);
			//this._this.adsManager.FaceBookLevelEntry(this._this.curSongId + 1);
				this._PC = -1;
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _ResetGemRate_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _random___0;

			internal int _totalGem___0;

			internal int _i___1;

			internal int _randomId___2;

			internal GameManager _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _ResetGemRate_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._random___0 = UnityEngine.Random.Range(10f + (float)this._this.speedScale, 12f + (float)this._this.speedScale);
					this._totalGem___0 = (int)((float)this._this.musicData.RhythmCount * this._random___0 / 100f);
					GameManager.gemRate.Clear();
					this._i___1 = 0;
					break;
				case 1u:
					this._randomId___2 = UnityEngine.Random.Range(0, this._this.musicData.ValidRhythmCount);
					if (!GameManager.gemRate.Contains(this._randomId___2))
					{
						GameManager.gemRate.Add(this._randomId___2);
					}
					this._i___1++;
					break;
				default:
					return false;
				}
				if (this._i___1 < this._totalGem___0)
				{
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._PC = -1;
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private int reviveCount;

		public UIGameManager uiGameManager;

		public float initScaleZ = 2f;

		private bool addSpeed;

		private bool forcePlatformIndex;

		private int speedScale;

		private int maxSpeedScale = 20;

		private float speedScaleStep = 0.1f;

		private PlatformParent firstPlatForm;

		private Player curPlayer;

		private List<int> changeColorDot;

		private int perfectCount;

		private bool isPerfect;

		private int totalScore;

		private int totalExp;

		private List<int> platformScore;

		private List<int> platformPerfect;

		private int bestPerfects;

		private int curSongId;

		private int bestScore;

		private int gemCount;

		private int curStar;

		private static List<int> gemRate;

		private int reviveBack = 2;

		private bool isGameGuid;

	//private AdsManager adsManager;

		private List<PlatformParent> platformParents;

		private List<GameManager.MusicRevivePoint> musicRevivePoints;

		[SerializeField]
		private int m_point;

		[SerializeField]
		private float _pointSpeed;

		[NonSerialized]
		public int spawnCount;

		public int numPlatformAtStart;

		private MusicRhythms musicData;

		private Obstacle currentObstacle;

		private Obstacle lastObstacle;

		private Transform currPlatForm;

		private Transform lastPlatForm;

		private float lastPosZ = -1f;

		private float lastScaleZ = -1f;

		private float cubeScaleZ = 3f;

		public float pointSpeed
		{
			get
			{
				return this._pointSpeed;
			}
			set
			{
				float num = value;
				if (num < 0f)
				{
					num = 0f;
				}
				this._pointSpeed = num;
				EventManager.DOSetSpeed(num);
			}
		}

		private int point
		{
			get
			{
				return this.m_point;
			}
			set
			{
				this.m_point = value;
			}
		}

		public static List<int> GetGemRate()
		{
			return GameManager.gemRate;
		}

		public int Add1Point()
		{
			this.point++;
			return this.point;
		}

		private void OnFirstTap(TouchDirection td)
		{
			InputTouch.OnTouched -= new InputTouch.OnTouch(this.OnFirstTap);
			this.pointSpeed = this.musicData.Speed * this.initScaleZ * 1f / 60f;
			EventManager.DOMusicSpeedEvent(1f);
		}

		private void Awake()
		{
            //this.adsManager = AdsManager.Instance;
            Application.targetFrameRate = 60;
			this.musicRevivePoints = new List<GameManager.MusicRevivePoint>();
			this.changeColorDot = new List<int>();
			this.platformScore = new List<int>();
			this.platformPerfect = new List<int>();
			GameManager.gemRate = new List<int>();
			this.curPlayer = base.player;
			this.firstPlatForm = UnityEngine.Object.FindObjectOfType<PlatformParent>();
			this.platformParents = new List<PlatformParent>();
			if (Time.realtimeSinceStartup < 3f)
			{
				DOTween.Init(null, null, null);
			}
		}

		private IEnumerator Start()
		{
			GameManager._Start_c__Iterator0 _Start_c__Iterator = new GameManager._Start_c__Iterator0();
			_Start_c__Iterator._this = this;
			return _Start_c__Iterator;
		}

		private IEnumerator ResetGemRate()
		{
			GameManager._ResetGemRate_c__Iterator1 _ResetGemRate_c__Iterator = new GameManager._ResetGemRate_c__Iterator1();
			_ResetGemRate_c__Iterator._this = this;
			return _ResetGemRate_c__Iterator;
		}

		private void OnEnable()
		{
			InputTouch.OnTouched += new InputTouch.OnTouch(this.OnFirstTap);
			EventManager.OnDespawnPlatformEvent += new EventManager.DespawnPlatformEvent(this.OnDespawnPlatformEvent);
			EventManager.OnAddOnePoint += new EventManager.AddOnePoint(this.OnAddOnePoint);
			EventManager.OnDesactivatePointTrigger += new EventManager.DesactivatePointTrigger(this.OnDesactivatePointTrigger);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnAnimWinEvent += new EventManager.AnimWinEvent(this.OnAnimWinEvent);
			EventManager.OnAnimGameEndEvent += new EventManager.AnimGameEndEvent(this.OnAnimGameEndEvent);
			EventManager.OnAnimReviveEvent += new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
			EventManager.OnUpSpeed += new EventManager.UpSpeed(this.OnUpSpeed);
			EventManager.OnAddOnePerfect += new EventManager.AddOnePerfect(this.OnAddOnePerfect);
			EventManager.OnAddGem += new EventManager.AddGem(this.OnAddGem);
			EventManager.OnCheckPerfectJump += new EventManager.CheckPerfectJump(this.OnCheckPerfectJump);
			EventManager.OnSkipGuid += new EventManager.SkipGuid(this.SkipGuid);
		}

		private void OnDisable()
		{
			InputTouch.OnTouched -= new InputTouch.OnTouch(this.OnFirstTap);
			EventManager.OnDespawnPlatformEvent -= new EventManager.DespawnPlatformEvent(this.OnDespawnPlatformEvent);
			EventManager.OnAddOnePoint -= new EventManager.AddOnePoint(this.OnAddOnePoint);
			EventManager.OnDesactivatePointTrigger -= new EventManager.DesactivatePointTrigger(this.OnDesactivatePointTrigger);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnAnimWinEvent -= new EventManager.AnimWinEvent(this.OnAnimWinEvent);
			EventManager.OnAnimGameEndEvent -= new EventManager.AnimGameEndEvent(this.OnAnimGameEndEvent);
			EventManager.OnAnimReviveEvent -= new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
			EventManager.OnUpSpeed -= new EventManager.UpSpeed(this.OnUpSpeed);
			EventManager.OnAddOnePerfect -= new EventManager.AddOnePerfect(this.OnAddOnePerfect);
			EventManager.OnAddGem -= new EventManager.AddGem(this.OnAddGem);
			EventManager.OnCheckPerfectJump -= new EventManager.CheckPerfectJump(this.OnCheckPerfectJump);
			EventManager.OnSkipGuid += new EventManager.SkipGuid(this.SkipGuid);
		}

		private void OnAnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.start)
			{
				this.perfectCount = 0;
				this.isPerfect = false;
				PlayerPrefsManager.SetGameFailInfo(PlayerPrefsManager.GetSelectedSongId(), this.speedScale, this.point);
			}
		}

		private void OnAnimGameEndEvent()
		{
			this.CheckGameResult();
			if (this.isGameGuid)
			{
				this.AddGuidReward();
			}
			EventManager.DoRefreshUIStatus();
			PlayerPrefsManager.SetGameInfo(PlayerPrefsManager.GetSelectedSongId(), this.speedScale);
			if (this.addSpeed || this.speedScale > 0)
			{
				EventManager.DOAnimWinEvent(AnimStatus.end);
				//this.adsManager.FaceBookLevelCompleted(this.curSongId + 1, this.speedScale + 1, this.reviveCount);
			//this.adsManager.AppsFlyLevelAchieved(this.curSongId + 1);
			}
			else
			{
				EventManager.DOAnimGameOverEvent();
			}
		}

		private void AddGuidReward()
		{
			int num = GameManager.gemRate.Count * this.point / this.musicData.ValidRhythmCount;
			int num2 = PlayerPrefsManager.GetGold() + num;
			PlayerPrefsManager.SetGold(num2);
			//this.adsManager.FaceBookCoin(true, num, num2, "pass guid");
		}

		private void OnAnimWinEvent(AnimStatus status)
		{
			if (status == AnimStatus.start)
			{
				this.CheckGameResult();
				EventManager.DOAnimWinEvent(AnimStatus.end);
				//this.adsManager.FaceBookLevelCompleted(this.curSongId + 1, this.speedScale + 1, this.reviveCount);
			//AdsManager.Instance.AppsFlyLevelAchieved(this.curSongId + 1);
			}
		}

		private int checkStar()
		{
			if (this.speedScale > 0)
			{
				return this.curStar;
			}
			int num = (this.musicData.ValidRhythmCount - 2) * 100;
			int result = 0;
			if ((float)this.totalScore >= (float)num * 0.9f)
			{
				result = 3;
			}
			else if ((float)this.totalScore >= (float)num * 0.6f)
			{
				result = 2;
			}
			else if ((float)this.totalScore >= (float)num * 0.3f)
			{
				result = 1;
			}
			this.curStar = result;
			return result;
		}

		private void CheckGameResult()
		{
			if (this.totalScore > PlayerPrefsManager.GetBestScore(this.curSongId))
			{
				PlayerPrefsManager.SetBestScore(this.curSongId, this.totalScore);
			}
			if (this.curStar > PlayerPrefsManager.GetStar(this.curSongId))
			{
				PlayerPrefsManager.SetStar(this.curSongId, this.curStar);
			}
			if (this.curSongId > PlayerPrefsManager.MaxLevel)
			{
				PlayerPrefsManager.MaxLevel = this.curSongId;
			//this.adsManager.FaceBookAchievedLevel(this.curSongId + 1);
			}
			this.totalExp += (100 + this.speedScale * 20) * this.point / this.musicData.ValidRhythmCount;
			PlayerPrefsManager.SetExp(PlayerPrefsManager.GetExp() + this.totalExp);
			int exp = PlayerPrefsManager.GetExp();
			int musicOpenLv = PlayerPrefsManager.GetMusicOpenLv();
			int num = 0;
			int num2 = MusicList.Instance.GetMusicCount() / 4;
			int num3 = PlayerPrefsManager.UpgradeExp.Length;
			for (int i = 1; i < num2; i++)
			{
				if (musicOpenLv < i)
				{
					int num4 = i - 1;
					if (num4 >= num3)
					{
						num4 = num3 - 1;
					}
					if (exp > PlayerPrefsManager.UpgradeExp[num4])
					{
						num = i;
					}
					break;
				}
			}
			if (num > 0)
			{
				PlayerPrefsManager.SetMusicOpenLv(num);
				PlayerPrefsManager.SetNewMusicLv(num);
				PlayerPrefsManager.SetExp(exp - PlayerPrefsManager.UpgradeExp[num - 1]);
				int num5 = PlayerPrefsManager.UPGRADE_REWARD_GEM[num];
				PlayerPrefsManager.SetGold(num5 + PlayerPrefsManager.GetGold());
			//this.adsManager.FaceBookCoin(true, num5, PlayerPrefsManager.GetGold(), "upgrade lv");
				PlayerPrefsManager.SetUpgradeGem(num5);
				int num6 = 4 * num;
				int num7 = num6 + 3;
				for (int j = num6; j < num7; j++)
				{
					PlayerPrefsManager.SetSongStatus(j, 2);
				}
				PlayerPrefsManager.SetSongStatus(num7, 0);
			}
			else
			{
				PlayerPrefsManager.SetNewMusicLv(0);
			}
			this.bestPerfects += this.platformPerfect.Count + 2;
			int curPerfect = this.bestPerfects * 100 / this.musicData.ValidRhythmCount;
			PlayerPrefsManager.SetCurPerfect(curPerfect);
			PlayerPrefsManager.SetCurExp(this.totalExp);
			PlayerPrefsManager.SetCurGem(this.gemCount);
			PlayerPrefsManager.SetCurStar(this.curStar);
			PlayerPrefsManager.SetCurSpeedLv(this.speedScale);
			PlayerPrefsManager.SetCurScore(this.totalScore);
			if (this.speedScale > 0)
			{
				PlayerPrefsManager.SetCurGamePercent(100);
			}
			else
			{
				PlayerPrefsManager.SetCurGamePercent(this.point * 100 / this.musicData.ValidRhythmCount);
			}
			int passDifficulty = PlayerPrefsManager.GetPassDifficulty(this.curSongId);
			if (passDifficulty < this.speedScale)
			{
				PlayerPrefsManager.SetPassDifficulty(this.curSongId, this.speedScale);
			}
			int num8 = this.curSongId + 1;
			bool flag = (num8 + 1) % 4 == 0;
			if (flag)
			{
				int songStatus = PlayerPrefsManager.GetSongStatus(num8);
				if (songStatus == 0 || songStatus == 1)
				{
					int num9 = PlayerPrefsManager.UpgradeStar.Length;
					int num10 = num8 / 4;
					if (num10 >= num9)
					{
						num10 = num9 - 1;
					}
					int num11 = PlayerPrefsManager.UpgradeStar[num10];
					int num12 = 0;
					int num13 = num8 - 3;
					int num14 = num13 + 3;
					for (int k = num13; k < num14; k++)
					{
						num12 += PlayerPrefsManager.GetStar(k);
					}
					if (num12 >= num11)
					{
						PlayerPrefsManager.SetSongStatus(num8, 2);
					}
				}
			}
		}

		public void OnAnimReviveEvent(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				return;
			}
			//AdsManager.Instance.AppsFlyRebornByWatchVideo();
			this.reviveCount++;
			this.firstPlatForm.gameObject.SetActive(true);
			this.curPlayer.gameObject.SetActive(true);
			int count = this.musicRevivePoints.Count;
			if (this.platformScore.Count < this.point)
			{
				this.point = this.platformScore.Count;
			}
			if (count == 0)
			{
				this.point = 0;
				this.spawnCount = 0;
				this.musicData.RhythmsIndex = 0;
			}
			for (int i = count - 1; i >= 0; i--)
			{
				GameManager.MusicRevivePoint musicRevivePoint = this.musicRevivePoints[i];
				if (musicRevivePoint.PlatfromIndex == this.point)
				{
					this.point = musicRevivePoint.PlatfromIndex;
					this.spawnCount = musicRevivePoint.PlatfromIndex;
					this.musicData.RhythmsIndex = musicRevivePoint.RhythmsIndex;
					break;
				}
			}
			while (this.platformPerfect.Count > 0 && this.platformPerfect[this.platformPerfect.Count - 1] > this.point)
			{
				this.platformPerfect.RemoveAt(this.platformPerfect.Count - 1);
			}
			if (this.platformPerfect.Count > 0 && this.platformPerfect[this.platformPerfect.Count - 1] == this.point)
			{
				this.perfectCount = 0;
				int j = this.platformPerfect.Count - 1;
				int num = 0;
				while (j >= 0)
				{
					if (this.platformPerfect[j] != this.point - num)
					{
						break;
					}
					this.perfectCount++;
					j--;
					num++;
				}
			}
			this.musicRevivePoints.Clear();
			this.platformParents.Clear();
			if (this.changeColorDot.Count > 0)
			{
				int index = this.changeColorDot.Count - 1;
				if (this.changeColorDot[index] > this.point)
				{
					this.changeColorDot.RemoveAt(index);
				}
			}
			if (this.spawnCount > 0)
			{
				this.musicData.JumpToCurrentRhythms(this.musicData.RhythmsIndex - this.reviveBack);
			}
			else
			{
				this.musicData.JumpToCurrentRhythms();
			}
			base.objectPooling.DespawnAll();
			this.FirstPlatForm(true);
			for (int k = 0; k < this.numPlatformAtStart; k++)
			{
				this.SpawnPlatform();
			}
			this.StartGuid(0, 1);
		}

		private void OnCheckPerfectJump(AnimStatus status)
		{
			if (status == AnimStatus.start)
			{
				int num;
				if (this.isPerfect)
				{
					this.isPerfect = false;
					this.perfectCount++;
					this.platformPerfect.Add(this.point);
					EventManager.DOSetComboCount(this.perfectCount);
					if (this.perfectCount > 1)
					{
						num = (int)(100f * (1f + 0.5f * (float)this.speedScale));
					}
					else
					{
						num = (int)(50f * (1f + 0.5f * (float)this.speedScale));
					}
				}
				else
				{
					this.perfectCount = 0;
					num = (int)(10f * (1f + 0.5f * (float)this.speedScale));
				}
				this.totalScore += num;
				this.platformScore.Add(num);
				this.uiGameManager.RefreshScore(this.totalScore);
				int star = this.checkStar();
				this.uiGameManager.RefreshStar(star);
			}
			else
			{
				for (int i = this.changeColorDot.Count - 1; i >= 0; i--)
				{
					if (this.point == this.changeColorDot[i])
					{
						EventManager.DOPlatformColor((i % 2 != 0) ? 0 : 1);
					}
				}
				if (this.addSpeed && (this.point == this.musicData.ValidRhythmCount || this.spawnCount >= 11))
				{
					this.addSpeed = false;
					this.totalExp += 100 + this.speedScale * 20;
					this.speedScale++;
					if (this.speedScale > this.maxSpeedScale)
					{
						this.speedScale = this.maxSpeedScale;
					}
					if (this.platformParents.Count > 0)
					{
						this.platformParents.RemoveAt(0);
					}
					if (this.isGameGuid)
					{
						this.uiGameManager.gameIntro.ShowEnd();
					}
					this.EndGuid();
					EventManager.DoUpSpeed(AnimStatus.start);
					EventManager.DoUpSpeed(AnimStatus.end);
					base.StartCoroutine(this.ResetGemRate());
					this.point = 0;
					this.musicRevivePoints.Clear();
					this.platformScore.Clear();
					this.changeColorDot.Clear();
					this.bestPerfects += this.platformPerfect.Count;
					this.platformPerfect.Clear();
				}
			}
		}

		private void OnAddOnePerfect(GameObject pointTrigger)
		{
			this.isPerfect = true;
		}

		private void OnAddOnePoint(GameObject pointTrigger)
		{
			this.Add1Point();
			if (this.addSpeed && (this.point == this.musicData.ValidRhythmCount || this.spawnCount >= 11))
			{
				float value = 1f + (float)(this.speedScale + 1) * this.speedScaleStep;
				this.uiGameManager.OnMusicSpeedEvent(value);
				this.uiGameManager.ShowUpSpeed();
			}
		}

		private void OnAddGem()
		{
			this.gemCount++;
			PlayerPrefsManager.SetGold(PlayerPrefsManager.GetGold() + 1);
		//this.adsManager.FaceBookCoin(true, 1, PlayerPrefsManager.GetGold(), "game pickup");
		}

		private void OnDesactivatePointTrigger(GameObject pointTrigger)
		{
			pointTrigger.SetActive(false);
			ObjectPooling.Despawn(pointTrigger);
		}

		private void OnDespawnPlatformEvent(PlatformParent platformcs)
		{
			this.SpawnPlatform();
		}

		private void FirstPlatForm(bool revive = false)
		{
			PlatformParent platformParent = this.firstPlatForm;
			Transform transform = platformParent.transform;
			int num = this.GetPlatformScaleZ();
			this.cubeScaleZ = platformParent.CubePlatforms[0].transform.localScale.z;
			if (revive && this.spawnCount > 0)
			{
				num += this.reviveBack;
			}
			if (this.spawnCount == 0)
			{
				transform.localScale = new Vector3(1f, 1f, (float)num + 2f);
				transform.position = new Vector3(0f, 0f, transform.localScale.z * this.cubeScaleZ + 0.5f * this.cubeScaleZ);
			}
			else
			{
				transform.localScale = new Vector3(1f, 1f, (float)num + 2f);
				transform.position = new Vector3(0f, 0f, transform.localScale.z * this.cubeScaleZ + 0.5f * this.cubeScaleZ);
			}
			transform.gameObject.SetActive(true);
			this.lastPosZ = transform.position.z;
			this.lastScaleZ = transform.localScale.z;
			this.currPlatForm = transform;
			this.currentObstacle = new Obstacle(this.spawnCount, 0);
			this.lastObstacle = null;
			this.lastPlatForm = null;
			platformParent.Set(this.currentObstacle);
			if (!this.isGameGuid && this.CheckGemRate(this.spawnCount))
			{
				platformParent.DoActiveGem();
			}
			this.spawnCount++;
			this.platformParents.Add(platformParent);
		}

		public Transform SpawnPlatform()
		{
			GameObject gameObject = base.objectPooling.Spawn("PlatformParentPrefab");
			gameObject.SetActive(true);
			Transform transform = gameObject.transform;
			int platformScaleZ = this.GetPlatformScaleZ();
			if (this.spawnCount == 0)
			{
				transform.localScale = new Vector3(1f, 1f, (float)platformScaleZ);
				transform.position = new Vector3(0f, 0f, this.lastPosZ + (float)platformScaleZ * this.cubeScaleZ - this.cubeScaleZ);
			}
			else if (platformScaleZ == 1)
			{
				transform.localScale = new Vector3(1f, 1f, (float)platformScaleZ + 1f);
				transform.position = new Vector3(0f, 0f, this.lastPosZ + (float)platformScaleZ * this.cubeScaleZ);
			}
			else
			{
				transform.localScale = new Vector3(1f, 1f, (float)platformScaleZ + 1f);
				transform.position = new Vector3(0f, 0f, this.lastPosZ + (float)platformScaleZ * this.cubeScaleZ);
			}
			this.lastPosZ = transform.position.z;
			this.lastScaleZ = transform.localScale.z;
			int exceptIndex = -1;
			if (this.lastObstacle != null && (platformScaleZ <= 2 || this.lastPlatForm.localScale.z < 2f))
			{
				exceptIndex = this.lastObstacle.getActiveIndex();
			}
			Obstacle obstacle;
			if (this.forcePlatformIndex && this.currentObstacle != null)
			{
				this.forcePlatformIndex = false;
				obstacle = new Obstacle(this.spawnCount, this.currentObstacle.getActiveIndex());
			}
			else
			{
				obstacle = new Obstacle(this.currentObstacle, this.spawnCount, exceptIndex);
			}
			this.lastObstacle = this.currentObstacle;
			this.lastPlatForm = this.currPlatForm;
			this.currentObstacle = obstacle;
			this.currPlatForm = transform;
			PlatformParent component = transform.GetComponent<PlatformParent>();
			component.Set(this.currentObstacle);
			if (platformScaleZ < 2)
			{
				component.ActivePlatformRotate45();
			}
			if (!this.isGameGuid && this.CheckGemRate(this.spawnCount))
			{
				component.DoActiveGem();
			}
			this.spawnCount++;
			this.platformParents.Add(component);
			return transform;
		}

		private bool CheckGemRate(int platformId)
		{
			bool result = false;
			for (int i = 0; i < GameManager.gemRate.Count; i++)
			{
				if (platformId == GameManager.gemRate[i])
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private int GetPlatformScaleZ()
		{
			int num = 1;
			int rhythmsIndex = this.musicData.RhythmsIndex;
			char c = this.musicData.NextRhythms();
			if (c == ',')
			{
				this.changeColorDot.Add(this.spawnCount);
				rhythmsIndex = this.musicData.RhythmsIndex;
				c = this.musicData.NextRhythms();
			}
			else if (c == '@')
			{
				this.addSpeed = true;
				this.forcePlatformIndex = true;
				this.spawnCount = 0;
				this.musicData.RhythmsIndex = 0;
				rhythmsIndex = this.musicData.RhythmsIndex;
				c = this.musicData.NextRhythms();
			}
			if (c == '0')
			{
				while (this.musicData.NextRhythms() == '0')
				{
					num++;
				}
				num++;
			}
			this.musicRevivePoints.Add(new GameManager.MusicRevivePoint(this.spawnCount, rhythmsIndex));
			return num;
		}

		private void OnUpSpeed(AnimStatus animStatus)
		{
			float num = 1f + (float)this.speedScale * this.speedScaleStep;
			if (animStatus == AnimStatus.start)
			{
				EventManager.DOMusicSpeedEvent(num);
			}
			else if (animStatus == AnimStatus.end)
			{
				EventManager.DOMusicSpeedEvent(num);
				this.pointSpeed = this.musicData.Speed * this.initScaleZ * num / 60f;
				this.uiGameManager.RefreshSpeedLv(this.speedScale);
			}
		}

		public float TimeOneRhythms()
		{
			float num = 1f + (float)this.speedScale * this.speedScaleStep;
			return 60f / (this.musicData.Speed * num);
		}

		private void OnDestroy()
		{
			DOTween.KillAll(false);
			PlayerPrefs.Save();
		}

		public TouchDirection NextPlatformDirection()
		{
			TouchDirection result = TouchDirection.none;
			if (this.platformParents.Count > 0)
			{
				PlatformParent platformParent = this.platformParents[1];
				PlatformParent platformParent2 = this.platformParents[0];
				int activeCubeIndex = platformParent.GetActiveCubeIndex();
				int activeCubeIndex2 = platformParent2.GetActiveCubeIndex();
				int num = platformParent.transform.childCount - 1;
				this.platformParents.RemoveAt(0);
				if (activeCubeIndex2 == 0 && activeCubeIndex == num)
				{
					result = TouchDirection.left;
				}
				else if (activeCubeIndex2 == num && activeCubeIndex == 0)
				{
					result = TouchDirection.right;
				}
				else if (activeCubeIndex == activeCubeIndex2)
				{
					result = TouchDirection.none;
				}
				else if (activeCubeIndex > activeCubeIndex2)
				{
					result = TouchDirection.right;
				}
				else
				{
					result = TouchDirection.left;
				}
				if (this.isGameGuid && this.speedScale > 0)
				{
					this.EndGuid();
				}
				if (this.isGameGuid)
				{
					platformParent2.DisActiveGameIntro();
					this.StartGuid(0, 1);
				}
			}
			return result;
		}

		public bool IsGuid()
		{
			return this.isGameGuid;
		}

		private void SkipGuid()
		{
			PlatformParent platformParent = this.platformParents[0];
			PlayerPrefsManager.SetGameGuideOver(this.curSongId);
			platformParent.DisActiveGameIntro();
			this.isGameGuid = false;
			this.gemCount = GameManager.gemRate.Count;
			this.AddGuidReward();
			this.uiGameManager.OnEndGuid();
		}

		private void EndGuid()
		{
			if (this.isGameGuid)
			{
				this.SkipGuid();
				PlayerPrefsManager.GameGuide = 0;
				PlayerPrefsManager.SetGameGuideOver(0);
				PlayerPrefsManager.SetGameGuideOver(1);
				PlayerPrefsManager.SetGameGuideOver(2);
			}
		}

		private void StartGuid(int curIndex, int nextIndex)
		{
			if (this.platformParents.Count > nextIndex)
			{
				PlatformParent platformParent = this.platformParents[nextIndex];
				PlatformParent platformParent2 = this.platformParents[curIndex];
				int activeCubeIndex = platformParent.GetActiveCubeIndex();
				int activeCubeIndex2 = platformParent2.GetActiveCubeIndex();
				int num = platformParent.transform.childCount - 1;
				TouchDirection touchDirection;
				if (activeCubeIndex2 == 0 && activeCubeIndex == num)
				{
					touchDirection = TouchDirection.left;
				}
				else if (activeCubeIndex2 == num && activeCubeIndex == 0)
				{
					touchDirection = TouchDirection.right;
				}
				else if (activeCubeIndex == activeCubeIndex2)
				{
					touchDirection = TouchDirection.none;
				}
				else if (activeCubeIndex > activeCubeIndex2)
				{
					touchDirection = TouchDirection.right;
				}
				else
				{
					touchDirection = TouchDirection.left;
				}
				if (this.isGameGuid)
				{
					if (touchDirection == TouchDirection.left)
					{
						platformParent2.DoActiveGameIntro(true);
					}
					else if (touchDirection == TouchDirection.right)
					{
						platformParent2.DoActiveGameIntro(false);
					}
				}
			}
		}
	}
}
