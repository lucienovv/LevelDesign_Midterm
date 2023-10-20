using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public float turnNumber = 1;
    public bool playerTurn = false;
    public bool enemyTurn = false;
    public bool actionButton;

    //manage enemies
   
    public GameObject[] numberOfEnemies;
    public List<Boolean> enemiesDone = new List<Boolean>();
    public int enemiesDoneCount = 0;

    public static GameManagerScript main {  get; set; }
    void Awake()
    {
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
        numberOfEnemies = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < numberOfEnemies.Length; i++)
        {
            numberOfEnemies[i].GetComponent<EnemyTurnDone>().enemyID = i;
        }
        for (int i = 0; i < numberOfEnemies.Length; i++)
        {
            enemiesDone.Add(false);
        }
        ;
        
    }

    private void Update()
    {
        //check all enemies for finished turn

        enemiesDoneCount = 0;
        if (enemyTurn)
        {
            for (int i = 0; i < numberOfEnemies.Length; i++)
            {
                if (numberOfEnemies[i].GetComponent<EnemyTurnDone>().turnDone)
                {
                    enemiesDone[i] = true;
                }
            }

            for (int i = 0; i < enemiesDone.Count; i++)
            {
                if (enemiesDone[i])
                {
                    enemiesDoneCount++;
                }
            }

            if (enemiesDoneCount == numberOfEnemies.Length)
            {
                enemiesDoneCount = 0;
                turnNumber++;
                for (int i = 0; i < numberOfEnemies.Length; i++)
                {
                    numberOfEnemies[i].GetComponent<EnemyTurnDone>().turnDone = false;
                }
            }
        }


        if (turnNumber % 2 != 0)
        {
            playerTurn = true;
            enemyTurn = false;
            
        }
        else if (turnNumber % 2 == 0)
        {
            enemyTurn = true;
            playerTurn = false;
        }
        
        
        actionButton = Input.GetKeyDown(KeyCode.R);
    }

    public void Centering(Transform target)
    {
        target.position = new Vector3(Mathf.Round(target.transform.position.x), Mathf.Round(target.transform.position.y), Mathf.Round(target.transform.position.z));
    }

}
