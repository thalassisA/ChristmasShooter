using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PresentsBehaviourScript : MonoBehaviour {

	public float mScaleMax	= 2f;
	public float mScaleMin	= 0.5f;

	public int mPresentHealth	= 100;



	// Orbit max Speed
	public float mOrbitMaxSpeed = 30f;

	// Orbit speed
	private float mOrbitSpeed;

	// Anchor point for the Present to rotate around
	private Transform mOrbitAnchor;

	// Orbit direction
	private Vector3 mOrbitDirection;

	// Max Present Scale
	private Vector3 mPresentMaxScale;
	// Growing Speed
	public float mGrowingSpeed	= 10f;
	private bool mIsPresentScaled	= false;

	private bool mIsAlive		= true;
	private AudioSource mExplosionFx;
	public int life;
	public GameObject LifeNumber;

	// Use this for initialization
	void Start () {

		LifeNumber = GameObject.Find ("Life");
		life= LifeScript.life;

		LifeNumber.GetComponentInChildren<Text> ().text = life.ToString ();

		PresentSettings();
	}

	// Update is called once per frame
	void Update () {
		// makes the cube orbit and rotate
		RotatePresent();

		// scale cube if needed
		if ( !mIsPresentScaled )
			ScaleObj();
	}


	private void PresentSettings(){
		// defining the anchor point as the main camera
		mOrbitAnchor = Camera.main.transform;

		// defining the orbit direction
		float x = Random.Range(-1f,1f);
		float y = Random.Range(-1f,1f);
		float z = Random.Range(-1f,1f);
		mOrbitDirection = new Vector3( x, y , z );

		// defining speed
		mOrbitSpeed = Random.Range( 5f, mOrbitMaxSpeed );

		// defining scale
		float scale = Random.Range(mScaleMin, mScaleMax);
		mPresentMaxScale = new Vector3( scale, scale, scale );

		// set cube scale to 0, to grow it lates
		transform.localScale = Vector3.zero;

		// getting Explosion Sound Effect
		mExplosionFx = GetComponent<AudioSource>();
	}

	// Makes the cube rotate around a anchor point
	// and rotate around its own axis
	private void RotatePresent(){
		// rotate cube around camera
		transform.RotateAround(
			mOrbitAnchor.position, mOrbitDirection, mOrbitSpeed * Time.deltaTime);

		// rotating around its axus
		transform.Rotate( mOrbitDirection * 30 * Time.deltaTime);
	}

	// Scale object from 0 to 1
	private void ScaleObj(){

		// growing obj
		if (transform.localScale != mPresentMaxScale) {

			float score = GetScore();
			mGrowingSpeed = mGrowingSpeed *(1 + 0.0f * score);

			transform.localScale = Vector3.Lerp (transform.localScale, mPresentMaxScale, Time.deltaTime * mGrowingSpeed);


		} else {

			life = life - 10;

			LifeScript.life = life;
			LifeNumber.GetComponentInChildren<Text> ().text = life.ToString ();

			if (life > 30) {
				LifeNumber.GetComponentInChildren<Text> ().color = Color.green;
			} else  if (life>0){
				LifeNumber.GetComponentInChildren<Text> ().color = Color.red;
			}

			else 

			{EndGame();}

			mIsPresentScaled = true;
		}
	}


	// Present got Hit
	// return 'false' when present was destroyed
	public bool Hit( int hitDamage ){
		mPresentHealth -= hitDamage;
		if ( mPresentHealth >= 0 && mIsAlive ) {
			StartCoroutine( DestroyPresent());
			return true;
		}
		return false;
	}

	// Destroy Present
	private IEnumerator DestroyPresent(){
		mIsAlive = false;

		mExplosionFx.Play();

		GetComponent<Renderer>().enabled = false;

		yield return new WaitForSeconds(mExplosionFx.clip.length);
		Destroy(gameObject);
	}


	private void EndGame ()

	{   SceneManager.LoadScene ("End");	  }



	private float GetScore ()
	{

		int score = LifeScript.score;

		float scorefloat = (float)score;

		return scorefloat;

	}

}
