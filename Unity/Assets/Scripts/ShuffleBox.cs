using UnityEngine;
using System.Collections;

public class ShuffleBox : MonoBehaviour {

	public float speedY = 1.0f;
	public float speedX = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * speedY);
		transform.RotateAround (transform.position, transform.right, Time.deltaTime * speedX);
	}
}
