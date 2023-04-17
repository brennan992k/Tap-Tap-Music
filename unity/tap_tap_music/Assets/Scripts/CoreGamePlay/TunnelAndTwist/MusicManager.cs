using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class MusicManager : MonoBehaviour
	{
		private sealed class _LoadMusic_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal string musicfile;

			internal ResourceRequest _r___0;

			internal MusicManager _this;

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

			public _LoadMusic_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._this.isMusicLoading = true;
					this._r___0 = Resources.LoadAsync(this.musicfile, typeof(AudioClip));
					this._r___0.allowSceneActivation = true;
					this._r___0.priority = 0;
					this._this.isTestListen = false;
					break;
				case 1u:
					if (this._r___0.progress > 0.1f && !this._this.isTestListen)
					{
						this._this.isTestListen = true;
						this._this.audioSource.clip = (this._r___0.asset as AudioClip);
						if (this._this.isMusicOpen)
						{
							this._this.audioSource.Play();
						}
					}
					break;
				default:
					return false;
				}
				if (!this._r___0.isDone)
				{
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.isTestListen = false;
				this._this.isMusicLoading = false;
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

		private sealed class _LoadAllMusic_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _selectedSongId___0;

			internal MusicList _musicList___0;

			internal int _musicCount___0;

			internal int _i___1;

			internal ResourceRequest _r___2;

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

			public _LoadAllMusic_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._selectedSongId___0 = PlayerPrefsManager.GetSelectedSongId();
					this._musicList___0 = MusicList.Instance;
					this._musicCount___0 = PlayerPrefsManager.GetMusicOpenLv() * 4;
					this._i___1 = 0;
					break;
				case 2u:
					//IL_DB:
					if (!this._r___2.isDone)
					{
						this._current = null;
						if (!this._disposing)
						{
							this._PC = 2;
						}
						return true;
					}
					this._i___1++;
					break;
				default:
					return false;
				}
				if (this._i___1 < this._musicCount___0)
				{
					this._r___2 = Resources.LoadAsync(this._musicList___0.GetValue(this._i___1).Mp3File, typeof(AudioClip));
					this._r___2.allowSceneActivation = true;
					this._r___2.priority = 0;
					goto IL_DB;
				}
				this._PC = -1;

                IL_DB:
                if (!this._r___2.isDone)
                {
                    this._current = null;
                    if (!this._disposing)
                    {
                        this._PC = 2;
                    }
                    return true;
                }
                this._i___1++;
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

		public AudioSource audioSource;

		public AudioClip[] audioClips;

		public Text audioTimeText;

		public Text audioNameText;

		private int currentHour;

		private int currentMinute;

		private int currentSecond;

		private bool isMusicLoading = true;

		private bool isMusicOpen = true;

		private bool isTestListen;

		private void OnEnable()
		{
			EventManager.OnListeningTestEvent += new EventManager.ListeningTestEvent(this.ListeningTest);
		}

		private void OnDisable()
		{
			EventManager.OnListeningTestEvent -= new EventManager.ListeningTestEvent(this.ListeningTest);
		}

		private void Awake()
		{
			this.isMusicOpen = (PlayerPrefsManager.GetSoundOpen() == 1);
			this.audioSource.volume = 1f;
			this.audioSource.loop = true;
			int selectedSongId = PlayerPrefsManager.GetSelectedSongId();
			MusicInfo value = MusicList.Instance.GetValue(selectedSongId);
			this.audioNameText.text = value.MusicName;
			this.audioTimeText.text = "00:00:00";
			this.audioSource.clip = (Resources.Load(value.Mp3File) as AudioClip);
			this.PlayMusic();
		}

		public void ListeningTest(int selectedSongId)
		{
			MusicInfo value = MusicList.Instance.GetValue(selectedSongId);
			this.audioNameText.text = value.MusicName;
			this.audioTimeText.text = "00:00:00";
			string mp3File = value.Mp3File;
			base.StartCoroutine(this.LoadMusic(mp3File));
		}

		private IEnumerator LoadMusic(string musicfile)
		{
			MusicManager._LoadMusic_c__Iterator0 _LoadMusic_c__Iterator = new MusicManager._LoadMusic_c__Iterator0();
			_LoadMusic_c__Iterator.musicfile = musicfile;
			_LoadMusic_c__Iterator._this = this;
			return _LoadMusic_c__Iterator;
		}

		private IEnumerator LoadAllMusic()
		{
			return new MusicManager._LoadAllMusic_c__Iterator1();
		}

		public ResourceRequest AsyncLoadMusic(int index)
		{
			MusicInfo value = MusicList.Instance.GetValue(index);
			return Resources.LoadAsync(value.Mp3File, typeof(AudioClip));
		}

		public void SetMusicOpen(bool open)
		{
			this.isMusicOpen = open;
			if (!this.isMusicOpen)
			{
				this.PauseMusic();
			}
			else
			{
				this.PlayMusic();
			}
		}

		public void PlayMusic()
		{
			if (this.isMusicOpen && this.audioSource != null)
			{
				this.audioSource.Play();
			}
		}

		public void PauseMusic()
		{
			if (this.audioSource != null)
			{
				this.audioSource.Pause();
			}
		}

		private void Update()
		{
			this.ShowAudioTime();
		}

		private void ShowAudioTime()
		{
			if (this.isMusicLoading)
			{
				return;
			}
			float num = this.audioSource.clip.length - this.audioSource.time;
			this.currentHour = (int)num / 3600;
			this.currentMinute = (int)(num - (float)(this.currentHour * 3600)) / 60;
			this.currentSecond = (int)(num - (float)(this.currentHour * 3600) - (float)(this.currentMinute * 60));
			this.audioTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2} ", this.currentHour, this.currentMinute, this.currentSecond);
		}
	}
}
