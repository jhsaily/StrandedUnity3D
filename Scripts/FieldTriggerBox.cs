using UnityEngine;
using System.Collections;

public class FieldTriggerBox : MonoBehaviour {

	public GameObject Player;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player") {
			if (PlayerHUD.genStage == 0) {
				Player.SendMessage("SendHint", "Looks like the field generators are down. There seems to be one missing...");
				PlayerHUD.genStage = 1;
			} else if (PlayerHUD.genStage == 2) {
				Player.SendMessage("SendHint", "It seems I can place the generator I have to start the field back up.");
			}
			Destroy(gameObject);
		}
	}
}
