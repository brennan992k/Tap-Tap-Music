using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class SelectMusicSongList : MonoBehaviour
	{
		private sealed class _LoadingPanel_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _count___0;

			internal int _i___1;

			internal SelectMusicLvPanel _panel___2;

			internal SelectMusicSongList _this;

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

			public _LoadingPanel_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._count___0 = this._this.panels.Count;
					this._i___1 = 0;
					break;
				case 1u:
					this._panel___2 = this._this.panels[this._i___1];
					this._panel___2.RefreshItem(this._i___1);
					this._i___1++;
					break;
				case 2u:
					this._PC = -1;
					return false;
				default:
					return false;
				}
				if (this._i___1 >= this._count___0)
				{
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
					}
				}
				else
				{
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
				}
				return true;
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

		public Transform scrollContent;

		public Transform lvPanel;

		private List<SelectMusicLvPanel> panels;

		private void Awake()
		{
			this.panels = new List<SelectMusicLvPanel>();
			this.panels.Add(this.lvPanel.GetComponent<SelectMusicLvPanel>());
			int musicCount = MusicList.Instance.GetMusicCount();
			int num = 10;
			for (int i = 1; i < num; i++)
			{
				Transform transform = UnityEngine.Object.Instantiate<Transform>(this.lvPanel, this.scrollContent.transform);
				this.panels.Add(transform.GetComponent<SelectMusicLvPanel>());
			}
		}

		private void OnEnable()
		{
			base.StartCoroutine(this.LoadingPanel());
		}

		private IEnumerator LoadingPanel()
		{
			SelectMusicSongList._LoadingPanel_c__Iterator0 _LoadingPanel_c__Iterator = new SelectMusicSongList._LoadingPanel_c__Iterator0();
			_LoadingPanel_c__Iterator._this = this;
			return _LoadingPanel_c__Iterator;
		}
	}
}
