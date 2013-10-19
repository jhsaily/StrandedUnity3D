using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject target;
	public float moveSpeed = 30f;
	public float startX;
	public float startY;
	public float startZ;
	public bool attacking = false;
	void Start () {
		animation.wrapMode = WrapMode.Loop;
		animation["walk"].speed = 2;
	}
	
	// Update is called once per frame
	void Update () {
		float step = moveSpeed * Time.deltaTime;
		if (GameTime._timeOfDay < GameTime._dayCycleInSeconds/4 || GameTime._timeOfDay > (GameTime._dayCycleInSeconds/4)*3){
			if (PlayerHUD.safe == false) {
				if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.transform.position.x, target.transform.position.z)) > 10) {
					transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), step);
					transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
					animation.Play("walk");
					attacking = false;
				} else {
					if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.transform.position.x, target.transform.position.z)) > 8){ 
						transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), step);
						transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
					}
					transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
					animation.Play("attack");
					attacking = true;
				}
			} else {
				if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.transform.position.x, target.transform.position.z)) > 60) {
					transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), step);
					transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
					animation.Play("walk");
					attacking = false;
				} else {
					transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
					animation.Play("idle");
					attacking = false;
				}
			}
		} else {
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(startX, startZ)) > 5){
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(startX, transform.position.y, startZ), step);
				transform.LookAt(new Vector3(startX, transform.position.y, startZ));
				animation.Play("walk");
				attacking = false;
			} else {
				animation.Play("idle");
				attacking = false;
			}
		}
		if (animation.IsPlaying("idle")) {
			//rigidbody.isKinematic = true;
			rigidbody.velocity = Vector3.zero;
		} else {
			//rigidbody.isKinematic = false;
		}
	}
}
