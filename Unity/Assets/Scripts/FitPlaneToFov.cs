using UnityEngine;
using System.Collections;

public class FitPlaneToFov : MonoBehaviour {

	public Camera mainCamera;
	public bool keepRatio = false;

	private float ratio = 1.0f;

	// Use this for initialization
	void Start ()
	{
		ratio = transform.localScale.x / transform.localScale.y;
		ResizeVideo();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	void ResizeVideo()
	{ 
		// Distance from camera
		float pos = transform.position.z;

		// Place and oriente plane
		transform.position = mainCamera.transform.position + mainCamera.transform.forward* pos;
		transform.LookAt (mainCamera.transform);
		transform.Rotate (90.0f, 0.0f, 0.0f);

		// Calculate new size to fit screen
		float h = (Mathf.Tan(mainCamera.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f) / 10.0f;
		float w = h * mainCamera.aspect;

		// If keeping the original plane ration
		if (keepRatio)
			w = h * ratio;

		// Final size
		transform.localScale = new Vector3(w, 1.0f, h);
	}
}
