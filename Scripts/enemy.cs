using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject projectile;
    public int health = 40;
    public float projectileSpeed = 2f;
    public int damage = 10;
    public float shotsPerSec = 0.5f;
    public ScoreKeeper scoreKeeper;
    public int scoreValue = 25;
    public AudioSource ela;
    public AudioClip eda;


    // Use this for initialization
    void Start ()
    {
        
        ela = GetComponent<AudioSource>();
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float probability = shotsPerSec * Time.deltaTime;
        //andom.value is between 0  & 1
        if (Random.value < probability)
        {
            Fire();
        }
	}

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1f, 0);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        ela.Play();
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        laserBlue01 missile = col.gameObject.GetComponent<laserBlue01>();
        
        if(missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0)
            {
                AudioSource.PlayClipAtPoint(eda, transform.position);
                Debug.Log(eda);
                Destroy(gameObject);
                scoreKeeper.Score(scoreValue);
                
            }
        }
        /*Destroy(col.gameObject);
        health -= damageHealth;*/
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }


}
