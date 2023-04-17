using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class SoundManager : MonoBehaviorHelper
	{
		[SerializeField]
		private AudioClip musicGame;

		[SerializeField]
		private AudioClip soundWin;

		[SerializeField]
		private AudioClip soundFail;

		[SerializeField]
		private AudioClip soundCoin;

		[SerializeField]
		private AudioClip soundTouch;

		public AudioSource audioSourceMusic;

		[SerializeField]
		private AudioSource audioSource;

		private AudioClip currentMusic;

		private bool isMusicOpen = true;

		private void Awake()
		{
			int selectedSongId = PlayerPrefsManager.GetSelectedSongId();
			MusicInfo value = MusicList.Instance.GetValue(selectedSongId);
			this.musicGame = (Resources.Load(value.Mp3File) as AudioClip);
			this.audioSourceMusic.volume = 1f;
			this.audioSource.volume = 1f;
			this.audioSourceMusic.loop = false;
		//	AdsManager.Instance.UmEvent(UMConstants.umGameMusic, selectedSongId.ToString());
		}

		private void OnEnable()
		{
			EventManager.OnAddGem += new EventManager.AddGem(this.PlaySoundCoin);
			EventManager.OnAnimFailEvent += new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnAnimWinEvent += new EventManager.AnimWinEvent(this.OnAnimWinEvent);
			EventManager.OnPlayerStartEvent += new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnMusicSpeedEvent += new EventManager.MusicSpeedEvent(this.OnMusicSpeedEvent);
			EventManager.OnUpSpeed += new EventManager.UpSpeed(this.OnUpSpeed);
			this.isMusicOpen = (PlayerPrefsManager.GetSoundOpen() == 1);
		}

		private void OnDisable()
		{
			EventManager.OnAddGem -= new EventManager.AddGem(this.PlaySoundCoin);
			EventManager.OnAnimFailEvent -= new EventManager.AnimFailEvent(this.OnAnimFailEvent);
			EventManager.OnAnimWinEvent -= new EventManager.AnimWinEvent(this.OnAnimWinEvent);
			EventManager.OnPlayerStartEvent -= new EventManager.PlayerStartEvent(this.OnPlayerStartEvent);
			EventManager.OnMusicSpeedEvent -= new EventManager.MusicSpeedEvent(this.OnMusicSpeedEvent);
			EventManager.OnUpSpeed -= new EventManager.UpSpeed(this.OnUpSpeed);
		}

		private void OnPlayerStartEvent()
		{
			this.PlayMusic(this.musicGame);
		}

		private void OnAnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.start)
			{
				this.PauseMusic();
				this.PlaySoundGameOver();
			}
		}

		private void OnAnimWinEvent(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				this.PauseMusic();
				this.PlaySoundWin();
			}
		}

		private void PlayMusic(AudioClip nextMusic)
		{
			this.currentMusic = nextMusic;
			this.audioSourceMusic.clip = this.currentMusic;
			if (this.isMusicOpen)
			{
				this.audioSourceMusic.Play();
			}
		}

		private void PlayMusic()
		{
			if (this.isMusicOpen)
			{
				this.audioSourceMusic.Play();
			}
		}

		private void PauseMusic()
		{
			this.audioSourceMusic.Pause();
		}

		private void ResumeMusic()
		{
			if (this.isMusicOpen)
			{
				this.audioSourceMusic.UnPause();
			}
		}

		private void PlaySoundGameOver()
		{
			if (this.isMusicOpen)
			{
				this.audioSource.PlayOneShot(this.soundFail, 0.5f);
			}
		}

		private void PlaySoundWin()
		{
			if (this.isMusicOpen)
			{
				this.audioSource.PlayOneShot(this.soundWin, 0.5f);
			}
		}

		private void PlaySoundCoin()
		{
			if (this.isMusicOpen)
			{
				this.audioSource.PlayOneShot(this.soundCoin, 0.5f);
			}
		}

		private void PlaySoundTouch()
		{
			if (this.isMusicOpen)
			{
				this.audioSource.PlayOneShot(this.soundTouch, 0.5f);
			}
		}

		private void OnMusicSpeedEvent(float value)
		{
			this.audioSourceMusic.pitch = value;
		}

		private void OnUpSpeed(AnimStatus status)
		{
			if (status == AnimStatus.end)
			{
				this.audioSourceMusic.time = 0f;
				if (this.isMusicOpen)
				{
					this.audioSourceMusic.Play();
				}
				this.audioSourceMusic.time = 0f;
			}
		}

		private void OnPauseForGuid(AnimStatus animStatus)
		{
			if (animStatus == AnimStatus.start)
			{
				this.PauseMusic();
			}
			else
			{
				this.ResumeMusic();
			}
		}
	}
}
