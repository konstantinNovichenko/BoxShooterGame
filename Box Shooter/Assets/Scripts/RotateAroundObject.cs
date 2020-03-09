using UnityEngine;
using System.Collections;

public class RotateAroundObject : MonoBehaviour 
{
    public GameObject target;
	public bool flipDirection = false;
    public int speed; // the speed of rotation
    public float mediumHealthPoints;
    public int mediumHealthSpeedAcceleration;
    public float lowHealthPoints;
    public int lowHealthSpeedAcceleration;
    public float motionMagnitude;


    private Health healthPoints;
    private int maxSpeedMediumHealth;
    private int maxSpeedLowHealth;



    private void Start()
    {
        maxSpeedMediumHealth = speed + mediumHealthSpeedAcceleration;
        maxSpeedLowHealth = maxSpeedMediumHealth + lowHealthSpeedAcceleration;
    }
    void Update () 
	{

        healthPoints = gameObject.GetComponent<Health>();
        Vector3 direction = flipDirection ? -target.transform.up : target.transform.up;
        transform.RotateAround(target.transform.position,
            direction,
            (speed * Time.deltaTime));

        if (healthPoints.healthPoints > mediumHealthPoints)
        {
            flipDirection = false;
            
        }
        

        else if (healthPoints.healthPoints < mediumHealthPoints && healthPoints.healthPoints > lowHealthPoints)
        {
            flipDirection = true;           
            speed += mediumHealthSpeedAcceleration;
            if (speed > maxSpeedMediumHealth)
            {
                speed = maxSpeedMediumHealth;
            }
           

        }

        else if (healthPoints.healthPoints < lowHealthPoints)
        {
            flipDirection = false;
            speed += lowHealthSpeedAcceleration;
            if (speed > maxSpeedLowHealth)
            {
                speed = maxSpeedLowHealth;
            }
            
            gameObject.transform.Translate(Vector3.up * Mathf.Cos(Time.timeSinceLevelLoad) * motionMagnitude);
        }


       

    }
}
