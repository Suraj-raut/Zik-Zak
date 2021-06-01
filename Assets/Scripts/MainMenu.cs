using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{

	public string SAVE_FOLDER;
	public AudioSource ButtonClick;
	public GameObject LoadingScreen;
	public GameObject BestScore;
	public int HighScore;
	
	public GameObject Diamonds;
	public int RewardPoint;
	
	
		
	public class SaveObject
	{
	 public int Diamonds;
	 public int HighScore;
 
    }

	
	void Start()
	{
		
		SAVE_FOLDER = PlayerData.SAVE_FOLDER;                            //----Get the saved file and show the result in menu----//
        string MainSaveString = File.ReadAllText(SAVE_FOLDER + "Current_save.txt");
		SaveObject MainsaveObject = JsonUtility.FromJson<SaveObject>(MainSaveString);
		
		HighScore = MainsaveObject.HighScore;
		BestScore.GetComponent<Text>().text = " " + HighScore;
		
		RewardPoint = MainsaveObject.Diamonds;
		Diamonds.GetComponent<Text>().text = "Diamonds : " + RewardPoint;
	}
	
	void Update()
	{
		
		
		
	}

	public void PlayButton()
	{
		ButtonClick.Play();
		LoadingScreen.SetActive(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("Quit!");
	}
	

	
}

