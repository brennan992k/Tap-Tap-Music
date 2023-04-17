using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class SelectMusicLvPanel : MonoBehaviour
	{
		public MaterialManager materialManager;

		public GameObject panelComingsoon;

		public Transform songItem;

		public Text textLv;

		public float itemHeight;

		public Image[] allImages;

		public Text[] allTexts;

		private int index;

		private List<SelectMusicSongItem> songItems;

		public void InitList()
		{
			this.songItems = new List<SelectMusicSongItem>();
			this.songItems.Add(this.songItem.GetComponent<SelectMusicSongItem>());
			Vector3 localPosition = this.songItem.transform.localPosition;
			for (int i = 1; i < 4; i++)
			{
				Transform transform = UnityEngine.Object.Instantiate<Transform>(this.songItem, this.songItem.parent);
				transform.parent = base.transform;
				transform.transform.localPosition = new Vector3(localPosition.x, localPosition.y - this.itemHeight * (float)i, localPosition.z);
				this.songItems.Add(transform.GetComponent<SelectMusicSongItem>());
			}
		}

		public void RefreshItem(int index)
		{
			if (this.songItems == null)
			{
				this.InitList();
			}
			this.index = index;
			this.textLv.text = "LV  " + (index + 1).ToString();
			int count = this.songItems.Count;
			for (int i = 0; i < count; i++)
			{
				SelectMusicSongItem selectMusicSongItem = this.songItems[i];
				selectMusicSongItem.RefreshItem(index * 4 + i);
			}
			int musicOpenLv = PlayerPrefsManager.GetMusicOpenLv();
			if (index >= musicOpenLv)
			{
				this.SetGray();
			}
			if (this.index >= MusicList.Instance.GetMusicCount() / 4)
			{
				this.panelComingsoon.SetActive(true);
			}
			else
			{
				this.panelComingsoon.SetActive(false);
			}
			int childCount = base.transform.childCount;
			this.panelComingsoon.transform.SetSiblingIndex(childCount - 1);
		}

		private void SetGray()
		{
			for (int i = 0; i < this.allImages.Length; i++)
			{
				this.allImages[i].material = this.materialManager.uiGrayMaterial;
			}
			for (int j = 0; j < this.allTexts.Length; j++)
			{
				this.allTexts[j].color = this.materialManager.textGrayColor;
			}
		}
	}
}
