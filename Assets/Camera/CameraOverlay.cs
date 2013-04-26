using System;
using UnityEngine;
using System.Collections;


public class CameraOverlay: MonoBehaviour
{
	private GameObject cameraOverlay;
	
	public void Start()
	{
		cameraOverlay = GameObject.Find ("cameraOverlay");
		cameraOverlay.active = false;
	}
	
	public void OnEnable ()
	{
	}
	public void OnDisable ()
	{
	}
	
	public void Update()
	{
		if(Input.GetKeyDown (KeyCode.C))
		{
			cameraOverlay.active = !cameraOverlay.active;
		}
	}
}
		


