using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingTextScript : MonoBehaviour
{
	private float DestroyTime = 1f;
	private Vector3 Offset = new Vector3(0, 3, 0);
	public float speed;
	public Vector3 direction;
	public float FadeTime;

	
   
    void Start()
    {
		//Destroy the Floating text after destory time
        Destroy(gameObject, DestroyTime);  
		transform.localPosition += Offset;
    }


   
    void Update()
    {
		//Move the Floating text in direction 
        float translation = speed * Time.deltaTime;
		transform.Translate(direction * translation);
    }
	
	public void Initialize(float speed, Vector3 direction, float FadeTime)
	{
		this.speed = speed;
		this.FadeTime = FadeTime;
		this.direction = direction;
		
		StartCoroutine(Fadeout());
	}
	
	private IEnumerator Fadeout()
	{
		//fading to text with time using alpha 
		float startAlpha = GetComponent<Text>().color.a;
		float rate = 1.0f / FadeTime;
		float progress = 0.0f;
		
		while(progress < 1.0)
		{
			Color tmpColor = GetComponent<Text>().color;
			
			//slowly color lerps to fade until it reachs the progress to 1.0
			GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));
			
			progress += rate * Time.deltaTime;
			yield return null;
			
		}
		
	}
}
