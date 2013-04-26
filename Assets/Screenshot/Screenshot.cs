// ******  Notice : It doesn't works in Wep Player environment.  ******
// ******    It works in PC environment.                         ******
// Default method have some problem, when you take a Screen shot for your game. 
// So add this script.
// CF Page : http://technology.blurst.com/unity-jpg-encoding-javascript/
// made by Jerry ( sdragoon@nate.com )
 
using UnityEngine;
using System.Collections;
using System.IO;
 
public class Screenshot : MonoBehaviour
{
    private int count = 0;
	private int promCount = 0;
 	private GameObject cameraOverlay;
	private GameObject superMan;
	
	void Start()
	{
		cameraOverlay = GameObject.Find ("cameraOverlay");
		superMan = GameObject.Find ("superman");
	}
    void Update()
    {
        if (Input.GetKeyDown("k") && cameraOverlay.activeSelf)
		{
			//cameraOverlay.SetActive (false);
            
			if(superMan.renderer.isVisible)
			{
				StartCoroutine(ScreenshotEncode(true));
				Debug.Log ("yeah");
			}
			else 
			{
				StartCoroutine(ScreenshotEncode(false));
				Debug.Log ("ney");
			}
			
			
		}
    }
 
    IEnumerator ScreenshotEncode(bool prom)
    {
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
 		
        // create a texture to pass to encoding
        Texture2D texture = new Texture2D(512, 418, TextureFormat.RGB24, false);
 
        // put buffer into texture
		
        texture.ReadPixels(new Rect(460, 170, 512, 418), 0, 0);
		//cameraOverlay.SetActive (true);
        texture.Apply();
 
        // split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;
 
        byte[] bytes = texture.EncodeToPNG();
 
        // save our test image (could also upload to WWW)
		if(prom)
		{
        	File.WriteAllBytes(Application.dataPath + "/../pictures/promi-" + promCount + ".png", bytes);
        	promCount++;
		}
		else
		{
			File.WriteAllBytes(Application.dataPath + "/../pictures/picture-" + count + ".png", bytes);
        	count++;
		}
 
        // Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        DestroyObject( texture );
 
        //Debug.Log( Application.dataPath + "/../testscreen-" + count + ".png" );
		
    }
}