using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int treasureCollected;

    void Start()
    {
        health = 3;
        treasureCollected = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            health -= 1;
        }

        if (collision.gameObject.CompareTag("treasure"))
        {
            treasureCollected += 1;
        }
    }
}
