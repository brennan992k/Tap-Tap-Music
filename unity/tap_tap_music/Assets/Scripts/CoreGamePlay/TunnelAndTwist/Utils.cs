using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public static class Utils
	{
		private sealed class _ActivateCanavsGroup_c__AnonStorey0
		{
			internal List<CanvasGroup> cgList;

			internal float valueEnd;

			internal void __m__0()
			{
				foreach (CanvasGroup current in this.cgList)
				{
					if (this.valueEnd >= 0.99f)
					{
						current.interactable = true;
						current.blocksRaycasts = true;
					}
					else
					{
						current.interactable = false;
						current.blocksRaycasts = false;
						current.gameObject.SetActive(false);
					}
				}
			}
		}

		private static System.Random Random = new System.Random();

		public static int bestScore
		{
			get
			{
				return PlayerPrefs.GetInt("BEST_SCORE", 0);
			}
			set
			{
				PlayerPrefs.SetInt("BEST_SCORE", value);
			}
		}

		public static int lastScore
		{
			get
			{
				return PlayerPrefs.GetInt("LAST_SCORE", 0);
			}
			set
			{
				PlayerPrefs.SetInt("LAST_SCORE", value);
			}
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			int i = list.Count;
			while (i > 1)
			{
				i--;
				int index = Utils.Random.Next(i + 1);
				T value = list[index];
				list[index] = list[i];
				list[i] = value;
			}
		}

		public static Color GetRandomColor()
		{
			return new TUNNELColor
			{
				h = Utils.RandomValue(),
				s = Utils.RandomRange(0.3f, 0.4f),
				l = Utils.RandomRange(0.45f, 0.6f),
				a = 1f
			};
		}

		public static bool IsEqual(this Color c, Color d)
		{
			return c.r == d.r && c.g == d.g && c.b == d.b && c.a == d.a;
		}

		public static float RandomValue()
		{
			return (float)Utils.Random.NextDouble();
		}

		public static float RandomRange(float min, float max)
		{
			return min + (float)Utils.Random.NextDouble() * (max - min);
		}

		public static int RandomRange(int min, int max)
		{
			return Utils.Random.Next(min, max);
		}

		public static bool IsVisibleFrom(this Transform transform, Camera camera)
		{
			return transform.gameObject.activeInHierarchy && transform.position.IsVisibleFrom(camera);
		}

		public static bool IsVisibleFrom(this Vector3 pos, Camera camera)
		{
			float width = camera.GetWidth();
			float height = camera.GetHeight();
			float num = camera.transform.position.x - width / 2f;
			float num2 = camera.transform.position.x + width / 2f;
			float num3 = camera.transform.position.y + height / 2f;
			float num4 = camera.transform.position.y - height / 2f;
			return num < pos.x && pos.x < num2 && num4 < pos.y && pos.y < num3;
		}

		public static float GetHeight(this Camera cam)
		{
			if (cam == null)
			{
				return 0f;
			}
			return 2f * cam.orthographicSize;
		}

		public static float GetWidth(this Camera cam)
		{
			if (cam == null)
			{
				return 0f;
			}
			return cam.GetHeight() * cam.aspect;
		}

		public static float GetMovePlayerSpeed(float pointSpeed)
		{
			return 5f + Mathf.Pow(pointSpeed + 1f, 0.75f);
		}

		public static float GetZScaleCube(float pointSpeed)
		{
			return 2f + Mathf.Pow(pointSpeed + 1f, 0.535714269f);
		}

		public static void ActivateCanavsGroup(this List<CanvasGroup> cgList, float valueStart, float valueEnd, float time)
		{
			foreach (CanvasGroup current in cgList)
			{
				current.interactable = false;
				current.blocksRaycasts = false;
				current.alpha = valueStart;
				int num = 0;
				int num2 = 0;
				if (valueStart == 0f)
				{
					num = 90;
				}
				if (valueEnd == 0f)
				{
					num2 = 90;
				}
				current.transform.eulerAngles = Vector3.right * (float)num;
				current.gameObject.SetActive(true);
				if (time == 0f)
				{
					current.alpha = valueEnd;
					if (valueEnd >= 0.99f)
					{
						current.interactable = true;
						current.blocksRaycasts = true;
					}
					else
					{
						current.interactable = false;
						current.blocksRaycasts = false;
						current.gameObject.SetActive(false);
					}
					current.transform.eulerAngles = Vector3.right * (float)num2;
				}
				else
				{
					current.transform.DORotate(Vector3.right * (float)num2, time, RotateMode.Fast);
					current.DOFade(valueEnd, time).OnComplete(delegate
					{
						foreach (CanvasGroup current2 in cgList)
						{
							if (valueEnd >= 0.99f)
							{
								current2.interactable = true;
								current2.blocksRaycasts = true;
							}
							else
							{
								current2.interactable = false;
								current2.blocksRaycasts = false;
								current2.gameObject.SetActive(false);
							}
						}
					});
				}
			}
		}

		public static Color ToColor(TUNNELColor c)
		{
			float a = c.a;
			float b;
			float r;
			float g = r = (b = c.l);
			if (c.l <= 0f)
			{
				c.l = 0.001f;
			}
			if (c.l >= 1f)
			{
				c.l = 0.999f;
			}
			if (c.s != 0f)
			{
				float num = (c.l >= 0.5f) ? (c.l + c.s - c.l * c.s) : (c.l * (c.s + 1f));
				float v = c.l * 2f - num;
				r = Utils.GetRGB(v, num, c.h + 0.333333343f);
				g = Utils.GetRGB(v, num, c.h);
				b = Utils.GetRGB(v, num, c.h - 0.333333343f);
			}
			return new Color(r, g, b, a);
		}

		public static TUNNELColor FromColor(Color color)
		{
			float num = 0f;
			float s = 0f;
			float a = color.a;
			float num2 = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
			float num3 = Mathf.Min(color.r, Mathf.Min(color.g, color.b));
			float num4 = (num2 + num3) / 2f;
			if (num3 != num2)
			{
				float num5 = num2 - num3;
				s = ((num4 <= 0.5f) ? (num5 / (num3 + num2)) : (num5 / (2f - num2 - num3)));
				if (num2 == color.r)
				{
					num = (color.g - color.b) / num5 + ((color.g >= color.b) ? 0f : 6f);
				}
				else if (num2 == color.g)
				{
					num = (color.b - color.r) / num5 + 2f;
				}
				else if (num2 == color.b)
				{
					num = (color.r - color.g) / num5 + 4f;
				}
				num /= 6f;
			}
			return new TUNNELColor(num, s, num4, a);
		}

		private static float GetRGB(float v1, float v2, float h)
		{
			if (h < 0f)
			{
				h += 1f;
			}
			if (h > 1f)
			{
				h -= 1f;
			}
			if (h * 6f < 1f)
			{
				return v1 + (v2 - v1) * h * 6f;
			}
			if (h * 2f < 1f)
			{
				return v2;
			}
			if (h * 3f < 2f)
			{
				return v1 + (v2 - v1) * (0.6666667f - h) * 6f;
			}
			return v1;
		}

		public static bool Fadot(bool[] ees, int l, int h, int t)
		{
			int num = 0;
			for (int i = l; i < h + 1; i++)
			{
				if (ees[i])
				{
					num++;
				}
			}
			return num >= t;
		}

		public static int Gre(float tauxDEchantillonnage, int dza, float e)
		{
			float num = tauxDEchantillonnage / (float)dza;
			if (e < num / 2f)
			{
				return 0;
			}
			if (e > tauxDEchantillonnage / 2f - num / 2f)
			{
				return dza / 2 - 1;
			}
			float num2 = e / tauxDEchantillonnage;
			return (int)((float)dza * num2);
		}

		public static int BASS(int sfg, bool[] ees)
		{
			int result = 0;
			int h = (6 < sfg) ? 6 : sfg;
			if (Utils.Fadot(ees, 1, h, 2))
			{
				result = 1;
			}
			return result;
		}

		public static int MID(int sfg, bool[] ees)
		{
			int result = 0;
			int num = (8 < sfg) ? 8 : sfg;
			int num2 = sfg - 5;
			int t = (num2 - num) / 3;
			if (Utils.Fadot(ees, num, num2, t))
			{
				result = 2;
			}
			return result;
		}

		public static int HIGH(int sfg, bool[] ees)
		{
			int result = 0;
			int l = (sfg - 6 >= 0) ? (sfg - 6) : 0;
			int h = sfg - 1;
			if (Utils.Fadot(ees, l, h, 1))
			{
				result = 4;
			}
			return result;
		}
	}
}
