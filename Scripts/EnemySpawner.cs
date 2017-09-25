using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 5;
    public float height = 5;
    public PlayerController playerController;
    public float speed = 5;
    public float spawnDelay = 0.5f;
   
   

    bool movingRight = true;
    float xmax;
    float xmin;
	
	void Start ()
    {
       
        float distanceToCamera = transform.position. z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0,distanceToCamera));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightEdge.x;
        xmin = leftEdge.x;

        //loop that is true for every child that is a current child of this gameObject
        SpawnUntilFull();
        
	}

    void Update()
    {
        // float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
       
        if (movingRight)
        {
            //Vector3.right * speed * Time.deltaTime;
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        }
        else
        {
            //Vector3.left * speed * Time.deltaTime;
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }
        
        //old code made it so if it went outside playspace it wouldn't make it back into the play space cause it was constantly switching
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin) 
        {
            movingRight = !movingRight;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }

        if(AllMembersDead())
        {
            Debug.Log("emypy formation");
            SpawnUntilFull();
        }
        
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            //instantiates things as objects so you have to tell it it's a gameObject
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            //sets enemy as child of enemySpawner
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            //sets enemy as child of enemySpawner
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
        Invoke("SpawnUntilFull", spawnDelay);
    }

    public Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount <= 0)
            {
                return childPositionGameObject;
                
            }
        }
        return null;
    }

    public bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                Debug.Log(childPositionGameObject.childCount);
                return false;
            }
        }
        return true;
    }

    

    public void OnDrawGizmos()
    {
        //don't need the '0' at the end
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    // Update is called once per frame
    
}
