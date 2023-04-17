using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class Player : MonoBehaviorHelper
	{
		private sealed class _RotateTo_c__AnonStorey1
		{
			internal float jumpTime;

			internal Player _this;

			internal void __m__0()
			{
				this._this.transform.DOLocalMoveY(-2.53f, this.jumpTime * 0.3f, false).OnComplete(delegate
				{
					this._this.CollisionPlatForm();
					this._this.transform.DOLocalMoveY(-3.23f, this.jumpTime * 0.2f, false).SetEase(Ease.OutSine).OnComplete(delegate
					{
						this._this.transform.DOLocalMoveY(-2.73f, this.jumpTime * 0.1f, false).SetEase(Ease.InSine).OnComplete(new TweenCallback(this._this.JumpComplete));
					});
				});
			}

			internal void __m__1()
			{
				this._this.shadow.DOScale(this._this.shadowOriginalScale, this.jumpTime * 0.4f).SetEase(Ease.InSine);
			}

			internal void __m__2()
			{
				this._this.CollisionPlatForm();
				this._this.transform.DOLocalMoveY(-3.23f, this.jumpTime * 0.2f, false).SetEase(Ease.OutSine).OnComplete(delegate
				{
					this._this.transform.DOLocalMoveY(-2.73f, this.jumpTime * 0.1f, false).SetEase(Ease.InSine).OnComplete(new TweenCallback(this._this.JumpComplete));
				});
			}

			internal void __m__3()
			{
				this._this.transform.DOLocalMoveY(-2.73f, this.jumpTime * 0.1f, false).SetEase(Ease.InSine).OnComplete(new TweenCallback(this._this.JumpComplete));
			}
		}

		private sealed class _ShowBallTrailer_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Player _this;

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

			public _ShowBallTrailer_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(0.1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (!this._this.isJumping)
					{
						this._this.ballTrailer.Clear();
						this._this.ballTrailer.enabled = true;
					}
					this._PC = -1;
					break;
				}
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

		[SerializeField]
		private float currentDegree;

		[SerializeField]
		private Transform groundCheck;

		[SerializeField]
		private Transform shadow;

		[SerializeField]
		private Transform ball;

		[SerializeField]
		private TrailRenderer ballTrailer;

		[SerializeField]
		private new GameManager gameManager;

		private bool sleep;

		public bool m_isJumping;

		private bool isGameOver;

		private bool isStarted;

		[SerializeField]
		private float timeSinceGameStarted;

		private Vector3 originalPosition = new Vector3(0f, -2.73f, 8.99f);

		private Vector3 parentOriginalPosition = new Vector3(0f, 0f, -4.49f);

		private Vector3 shadowOriginalPosition = new Vector3(0f, -3.48f, 8.99f);

		private Vector3 shadowOriginalScale = new Vector3(0.1f, 0.1f, 1f);

		private Vector3 shadowDistance;

		[SerializeField]
		private ParticleSystem particleExplosion;

		private Tweener tweener;

		private List<int> arrDirection;

		private int direction;

		private Vector3 localPositionStart
		{
			get
			{
				Vector3 result = this.originalPosition;
				result.x = this.originalPosition.x * 50f;
				result.y = this.originalPosition.y * 50f;
				return result;
			}
		}

		public Transform Ball
		{
			get
			{
				return this.ball;
			}
		}

		public bool isJumping
		{
			get
			{
				return this.m_isJumping || DOTween.IsTweening(base.transform) || DOTween.IsTweening(base.transform.parent);
			}
			set
			{
				this.m_isJumping = value;
			}
		}

		private void Awake()
		{
			string path = "GameBalls/Ball" + PlayerPrefsManager.GetSelectedBallId().ToString();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load(path) as GameObject, this.ball.position, this.ball.rotation, this.ball.parent);
			gameObject.transform.localScale = this.ball.localScale;
			UnityEngine.Object.Destroy(this.ball.gameObject);
			this.ball = gameObject.transform;
			base.transform.localPosition = this.originalPosition;
			this.currentDegree = 0f;
			this.isJumping = false;
			this.isGameOver = true;
			this.isStarted = false;
			this.shadowDistance = this.shadowOriginalPosition - this.originalPosition;
			this.arrDirection = new List<int>();
		}

		private void OnEnable()
		{
			EventManager.OnAnimIntroEvent += new EventManager.AnimIntroEvent(this.OnAnimIntroEvent);
			EventManager.OnAnimReviveEvent += new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
			EventManager.OnAnimLearningEvent += new EventManager.AnimLearningEvent(this.OnAnimLearningEvent);
			EventManager.OnAnimGameEndEvent += new EventManager.AnimGameEndEvent(this.OnAnimGameEndEvent);
		}

		private void OnDisable()
		{
			if (this.tweener != null)
			{
				this.tweener.Kill(false);
			}
			InputTouch.OnTouched -= new InputTouch.OnTouch(this.OnTouched);
			EventManager.OnAnimIntroEvent -= new EventManager.AnimIntroEvent(this.OnAnimIntroEvent);
			EventManager.OnAnimReviveEvent -= new EventManager.AnimReviveEvent(this.OnAnimReviveEvent);
			EventManager.OnAnimLearningEvent -= new EventManager.AnimLearningEvent(this.OnAnimLearningEvent);
			EventManager.OnAnimGameEndEvent += new EventManager.AnimGameEndEvent(this.OnAnimGameEndEvent);
		}

		private void Start()
		{
			this.ballTrailer.gameObject.SetActive(false);
			base.transform.localPosition = new Vector3(this.localPositionStart.x, this.localPositionStart.y, base.transform.localPosition.z);
			this.shadow.transform.position = base.transform.position + this.shadowDistance;
			this.tweener = base.transform.DOLocalMove(this.originalPosition, 1f, false).OnUpdate(delegate
			{
				this.shadow.transform.position = base.transform.position + this.shadowDistance;
			}).SetEase(Ease.Linear).SetEase(Ease.OutQuart).OnComplete(delegate
			{
				this.ballTrailer.gameObject.SetActive(true);
			});
		}

		private void OnAnimIntroEvent(AnimIntro g)
		{
			if (g == AnimIntro.end)
			{
				if (PlayerPrefsManager.Learning == 1)
				{
					PlayerPrefsManager.Learning = 0;
					EventManager.DoAnimLearningEvent(AnimIntro.start);
				}
				else
				{
					InputTouch.OnTouched += new InputTouch.OnTouch(this.OnTouched);
					EventManager.OnAnimIntroEvent -= new EventManager.AnimIntroEvent(this.OnAnimIntroEvent);
				}
				this.gameManager.uiGameManager.showTapToStart();
			}
		}

		private void OnAnimLearningEvent(AnimIntro intro)
		{
			if (intro == AnimIntro.end)
			{
				InputTouch.OnTouched += new InputTouch.OnTouch(this.OnTouched);
				EventManager.OnAnimIntroEvent -= new EventManager.AnimIntroEvent(this.OnAnimIntroEvent);
			}
		}

		private void OnAnimReviveEvent(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				base.transform.localPosition = this.originalPosition;
				base.transform.parent.localPosition = this.parentOriginalPosition;
				base.transform.parent.localRotation = new Quaternion(0f, 0f, 0f, 0f);
				this.shadow.transform.localPosition = this.shadowOriginalPosition;
				this.currentDegree = 0f;
				this.isJumping = false;
				this.isGameOver = true;
				this.isStarted = false;
				this.sleep = false;
				this.shadow.gameObject.SetActive(true);
				this.ballTrailer.gameObject.SetActive(false);
				base.transform.localPosition = new Vector3(this.localPositionStart.x, this.localPositionStart.y, base.transform.localPosition.z);
				this.shadow.transform.position = base.transform.position + this.shadowDistance;
				this.tweener = base.transform.DOLocalMove(this.originalPosition, 1f, false).OnUpdate(delegate
				{
					this.shadow.transform.position = base.transform.position + this.shadowDistance;
				}).SetEase(Ease.Linear).SetEase(Ease.OutQuart).OnComplete(delegate
				{
					this.ballTrailer.gameObject.SetActive(true);
				});
			}
		}

		private void OnAnimGameEndEvent()
		{
			InputTouch.OnTouched -= new InputTouch.OnTouch(this.OnTouched);
		}

		private void OnTouched(TouchDirection td)
		{
			if (!this.isStarted)
			{
				this.direction = 0;
				this.isGameOver = false;
				this.isStarted = true;
				EventManager.DOPlayerStartEvent();
				return;
			}
			if (td == TouchDirection.left || td == TouchDirection.right)
			{
				td = this.gameManager.NextPlatformDirection();
			}
			if (this.gameManager.IsGuid())
			{
				EventManager.DoPauseForGuid(AnimStatus.end);
			}
			if (td != TouchDirection.none)
			{
				if (td != TouchDirection.left)
				{
					if (td == TouchDirection.right)
					{
						this.direction = 1;
					}
				}
				else
				{
					this.direction = -1;
				}
			}
			else
			{
				this.direction = 0;
			}
			if (this.direction != 0)
			{
				this.arrDirection.Add(this.direction);
			}
		}

		private void Update()
		{
			if (this.isStarted)
			{
				this.shadow.localPosition = this.shadowOriginalPosition;
			}
			if (this.isGrounded())
			{
				if (this.isStarted)
				{
					this.shadow.gameObject.SetActive(!this.isGameOver);
				}
			}
			else
			{
				if (!this.isJumping)
				{
					if (this.isStarted)
					{
						this.shadow.gameObject.SetActive(false);
					}
				}
				else if (this.isStarted)
				{
					this.shadow.gameObject.SetActive(!this.isGameOver);
				}
				if (!this.isJumping && !this.isGameOver)
				{
					this.AnimationGameOver();
				}
			}
			if (!this.isStarted)
			{
				this.timeSinceGameStarted = Time.realtimeSinceStartup;
				return;
			}
			if (Time.realtimeSinceStartup - this.timeSinceGameStarted < 0.1f)
			{
				return;
			}
			if (this.isGameOver)
			{
				return;
			}
			if (this.arrDirection.Count > 0)
			{
				this.direction = this.arrDirection[0];
				this.arrDirection.RemoveAt(0);
			}
			else
			{
				this.direction = 0;
			}
			if (this.direction == -1)
			{
				this.MoveLeft();
			}
			else if (this.direction == 1)
			{
				this.MoveRight();
			}
		}

		public void AnimationGameOver()
		{
			if (this.isGameOver)
			{
				return;
			}
			this.arrDirection.Clear();
			this.isGameOver = true;
			DOTween.KillAll(false);
			EventManager.DOAnimFailEvent(AnimFail.start);
			base.transform.DOShakePosition(0.5f, 0.5f, 100, 90f, false).SetUpdate(true).OnComplete(delegate
			{
				base.transform.DOLocalMoveY(-10f, 1f, false).SetUpdate(true).OnComplete(delegate
				{
					base.gameObject.SetActive(false);
					EventManager.DOAnimFailEvent(AnimFail.end);
				});
			});
		}

		private void MoveLeft()
		{
			if (this.isJumping || this.isGameOver)
			{
				return;
			}
			this.DoMove(-1);
		}

		private void MoveRight()
		{
			if (this.isJumping || this.isGameOver)
			{
				return;
			}
			this.DoMove(1);
		}

		private void DoMove(int sign)
		{
			this.AnimationJumpStarted();
			EventManager.DOPlayerJumpEvent();
			this.currentDegree += (float)sign * 45f;
			this.RotateTo((float)sign);
		}

		private void RotateTo(float dir)
		{
			float z = base.transform.parent.rotation.eulerAngles.z;
			float d = z + dir * 45f;
			float num = this.gameManager.TimeOneRhythms();
			float jumpTime = num / 2f;
			base.transform.parent.DORotate(d * Vector3.forward, jumpTime * 0.8f, RotateMode.Fast).SetEase(Ease.OutSine).SetUpdate(true).OnUpdate(new TweenCallback(this.AnimationJumpStarted)).OnStart(new TweenCallback(this.AnimationJumpStarted)).OnComplete(new TweenCallback(this.AnimationJumpCompleted));
			base.transform.DOLocalMoveY(-1.03f, jumpTime * 0.3f, false).SetUpdate(true).SetEase(Ease.OutSine).OnStart(new TweenCallback(this.StartJump)).OnComplete(delegate
			{
				this.transform.DOLocalMoveY(-2.53f, jumpTime * 0.3f, false).OnComplete(delegate
				{
					this.CollisionPlatForm();
					this.transform.DOLocalMoveY(-3.23f, jumpTime * 0.2f, false).SetEase(Ease.OutSine).OnComplete(delegate
					{
						this.transform.DOLocalMoveY(-2.73f, jumpTime * 0.1f, false).SetEase(Ease.InSine).OnComplete(new TweenCallback(this.JumpComplete));
					});
				});
			});
			this.shadow.DOScale(0f * this.shadowOriginalScale, jumpTime * 0.4f).SetUpdate(true).SetEase(Ease.OutSine).OnComplete(delegate
			{
				this.shadow.DOScale(this.shadowOriginalScale, jumpTime * 0.4f).SetEase(Ease.InSine);
			});
		}

		public void AnimationJumpStarted()
		{
			this.isJumping = true;
			this.ballTrailer.enabled = false;
			base.StopCoroutine(this.ShowBallTrailer());
		}

		public void AnimationJumpCompleted()
		{
			this.isJumping = false;
		}

		public void CollisionPlatForm()
		{
			this.AnimationJumpStarted();
			EventManager.DOCollidePlatForm();
		}

		private void StartJump()
		{
			this.AnimationJumpStarted();
			EventManager.DoCheckPerfectJump(AnimStatus.start);
		}

		private void JumpComplete()
		{
			this.AnimationJumpCompleted();
			base.StopCoroutine(this.ShowBallTrailer());
			base.StartCoroutine(this.ShowBallTrailer());
		}

		private IEnumerator ShowBallTrailer()
		{
			Player._ShowBallTrailer_c__Iterator0 _ShowBallTrailer_c__Iterator = new Player._ShowBallTrailer_c__Iterator0();
			_ShowBallTrailer_c__Iterator._this = this;
			return _ShowBallTrailer_c__Iterator;
		}

		public void DoExplosionParticle()
		{
			this.particleExplosion.Emit(40);
			EventManager.DOGetPointEvent();
		}

		private bool isGrounded()
		{
			if (this.isJumping)
			{
				return true;
			}
			Vector3 vector = base.transform.TransformDirection(Vector3.down);
			return Physics.Raycast(this.groundCheck.position, vector, 2f);
		}

		public int GetPlayerPosition()
		{
			float z = base.transform.parent.rotation.eulerAngles.z;
			if (-22.5f <= z && z < 22.5f)
			{
				return 0;
			}
			if (22.5f <= z && z < 67.5f)
			{
				return 1;
			}
			if (67.5f <= z && z < 112.5f)
			{
				return 2;
			}
			if (112.5f <= z && z < 157.5f)
			{
				return 3;
			}
			if (157.5f <= z && z < 202.5f)
			{
				return 4;
			}
			if (202.5f <= z && z < 247.5f)
			{
				return 5;
			}
			if (247.5f <= z && z < 292.5f)
			{
				return 6;
			}
			if (292.5f <= z && z < 337.5f)
			{
				return 7;
			}
			if (337.5f <= z && z < 382.5f)
			{
				return 0;
			}
			UnityEngine.Debug.LogWarning("NO POSITION????????? : " + z);
			return 0;
		}
	}
}
