using UnityEngine;
using System.Collections;

public class PlaceGenerator : MonoBehaviour {

	// Use this for initialization
	public GameObject[] Fields;
	public GameObject[] Generators;
	public GameObject Player;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player" && PlayerHUD.forceFieldGenerators == 1) {
			for(int i = 0; i < Fields.Length; i++) {
				Fields[i].SetActive(true);
			}
			for(int i = 0; i < Generators.Length; i++) {
				Generators[i].SetActive(true);
				Generators[i].audio.mute = false;
			}
			Player.SendMessage("SendHint", "I got the generators up an running. I hope they protect me from whatever is out here.");
			PlayerHUD.genStage = 3;
			Destroy(gameObject);
		}
	}
}
