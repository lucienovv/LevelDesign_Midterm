using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public List<Boolean> Keys = new List<Boolean>(10);
    public List<Color> KeyColors = new List<Color>(10);
    public List<Boolean> Doors = new List<Boolean>(10);
    public GameObject player;
    int KeyID;
    public static KeyManager main { get; set; }
    void Awake()
    {
        transform.parent = null;
        if (main == null)
        {
            main = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (GameManagerScript.main.actionButton && player.GetComponent<PlayerScript>().hitKey)
        {
            FlipSwitch();
        }
    }

    private void FlipSwitch()
    {

        KeyID = player.GetComponent<PlayerScript>().KeyID;
        Keys[KeyID] = !Keys[KeyID];
        Doors[KeyID] = !Doors[KeyID];


    }

}
