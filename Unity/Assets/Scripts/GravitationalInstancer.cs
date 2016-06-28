using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravitationalInstancer : MonoBehaviour {

	public GameObject sourceObject;
	public int instancesNumber = 10;
	public float randomMaxDistance = 5.0f;
	public float lerpTime = 0.4f;
	public float targetResetTime = 4.0f;

	private float timer = 0.0f;
    public float musicPulseMin = 0.01f;

    public float rotationYGlobalSpeed = 1.0f;
    public float rotationYLocalSpeed = 1.0f;
    public float rotationXLocalSpeed = 1.0f;

    private List<GameObject> gravitationalObjects = new List<GameObject> ();
	private List<Vector3> gravitationalTargetPositions = new List<Vector3> ();
    private List<float> rotationSigns = new List<float>();

    // Use this for initialization
    private void Start ()
	{
		CreateInstances ();
	}
	
	// Update is called once per frame
	private void Update ()
	{
		timer += Time.deltaTime;

        RandomMoveObjects();
        RotateObjects();
		
	}

	private void CreateInstances()
	{
		for (int i = 0; i < instancesNumber; i++)
		{
			GameObject instance = Instantiate (sourceObject);
			instance.transform.SetParent (sourceObject.transform.parent);
			gravitationalObjects.Add(instance);
			gravitationalTargetPositions.Add (GetRandomVector3 ());
            rotationSigns.Add(GetRandomRotationSign());


            instance.transform.position = GetRandomVector3();
		}

        sourceObject.GetComponent<Renderer>().enabled = false;

    }

	private void RandomMoveObjects()
	{
        //Debug.Log(MusicListener.GetSpectrumAverage());

		for (int i = 0; i < instancesNumber; i++)
		{
			//if (timer > targetResetTime)
            if(MusicListener.GetSpectrumAverage() > musicPulseMin)
			{
				gravitationalTargetPositions[i] = GetRandomVector3 ();
            }
				
			gravitationalObjects[i].transform.position = Vector3.Lerp(gravitationalObjects[i].transform.position,
																		gravitationalTargetPositions[i],
																		lerpTime);
		}

		if (timer > targetResetTime)
		{
			timer = 0.0f;
		}
	}

	private Vector3 GetRandomVector3()
	{
		return new Vector3 ( Random.Range(-randomMaxDistance, randomMaxDistance),
							Random.Range(-randomMaxDistance, randomMaxDistance),
							Random.Range(-randomMaxDistance, randomMaxDistance));
	}

    private float GetRandomRotationSign()
    {
        float rotationSign = Random.Range(-1.0f, 1.0f);
        rotationSign = rotationSign / Mathf.Abs(rotationSign);
        return rotationSign;
    }

    private void RotateObjects()
    {
        for (int i = 0; i < instancesNumber; i++)
        {
            gravitationalObjects[i].transform.RotateAround(gravitationalObjects[i].transform.position, Vector3.up, rotationYLocalSpeed * rotationSigns[i]);
            gravitationalObjects[i].transform.RotateAround(gravitationalObjects[i].transform.position, Vector3.right, rotationXLocalSpeed * rotationSigns[i]);

            gravitationalTargetPositions[i] = Quaternion.AngleAxis(rotationYGlobalSpeed * rotationSigns[i], Vector3.up) * gravitationalTargetPositions[i];
        }
    }
}
