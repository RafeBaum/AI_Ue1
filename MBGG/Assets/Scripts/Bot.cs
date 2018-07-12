using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {

	private Rigidbody rb;
    private LifeControll life;
	float t, hitTimer;
	int dmgDealt = 1;

	Vector2 move = new Vector2 (-5, -5);

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		life = GameObject.FindGameObjectWithTag("GameController").GetComponent<LifeControll>();
		hitTimer = 0.5f;

    }
	
	void Update(){
		if (t > 0)
			t -= Time.deltaTime;
		if (t < 0)
			t = 0;
	}

	void FixedUpdate () {
		
			rb.AddForce (move);
		}
			

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("turn")) {
			move = move * -1;
		}
	}

	void OnCollisionEnter (Collision other){
		if (!other.gameObject.CompareTag("Player")) {
			move = move * -1;
		}
		if (other.gameObject.CompareTag ("Player")) {
			if (t == 0) {
				life.dealDmg (dmgDealt);
				t = hitTimer;
			}
		}
	}


}