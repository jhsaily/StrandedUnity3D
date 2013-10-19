using UnityEngine;
using System.Collections;

public class DishTrigger : MonoBehaviour {
	
	public GameObject Player;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player") {
			if (PlayerHUD.dishStage == 0 && PlayerHUD.gasAmount < 4) {
				Player.SendMessage("SendHint", "There's an old satellite dish here. It seems to run on fossil fuels. Damned 20th century tech.");
				PlayerHUD.dishStage = 1;
				PlayerHUD.fuelStage = 1;
			} else if (PlayerHUD.dishStage == 1 && PlayerHUD.gasAmount < 4) {
				Player.SendMessage("SendHint", "This still needs more fuel. Hopefully I can find some more, or else I'm stuck here.");
			} else if (PlayerHUD.dishStage == 2) {
				if (PlayerHUD.genStage != 3) {
					Player.SendMessage("SendHint", "It will be some time before anyone hears this. I should get these field generators running.");
				}
			} else {
				PlayerHUD.fuelStage = 1;
				Player.SendMessage("SendHint", "Looks like I have enough fuel to turn this thing back on. Hopefully somebody hears me.");
				PlayerHUD.dishStage = 2;
				transform.parent.gameObject.audio.mute = false;
			}
		}
	}
}
