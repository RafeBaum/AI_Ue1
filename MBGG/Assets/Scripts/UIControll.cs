using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControll : MonoBehaviour {

	public Text endText, explain;
	public string loseRep,loseQuit,loseText, winRep,winQuit,winText;
	public Button quit, reset;
	public bool gameOver, win;

	// Use this for initialization
	void Start () {
		endText.gameObject.SetActive (false);
		quit.gameObject.SetActive (false);
		reset.gameObject.SetActive (false);
		gameOver = false;
		win = false;
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Player")){
			explain.gameObject.SetActive(false);
		}
	}

	public void Win(){
		endText.text = winText;
		quit.GetComponentInChildren<Text>().text = winQuit;
		reset.GetComponentInChildren<Text>().text = winRep;
		TextActive ();
		win = true;
		gameOver = true;


	}

	public void Rip(){
		endText.text = loseText;
		quit.GetComponentInChildren<Text>().text = loseQuit;
		reset.GetComponentInChildren<Text>().text = loseRep;
		TextActive ();
		gameOver = true;

	}

	public void Oob(){

		TextActive ();

	}


	void TextActive(){
		endText.gameObject.SetActive (true);
		quit.gameObject.SetActive (true);
		reset.gameObject.SetActive (true);

		quit.onClick.AddListener (End);
		reset.onClick.AddListener (Reload);
	}


	void End(){
		Application.Quit ();
	}

	void Reload(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	IEnumerator DelayByX (float t){
		yield return new WaitForSecondsRealtime (t);

		Time.timeScale = 0;
	}
}
