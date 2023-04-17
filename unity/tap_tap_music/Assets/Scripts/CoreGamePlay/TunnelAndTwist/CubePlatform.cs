using DG.Tweening;
using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class CubePlatform : MonoBehaviorHelper
	{
		public Vector3 localPositionDefault;

		private bool isCollisePlayer;

		private float initScaleZ = 2f;

		private Quaternion localRotate;

		[SerializeField]
		private GameObject m_itemPosition;

		[SerializeField]
		private Renderer m_renderer;

		[SerializeField]
		private Collider m_collider;

		private GameObject point;

		private GameObject gemObj;

		private GameObject gameIntro;

		private Tweener tweener;

		private Vector3 localPositionStart
		{
			get
			{
				Vector3 result = this.localPositionDefault;
				result.x = this.localPositionDefault.x * 50f;
				result.y = this.localPositionDefault.y * 50f;
				return result;
			}
		}

		private void Awake()
		{
			this.localPositionDefault = base.transform.localPosition;
			this.localRotate = base.transform.localRotation;
		}

		private void OnCollidePlatForm()
		{
			if (this.isCollisePlayer || (this.m_renderer && !this.m_renderer.enabled))
			{
				return;
			}
			float num = base.transform.position.z - base.transform.parent.parent.localScale.z * this.initScaleZ / 2f + this.initScaleZ;
			Vector3 vector = new Vector3(base.transform.position.x, base.transform.position.y, num);
			float num2 = base.gameManager.TimeOneRhythms();
			float num3 = num2 / 2f;
			if (base.ballTransform != null && Mathf.Abs(base.ballTransform.position.z - num) < this.initScaleZ * 1.5f && Mathf.Abs(vector.y - base.ballTransform.position.y) < 1.2f)
			{
				this.isCollisePlayer = true;
				Sequence sequence = DOTween.Sequence();
				sequence.Append(base.transform.DOLocalMoveY(this.localPositionDefault.y - 0.7f, num3 * 0.2f, false)).SetEase(Ease.InOutSine);
				sequence.Append(base.transform.DOLocalMoveY(this.localPositionDefault.y, num3 * 0.2f, false)).SetEase(Ease.OutSine);
				sequence.OnUpdate(new TweenCallback(this.UpdatePointPosition));
			}
		}

		private void OnEnable()
		{
			this.isCollisePlayer = false;
			EventManager.OnCollidePlatForm += new EventManager.CollidePlatForm(this.OnCollidePlatForm);
		}

		private void OnDisable()
		{
			this.ResetRotate();
			this.isCollisePlayer = false;
			EventManager.OnCollidePlatForm -= new EventManager.CollidePlatForm(this.OnCollidePlatForm);
			base.StopAllCoroutines();
			if (this.tweener != null)
			{
				this.tweener.Kill(false);
			}
			base.transform.DOKill(false);
			base.StopAllCoroutines();
			this.DisActivatePoint();
			this.DisGemPoint();
			this.DisGameIntro();
		}

		private void OnReloadScene()
		{
			this.DisActivatePoint();
			this.DisGemPoint();
			this.DisGameIntro();
			if (this.tweener != null)
			{
				this.tweener.Kill(false);
			}
			base.transform.DOKill(false);
			base.StopAllCoroutines();
			base.transform.localPosition = this.localPositionDefault;
		}

		public GameObject ActivatePoint()
		{
			this.point = null;
			Vector3 position = this.m_itemPosition.transform.position;
			position = new Vector3(position.x, position.y, base.transform.parent.parent.position.z - 1f);
			this.point = base.objectPooling.Spawn("PointPrefab", position, base.transform.rotation);
			this.point.SetActive(true);
			return this.point;
		}

		public void DisActivatePoint()
		{
			if (this.point != null)
			{
				this.point.SetActive(false);
			}
			this.point = null;
		}

		public GameObject ActivateGem()
		{
			this.gemObj = null;
			Vector3 position = this.m_itemPosition.transform.position;
			position = new Vector3(position.x, position.y, base.transform.parent.parent.position.z);
			this.gemObj = base.objectPooling.Spawn("GemPrefab", position, base.transform.rotation);
			this.gemObj.transform.Rotate(new Vector3(-90f, 0f, 0f));
			this.gemObj.SetActive(true);
			return this.gemObj;
		}

		public void DisGemPoint()
		{
			if (this.gemObj != null)
			{
				this.gemObj.SetActive(false);
			}
			this.gemObj = null;
		}

		public GameObject ActivateGameIntro(bool isTurnToLeft)
		{
			this.gameIntro = null;
			Vector3 position = this.m_itemPosition.transform.position;
			position = new Vector3(position.x, position.y, base.transform.parent.parent.position.z - base.transform.localScale.z * 0.5f);
			this.gameIntro = base.objectPooling.Spawn("GameIntro", position, base.transform.rotation);
			this.gameIntro.SetActive(true);
			this.gameIntro.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			GameIntroTrigger component = this.gameIntro.GetComponent<GameIntroTrigger>();
			component.isLeft = isTurnToLeft;
			return this.gameIntro;
		}

		public void DisGameIntro()
		{
			if (this.gameIntro != null)
			{
				this.gameIntro.SetActive(false);
			}
			this.gameIntro = null;
		}

		public void SetGameIntroActive(bool active)
		{
			if (this.gameIntro != null)
			{
				this.gameIntro.SetActive(active);
			}
		}

		public void DesactivateRendererAndCollider()
		{
			this.DisActivatePoint();
			this.point = null;
			this.gemObj = null;
			this.gameIntro = null;
			this.m_renderer.enabled = false;
			this.m_collider.enabled = false;
			this.m_itemPosition.SetActive(false);
			base.transform.DOKill(false);
			base.StopAllCoroutines();
		}

		public void ActivatedRendererAndCollider()
		{
			this.DisActivatePoint();
			this.gemObj = null;
			this.point = null;
			this.gameIntro = null;
			if (this.tweener != null)
			{
				this.tweener.Kill(false);
			}
			base.transform.DOKill(false);
			base.StopAllCoroutines();
			this.m_renderer.gameObject.SetActive(true);
			this.m_collider.gameObject.SetActive(true);
			this.m_renderer.enabled = true;
			this.m_collider.enabled = true;
			this.m_itemPosition.SetActive(true);
			float duration = 1f;
			base.transform.localPosition = new Vector3(this.localPositionStart.x, this.localPositionStart.y, base.transform.localPosition.z);
			this.tweener = base.transform.DOLocalMove(this.localPositionDefault, duration, false).SetEase(Ease.Linear).SetEase(Ease.OutQuart).OnUpdate(new TweenCallback(this.UpdatePointPosition));
		}

		public bool IsActive()
		{
			return this.m_renderer.enabled;
		}

		public void UpdatePointPosition()
		{
			if (this.point != null)
			{
				Vector3 position = this.m_itemPosition.transform.position;
				position = new Vector3(position.x, position.y, base.transform.parent.parent.position.z - base.transform.localScale.z);
				this.point.transform.position = position;
				this.point.transform.rotation = this.m_itemPosition.transform.rotation;
				if (base.transform.parent.parent.localScale.z <= 2f)
				{
					this.point.transform.Rotate(0f, 0f, 45f);
				}
			}
			if (this.gemObj != null)
			{
				Vector3 position2 = this.m_itemPosition.transform.position;
				position2 = new Vector3(position2.x, position2.y, base.transform.parent.parent.position.z - base.transform.localScale.z);
				Quaternion rotation = this.m_itemPosition.transform.rotation;
				this.gemObj.transform.position = position2;
				this.gemObj.transform.rotation = rotation;
				this.gemObj.transform.Rotate(new Vector3(-90f, 0f, 0f));
			}
			if (this.gameIntro != null)
			{
				Vector3 position3 = this.m_itemPosition.transform.position;
				position3 = new Vector3(position3.x, position3.y, base.transform.parent.parent.position.z - base.transform.localScale.z);
				Quaternion rotation2 = base.transform.rotation;
				this.gameIntro.transform.position = position3;
				this.gameIntro.transform.rotation = rotation2;
			}
		}

		public void Rotate45()
		{
			base.transform.Rotate(new Vector3(0f, 45f, 0f));
		}

		public void ResetRotate()
		{
			base.transform.localRotation = this.localRotate;
		}
	}
}
