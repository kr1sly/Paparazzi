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
	private ArrayList promis;
	private ArrayList textures;
	private ArrayList found;
	private GUIStyle TextStyle = new GUIStyle();
	
	void Start()
	{
		Screen.showCursor = false;
		cameraOverlay = GameObject.Find ("cameraOverlay");
		promis = new ArrayList();
		promis.Add (GameObject.Find ("Superman"));
		promis.Add (GameObject.Find ("Robin"));
		promis.Add (GameObject.Find ("Batman"));
		promis.Add (GameObject.Find ("Rikku"));
		promis.Add (GameObject.Find ("Altair"));
		promis.Add (GameObject.Find ("Harry"));
		promis.Add (GameObject.Find ("Gordon"));
		promis.Add (GameObject.Find ("Clown"));
		textures = new ArrayList();
		found = new ArrayList();
		
		TextStyle.normal.textColor = Color.red;
		TextStyle.fontStyle = FontStyle.Bold;
		TextStyle.fontSize = 25;
	}
    void Update()
    {
		if(cameraOverlay == null)
		{
		cameraOverlay = GameObject.Find ("cameraOverlay");
		}
		//Debug.Log (cameraOverlay.name);
        if (Input.GetKeyDown("k") && cameraOverlay.activeSelf)
		{
			
			audio.Play ();
			//cameraOverlay.SetActive (false);
            int i = 0;
			int rem = -1;
			foreach(GameObject promi in promis)
			{
				if(promi.renderer.isVisible)
				{
					float distance = Vector3.Distance(promi.transform.position, gameObject.transform.position);
	    			if(distance <= 15.0f)
					{
						StartCoroutine(ScreenshotEncode(promi));
						rem = i;
					}
					
				}
				else 
				{
					StartCoroutine(ScreenshotEncode(null));
				}
				i++;
			}
			
			if(rem != -1)
			{
				promis.RemoveAt(rem);
			}
			//else if (promis.Count == 0) cameraOverlay.SetActive (true);
			
		}
    }
	
	void OnGUI()
	{
		int offset = 0;
		int cnt = 0;
		GUI.Label (new Rect (Screen.width/2, 20, 50, 20), ""+found.Count+"/"+(found.Count+promis.Count)+"", TextStyle);
		foreach (Texture t in textures)
		{
			GUI.Label (new Rect (10, 10+offset, 100, 100), (Texture)t);
			GUI.Label (new Rect (120, 30+offset, 100, 40), found[cnt] as string, TextStyle);
			offset += 100;
			cnt++;
		}
	}
 
    IEnumerator ScreenshotEncode(GameObject prom)
    {
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
 		
        // create a texture to pass to encoding
        //Texture2D texture = new Texture2D(512, 418, TextureFormat.RGB24, false);
		//Debug.Log ("width: "+Screen.width/2+", height: "+(Screen.height/2));
		//Texture2D texture = new Texture2D(Screen.width-100, Screen.height, TextureFormat.RGB24, false);
		Texture2D texture = new Texture2D(320, 250, TextureFormat.RGB24, false);
 		//Texture2D texture = new Texture2D((int)Mathf.Round ((float)(Screen.width/2)), (int)Mathf.Round ((float)(Screen.height/1.837)), TextureFormat.RGB24, false);
        // put buffer into texture
		texture.ReadPixels(new Rect(145, 250, 320, 250), 0, 0);
        //texture.ReadPixels(new Rect(460, 170, 512, 418), 0, 0);
		//texture.ReadPixels (new Rect(50, 0, Screen.width-50, Screen.height), 0,0);
		//texture.ReadPixels(new Rect((int)Mathf.Round ((float)(Screen.width/2.226)), (int)Mathf.Round ((float)(Screen.height/4.517)), (int)Mathf.Round ((float)(Screen.width/2)), (int)Mathf.Round ((float)(Screen.height/1.837))), 0, 0);
        texture.Apply();
 		//cameraOverlay.SetActive (true);
        // split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;
 
        byte[] bytes = texture.EncodeToPNG();
 
        // save our test image (could also upload to WWW)
		if(prom != null)
		{
        	File.WriteAllBytes(Application.dataPath + "/../pictures/promi-" + promCount + ".png", bytes);
        	promCount++;
			//texture.Resize (100,100);
			texture.Compress (false);
			textures.Add (texture);
			found.Add (prom.name);
		}
		else
		{
			File.WriteAllBytes(Application.dataPath + "/../pictures/picture-" + count + ".png", bytes);
        	count++;
			DestroyObject( texture );
		}
 		
        // Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        //DestroyObject( texture );
 
        //Debug.Log( Application.dataPath + "/../testscreen-" + count + ".png" );
		
    }
}