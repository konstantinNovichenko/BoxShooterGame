using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	// public variables
	public int score=0;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;
    

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;
    public AudioClip bossFightAudioclip;
    public AudioClip finalAudioclip;
    

	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

    public GameObject restartGameButtons;
    public string levelToRestarts;
   
    public GameObject exitGameButtons;

    private float currentTime;

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager>();

		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
        
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons && SceneManager.GetActiveScene().name != "Level0")
			playAgainButtons.SetActive (false);

		// inactivate the nextLevelButtons gameObject, if it is set
		if (nextLevelButtons)
			nextLevelButtons.SetActive (false);

        // inactivate the restartGameButtons gameObject, if it is set
        if (restartGameButtons)
            restartGameButtons.SetActive(false);

        if (exitGameButtons && SceneManager.GetActiveScene().name != "Level0")
            exitGameButtons.SetActive(false);
        
         

    }

    // this is the main game event loop
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level0")
        {
            musicAudioSource.pitch = 0.5f;
        }

        if (!gameIsOver)
        {
            if (canBeatLevel && (score >= beatLevelScore))
            {  // check to see if beat game
                BeatLevel();
            }
            else if (currentTime < 0)
            { // check to see if timer has run out
                mainTimerDisplay.color = Color.white;
                // repurpose the timer to display a message to the player
                mainTimerDisplay.text = "TIME IS UP";
                EndGame();
            }
            else
            { // game playing state, so update the timer
                currentTime -= Time.deltaTime;
                mainTimerDisplay.text = currentTime.ToString("0.00");
                if (currentTime <= 10.0)
                {
                    mainTimerDisplay.color = Color.red;
                }
                else
                {
                    mainTimerDisplay.color = Color.white;
                }
            }
            
        }

        
    }

       

	public void EndGame() {
		// game is over
		gameIsOver = true;

		

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
	
		// activate the playAgainButtons and exitGameButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

        
        if (restartGameButtons)
            restartGameButtons.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

        // repurpose the timer to display a message to the player
        mainTimerDisplay.color = Color.white;
		mainTimerDisplay.text = "LEVEL COMPLETE";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		// activate the nextLevelButtons gameObject, if it is set 
		if (nextLevelButtons)
			nextLevelButtons.SetActive (true);
        if (restartGameButtons)
            restartGameButtons.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

    public void WonGame()
    {
        // game is over
        gameIsOver = true;

        // repurpose the timer to display a message to the player
        mainTimerDisplay.color = Color.green;
        mainTimerDisplay.text = "YOU WON!";

        // activate the gameOverScoreOutline gameObject, if it is set 
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(true);

        // activate the nextLevelButtons gameObject, if it is set 
        if (restartGameButtons)
            restartGameButtons.SetActive(true);

        if (exitGameButtons)
            exitGameButtons.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (musicAudioSource)
            musicAudioSource.clip = finalAudioclip;
            musicAudioSource.Play();
            musicAudioSource.pitch = 0.5f; // slow down the music
    }

    // public function that can be called to update the score or time
    public void targetHit (int scoreAmount, float timeAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();
		
		// increase the time by the timeAmount
		currentTime += timeAmount;
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
        // we are just loading a scene (or reloading this scene)
        // which is an easy way to restart the level
        SceneManager.LoadScene(playAgainLevelToLoad);
		//Application.LoadLevel (playAgainLevelToLoad);
	}

    // public function to restart game from the beginning
    public void ReplayFromBeginning ()
    {
        SceneManager.LoadScene(levelToRestarts);
        //Application.LoadLevel(levelToRestarts);
    }

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
        // we are just loading the specified next level (scene)
        SceneManager.LoadScene(nextLevelToLoad);
		//Application.LoadLevel (nextLevelToLoad);
        
	}

    

    // public function to exit game
    public void ExitGame ()
    {
        Application.Quit();
    }

    public void BossMusic ()
    {
        musicAudioSource.clip = bossFightAudioclip;
        musicAudioSource.Play();
    }

    
    
    
}


