using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameObject[] playObjects;
	GameObject[] pauseObjects;
	GameObject  Elf;
	GameObject Santa;
	GameObject Helper;
	GameObject Score;
	GameObject Result;
	public int score;
	//String ttt;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;


		if (SceneManager.GetActiveScene ().name == "Play") {

			playObjects = GameObject.FindGameObjectsWithTag ("HideOnPause");
			pauseObjects = GameObject.FindGameObjectsWithTag ("ShowonPause");

			hidePaused ();
		} else if (SceneManager.GetActiveScene ().name == "End") {

			Elf = GameObject.FindGameObjectWithTag ("Elf");
			Helper = GameObject.FindGameObjectWithTag ("Helper");
			Santa = GameObject.FindGameObjectWithTag ("Santa");
			Result = GameObject.FindGameObjectWithTag ("Result");
			Score = GameObject.FindGameObjectWithTag ("Score");


			score = LifeScript.score;

		
			Score.GetComponentInChildren<Text> ().text = score.ToString ();
			//gets all objects with tag ShowOnPause
			//	finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");			//gets all objects with tag ShowOnFinish






			if (score > 20) {
				Santa.SetActive (true);
				Elf.SetActive (false);
				Helper.SetActive (false);
				Result.GetComponentInChildren<Text> ().text = "Hohoho! Santa is proud of you!";
			} else if (score < 10) {

				Santa.SetActive (false);
				Elf.SetActive (true);
				Helper.SetActive (false);
				Result.GetComponentInChildren<Text> ().text = "You are an Elf! Want to try again?";
			} else {
				Santa.SetActive (false);
				Elf.SetActive (false);
				Helper.SetActive (true);
				Result.GetComponentInChildren<Text> ().text = "You are a Santa helper! Better luck next time!";
			}

		}

	}

	// Update is called once per frame
	void Update () {




	}

	//Reloads the Level
	public void Reload(){

		LifeScript.life = 100;
		SceneManager.LoadScene ("Play");
	}

	//controls the pausing of the scene
	public void pauseControl(){
		
			Time.timeScale = 0;
			showPaused();

	}


	public void continueControl(){
		
			Time.timeScale = 1;
			hidePaused();

	}

	//shows objects with ShowOnPause tag
	public void showPaused(){


		foreach(GameObject h in playObjects){
			h.SetActive(false);
		}


		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject h in playObjects){
			h.SetActive(true);
		}

		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}


	//loads inputted level
	public void LoadLevel(string level){
	SceneManager.LoadScene (level);
	}

	public void GameExit()
	{ Application.Quit ();}

}
