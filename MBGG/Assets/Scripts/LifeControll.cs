using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeControll : MonoBehaviour {

	public int playerlife;
	public GameObject player;
	public Text lifeText;
	private UIControll uiC ;
	Animator anim;


	// Use this for initialization
	void Start () {
		playerlife = 10;
		uiC = GetComponent<UIControll> ();
		anim = player.GetComponentInChildren <Animator> ();
	}

	public void dealDmg(int dmg){		
		playerlife = playerlife - dmg;
		if (playerlife<= 0) {
			Dead ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerlife < 0)
			playerlife = 0;
		lifeText.text = "Life: " + playerlife.ToString();
	}

	public void Dead(){
		if (!uiC.win) {
			anim.Play ("DAMAGED01");
			uiC.Rip ();
		} else {
			anim.Play ("WIN00");
			uiC.Win ();
		}

		StartCoroutine (DelayByX (2));
		player.GetComponent<Rigidbody> ().isKinematic = true;
	}

	IEnumerator DelayByX (float t){
		yield return new WaitForSecondsRealtime (t);

		Time.timeScale = 0;
	}
}

