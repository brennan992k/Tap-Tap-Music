using System;
using UnityEngine;

public class ChannelDebug : MonoBehaviour
{
	public GameObject theObject;

	public int audioChannel = 4;

	public float audioSensibility = 0.15f;

	public float scalefactor = 2f;

	public float lerpTime = 5f;

	private int currentChannel = 4;

	private Vector3 oldLocalScale;

	private void Start()
	{
		this.oldLocalScale = this.theObject.transform.localScale;
		this.currentChannel = this.audioChannel;
	}

	private void Update()
	{
		if (SpectrumKernel.spects[this.audioChannel] * SpectrumKernel.threshold >= this.audioSensibility)
		{
			this.theObject.transform.localScale = new Vector3(this.scalefactor, this.scalefactor, this.scalefactor);
		}
		this.theObject.transform.localScale = Vector3.Lerp(this.theObject.transform.localScale, this.oldLocalScale, this.lerpTime * Time.deltaTime);
	}

	private void OnGUI()
	{
		GUI.Box(new Rect(10f, 10f, (float)(Screen.width - 20), 25f), "USE THIS TOOL TO SETUP THE COMPONENTS VARS : AUDIO CHANNEL / AUDIO SENSIBILITY (Usually, only channels 0 => 20 are useful)");
		for (int i = 0; i < 40; i++)
		{
			if (this.currentChannel == i)
			{
				GUI.backgroundColor = Color.red;
			}
			else
			{
				GUI.backgroundColor = Color.white;
			}
			if (GUI.Button(new Rect((float)(10 + (i * (Screen.width / 40) + 2)), (float)(Screen.height - 100), (float)(Screen.width / 40), 25f), string.Empty + i))
			{
				this.currentChannel = i;
				this.audioChannel = i;
			}
		}
		GUI.backgroundColor = Color.black;
		this.audioSensibility = GUI.HorizontalSlider(new Rect(15f, (float)(Screen.height - 60), (float)(Screen.width - 30), 40f), this.audioSensibility, 0f, 1f);
		GUI.backgroundColor = Color.white;
		GUI.Button(new Rect(10f, (float)(Screen.height + 6 - Screen.height / 4) - this.audioSensibility * ((float)Screen.height / 1.1f), (float)(Screen.width - 20), 0f), string.Empty);
		GUI.backgroundColor = Color.black;
		GUI.Box(new Rect(10f, (float)(Screen.height - 30), (float)(Screen.width - 20), 25f), string.Concat(new object[]
		{
			"AUDIO CHANNEL = ",
			this.currentChannel,
			" /  AROUND AUDIO SENSIBILITY VALUE = ",
			this.audioSensibility
		}));
	}
}
