using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {

	public string levelToLoad;
	public Texture2D normalTexture;
	public Texture2D rollOverTexture;
	public AudioClip click;
	public bool quitButton = false;
	
	private float alphaFadeVal = 0;
	private bool fade = false;
	public Texture2D blackTexture;
	// Update is called once per frame
	void Update () {
	}
	
	void OnMouseEnter() {
		guiTexture.texture = rollOverTexture;
	}
	void OnMouseExit() {
		guiTexture.texture = normalTexture;
	}
	
	void OnMouseUp() {
		audio.PlayOneShot(click);
		fade = true;
	}
	
	void OnGUI() {
		if (fade){
			alphaFadeVal += Mathf.Clamp01(Time.deltaTime / 3);
			GUI.color = new Color(0, 0, 0, alphaFadeVal);
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), blackTexture);
			if (alphaFadeVal >= 1) {
				if (quitButton) {
					Application.Quit();
				}else {
					Application.LoadLevel(levelToLoad);
				}
			}
		}
	}
}
