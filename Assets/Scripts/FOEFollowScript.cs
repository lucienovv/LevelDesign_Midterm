using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FOEFollowScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerFound = false;
    public Transform player;
    public Vector3 playerPosition;
    public Transform moveDir;
    public float moveSpeed = 3f;
    public float moveSpeedRotation = 180f;

    //rotation
    public float playerLookAngle;
    public float shortestDistance = 100;
    public Vector3 newPosition;
    public Vector3 oldPosition;


    //movement
    public List<Vector3> openGrid = new List<Vector3>();


    void Start()
    {
        GameManagerScript.main.Centering(transform);
        playerPosition = player.position;
        moveDir.parent = null;
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        playerPosition = player.position;
        
            

        if (GameManagerScript.main.enemyTurn)
        {
            

            if (playerFound && transform.position == moveDir.position)
            {
                Debug.Log(1);
                shortestDistance = 100;
                for (int i = 0; i < openGrid.Count; i++)
                {
                    Debug.Log(2);
                    if (Vector3.Distance(openGrid[i], playerPosition) < shortestDistance)
                    {
                        shortestDistance = Vector3.Distance(openGrid[i], playerPosition);
                        oldPosition = newPosition;
                        newPosition = openGrid[i];
                    }
                }

                openGrid = new List<Vector3>();

                //attackPlayer();

                openGrid = new List<Vector3>();

                if (Physics.CheckSphere(moveDir.position, .5f, LayerMask.GetMask("Player")))
                {
                    moveDir.position = playerPosition;
                }

                else moveDir.position = newPosition;

                if (oldPosition != newPosition)
                {
                    GetComponent<EnemyTurnDone>().turnDone = true;
                }
                

            }
            
            if (transform.position == moveDir.position)
                {
                    Debug.Log(3);
                    GetComponent<EnemyTurnDone>().turnDone = true;
                }

            if (!playerFound)
            {
                Debug.Log(4);
                GetComponent<EnemyTurnDone>().turnDone = true;
            }

            

        }
        
        FindPlayer();

        checkSpace();
        
        
        transform.position = Vector3.MoveTowards(transform.position, moveDir.position, moveSpeed * Time.deltaTime);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, moveDir.rotation, moveSpeedRotation * Time.deltaTime);
        

       

        

    }

    void checkSpace()
    {
        openGrid = new List<Vector3>();

        if (Physics.Raycast(moveDir.position, moveDir.forward, out RaycastHit hitF, LayerMask.GetMask("Wall")))
        {
            if (hitF.distance > 1)
            {
                openGrid.Add(moveDir.position + moveDir.forward);
            }
        }
        if (Physics.Raycast(moveDir.position, -moveDir.forward, out RaycastHit hitB, LayerMask.GetMask("Wall")))
        {
            if (hitB.distance > 1)
            {
                openGrid.Add(moveDir.position - moveDir.forward);
            }
        }
        if (Physics.Raycast(moveDir.position, moveDir.right, out RaycastHit hitR, LayerMask.GetMask("Wall")))
        {
            if (hitR.distance > 1)
            {
                openGrid.Add(moveDir.position + moveDir.right);
            }
        }
        if (Physics.Raycast(moveDir.position, -moveDir.right, out RaycastHit hitL, LayerMask.GetMask("Wall")))
        {
            if (hitL.distance > 1)
            {
                openGrid.Add(moveDir.position - moveDir.right);
            }
        }
    }


    private void FindPlayer()
    {
        if (Physics.CheckSphere(transform.position, 10f, LayerMask.GetMask("Player")))
        {
            if (!Physics.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Wall")))
            {
                playerFound = true;
            }
            else if (Physics.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Wall")))
            {
                playerFound = false;
            }
        }
        else playerFound = false;
    }

    void attackPlayer()
    {
        if (Physics.Raycast(moveDir.position, moveDir.forward, out RaycastHit hitF, LayerMask.GetMask("Player")))
        {
            if (hitF.distance < 1)
            {
                newPosition = moveDir.position + moveDir.forward;
            }
        }
        if (Physics.Raycast(moveDir.position, -moveDir.forward, out RaycastHit hitB, LayerMask.GetMask("Player")))
        {
            if (hitB.distance < 1)
            {
                newPosition = moveDir.position - moveDir.forward;
            }
        }
        if (Physics.Raycast(moveDir.position, moveDir.right, out RaycastHit hitR, LayerMask.GetMask("Player")))
        {
            if (hitR.distance < 1)
            {
                newPosition = moveDir.position + moveDir.right;
            }
        }
        if (Physics.Raycast(moveDir.position, -moveDir.right, out RaycastHit hitL, LayerMask.GetMask("Player")))
        {
            if (hitL.distance < 1)
            {
                newPosition = moveDir.position - moveDir.right;
            }
        }
    }

}
