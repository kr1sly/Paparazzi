using System;
using UnityEngine;
using System.Collections;


public class CameraOverlay: MonoBehaviour
{
	private GameObject cameraOverlay;
	private GameObject intro1;
	private GameObject intro2;
	
	public void Start()
	{
		cameraOverlay = GameObject.Find ("cameraOverlay");
		intro1 = GameObject.Find ("intro1");
		intro2 = GameObject.Find ("intro2");
		cameraOverlay.SetActive(false);
		intro1.SetActive(false);
		intro2.SetActive(false);
	}
	
	public void OnEnable ()
	{
	}
	public void OnDisable ()
	{
	}
	
	public void Update()
	{
		if (Time.frameCount <= 100)
		{
			intro1.SetActive (true);
		}
		else if (Time.frameCount <= 200)
		{
			intro1.SetActive (false);
			intro2.SetActive (true);
		}
		else intro2.SetActive (false);
		if(Input.GetKeyDown (KeyCode.C))
		{
			cameraOverlay.SetActive(!cameraOverlay.activeSelf);
		}
	}
}
		


