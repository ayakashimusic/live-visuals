using UnityEngine;
using System.Collections;

public class MusicListener : MonoBehaviour
{

    private string micName = "";
    private AudioSource audioSource;
    private float[] spectrum = new float[64];
    private static float spectrumAverage = 0.0f;

	public float restartTimeInSeconds = 10.0f;
	private float timer = 0.0f;

	// Start
	private void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		StartListening ();
		SetMicrophoneDevice ();
	}


	// Set microphone
	private void SetMicrophoneDevice()
	{
		foreach (string device in Microphone.devices)
		{
			Debug.Log("Mic name: " + device);
		}

		if (Microphone.devices.Length < 1)
		{
			Debug.Log ("No microphone detected");
			return;
		}

		micName = Microphone.devices[0];
	}


	// StartListening
    private void StartListening()
    {
        audioSource.clip = Microphone.Start(micName, true, 1, 44100);
        audioSource.loop = true;

		while (!(Microphone.GetPosition(micName) > 0)) { Debug.Log ("wait for mic position");} //Very important to be placed before Play() to avoid latency
        audioSource.Play(); // Play the audio source
    }


    // Update
    private void Update()
    {
		if (micName == "")
		{
			SetMicrophoneDevice ();
			return;
		}

        UpdateSpectrum();
		RestartFromTimer ();
    }


    // Update spectrum
    private void UpdateSpectrum()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris); // channel 0 = all channels

        // Average spectrum values
        float sum = 0.0f;
        for (int i = 1; i < spectrum.Length; i++)
        {
            sum += spectrum[i];
        }

        spectrumAverage = sum / spectrum.Length;
    }


	private void RestartFromTimer()
	{
		timer += Time.deltaTime;

		if (timer < restartTimeInSeconds)
			return;

		timer = 0.0f;

		Destroy(audioSource.clip);
		StartListening ();

		Debug.Log ("Restart mic");
	}


    // Return spectrum average
    public static float GetSpectrumAverage()
    {
        return spectrumAverage;
    }
}
