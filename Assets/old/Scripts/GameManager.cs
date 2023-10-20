using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float timer = 0;
    public bool turnActive = false;
    bool actionButton;

        public static GameManager Instance { get; set; }
        void Awake()
        {
        actionButton = Input.GetKeyDown(KeyCode.R);

        if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

    private void Update()
    {
        //Time
        timer += Time.deltaTime;
    }

    
    
}
