using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 20;
    public float padding = 0.5f;
    public float xmin;
    public float xmax;
    public GameObject Projectile;
    Rigidbody2D beamrb;
    public float fireRate = 0.2f;
    public float health = 100f;
    public AudioSource laserAudio;
    public sceneChange sc;


    // Use this for initialization
    void Start()
    {
        sc = GetComponent<sceneChange>();
        laserAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        float distance = transform.position.z - Camera.main.transform.position.z;
        //Vector3 position in the world we will use to clamp gameObject to
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 1f, 0);
        //stating that beam is of type GameObject and that Instantiate returns a GameObject
        GameObject beam = Instantiate(Projectile,startPosition, Quaternion.identity) as GameObject;
        laserAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //2nd parameter is time before first invoktion
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }


        float mh = Input.GetAxis("Horizontal");
        float mv = Input.GetAxis("Vertical");

        Vector3 m = new Vector3(mh, 0, mv) * speed * Time.deltaTime;

        rb.MovePosition(transform.position + m);

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        laserBlue01 missile = col.gameObject.GetComponent<laserBlue01>();

        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
               sceneChange man = GameObject.Find("LevelManager").GetComponent<sceneChange>();
                man.LevelLoad("win"); 
                Destroy(gameObject);
               
            }
        }




    }

}
