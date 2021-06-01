using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	
     public AudioSource [] sounds;
	
    // Start is called before the first frame update
    void Start()
    {
		
	//Random Music from 4 different tracks on every play
        int randomIndex = Random.Range(0,4);
		
		if(randomIndex == 0)
		{
			sounds[0].Play();
		}
		if(randomIndex == 1)
		{
			sounds[1].Play();
		}
		if(randomIndex == 2)
		{
			sounds[2].Play();
		}
		if(randomIndex == 3)
		{
			sounds[3].Play();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
