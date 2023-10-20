using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FOEPatternScript : MonoBehaviour
{
    //traversable points
    public Transform PointsAll;
    public List<Transform> Points;
    public Transform moveDir;
    public List<Boolean> ReachedPoint;
    public int nextPoint = 1;

    public Vector3 point;

    //Movement
    float moveSpeed = 7f;

    //turn management
    
    

    // Start is called before the first frame update
    void Start()
    {
        //make sure starting point is int
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

        //seperate transforms so they don't move with object
        PointsAll.parent = null;
        moveDir.parent = null;
        

        //remove empty points
        for (int i = 9; i > 0; i--)
        {
            if (Points[i].localPosition == Vector3.zero)
            {
                Points.RemoveAt(i);
            }
            else if (Points[i].localPosition != Vector3.zero)
            {
                Points[i].position = new Vector3(Mathf.Round(Points[i].position.x), Mathf.Round(Points[i].position.y), Mathf.Round(Points[i].position.z));
            }
        }

        //initiate starting position
        moveDir.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDir.position, moveSpeed * Time.deltaTime);

        if(GameManagerScript.main.enemyTurn)
        {
            FOEMove();
        }
        
    }

    void FOEMove()
    {   
        

        
        
        if (moveDir.position == Points[nextPoint].position)
        {
            nextPoint += 1;
        }

        if (nextPoint == Points.Count)
        {
            nextPoint = 0;
        }

        if (transform.position == moveDir.position)
        {
            GetComponent<EnemyTurnDone>().turnDone = true;
            moveDir.position += (Points[nextPoint].position - transform.position).normalized;
            moveDir.position = new Vector3(Mathf.Round(moveDir.position.x), Mathf.Round(moveDir.position.y), Mathf.Round(moveDir.position.z));
             //turn End
        }
        
        
        
        
      
    }
}
