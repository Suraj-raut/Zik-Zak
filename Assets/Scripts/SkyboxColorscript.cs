using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyboxColorscript : MonoBehaviour
{
	public Material Skybox1;
	public Material Skybox2;
	public float Timer = 30f;
	

    // Start is called before the first frame update
    void Start()
    {
		// Default Skybox
		RenderSettings.skybox = Skybox2;
		
	 }

    // Update is called once per frame
	
	void Update()
	{
		
		if(Timer > 0)
		{
			Timer = Timer - Time.deltaTime;  // 30 - 20  -- Skybox2;
			
		}
		 
	    if(Timer <= 20 )                     // 20 - 10 -- Skybox1;
	    {
			RenderSettings.skybox = Skybox1;
		
	    }
		
	   if(Timer < 10 )                       // less than 10 -- again Skybox2 
		{
		
		RenderSettings.skybox = Skybox2;
			
			
			for(int i = 0; i < 20; i++)     // it remain Skybox2 until Timer reaches back again to 20 
			{
			Timer = Timer + Time.deltaTime;
			Timer++;
			}                               // after 20 repeat
		}
		

}
	
	
}
