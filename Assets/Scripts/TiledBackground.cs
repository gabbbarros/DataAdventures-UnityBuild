using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour {

	public int  textureSizeX = 32;
	public int textureSizeY = 32;

	public bool scaleHorizontal = true;
	public bool scaleVertical = true;

	// Use this for initialization
	void Start () {
		var newWidth = !scaleHorizontal ? 1 : Mathf.Ceil (Screen.width/ (textureSizeX * PixelPerfectCamera.scale));
		var newHeight = !scaleVertical ? 1 : Mathf.Ceil (Screen.height/ (textureSizeY * PixelPerfectCamera.scale));

		transform.localScale = new Vector3(newWidth * textureSizeX, newHeight * textureSizeY, 1);

		GetComponent <Renderer>().material.mainTextureScale = new Vector3 (newWidth, newHeight,1);
	}

}
