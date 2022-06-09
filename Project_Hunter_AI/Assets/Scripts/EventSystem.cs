using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public Hunter hunter;
    public HuntManager huntManager;
    public ScoreManager scoreManager;
    public GameObject textMoves;

    public GameObject loopSprite;
    public GameObject arrowSprite;

    private GameObject[] hunts;

    private Movement movement;

    public bool executing;
    public float stepDelay;

    public int moves;

    private int score;

    private bool finished;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();

        moves = 0;
        executing = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        hunts = GameObject.FindGameObjectsWithTag("Hunt");

        StartCoroutine(simulation());
    }

    // Update is called once per frame
    void Update()
    {
        score = scoreManager.huntCount;

        if (score >= huntManager.huntToSpawn)
        {
            finished = true;
        }

        if (!finished)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                executeSimulation();

            else if (Input.GetKeyDown(KeyCode.H))
            {
                executing = !executing;
            }
        }

        textMoves.GetComponent<TMPro.TextMeshPro>().text = "Moves = " + moves;
    }

    public void executeSimulation()
    {
        /*
         * checkHuntIsInRange()
         * detectCollision()
         * callMoveEntity()
         * 
         */

        checkHuntIsInRange();
        detectCollision();
        callMoveEntity();

        moves++;
    }

    void checkHuntIsInRange()
    {
        int hunterPosX = (int)hunter.transform.position.x;
        int hunterPosY = (int)hunter.transform.position.y;

        //Debug.Log("############################");

        foreach(GameObject hunt in hunts)
        {
            hunt.GetComponent<Hunt>().isFleeing = false;

            for (int x = hunterPosX - 5; x <= hunterPosX + 5; x++)
            {
                for (int y = hunterPosY - 5; y <= hunterPosY + 5; y++)
                {
                    Vector3 tempVector3 = new Vector3(x, y, 0);

                    //Debug.Log("temp = " + tempVector3 + " hunt = " + hunt.transform.position);

                    if (hunt.transform.position == tempVector3)
                    {
                        //Debug.Log("################# Found Hunt!! " + tempVector3);

                        hunt.GetComponent<Hunt>().isFleeing = true;
                    }
                }
            }
        }
        /*
        for (int x = hunterPosX - 2; x <= hunterPosX + 2; x++)
        {
            for (int y = hunterPosY - 2; y <= hunterPosY + 2; y++)
            {
                Vector3 tempVector3 = new Vector3(x, y, 0);

                foreach (GameObject hunt in hunts)
                {
                    Debug.Log("temp = " + tempVector3 + " hunt = " + hunt.transform.position);

                    hunt.GetComponent<Hunt>().isBeingHunted = false;

                    if (hunt.transform.position == tempVector3)
                    {
                        Debug.Log("################# Found Hunt!! " + tempVector3);

                        hunt.GetComponent<Hunt>().isBeingHunted = true;
                    }
                }
            }
        }
        */
    }

    void callMoveEntity()
    {
        foreach (GameObject hunt in hunts)
        {
            //hunt.transform.position = movement.moveEntityRandomly("hunt", hunt.transform.position);
            if(hunt.GetComponent<Hunt>().isAlive)
                hunt.transform.position = movement.moveHunt(hunt.transform.position, hunt.GetComponent<Hunt>().isFleeing);
        }

        //hunter.transform.position = movement.moveEntityRandomly("hunter", hunter.transform.position);
        hunter.transform.position = movement.moveHunter(hunter.transform.position);

    }

    void detectCollision()
    {
        int hunterPosX = Mathf.RoundToInt(hunter.transform.position.x);
        int hunterPosY = Mathf.RoundToInt(hunter.transform.position.y);

        //Debug.Log("#############################");

        for (int x = hunterPosX - 1; x <= hunterPosX + 1; x++)
        {
            for (int y = hunterPosY - 1; y <= hunterPosY + 1; y++)
            {
                //Debug.Log("(" + ((hunterPosX+x)- hunterPosX) + ", " + ((hunterPosY+y)- hunterPosY) + ")");

                foreach (GameObject hunt in hunts)
                {
                    if (hunt.transform.position == new Vector3(x, y, 0))
                    {
                        float tempPosX = -0.5f + (scoreManager.huntCount * 3);

                        hunt.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                        //hunt.SetActive(false);
                        hunt.GetComponent<Hunt>().isAlive = false;
                        hunt.transform.position = new Vector3(tempPosX, 32, -5);

                        scoreManager.huntCount++;
                    }
                }
            }
        }
    }

    public IEnumerator simulation()
    {
        WaitForSeconds waitTime = new WaitForSeconds(stepDelay);

        while (true)
        {
            if (executing)
                executeSimulation();

            yield return waitTime;
        }
    }
}
