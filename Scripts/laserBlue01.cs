using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBlue01 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float projectileSpeed = 10;
    public int damage = 20;
    

    // Use this for initialization
    void Start ()
    {   
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       rb.velocity = new Vector3(0, projectileSpeed);
       Destroy(gameObject, 2f);
    }

    //parameters I think the type of collider you're looking for and second parameter is the name the other object that collides is given
  

    public int GetDamage()
    {
        return damage;
        
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
