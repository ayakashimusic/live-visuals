using UnityEngine;
using System.Collections;

public class VideoBackground : MonoBehaviour {

    public Camera mainCamera;

	private Renderer renderer;
	private MovieTexture movie;

	private void Awake()
	{
		renderer = GetComponent<Renderer>();

	}

	// Use this for initialization
	private void Start ()
	{
	}

	public void SetVideoTexture(MovieTexture tex)
	{
		movie = (MovieTexture)renderer.material.mainTexture;
		movie.Stop ();
		renderer.material.mainTexture = tex;
		movie = (MovieTexture)renderer.material.mainTexture;
		movie.loop = true;
		movie.Play ();
	}

	// Update is called once per frame
	private void Update () {
	
	}


}
