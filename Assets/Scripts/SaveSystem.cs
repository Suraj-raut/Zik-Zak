using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;      //-- Used to save the data in binary form --//

public class SaveSystem : MonoBehaviour
{
	
	public int HighScore;
	public int Diamonds;
	public int playerDiamonds;
	public int playerHighScore;
	public SaveObject so;
	public GameObject _player;
	public Playerscript PS;
	public bool isPlaying;
	
 
	
	void Awake()
	{
		
		PS = _player.GetComponent<Playerscript>();	
		PlayerData.Init();
		Load();
	}

	
	void Update()
	{
		
		isPlaying = PS.isDead;                               
		
		
		if(isPlaying == true)                                          //-------If isDead is true save the game---//
		{
			Save(so);
		
		}
		
	}
	
	
	public void Save(SaveObject _saveObject)
	{
		
		Debug.Log("Saved");
		
         playerDiamonds =  PlayerData.Diamonds + _saveObject.Diamonds;   //----Previous diamonds saved + current diamonds --//
		 playerHighScore = PlayerData.HighScore;
		
		
		
		SaveObject saveObject = new SaveObject{                         //---New SaveObject based on new diamonds count --//
			
		    Diamonds = playerDiamonds,
			HighScore = playerHighScore,
				
		};
		
		string json = JsonUtility.ToJson(saveObject);                //---Convert the object to a string by JsonUtility --//
		
		PlayerData.Save(json);
		
//		BinaryFormatter bf = new BinaryFormatter();                 //--- Binary save method for data security purpose--//
//		FileStream stream = new FileStream(SAVE_FOLDER + "/Player.sav", FileMode.Create);
//		bf.Serialize(stream, saveObject);
//		stream.Close();
		
	}
	
	public SaveObject Load()
	{
		
		string saveString = PlayerData.Load();
		
		if(saveString != null)
		{
			Debug.Log("Loaded: " + saveString);
			SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);  //--Create the Object back from saveString--//
			so = saveObject;
			
		}
		else{
			Debug.Log("No Saves");
			return null;
		}
		
		return so;                                                               //---return SaveObject for next save--//
		
//		if(File.Exists(Application.persistentDataPath + SAVE_FOLDER  + "/Player.sav"))      // --Binary load method ---//
//		{
//			BinaryFormatter bf = new BinaryFormatter();
//			FileStream stream = new FileStream(SAVE_FOLDER + "/Player.sav", FileMode.Open);
//			SaveObject so = bf.Deserialize(stream) as SaveObject;
//			stream.Close();
//			Debug.Log("Loaded : " + so.Diamonds + so.HighScore);
//			return so;
//	
//			
//		}
//		else { 
//			
//			Debug.Log("No Save file");
//			return null;
//			
//		}	
		
			
	}
	
	[System.Serializable]                          //-----Savable Object class-----//
	public class SaveObject
	{
		
	 public int Diamonds;
	 public int HighScore;	
	 
    }
	
			
	}
	
	
	

