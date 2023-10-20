using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoeMovementFollow : MonoBehaviour
{



    //points positions

    public bool point1 = false;


    //points
    public GameObject point;
    

    float moveSpeed = 7f;

    //clamp to turn
    public static float distTravel;
    public float distTravelperF;
    Vector3 prevPosition;
    public bool turnTaken;

    //player position
    public GameObject player;
    public Vector3 playerPosition;



    // Start is called before the first frame update
    void Start()
    {

       prevPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.GetComponent<Transform>().transform.position;

        if (GetComponent<FOV>().playerInVision)
        {
            if (distTravel + distTravelperF >= 1)
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
                turnTaken = true;
            }
            if (distTravel > 1)
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
                turnTaken = true;

            }

            if (!GameManager.Instance.turnActive && turnTaken)
            {
                distTravel = 0;
            }

            if (GameManager.Instance.turnActive && distTravel + distTravelperF < 1)
            {
                turnTaken = false;
                if (!point1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
                    if (transform.position == playerPosition)
                    {
                        point1 = true;
                    }
                }
            }









            distTravel += Vector3.Distance(prevPosition, transform.position);

            if (prevPosition != transform.position)
            {
                distTravelperF = Vector3.Distance(prevPosition, transform.position);

            }
            prevPosition = transform.position;

        }

    }
}
