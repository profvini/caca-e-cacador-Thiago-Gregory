using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    public Hunter hunter;
    public HuntManager huntManager;
    public ScoreManager scoreManager;
    public LoopButton loopSprite;

    public GameObject runtimeUI;
    public GameObject finishedUI;
    public GameObject textMoves;
    public GameObject textFinished;
    public GameObject huntDead;
    public GameObject arrowSprite;

    private Movement movement;
    private GameObject[] hunts;

    public bool executing;
    public float stepDelay;
    public int moves;

    private bool finished;
    private int score;

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
    }

    // Update is called once per frame
    void Update()
    {
        score = scoreManager.huntCount;

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Scene");

        if (score >= huntManager.huntToSpawn)
            finished = true;

        if (finished)
        {
            executing = false;

            textFinished.GetComponent<TMPro.TextMeshPro>().text = "Simulation finished\n\n\nTotal moves:\n" + moves;
            runtimeUI.SetActive(false);
            finishedUI.SetActive(true);

            movement.GetComponent<Movement>().enabled = false;
            hunter.gameObject.SetActive(false);
        }

        if (executing && !finished)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                stepDelay = 0.2f;
                restartCoroutine();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                stepDelay = 0.1f;
                restartCoroutine();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                stepDelay = 0.05f;
                restartCoroutine();
            }
        }

        textMoves.GetComponent<TMPro.TextMeshPro>().text = "Moves = " + moves;
    }

    void restartCoroutine()
    {
        manageCoroutine();
        manageCoroutine();
    }

    public void manageCoroutine()
    {
        if (!finished)
        {
            executing = !executing;

            if (executing)
                StartCoroutine(simulation());
            else
                StopAllCoroutines();
        }
    }

    public void executeSimulation()
    {
        checkHuntIsInRange();
        detectCollision();
        callMoveEntity();

        moves++;
    }

    void checkHuntIsInRange()
    {
        int hunterPosX = (int)hunter.transform.position.x;
        int hunterPosY = (int)hunter.transform.position.y;

        foreach(GameObject hunt in hunts)
        {
            hunt.GetComponent<Hunt>().isFleeing = false;

            for (int x = hunterPosX - 5; x <= hunterPosX + 5; x++)
            {
                for (int y = hunterPosY - 5; y <= hunterPosY + 5; y++)
                {
                    Vector3 tempVector3 = new Vector3(x, y, 0);

                    if (hunt.transform.position == tempVector3)
                        hunt.GetComponent<Hunt>().isFleeing = true;
                }
            }
        }
    }

    void callMoveEntity()
    {
        int huntIndex = 0;
        foreach (GameObject hunt in hunts)
        {
            if(hunt.GetComponent<Hunt>().isAlive)
                hunt.transform.position = movement.moveHunt(huntIndex, hunt.transform.position, hunt.GetComponent<Hunt>().isFleeing);

            huntIndex++;
        }

        hunter.transform.position = movement.moveHunter(hunter.transform.position);

    }

    void detectCollision()
    {
        int hunterPosX = Mathf.RoundToInt(hunter.transform.position.x);
        int hunterPosY = Mathf.RoundToInt(hunter.transform.position.y);

        for (int x = hunterPosX - 1; x <= hunterPosX + 1; x++)
        {
            for (int y = hunterPosY - 1; y <= hunterPosY + 1; y++)
            {
                foreach (GameObject hunt in hunts)
                {
                    if (hunt.transform.position == new Vector3(x, y, 0))
                    {
                        float tempPosX = -0.5f + (scoreManager.huntCount * 3.3333f);

                        hunt.GetComponent<Hunt>().isAlive = false;
                        hunt.transform.position = new Vector3(-15, 0, 0);

                        Destroy(scoreManager.huntHolderTemp[scoreManager.huntCount]);

                        Instantiate(huntDead, new Vector3(tempPosX, 32, -5), Quaternion.identity);

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
