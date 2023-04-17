using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace AppAdvisory.TunnelAndTwist
{
	public class BackgroundAnim : MonoBehaviorHelper
	{
		private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _t___1;

			internal TempoType _b___2;

			internal BackgroundAnim _this;

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
					break;
				case 1u:
					break;
				case 2u:
					break;
				default:
					return false;
				}
				IL_25:
				this._t___1 = 0;
				this._this.DoAudioStuff();
				this._this.Agregate();
				this._this.Organise();
				this._this.Finalise();
				this._t___1 = this._this.Result();
				if (this._t___1 != 0)
				{
					this._b___2 = TempoType.none;
					if (this._t___1 == 1)
					{
						this._b___2 = TempoType.bass;
					}
					if (this._t___1 == 2)
					{
						this._b___2 = TempoType.mid;
					}
					if (this._t___1 == 4)
					{
						this._b___2 = TempoType.high;
					}
					if (this._b___2 != TempoType.none)
					{
						if (this._b___2 != TempoType.bass)
						{
							if (this._b___2 != TempoType.mid)
							{
								if (this._b___2 == TempoType.high)
								{
									this._this.DoBeat();
								}
							}
							else
							{
								this._this.DoBeat();
							}
						}
						else
						{
							this._this.DoBeatScalePoint();
							this._this.DoBeat();
						}
						goto IL_148;
					}
					this._current = 0;
					if (!this._disposing)
					{
						this._PC = 1;
					}
				}
				else
				{
					this._current = 0;
					if (!this._disposing)
					{
						this._PC = 2;
					}
				}
				return true;
				IL_148:
				goto IL_25;
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

		private sealed class _ResetAndAnimateMaterial_c__AnonStorey1
		{
			internal Image image;

			internal BackgroundAnim _this;

			internal void __m__0()
			{
				this._this.AnimateMaterial(this.image);
			}
		}

		private sealed class _AnimateMaterial_c__AnonStorey2
		{
			internal Image image;

			internal BackgroundAnim _this;

			internal void __m__0()
			{
				this._this.AnimMaterialOUT(this.image);
			}
		}

		private sealed class _AnimMaterialIN_c__AnonStorey3
		{
			internal Action callback;

			internal void __m__0()
			{
				if (this.callback != null)
				{
					this.callback();
				}
			}
		}

		private float originalScale = 1f;

		public Image imageBorder;

		public Image imageMid;

		public Image imageCenter;

		private float aDefault = 0.3f;

		private AudioSource audioSource;

		[NonSerialized]
		public float scaleBeatPoint;

		private Tweener tweenerScaleBeatPoint;

		private int nh;

		private int ch;

		private float[] eh = new float[500];

		private float[] mh = new float[500];

		private int sommeur;

		private float[] jyiu = new float[50];

		private float[] jyiv = new float[50];

		private int ctr;

		private int sfg;

		private int lastForceDisable;

		private float tauxDEchantillonnage;

		private int moyenO;

		private float[] sp0;

		private float[] sp1;

		private float[] f0;

		private float[] f1;

		private float[,] fh = new float[50, 500];

		private float[,] mdh = new float[50, 500];

		private float[] moy = new float[50];

		private bool[] ees = new bool[50];

		private int jte;

		private int dza;

		private int kg;

		private float[] regG = new float[50];

		private void Awake()
		{
			this.audioSource = UnityEngine.Object.FindObjectOfType<SoundManager>().audioSourceMusic;
			this.originalScale = this.imageBorder.rectTransform.localScale.x;
			Camera.main.backgroundColor = Color.white;
			Color color = this.imageBorder.color;
			color.a = 0f;
			this.imageBorder.color = color;
			this.imageMid.color = color;
			this.imageCenter.color = color;
			this.sp0 = new float[1024];
			this.sp1 = new float[1024];
			this.f0 = new float[1024];
			this.f1 = new float[1024];
			this.setUpEnergy();
			this.setUptufuency();
			for (int i = 0; i < 50; i++)
			{
				this.regG[i] = Time.time;
			}
		}

		private IEnumerator Start()
		{
			BackgroundAnim._Start_c__Iterator0 _Start_c__Iterator = new BackgroundAnim._Start_c__Iterator0();
			_Start_c__Iterator._this = this;
			return _Start_c__Iterator;
		}

		private void DoBeatScalePoint()
		{
			if (this.tweenerScaleBeatPoint != null && this.tweenerScaleBeatPoint.IsPlaying())
			{
				this.tweenerScaleBeatPoint.Kill(false);
			}
			this.scaleBeatPoint = 0f;
			this.tweenerScaleBeatPoint = DOVirtual.Float(0f, 1f, 0.05f, delegate(float f)
			{
				this.scaleBeatPoint = f;
			}).OnComplete(delegate
			{
				this.tweenerScaleBeatPoint = DOVirtual.Float(1f, 0f, 0.3f, delegate(float f)
				{
					this.scaleBeatPoint = f;
				});
			});
		}

		private void ResetAndAnimateMaterial(Image image)
		{
			if (DOTween.IsTweening(image))
			{
				image.DOKill(false);
			}
			if (DOTween.IsTweening(image.rectTransform))
			{
				image.rectTransform.DOKill(false);
			}
			float num = image.color.a;
			num -= this.aDefault / 2f;
			if (num < 0f)
			{
				num = 0f;
			}
			RectTransform rectTransform = image.rectTransform;
			rectTransform.DOScale(this.originalScale * Vector3.one, 0.3f);
			image.DOFade(num, 0.3f).SetUpdate(true).OnComplete(delegate
			{
				this.AnimateMaterial(image);
			});
		}

		private void AnimateMaterial(Image image)
		{
			if (DOTween.IsTweening(image))
			{
				image.DOKill(false);
			}
			if (DOTween.IsTweening(image.rectTransform))
			{
				image.rectTransform.DOKill(false);
			}
			this.AnimMaterialIN(image, delegate
			{
				this.AnimMaterialOUT(image);
			});
		}

		private void AnimMaterialIN(Image image, Action callback)
		{
			image.rectTransform.DOScale(this.originalScale * Vector3.one * 1.1f / Time.timeScale, 0.05f).SetUpdate(true).OnComplete(delegate
			{
				if (callback != null)
				{
					callback();
				}
			});
			image.DOFade(1f, 0.05f).SetUpdate(true);
		}

		private void AnimMaterialOUT(Image image)
		{
			image.rectTransform.DOScale(this.originalScale * Vector3.one, 0.3f).SetUpdate(true);
			image.DOFade(0f, 1.5f).SetUpdate(true);
		}

		private void DoBeat()
		{
			if (!DOTween.IsTweening(this.imageBorder.rectTransform))
			{
				this.AnimateMaterial(this.imageBorder);
			}
			else if (!DOTween.IsTweening(this.imageMid.rectTransform))
			{
				this.AnimateMaterial(this.imageMid);
			}
			else if (!DOTween.IsTweening(this.imageCenter.rectTransform))
			{
				this.AnimateMaterial(this.imageCenter);
			}
			else
			{
				if (this.lastForceDisable == 0)
				{
					this.lastForceDisable = 1;
				}
				else if (this.lastForceDisable == 2)
				{
					this.lastForceDisable = 1;
				}
				else if (UnityEngine.Random.Range(0, 2) == 0)
				{
					this.lastForceDisable = 0;
				}
				else
				{
					this.lastForceDisable = 2;
				}
				if (this.lastForceDisable == 0)
				{
					this.ResetAndAnimateMaterial(this.imageBorder);
				}
				if (this.lastForceDisable == 1)
				{
					this.ResetAndAnimateMaterial(this.imageMid);
				}
				if (this.lastForceDisable == 2)
				{
					this.ResetAndAnimateMaterial(this.imageCenter);
				}
			}
		}

		private void DoAudioStuff()
		{
			this.audioSource.GetSpectrumData(this.sp0, 0, FFTWindow.BlackmanHarris);
			this.audioSource.GetSpectrumData(this.sp1, 1, FFTWindow.BlackmanHarris);
			this.audioSource.GetOutputData(this.f0, 0);
			this.audioSource.GetOutputData(this.f1, 1);
		}

		private void Agregate()
		{
			for (int i = 0; i < this.ctr; i++)
			{
				float num;
				if (i == 0)
				{
					num = 0f;
				}
				else
				{
					num = this.tauxDEchantillonnage / 2f / Mathf.Pow(2f, (float)(this.ctr - i));
				}
				float num2 = this.tauxDEchantillonnage / 2f / Mathf.Pow(2f, (float)(this.ctr - i - 1));
				float num3 = (num2 - num) / (float)this.moyenO;
				float num4 = num;
				for (int j = 0; j < this.moyenO; j++)
				{
					int num5 = j + i * this.moyenO;
					float num6 = this.gerg(num4, num4 + num3, this.sp0);
					float num7 = this.gerg(num4, num4 + num3, this.sp1);
					this.moy[num5] = num7;
					if (num6 > num7)
					{
						this.moy[num5] = num6;
					}
					num4 += num3;
				}
			}
		}

		private void Organise()
		{
			this.sommeur++;
			for (int i = 0; i < this.sfg; i++)
			{
				if (this.kg == 2)
				{
					this.jyiu[i] = this.moy[i];
					this.jyiv[i] = this.moy[i];
				}
				else
				{
					this.jyiu[i] += this.moy[i];
					if (this.moy[i] > this.jyiv[i])
					{
						this.jyiv[i] = this.moy[i];
					}
				}
			}
		}

		private void Finalise()
		{
			for (int i = 1; i < this.sfg; i++)
			{
				float num = this.moy[i];
				float num2 = 0f;
				for (int j = 0; j < this.nh; j++)
				{
					num2 += this.fh[i, j];
				}
				if (this.nh > 0)
				{
					num2 /= (float)this.nh;
				}
				float num3 = 0f;
				for (int k = 0; k < this.nh; k++)
				{
					num3 += (this.fh[i, k] - num2) * (this.fh[i, k] - num2);
				}
				if (this.nh > 0)
				{
					num3 /= (float)this.nh;
				}
				float num4 = -0.0025714f * num3 + 1.51428568f;
				float num5 = Mathf.Max(num - num4 * num2, 0f);
				float num6 = 0f;
				int num7 = 0;
				for (int l = 0; l < this.nh; l++)
				{
					if (this.mdh[i, l] > 0f)
					{
						num6 += this.mdh[i, l];
						num7++;
					}
				}
				if (num7 > 0)
				{
					num6 /= (float)num7;
				}
				float num8;
				float num9;
				if (i < 7)
				{
					num8 = 0.003f;
					num9 = 2f;
				}
				else if (i > 6 && i < 20)
				{
					num8 = 0.001f;
					num9 = 3f;
				}
				else
				{
					num8 = 0.001f;
					num9 = 4f;
				}
				if (Time.time - this.regG[i] < 0.05f)
				{
					this.ees[i] = false;
				}
				else if (num > num9 * num2 && num > num8)
				{
					this.ees[i] = true;
					this.regG[i] = Time.time;
				}
				else
				{
					this.ees[i] = false;
				}
				this.nh = ((this.nh >= this.jte) ? this.nh : (this.nh + 1));
				this.fh[i, this.ch] = num;
				this.mdh[i, this.ch] = num5;
				this.ch++;
				this.ch %= this.jte;
			}
		}

		private int Result()
		{
			return Utils.BASS(this.sfg, this.ees) | Utils.MID(this.sfg, this.ees) | Utils.HIGH(this.sfg, this.ees);
		}

		private void initDetector()
		{
			this.nh = 0;
			this.ch = 0;
			for (int i = 0; i < 500; i++)
			{
				this.eh[i] = 0f;
				this.mh[i] = 0f;
			}
			this.sommeur = 0;
			this.jte = 0;
		}

		private void setUpEnergy()
		{
			this.tauxDEchantillonnage = (float)AudioSettings.outputSampleRate;
			this.dza = 1024;
			this.jte = 43;
			this.nh = 0;
			this.ch = 0;
		}

		private void setUptufuency()
		{
			this.dza = 1024;
			this.jte = 43;
			this.tauxDEchantillonnage = (float)AudioSettings.outputSampleRate;
			this.nh = 0;
			this.ch = 0;
			float num = this.tauxDEchantillonnage / 2f;
			this.ctr = 1;
			while ((num /= 2f) > 60f)
			{
				this.ctr++;
			}
			this.moyenO = 3;
			this.sfg = this.ctr * this.moyenO;
			for (int i = 0; i < this.sfg; i++)
			{
				for (int j = 0; j < this.jte; j++)
				{
					this.fh[i, j] = 0f;
					this.mdh[i, j] = 0f;
				}
			}
		}

		private float gerg(float ltuf, float hituf, float[] hyre)
		{
			int num = Utils.Gre(this.tauxDEchantillonnage, this.dza, ltuf);
			int num2 = Utils.Gre(this.tauxDEchantillonnage, this.dza, hituf);
			float num3 = 0f;
			for (int i = num; i <= num2; i++)
			{
				num3 += hyre[i];
			}
			return num3 / (float)(num2 - num + 1);
		}
	}
}
