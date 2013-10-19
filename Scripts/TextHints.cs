using UnityEngine;
using System.Collections;

public class TextHints : MonoBehaviour {

	float timer = 0.0f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.tag == "TemporaryText" && guiText.enabled) {
			timer += Time.deltaTime;
			
			if(timer >= 8){
				guiText.enabled = false;
				timer = 0.0f;
			}
		}
	}
	
	void ShowMessage(string message) {
		guiText.text = message;
		
		if (!guiText.enabled) {
			guiText.enabled = true;
		}
		timer = 0.0f;
	}
}
