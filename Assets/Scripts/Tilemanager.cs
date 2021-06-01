using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tilemanager : MonoBehaviour
{
    public GameObject[] TilePrefabs;
	public GameObject Currenttile;

	private static Tilemanager instance;

	private Stack<GameObject> Lefttiles = new Stack<GameObject>();  // ---Save the left tile to stack and get the same --//
	public Stack<GameObject> Lefttile
	{
		get{ return Lefttiles; }
		set{ Lefttiles = value; }
	}
   
	private Stack<GameObject> Toptiles = new Stack<GameObject>();  // ---Save the top tile to stack and get the same --//
	public Stack<GameObject> Toptile
	{
		get{ return Toptiles; }
		set{ Toptiles = value; }
	}
	
	
	public static Tilemanager Instance  //--- Get instance of Tile Manager ----//
	{
		get
		{ 
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<Tilemanager>();
			}
			return instance;
		}
	}
	
	
    // Start is called before the first frame update
    void Start()
    {
		CreateTiles(100);
		
		for(int i = 0; i < 50; i++)   //---- Spawn 50 tiles ----//
		{
        SpawnTile();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void CreateTiles(int amount)    // --- put 100 tiles in stack--//
	{
		for(int i = 0; i < amount; i++ )
		{
			Lefttiles.Push(Instantiate(TilePrefabs[0]));  //---put the left tile to stack from the tileprefab array index[0]
		    Toptiles.Push(Instantiate(TilePrefabs[1]));   //---put the top tile to stack from the tileprefab array index[1]
			Toptiles.Peek().name = "Toptile";
			Toptiles.Peek().SetActive(false);
			Lefttiles.Peek().name = "Lefttile";
			Lefttiles.Peek().SetActive(false);
			
		}
	}
	
	
	public void SpawnTile()     
	{
		if(Lefttiles.Count == 0 || Toptiles.Count == 0)   //--- Spawn 10 tiles --/// 
		{
			CreateTiles(10);
		}
		
		
		int randomIndex = Random.Range(0,2);
		
		if(randomIndex == 0)                            // ----Create left tile at top or left spawnpoint i.e [0,1] --//
		{
			GameObject tmp = Lefttiles.Pop();
			tmp.SetActive(true);
			tmp.transform.position = Currenttile.transform.GetChild(0).transform.GetChild(randomIndex).position;
			Currenttile = tmp;
		}
		
		else if(randomIndex == 1)                      // ----Create top tile at top or left spawnpoint i.e [0,1] --//
		{   
			GameObject tmp = Toptiles.Pop();
			tmp.SetActive(true);
			tmp.transform.position = Currenttile.transform.GetChild(0).transform.GetChild(randomIndex).position;
			Currenttile = tmp;
		}
		
		int SpawnPickup = Random.Range(0, 10);        // ----Create Pickup reward randamly if its 0 ---//
			
			if(SpawnPickup == 0)
			{
				Currenttile.transform.GetChild(1).gameObject.SetActive(true);
			}
		
		
		// Currenttile = (GameObject)Instantiate(TilePrefabs[randomIndex],Currenttile.transform.GetChild(0).transform.GetChild(randomIndex).position, Quaternion.identity);
	}
	
	public void ResetGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void BackToMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
	
	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("Quit!");
	}
	
}
