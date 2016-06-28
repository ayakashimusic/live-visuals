using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class DecibelTest : MonoBehaviour {

	public float scaleMultiplier = 10.0f;
	public float lerpTime = 1.0f;
	private string micName;
	private AudioSource audioSource;
	private float[] spectrum = new float[64];
	private Transform[] transformList;


	// Start
	void Start()
	{
		foreach (string device in Microphone.devices)
		{
			Debug.Log("Mic name: " + device);
		}

		micName = Microphone.devices [0];

		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = Microphone.Start(micName, true, 1, 44100);
		audioSource.loop = true;

		while (!(Microphone.GetPosition(micName) > 0)){} //Very important to be placed before Play() to avoid latency
		audioSource.Play(); // Play the audio source

		// List children's transform and remove parent from array
		transformList = gameObject.GetComponentsInChildren<Transform> ();
		transformList = transformList.Skip(1).ToArray(); 
	}


	// Update
	private void Update()
	{
		UpdateScaleFromSpectrum ();
	}


	// Update spectrum scale
	private void UpdateScaleFromSpectrum()
	{
		audioSource.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris); // channel 0 = all channels

		// Average spectrum values
		float sum = 0.0f;
		for( int i = 1; i < spectrum.Length; i++) {
			sum += spectrum[i];
		}
		float average = sum / spectrum.Length;

		float intensity = 1.0f + average * scaleMultiplier;
		float lerpScale = Mathf.Lerp (transformList[0].localScale.x, intensity, lerpTime);


		for(int i = 0; i < transformList.Length; i++)
		{
			transformList[i].localScale = Vector3.one * lerpScale;
		}
	}
		

	// Update spectrum graph
	private void UpdateSpectrumGraph()
	{
		audioSource.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris); // channel 0 = all channels

		for (int i = 1; i < spectrum.Length; i++)
		{
			//Debug.DrawLine (new Vector3 (i - 1, spectrum [i] + 10, 0), new Vector3 (i, spectrum [i + 1] + 10, 0), Color.red);
			Debug.DrawLine (new Vector3 (i - 1, Mathf.Log (spectrum [i - 1]) + 10, 2), new Vector3 (i, Mathf.Log (spectrum [i]) + 10, 2), Color.cyan);
			//Debug.DrawLine (new Vector3 (Mathf.Log (i - 1), spectrum [i - 1] - 10, 1), new Vector3 (Mathf.Log (i), spectrum [i] - 10, 1), Color.green);
			//Debug.DrawLine (new Vector3 (Mathf.Log (i - 1), Mathf.Log (spectrum [i - 1]), 3), new Vector3 (Mathf.Log (i), Mathf.Log (spectrum [i]), 3), Color.yellow);
		}
	}
}
