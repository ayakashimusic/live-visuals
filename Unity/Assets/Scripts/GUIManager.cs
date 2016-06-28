using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GameObject gui;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			gui.SetActive (!gui.activeSelf);
	}
}
