using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDMG : MonoBehaviour {

	float t, hitTime;
	 LifeControll life;

	void Start(){
		life = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LifeControll> ();
		hitTime = 0.5f;
	}
	void Update(){
		if (t > 0)
			t -= Time.deltaTime;
		if (t < 0)
			t = 0;
	}


	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Player")){
			if (t == 0) {
				life.dealDmg (1);
				t = hitTime;
			}
		}	
	}
}
