using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class PlayerData 
{
   public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";  //--Create the folder inside asset---//
   public static int HighScore;
   public static int Diamonds;
   
		
	public static int GetHighScore(int _highscore)
	{
		HighScore = _highscore;
		return HighScore;
	}
	
	public static int GetTotalDiamonds(int _diamonds)
	{
		Diamonds = _diamonds;
	
		return Diamonds;
	}
	
	
	public static void Init()
	{
		if(!Directory.Exists(SAVE_FOLDER))
		{
			Directory.CreateDirectory(SAVE_FOLDER + "Current_save.txt");    //---Create the savefile in the saveFolder location--//
		}
		
	}
	
	public static void Save(string saveString)                              //---Save the data in current_save file--//
	{

    	File.WriteAllText(SAVE_FOLDER + "Current_save.txt", saveString);
			
		
	}
	
	public static string Load()
	{
		if(File.Exists(SAVE_FOLDER + "Current_save.txt"))                  //---Load the data from current_save file to a string--//
		{
			string saveString = File.ReadAllText(SAVE_FOLDER + "Current_save.txt");
			return saveString;
		}
		else{
			return null;
		}
	}
	
	

	
	
}
