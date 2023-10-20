using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
   
    public float timer = 0f;
    public int score = 0;

    //Movement
    public Transform moveDir;
    public float moveSpeed = 5f;
    public float moveSpeedRotation = 270f;
   
    public int rotatedDir = 1;

    //collision
    public bool wallCollision = false;
    public bool hitF;
    public bool hitB;
    public bool hitL;
    public bool hitR;
    public bool hitTeleport;
    public bool hitTeleportTwo;
    public bool hitKey;

    //check movement
    Vector3 prevPosition;
    public float distTravel;
    RaycastHit hit;
    Vector3 directionF;
    Vector3 directionB;
    Vector3 directionL;
    Vector3 directionR;
    float checkDir;
    


    // Start is called before the first frame update
    void Start()
    {
        moveDir.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        timer = GameManager.Instance.timer;

        //Check Collisions
        if (rotatedDir == 1)
        {
            directionF = new Vector3(1, 0, 0);
            directionB = new Vector3(-1, 0, 0);
            directionL = new Vector3(0, 0, 1);
            directionR = new Vector3(0, 0, -1);
        }
        if (rotatedDir == 2)
        {
            directionF = new Vector3(0, 0, -1);
            directionB = new Vector3(0, 0, 1);
            directionL = new Vector3(1, 0, 0);
            directionR = new Vector3(-1, 0, 0);
        }
        if (rotatedDir == 3)
        {
            directionF = new Vector3(-1, 0, 0);
            directionB = new Vector3(11, 0, 0);
            directionL = new Vector3(0, 0, -1);
            directionR = new Vector3(0, 0, 1);
        }
        if (rotatedDir == 4)
        {
            directionF = new Vector3(0, 0, 1);
            directionB = new Vector3(0, 0, -1);
            directionL = new Vector3(-1, 0, 0);
            directionR = new Vector3(1, 0, 0);
        }

        float dirCheck;
        hitF = false;
        hitB = false;
        hitR = false;
        hitL = false;
        hitTeleport = false;
        hitTeleportTwo = false;
        hitKey = false;
        if (Physics.Raycast(transform.position, directionF, 1))
        {
            hitF = true;
        }
        if (Physics.Raycast(transform.position, directionB, 1))
        {
            hitB = true;
        }
        if (Physics.Raycast(transform.position, directionL, 1))
        {
            hitL = true;
        }
        if(Physics.Raycast(transform.position, directionR, 1))
        {
            hitR = true;
        }

        //check for special objects
        if (Physics.Raycast(transform.position,directionF, 1f))
        {
            if (Physics.Raycast(transform.position, directionF, out hit))
            {
                if (hit.transform.CompareTag("teleport"))
                { 
                    hitTeleport = true;
                }

            }
            if (Physics.Raycast(transform.position, directionF, out hit))
            {
                if (hit.transform.CompareTag("teleportTwo"))
                {
                    hitTeleportTwo = true;
                }
            }
            if (Physics.Raycast(transform.position, directionF, out hit))
            {
                if (hit.transform.CompareTag("key"))
                {
                    hitTeleportTwo = true;
                }
            }


        }



        //Movement
        if (transform.position == moveDir.position && transform.rotation == moveDir.rotation)
        {

            
            

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                dirCheck = Input.GetAxisRaw("Vertical");
                if (dirCheck > 0 && hitTeleport)
                {
                        if (rotatedDir == 1)
                        {
                            playerMove(2f, 0f, 0f);
                        }
                        if (rotatedDir == 2)
                        {
                            playerMove(0f, 0f, -2f);
                        }
                        if (rotatedDir == 3)
                        {
                            playerMove(-2f, 0f, 0f);
                        }
                        if (rotatedDir == 4)
                        {
                            playerMove(0f, 0f, 2f);
                        }
                }
                if (dirCheck > 0 && hitTeleportTwo)
                {
                    if (rotatedDir == 1)
                    {
                        playerMove(3f, 0f, 0f);
                    }
                    if (rotatedDir == 2)
                    {
                        playerMove(0f, 0f, -3f);
                    }
                    if (rotatedDir == 3)
                    {
                        playerMove(-3f, 0f, 0f);
                    }
                    if (rotatedDir == 4)
                    {
                        playerMove(0f, 0f, 3f);
                    }
                }
                if ((dirCheck < 0 && !hitB) || (dirCheck > 0 && !hitF))
                {
                    if (rotatedDir == 1)
                    {
                        playerMove(Input.GetAxisRaw("Vertical"), 0f, 0f);
                    }
                    if (rotatedDir == 2)
                    {
                        playerMove(0f, 0f, -Input.GetAxisRaw("Vertical"));
                    }
                    if (rotatedDir == 3)
                    {
                        playerMove(-Input.GetAxisRaw("Vertical"), 0f, 0f);
                    }
                    if (rotatedDir == 4)
                    {
                        playerMove(0f, 0f, Input.GetAxisRaw("Vertical"));
                    }
                }


                // moveDir.position += new Vector3(Input.GetAxisRaw("Vertical"), 0f, 0f);

            }


            else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                dirCheck = Input.GetAxisRaw("Horizontal");
                if ((dirCheck < 0 && !hitL) || (dirCheck > 0 && !hitR))
                {
                    if (rotatedDir == 1)
                    {
                        playerMove(0f, 0f, -Input.GetAxisRaw("Horizontal"));
                    }
                    if (rotatedDir == 2)
                    {
                        playerMove(-Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }
                    if (rotatedDir == 3)
                    {
                        playerMove(0f, 0f, Input.GetAxisRaw("Horizontal"));
                    }
                    if (rotatedDir == 4)
                    {
                        playerMove(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }
                }
            
                

            }

            //rotation
            if (Input.GetKey(KeyCode.A))
            {
                moveDir.transform.Rotate(0, -90, 0);
                rotatedDir--;  
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDir.transform.Rotate(0, 90, 0);
                rotatedDir++;
            }
            if (rotatedDir > 4)
            {
                rotatedDir = 1;
            }
            if (rotatedDir < 1)
            {
                rotatedDir = 4;
            }

            
        }

        //Smooth out movement
        transform.rotation = Quaternion.RotateTowards(transform.rotation, moveDir.rotation, moveSpeedRotation*Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, moveDir.position, moveSpeed * Time.deltaTime);

        //check if moving
        if (prevPosition != transform.position)
        {
            GameManager.Instance.turnActive = true;
        }
        if (prevPosition == transform.position)
        {
            GameManager.Instance.turnActive = false;
            
        }


        distTravel += Vector3.Distance(prevPosition, transform.position);
        if (distTravel > 1)
        {
            GameManager.Instance.turnActive = false;
            distTravel = 0;
        }
        prevPosition = transform.position;

        if (distTravel / Mathf.Round(distTravel) == 1)
        {
            GameManager.Instance.turnActive = false;
        }
        
        //Collisions



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "wall") 
        {
            wallCollision = true;
        }
        else
        {
            wallCollision = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void playerMove(float x, float y, float z)

    {
        //GameManager.Instance.turnActive = true;
        moveDir.position += new Vector3(x, y ,z);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("collectable"))
        {
            score++;
        }
        
    }
}
