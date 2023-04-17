using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class MonoBehaviorHelper : MonoBehaviour
	{
		private GameManager _gameManager;

		private Player _player;

		private Transform _playerTransform;

		private Transform _ball;

		private ObjectPooling _objectPooling;

		private BackgroundAnim _backgroundAnim;

		public GameManager gameManager
		{
			get
			{
				if (this._gameManager == null)
				{
					this._gameManager = UnityEngine.Object.FindObjectOfType<GameManager>();
				}
				return this._gameManager;
			}
		}

		public Player player
		{
			get
			{
				if (this._player == null)
				{
					this._player = UnityEngine.Object.FindObjectOfType<Player>();
				}
				return this._player;
			}
		}

		public Transform playerTransform
		{
			get
			{
				if (this._playerTransform == null)
				{
					this._playerTransform = this.player.transform;
				}
				return this._playerTransform;
			}
		}

		public Transform ballTransform
		{
			get
			{
				if (this._ball == null)
				{
					this._ball = this.player.Ball;
				}
				return this._ball;
			}
		}

		public ObjectPooling objectPooling
		{
			get
			{
				if (this._objectPooling == null)
				{
					this._objectPooling = UnityEngine.Object.FindObjectOfType<ObjectPooling>();
				}
				return this._objectPooling;
			}
		}

		public BackgroundAnim backgroundAnim
		{
			get
			{
				if (this._backgroundAnim == null)
				{
					this._backgroundAnim = UnityEngine.Object.FindObjectOfType<BackgroundAnim>();
				}
				return this._backgroundAnim;
			}
		}
	}
}
