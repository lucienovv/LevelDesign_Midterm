using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 nDirection;
    public bool playerInVision = false;
    
    public float radius;
    public bool check;
    public bool checkWall;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        check = Physics.CheckSphere(transform.position, radius, LayerMask.GetMask("Player"));
        if (check)
        {
             
            if (!Physics.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Wall")))
            {
                    playerInVision = true;
            }
            else if (Physics.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Wall")))
            {
                    playerInVision = false;
            }
        }
        if (!check)
        {
            playerInVision = false;
        }
        

            //check = Physics.CheckSphere(transform.position, radius, LayerMask.GetMask("Player"));
            //if (check)
            //{
            //    playerInVision = true;
            //   
            //}
            //else if (!check)
            //{ 
            //    playerInVision = false;
            //}


    }

    //private IEnumerator TimerCheck()
    //{
    //    WaitForSeconds wait = new WaitForSeconds(.2f);
    //    while (true)
    //    {
    //        yield return wait;

    //    }
    //}


}
