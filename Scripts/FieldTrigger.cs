using UnityEngine;
using System.Collections;

public class FieldTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay (Collider col) {
		if(col.gameObject.tag == "Player") {
			PlayerHUD.safe = true;
		}
	}
	void OnTriggerExit (Collider col) {
		if(col.gameObject.tag == "Player") {
			PlayerHUD.safe = false;
		}
	}
}
