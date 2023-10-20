using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyScript : MonoBehaviour
{
    //setup
    public int KeyID;
    public Color KeyColor;
    public GameObject keyswitch;
    public GameObject handle;
    public Transform handleRotation;


    //switches
    public GameObject on;
    public GameObject off;
    public Light onLight;
    public Light offLight;

    //check if on




    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        keyswitch.GetComponent<Renderer>().material.color = KeyColor;
        KeyManager.main.KeyColors[KeyID] = KeyColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        //when on
        if (KeyManager.main.Keys[KeyID])
        {
            //onLight.enabled = true;
            //offLight.enabled = false;

            on.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            off.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");


            handleRotation.transform.rotation = Quaternion.Euler(40, 0, 0);
            

        }
        //when off
        else if (!KeyManager.main.Keys[KeyID])
        {
            //onLight.enabled= false;
            //offLight.enabled = true;

            off.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            on.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

            handleRotation.transform.rotation = Quaternion.Euler(-40, 0, 0);
        }

        handle.transform.rotation = Quaternion.RotateTowards(handle.transform.rotation, handleRotation.transform.rotation, 200f * Time.deltaTime);
    }
}
