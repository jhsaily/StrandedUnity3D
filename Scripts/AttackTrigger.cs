using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {

	public GameObject Player;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) {
		Transform parent = transform.parent.parent.parent.parent.parent.parent.parent;
		if(col.gameObject.tag == "Player" && parent.GetComponent<EnemyBehaviour>().attacking == true) {
			PlayerHUD.gameOver = true;
		}
	}
}
