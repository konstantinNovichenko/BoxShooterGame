using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {


    //base health points
    public float healthPoints = 1f;
	public bool isAlive = true;
    private GameObject boss;
		
	

	// Use this for initialization
	void Start () 
	{
        GameManager.gm.gameIsOver = false;
        isAlive = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0 && gameObject.tag == "Player")
        {
            isAlive = false;
            GameManager.gm.gameIsOver = true;
            GameManager.gm.mainTimerDisplay.color = Color.red;
            GameManager.gm.mainTimerDisplay.text = "YOU DIED";
            GameManager.gm.EndGame();
            
           
        }
        
    }
	
	
	public void ApplyDamage(float amount)
	{	
		healthPoints = healthPoints - amount;

}
	
}
