using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{


    //base health points
    public float healthPoints = 1f;
    private float healthPointsAmount;
    public bool isAlive = true;
    public GameObject explosionPrefab;
   


    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        

        if (healthPoints <= 0)
        {
            // if the object is 'dead'	
            if (gameObject.tag == "Boss")
            {

                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                }

                // load victory screen
                GameManager.gm.targetHit(100, 0);
                GameManager.gm.WonGame();
            }

            else
            {
                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                }
            }

            Destroy(gameObject);
        }

        // remove boss from the scene if it's still active after players dead
        else if (healthPoints > 0 && GameManager.gm.gameIsOver == true)
        {
            if (gameObject.tag == "Boss")
            {
                Destroy(gameObject);
            }
            else if (gameObject.tag == "BossProjectile")
            {
                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }


    }





    public void ApplyDamage(float amount)
    {
                
        healthPoints = healthPoints - amount;

    }

    
}

    
