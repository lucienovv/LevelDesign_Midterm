using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public int doorID;
    GameObject[] keys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keys = GameObject.FindGameObjectsWithTag("key");
        foreach (GameObject key in keys) 
        { 
        if (key.GetComponent<Key>().keyID  == doorID && key.GetComponent<Key>().keyCollected)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
