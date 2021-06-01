using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Playerscript : MonoBehaviour
{
	public float speed;
	private Vector3 dir;
	public GameObject ps;
	public bool isDead;
	public GameObject ResetButton;
	public int score = 0;
	public Text scoreText;
	public GameObject PickupTextPrefab;
	public Animator gameOverAnim;
	public Text newHighScore;
	public Image background;
	public Text [] scoreTexts;
	public LayerMask whatIsGround;
	private bool isPlaying = false;
	public Transform contactPoint;
	private int difficultyLevel = 1;
	private int maxDifficultyLevel = 10;
	private int scoreToNextLevel = 20;
	
	private bool isPaused = false;
	public GameObject PausedScreen;
	public GameObject BackToGameText;
	
	public AudioSource PointRewards;
	
	public int HighScoreinMenu;
	public int Diamonds = 0; 
	public GameObject DiamondDisplay;


    // Start is called before the first frame update
    void Start()
    {
		isDead = false;
		dir = Vector3.zero;
			
    }

    // Update is called once per frame
    void Update()
    {	
		if(isPaused == true)
			return;
		
	
	//Increase the speed after every 20 score	
	if(score >= scoreToNextLevel)
	{
		LevelUp();
	}
		
		if(!IsGrounded() && isPlaying)
			{
				// Kill player
				isDead = true;
				GameOver();
				
				ResetButton.SetActive(true);
				if(transform.childCount > 0)
				{
					transform.GetChild(0).transform.parent = null;
				}
				
			}
		
		//Player input to Move Player
        if(Input.GetMouseButtonDown(0) && !isDead)
		{
			
			isPlaying = true;
			score++;
		    scoreText.text = score.ToString();
			
			if(dir == Vector3.forward)
			{
				dir = Vector3.left;
			}
			
			else
			{
				dir = Vector3.forward;
			}
		}
		
		float amountToMove = speed * Time.deltaTime;
		
		transform.Translate(dir * amountToMove);
		
		//To Pause the Game
		if(Input.GetMouseButtonDown(1) && !isDead)
		{
			isPaused = true;
		    isPlaying = false;
			
			if(dir == Vector3.forward || dir == Vector3.left)
			{
			 	dir = Vector3.zero;
			    amountToMove = 0;
		     }
			
			PausedScreen.SetActive(true);
			
	    }
		
		PlayerData.GetTotalDiamonds(Diamonds);
		
		}
		
	// To Pickup the 3+ reward points to score 	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Pickup")
		{
			
			Diamonds++;
			DiamondDisplay.GetComponent<Text>().text = " " + Diamonds;
			PointRewards.Play();
			other.gameObject.SetActive(false);
			Instantiate(ps, transform.position, Quaternion.identity);
		    score+= 3;
		    scoreText.text = score.ToString();	
			ShowPickupText();
		}
	}
	
	void ShowPickupText()
	{
		Instantiate(PickupTextPrefab, transform.position, Quaternion.identity);
	}
	
    // Game Over
	public void GameOver()
	{
		gameOverAnim.SetTrigger("Game over");
		
		scoreTexts[1].text = score.ToString();
		
		int bestScore = PlayerPrefs.GetInt("BestScore", 0);
		HighScoreinMenu = bestScore;
		PlayerData.GetHighScore(HighScoreinMenu);
		
		if(score > bestScore)
		{
			PlayerPrefs.SetInt("BestScore", score);
			newHighScore.gameObject.SetActive(true);
			background.color = new Color32(255,18,246,250);
			foreach(Text txt in scoreTexts)
			{
				txt.color = Color.white;
			}
		}
		
		scoreTexts[3].text = PlayerPrefs.GetInt("BestScore", 0).ToString();
		
	
	}
	
	//Check the player is grounded or not 
	private bool IsGrounded()
	{
		Collider[] colliders = Physics.OverlapSphere(contactPoint.position, .5f, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].gameObject != gameObject)
			{
				return true;
			}
		}
		
		return false;
	}
	
	//Level Up after every 20 score by increasing the speed
	void LevelUp()
	{
		if(difficultyLevel == maxDifficultyLevel)
		{
			return;
		}
		
		else
			
		scoreToNextLevel *= 2;
		difficultyLevel++;
		speed += 1;
		
		
	}
	
	//Resume to Game
	public void ResumeGame()
	{
		PausedScreen.SetActive(false);
	
		StartCoroutine(BackToGame());
	}
	
	IEnumerator BackToGame()
	{
		BackToGameText.SetActive(true);
		yield return new WaitForSeconds(2);				 
		BackToGameText.SetActive(false);
		yield return new WaitForSeconds(1);
	
		isPaused = false;
		isPlaying = true;	
		
	}
	

	
	
}

