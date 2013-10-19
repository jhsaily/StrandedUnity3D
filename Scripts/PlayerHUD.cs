using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
	
	public static float oxygenAmount = 4;
	public static int gasAmount = 0;
	public static int forceFieldGenerators = 0;
	
	public AudioClip gasCollectedSound;
	public AudioClip oxygenCollectedSound;
	public AudioClip generatorCollectedSound;
	
	public GUIText textHints;
	public GUIText OxygenAmt;
	public GUIText GasAmt;
	private float oxygenPercent = 100f;
	
	public static int fuelStage = 0;
	public static int genStage = 0;
	public static int oxyStage = 0;
	public static int dishStage = 0;
	public static bool safe = false;
	
	private bool goodEnding = false;
	public static bool gameOver = false;
	private float alphaFadeVal = 0;
	public Texture2D blackTexture;
	public GameObject terrain;
	public GUIStyle style;
	public string menu;
	// Use this for initialization
	void Start () {
		forceFieldGenerators = 0;
		gasAmount = 0;
		oxygenAmount = 4;
		fuelStage = 0;
		genStage = 0;
		oxyStage = 0;
		dishStage = 0; safe = false;
		goodEnding = false;
		gameOver = false;
		alphaFadeVal = 0;
		textHints.SendMessage("ShowMessage", "It's a wonder I survived that crash. I see something in the distance, maybe I can find help there.");
		OxygenAmt.SendMessage("ShowMessage", "Oxygen Amount: 100%");
		GasAmt.SendMessage("ShowMessage", "");
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		oxygenAmount -= (4 / GameTime._dayCycleInSeconds) * Time.deltaTime;
		if (oxygenAmount < 0) {
			terrain.audio.mute = true;
			oxygenPercent = 0;
			gameOver = true;
		} else {
			oxygenPercent = ((oxygenAmount / 4)*100);
		}
		OxygenAmt.SendMessage("ShowMessage", "Oxygen Amount: " + oxygenPercent + "%");
		if (oxygenPercent < 10) {
			textHints.SendMessage("ShowMessage", "My oxygen levels are getting dangerously low... I hope there's more tanks on this rock.");
		}
		if (dishStage == 2 && genStage == 3) {
			terrain.audio.mute = true;
			goodEnding = true;
			gameOver = true;
		}
	}
	
	void GasPickup() {
		AudioSource.PlayClipAtPoint(gasCollectedSound, transform.position);
		gasAmount++;
		if (fuelStage == 0) {
			textHints.SendMessage("ShowMessage", "I wonder if I can use this to fuel something...");
			GasAmt.SendMessage("ShowMessage", "Fuel Amount: " + gasAmount);
		} else if (fuelStage == 1) {
			if (gasAmount < 4) {
				textHints.SendMessage("ShowMessage", "Looks like I only need " + (4 - gasAmount) + " more tanks. Better look around.");
			} else {
				textHints.SendMessage("ShowMessage", "I've got enough fuel for now.");
			}
			GasAmt.SendMessage("ShowMessage", "Fuel Amount: " + gasAmount + "/4");
		}
	}
	void OxygenPickup() {
		AudioSource.PlayClipAtPoint(oxygenCollectedSound, transform.position);
		oxygenAmount += 1f;
		if (oxyStage == 0) {
			textHints.SendMessage("ShowMessage", "Looks like this moon has air tanks littered about... maybe I should collect them.");
			oxyStage++;
		} else {
			textHints.SendMessage("ShowMessage", "Perfect! Another tank of air. Now I won't suffocate as quickly.");
		}
	}
	void GenPickup() {
		AudioSource.PlayClipAtPoint(generatorCollectedSound, transform.position);
		if (genStage == 0) {
			textHints.SendMessage("ShowMessage", "Looks like I found a misplaced shield generator. Maybe it goes somewhere.");
			genStage = 2;
		} else if (genStage == 1) {
			textHints.SendMessage("ShowMessage", "Looks like I found the misplaced shield generator, better go install it.");
			genStage = 2;
		}
		forceFieldGenerators++;
	}
	
	public void SendHint(string hint) {
		textHints.SendMessage("ShowMessage", hint);
	}
	
	void OnGUI() {
		if (gameOver) {
			Screen.showCursor = true;
			terrain.audio.mute = true;
			alphaFadeVal += Mathf.Clamp01(Time.deltaTime / 3);
			GUI.color = new Color(0, 0, 0, alphaFadeVal);
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), blackTexture);
			if (alphaFadeVal >= 1) {
				GUI.color = Color.white;
				if (goodEnding){
					GUI.Label( new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "You got everything up an running and survived! For now anyways. Just wait until the hunger strikes.", style);
				} else {
					GUI.Label( new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "Aww, you died. Oh well, maybe it's better this way. At least you don't have to wait to not get rescued!", style);
				}
				if (GUI.Button (new Rect (Screen.width/2 - 100,3 * (Screen.height/4),200,20), "Back to menu?")) {
					Application.LoadLevel(menu);
				}
			}
		}
	}
}
