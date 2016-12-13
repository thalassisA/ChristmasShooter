using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButtonDown {


	//ButtonDown buttondown = new ButtonDown();

	//public ButtonDown buttondown;
	public static bool clicked;
	// Use this for initialization
	public static void Start()
	{
		clicked = true;


	}


	public static bool OnClick()

	{


		if (clicked == true) {
			clicked = false;
		} else {
			clicked = true;
		}

		return clicked;

	}

}
