using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilesscript : MonoBehaviour
{
	
	private float fallDelay = 1.5f;
  
	void OnTriggerExit(Collider other)                   //---instantiate new tiles from tile manager--///
	{
		if(other.tag == "Player")
		{
			Tilemanager.Instance.SpawnTile();
			StartCoroutine(FallDown());
		}
	}
	
	IEnumerator FallDown()
	{
		yield return new WaitForSeconds(fallDelay);      //--- tiles fall after the fall delay --// 
		GetComponent<Rigidbody>().isKinematic = false;
		
		yield return new WaitForSeconds(2);
		switch (gameObject.name)
		{
			case "Lefttile" :
				Tilemanager.Instance.Lefttile.Push(gameObject);     //--put the left tile back to stack of lefttile--//
				gameObject.GetComponent<Rigidbody>().isKinematic = true;
				gameObject.SetActive(false);
					break;
				
			case "Toptile" :
				Tilemanager.Instance.Toptile.Push(gameObject);      //--put the top tile back to stack of topttile--//
				gameObject.GetComponent<Rigidbody>().isKinematic = true;
				gameObject.SetActive(false);
					break;
				
		}
		
		
	}
}
