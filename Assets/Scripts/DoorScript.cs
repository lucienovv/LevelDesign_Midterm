using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int doorID;
    public GameObject leftSide;
    public Vector3 leftSideOpen;
    public Vector3 rightSideOpen;
    public Vector3 origPosLeft;
    public Vector3 origPosRight;
    

    public GameObject rightSide;
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        origPosLeft = leftSide.transform.position;
        origPosRight = rightSide.transform.position;
        leftSideOpen = origPosLeft - leftSide.transform.forward;
        rightSideOpen = origPosRight + rightSide.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        leftSide.GetComponent<Renderer>().material.color = KeyManager.main.KeyColors[doorID];
        rightSide.GetComponent<Renderer>().material.color = KeyManager.main.KeyColors[doorID];

        if (KeyManager.main.Doors[doorID])
        {
            leftSide.transform.position = Vector3.MoveTowards(leftSide.transform.position, leftSideOpen, moveSpeed * Time.deltaTime);
            rightSide.transform.position = Vector3.MoveTowards(rightSide.transform.position, rightSideOpen, moveSpeed * Time.deltaTime);

        }
        if (!KeyManager.main.Doors[doorID])
        {
            leftSide.transform.position = Vector3.MoveTowards(leftSide.transform.position, origPosLeft, moveSpeed * Time.deltaTime);
            rightSide.transform.position = Vector3.MoveTowards(rightSide.transform.position, origPosRight, moveSpeed * Time.deltaTime);

        }


    }
}
