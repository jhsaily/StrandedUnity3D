using UnityEngine;
using System.Collections;

public class FFGen : MonoBehaviour {

	public float rotationSpeed = 100.0f;
	public GameObject Area;
	private float time = 0.0f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
		transform.Translate(new Vector3(0, Mathf.Sin(time*5)*0.01f, 0));
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("GenPickup");
			Area.SetActive(true);
			Destroy(gameObject);
		}
	}
}
