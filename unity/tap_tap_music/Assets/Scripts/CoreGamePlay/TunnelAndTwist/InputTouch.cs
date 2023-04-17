using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	public class InputTouch : MonoBehaviour
	{
		public delegate void OnTouch(TouchDirection td);



		private float heighMax;

	public static event InputTouch.OnTouch OnTouched;

		private void Awake()
		{
			Input.multiTouchEnabled = false;
		}

		private void Start()
		{
			this.heighMax = (float)Screen.height * 0.88f;
		}

		private void Update()
		{
#if !UNITY_EDITOR
            if (Application.isMobilePlatform || Application.isEditor)
			{
				int touchCount = UnityEngine.Input.touchCount;
				if (touchCount > 0)
				{
					Touch touch = UnityEngine.Input.GetTouch(0);
					TouchPhase phase = touch.phase;
					if (phase == TouchPhase.Began)
					{
						if (touch.position.x < (float)Screen.width / 2f && touch.position.y < this.heighMax)
						{
							if (InputTouch.OnTouched != null)
							{
								InputTouch.OnTouched(TouchDirection.left);
							}
						}
						else if (touch.position.y < this.heighMax && InputTouch.OnTouched != null)
						{
							InputTouch.OnTouched(TouchDirection.right);
						}
					}
					if (phase == TouchPhase.Ended && InputTouch.OnTouched != null)
					{
						InputTouch.OnTouched(TouchDirection.none);
					}
				}
				
			}
			

#endif
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
                    {
                        if (Input.mousePosition.x < (float)Screen.width / 2f && Input.mousePosition.y < this.heighMax)
                        {
                            if (InputTouch.OnTouched != null)
                            {
                                InputTouch.OnTouched(TouchDirection.left);
                            }
                        }
                        else if (Input.mousePosition.y < this.heighMax && InputTouch.OnTouched != null)
                        {
                            InputTouch.OnTouched(TouchDirection.right);
                        }
                    }
                    if (Input.GetMouseButtonUp(0) && InputTouch.OnTouched != null)
                    {
                        InputTouch.OnTouched(TouchDirection.none);
                    }


#endif

        }
	}
}
