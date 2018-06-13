using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

	public static float pixelsToUnit = 1f;
	public static float scale = 1f;

	public Vector2 nativeResolution =  new Vector2(240, 160);
	void Awake ()
	{
		// get a refreance to the camera object in the game
		var camera = GetComponent <Camera> ();

		if (camera.orthographic) {
			// update the scale by getting the screen height
			scale = Screen.height/nativeResolution.y;
			pixelsToUnit *= scale;

			// update the camera size
			camera.orthographicSize = (Screen.height/2.0f) / pixelsToUnit;
		}
	}

}
