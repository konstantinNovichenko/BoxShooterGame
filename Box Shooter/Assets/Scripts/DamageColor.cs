using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColor : MonoBehaviour {


    
    public float mediumHealthPoints;
    public float lowHealthPoints;

    public Material highHealth;
    public Material mediumHealth;
    public Material lowHealth;

    private Health healthPoints;
    private Renderer rend;
    private Animation anim;
    private Collider collision;

    // Use this for initialization
    void Start()
    {
        healthPoints = gameObject.GetComponent<Health>();
        anim = gameObject.GetComponent<Animation>();
        collision = gameObject.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update () {
        // update game object components
        healthPoints = gameObject.GetComponent<Health>();
        rend = gameObject.GetComponent<Renderer>();
        collision = gameObject.GetComponent<Collider>();


        // update material based on health
        if (healthPoints.healthPoints > mediumHealthPoints)
        {
            rend.material = highHealth;
            if (collision.gameObject.tag == "Projectile")
            {
                anim.Play("Boss Damage");
            }
        }

        else if (healthPoints.healthPoints < mediumHealthPoints &&
            healthPoints.healthPoints > lowHealthPoints)
        {
            rend.material = mediumHealth;
        }

        if (healthPoints.healthPoints < lowHealthPoints)
        {
            rend.material = lowHealth;
        }

    }

   
}
