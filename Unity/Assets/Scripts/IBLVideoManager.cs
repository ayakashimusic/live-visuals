using UnityEngine;
using System.Collections;


public class IBLVideoManager : MonoBehaviour {

	[System.Serializable]
	public class IBLVideo
	{
		public GameObject sky;
		public MovieTexture video;
	}

	public mset.SkyManager skyManager;
	public VideoBackground videoBackground;

	public int nextSkyIndex;
	private int currentSkyIndex;
	private GameObject currentSkyInstance;

	public IBLVideo[] iblVideos;

	// Use this for initialization
	void Start ()
	{
		StartSkyAndVideo (nextSkyIndex);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentSkyIndex != nextSkyIndex)
			StartSkyAndVideo (nextSkyIndex);
	}

	// Instantiate sky prefab and change video
	void StartSkyAndVideo(int index)
	{
		if (index > iblVideos.Length - 1)
			return;

		Destroy (currentSkyInstance);
		currentSkyInstance = Instantiate (iblVideos [index].sky);
		skyManager.GlobalSky = currentSkyInstance.GetComponent<mset.Sky>();
		videoBackground.SetVideoTexture(iblVideos[index].video);
		currentSkyIndex = nextSkyIndex;
	}
}
