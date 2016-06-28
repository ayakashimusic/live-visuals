using UnityEngine;
using System.Collections;

public class ScrollingUVs : MonoBehaviour {

	private Material scrollMaterial;
	public float scrollSpeed = 0.5f;
	public Vector2 scrollDirection = new Vector2(1.0f, 1.0f);

	// Use this for initialization
	void Start () {
		scrollMaterial = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update (){
		float offset = Mathf.Repeat(scrollSpeed * Time.time, 1.0f);
		scrollMaterial.SetTextureOffset("_MainTex", scrollDirection * offset);
	}
}
