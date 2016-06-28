using UnityEngine;
using System.Collections;

public class RandomizeAll : MonoBehaviour {

	public IBLVideoManager iblVideoManager;

	public float switchTimeInSeconds = 10.0f;
	private float timer = 0.0f;

	private int iblCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer < switchTimeInSeconds)
			return;

		timer = 0.0f;

		iblCounter++;
		if (iblCounter > iblVideoManager.iblVideos.Length - 1)
			iblCounter = 0;

		iblVideoManager.nextSkyIndex = iblCounter;
	}
}
