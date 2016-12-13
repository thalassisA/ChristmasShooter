using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class LifeScript {

	public static int life = 100;

	public static int score;

	// Use this for initialization
	public static void Start () {

		GameObject LifeNumber = GameObject.Find ("Life");

		LifeNumber.GetComponentInChildren<Text> ().text = life.ToString ();
		
	}
	
	// Update is called once per frame
	public static void Update () {
		
	}
}
