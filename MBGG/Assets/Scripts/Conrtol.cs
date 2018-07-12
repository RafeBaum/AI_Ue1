using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Conrtol : MonoBehaviour {


	public Animator anim;
	private LifeControll life;
	private UIControll uiC ;
	private Rigidbody rb;
	private GameObject player;
	private List<Quaternion> refList = new List<Quaternion>();
	private Quaternion refUpFaceRight, refUpFaceLeft, refDownFaceRight, refDownFaceLeft, refLeftFaceDown, refLeftFaceUp, refRightFaceUp, refRightFaceDown;
	private int rotDirec;
	public float speed;
	private float moveHorizontal, moveVertical;
	Vector2 grav = new Vector2 (); 
	float t;
	private bool isDelayed, gravHor = false;
	private bool faceRight = true, faceUp= false;
	public KeyCode gravUp, gravDown, gravLeft, gravRight;




	// Use this for initialization
	void Start () {
		rotDirec = 1;
		rb = GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		refUpFaceLeft = GameObject.Find ("RefUpFaceLeft").transform.localRotation;
		refList.Add (refUpFaceLeft);
		refUpFaceRight = GameObject.Find ("RefUpFaceRight").transform.localRotation;
		refDownFaceRight = GameObject.Find ("RefDownFaceRight").transform.localRotation;
		refList.Add (refDownFaceRight);
		refDownFaceLeft = GameObject.Find ("RefDownFaceLeft").transform.localRotation;
		refLeftFaceDown = GameObject.Find ("RefLeftFaceDown").transform.localRotation;
		refList.Add (refLeftFaceDown);
		refLeftFaceUp = GameObject.Find ("RefLeftFaceUp").transform.localRotation;
		refRightFaceUp= GameObject.Find ("RefRightFaceUp").transform.localRotation;
		refList.Add (refRightFaceUp);
		refRightFaceDown = GameObject.Find ("RefRightFaceDown").transform.localRotation;
		grav = new Vector2 (0, -35); 
		life = GameObject.Find ("GameController").GetComponent<LifeControll> ();
		uiC = GameObject.Find ("GameController").GetComponent<UIControll> ();
		gravHor = true;

	}
		
	void Update (){
		if(!uiC.gameOver){

			if (Input.GetKey (gravUp)) {
				grav = new Vector2 (0f, 35f);
				rotDirec = 0;
				anim.Play ("JUMP00", -1, 0.66f);
				StartCoroutine(ExecDelay(0.25f, rotDirec));
				gravHor = true;
				faceRight = false;
			}

			if (Input.GetKey (gravDown)) {
				grav = new Vector2 (0f, -35f);
				rotDirec = 1;
				anim.Play ("JUMP00", -1, 0.66f);
				StartCoroutine(ExecDelay(0.25f, rotDirec));
				gravHor = true;
				faceRight = true;

			}


			if (Input.GetKey (gravLeft)) {
				grav = new Vector2 (-35f, 0f);
				rotDirec = 2;
				anim.Play ("JUMP00", -1, 0.66f);
				StartCoroutine(ExecDelay(0.25f, rotDirec));
				gravHor = false;

			}

			if (Input.GetKey (gravRight)) {
				grav = new Vector2 (35f, 0f);
				rotDirec = 3;
				anim.Play ("JUMP00", -1, 0.66f);
				StartCoroutine(ExecDelay(0.25f, rotDirec));
				gravHor = false;

			}		

		}

	}

	IEnumerator ExecDelay(float t, int direc){

		if (isDelayed) {
			yield break;
		}

		isDelayed = true;
		yield return new WaitForSecondsRealtime(t);
	
		transform.rotation = refList [direc];

		

		isDelayed = false;
	
	}

	void FixedUpdate () {

	
		moveHorizontal = Input.GetAxisRaw ("Horizontal");

		if (moveHorizontal < 0 && rotDirec == 0 ) {
			//faceRight = false;
			transform.rotation = refUpFaceLeft;

		} 
		if (moveHorizontal > 0 && rotDirec == 0) {
			//faceRight = true;
			transform.rotation = refUpFaceRight;

		}
		if (moveHorizontal < 0 && rotDirec == 1 ) {
			//faceRight = false;
			transform.rotation = refDownFaceLeft;

		} 
		if (moveHorizontal > 0 && rotDirec == 1) {
			//faceRight = true;
			transform.rotation = refDownFaceRight;

		}

			
		moveVertical = Input.GetAxisRaw ("Vertical");

		if (moveVertical < 0  && rotDirec == 2) {
			transform.rotation = refLeftFaceDown;

		} 
		if (moveVertical > 0  && rotDirec == 2) {
			transform.rotation = refLeftFaceUp;

		} 
		if (moveVertical < 0  && rotDirec == 3) {
			transform.rotation = refRightFaceDown;

		} 
		if (moveVertical > 0  && rotDirec == 3) {
			transform.rotation = refRightFaceUp;

		} 

		anim.SetFloat ("inputH", moveHorizontal);
		anim.SetFloat ("inputV", moveVertical);
		anim.SetBool ("gravHor", gravHor); 

		Physics.gravity = grav;


		Vector2 move = new Vector3 (moveHorizontal * speed,  moveVertical * speed);

		rb.AddForce(move);
	}



	void OnTriggerEnter(Collider other){

	
		if (other.gameObject.CompareTag("d2")) {

			life.dealDmg (1);

		}
		if (other.gameObject.CompareTag ("oob")) {
			uiC.Oob ();
		}
		if (other.gameObject.CompareTag ("Finish")) {
			uiC.win = true;
			life.Dead ();
		}
		
	}

	void onCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("frenemie")) {
			life.dealDmg (2);
		}
			
	}
		

}


