using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaserScript : MonoBehaviour {

	public float mFireRate 	= .5f;
	public float mFireRange = 50f;
	public float mHitForce 	= 100f;
	public int mLaserDamage = 100;

	public int score=0;

	public Text GG;




	// Line render that will represent the Laser
	private LineRenderer mLaserLine;

	// Laser Sound
	private AudioSource mLaserSoundFX;

	// Define if laser line is showing
	private bool mLaserLineEnabled;

	// Time that the Laser lines shows on screen
	private WaitForSeconds mLaserDuration = new WaitForSeconds(0.05f);

	private float mNextFire;

	// Use this for initialization
	void Start () {
		// getting the Line Renderer
		mLaserLine = GetComponent<LineRenderer>();

		GameObject ScoreNumber = GameObject.Find ("Score");

		ScoreNumber.GetComponentInChildren<Text>().text = score.ToString();

		LifeScript.score = score;

		// Getting Laser Sound Effect
		mLaserSoundFX = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {


	}

	// Shot the Laser
	public void Fire(){
		// Get ARCamera Transform
		Transform cam = Camera.main.transform;

		// DEfine the time of the next fire
		mNextFire = Time.time + mFireRate;

		// Set the origin of the RayCast
		Vector3 rayOrigin = cam.position;

		// Show the Laser using a Coroutine
		StartCoroutine(LaserFx());

		// Holdes the Hit information
		RaycastHit hit;

		// Set the oorigin position of the Laser Line
		// It will always 10 units down from the ARCamera
		// We adopted this logic for simplicity
		mLaserLine.SetPosition(0, transform.up * -10f );

		// Checks if the RayCast hit something
		if ( Physics.Raycast( rayOrigin, cam.forward, out hit, mFireRange )){

			// Set the end of the Laser Line to the object hitted
			mLaserLine.SetPosition(1, hit.point );



			PresentsBehaviourScript PresentCtr = hit.collider.GetComponent<PresentsBehaviourScript>();
			if ( PresentCtr != null ) {
				if ( hit.rigidbody != null ) {
					hit.rigidbody.AddForce(-hit.normal*mHitForce);

					score = score+1;

					LifeScript.score = score;

					GameObject ScoreNumber = GameObject.Find ("Score");
					ScoreNumber.GetComponentInChildren<Text>().text = score.ToString();

					PresentCtr.Hit( mLaserDamage );
				}
			}

		} else {
			// Set the enfo of the laser line to be forward the camera
			// using the Laser range
			mLaserLine.SetPosition(1, cam.forward * mFireRange );
		}
	}

	// Show the Laser Effects
	private IEnumerator LaserFx(){
		mLaserLine.enabled = true;
		mLaserSoundFX.Play();

		// Way for a specific time to remove the LineRenderer
		yield return mLaserDuration;
		mLaserLine.enabled = false;
	}
}
