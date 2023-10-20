using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoeMovement : MonoBehaviour
{

   

    //points positions
    public Vector3 point0Pos;
    public Vector3 point1Pos;
    public Vector3 point2Pos;
    public Vector3 point3Pos;

    public bool point0 = false;
    public bool point1 = false;
    public bool point2 = false;
    public bool point3 = false;

    //points
    public GameObject point; 
    List<GameObject> Points = new List<GameObject>();

    float moveSpeed = 7f;

    //clamp to turn
    public static float distTravel;
    public float distTravelperF;
    Vector3 prevPosition;
    bool turnTaken;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i <4; i++)
        {
            Points.Add(GameObject.Instantiate(point));
        }
        Points[0].transform.position = point0Pos;
        Points[1].transform.position = point1Pos;
        Points[2].transform.position = point2Pos;
        Points[3].transform.position = point3Pos;

        transform.position = Points[0].transform.position;

    }

    // Update is called once per frame
    void Update()
    { 
        if (distTravel + distTravelperF >= 1)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            turnTaken = true;
        }
        if (distTravel > 100)
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
                transform.position = Vector3.MoveTowards(transform.position, point1Pos, moveSpeed * Time.deltaTime);
                if (transform.position == point1Pos)
                {
                    point1 = true;
                }
            }
            if (point1 && !point2)
            {
                transform.position = Vector3.MoveTowards(transform.position, point2Pos, moveSpeed * Time.deltaTime);
                if (transform.position == point2Pos)
                {
                    point2 = true;
                }
            }
            if (point2 && !point3)
            {
                transform.position = Vector3.MoveTowards(transform.position, point3Pos, moveSpeed * Time.deltaTime);
                if (transform.position == point3Pos)
                {
                    point3 = true;
                }
            }
            if (point3 && !point0)
            {
                transform.position = Vector3.MoveTowards(transform.position, point0Pos, moveSpeed * Time.deltaTime);
                if (transform.position == point0Pos)
                {
                    point0 = true;
                }
            }


            if (point0 && point1 && point2 && point3)
            {
                point0 = false;
                point1 = false;
                point2 = false;
                point3 = false;

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
