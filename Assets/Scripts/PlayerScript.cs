using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{


    //score tracking
    public int treasure = 0;

    //Turn tracking
    public bool turnEnd; //check if action ends turn
    public int playerTurn = 1; //local player turn

    //Movement
    public Transform moveDir;
    public float moveSpeed = 5f;
    public float moveSpeedRotation = 270f;
    public float dirCheck;

    //Collisions
    public bool hitF;
    public bool hitB;
    public bool hitL;
    public bool hitR;
    public bool hitKey;
    public bool hitSkip;

    //action objects
    public RaycastHit hit;
    public int skipAmount;
    public int KeyID;

    void Start()
    {
        GameManagerScript.main.Centering(transform);
        moveDir.parent = null;
        playerTurn = 0;
    }

    
    void Update()
    {
        






        //check if it's player's turn
        if (GameManagerScript.main.playerTurn)
        {
            
           
            PlayerMove(1);

            //check for keys/buttons
            if (CheckForActions("key", 1f))
            {
                    hitKey = true;
                    KeyID = hit.transform.gameObject.GetComponent<KeyScript>().KeyID;         
            }
            

            //check for skip
            if (CheckForActions("skip", .7f))
            {
                hitF = false;
                hitSkip = true;      
                
                if (Input.GetKeyDown(KeyCode.W))
                { 
                    SkipMove(hit.transform.gameObject.GetComponent<SkipScript>().skipAmount);
                }   

            }
       }
        
    }



    void PlayerMove(int distance)
    {
        if (transform.position == moveDir.position && transform.rotation == moveDir.rotation)
        {
            //ensure Player movement happens before turn end
            GameManagerScript.main.turnNumber += playerTurn;
            playerTurn = 0;

            //forward/backward movement
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {

                dirCheck = Input.GetAxisRaw("Vertical");
                if ((dirCheck < 0 && !hitB) || (dirCheck > 0 && !hitF))
                {
                    moveDir.position += moveDir.transform.forward * Input.GetAxisRaw("Vertical") * distance;
                    playerTurn++;
                }
            }

            //side movement
            else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                dirCheck = Input.GetAxisRaw("Horizontal");
                if ((dirCheck < 0 && !hitL) || (dirCheck > 0 && !hitR))
                {
                    moveDir.position += moveDir.transform.right * Input.GetAxisRaw("Horizontal") * distance;
                    playerTurn++;
                }
            }

            else if (Input.GetKey(KeyCode.A))
            {
                moveDir.transform.Rotate(0, -90, 0);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                moveDir.transform.Rotate(0, 90, 0);
            }

        }
        //Smooth out movement
        transform.rotation = Quaternion.RotateTowards(transform.rotation, moveDir.rotation, moveSpeedRotation * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, moveDir.position, moveSpeed * Time.deltaTime);

        //recheck collissions after movement
        UncheckCollisions();
        CheckWallCollisions();
    }

    void CheckWallCollisions()
    {
        

        if (Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall")))
        {
            hitF = true;
        }
        if (Physics.Raycast(transform.position, -transform.forward, 1, LayerMask.GetMask("Wall")))
        {
            hitB = true;
        }
        if (Physics.Raycast(transform.position, -transform.right, 1, LayerMask.GetMask("Wall")))
        {
            hitL = true;
        }
        if (Physics.Raycast(transform.position, transform.right, 1, LayerMask.GetMask("Wall")))
        {
            hitR = true;
        }

        
    }

    bool CheckForActions(string action, float distance)
    {
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (hit.transform.CompareTag(action))
            {
                return true;
                
            }
            else
                return false;
        }
        else
            return false;

        
    }

    void SkipMove(int distance)
    {
        moveDir.position = moveDir.position + moveDir.transform.forward * distance;
    }

    void UncheckCollisions ()
    {
        hitF = false;
        hitB = false;
        hitL = false;
        hitR = false;
        hitKey = false;
        hitSkip = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("treasure"))
        {
            treasure++;
        }
    }
}



