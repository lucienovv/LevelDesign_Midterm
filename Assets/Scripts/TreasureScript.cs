using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Collision = false;
    void Start()
    {
        GameManagerScript.main.Centering(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collision = true;
            Destroy(this.gameObject);
        }
    }

   
}
