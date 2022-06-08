using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [SerializeField] Hunter hunter;
    private GameObject[] hunts;

    private Movement movement;

    public bool executing;
    public float stepDelay;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();

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
        if (Input.GetKeyDown(KeyCode.Space))
            executeSimulation();

        else if (Input.GetKeyDown(KeyCode.H))
        {
            executing = !executing;
        }
    }

    void executeSimulation()
    {
        detectCollision();
        callMoveEntity();
    }

    void callMoveEntity()
    {
        foreach (GameObject hunt in hunts)
        {
            hunt.transform.position = movement.moveEntityRandomly(hunt.transform.position);
        }

        hunter.transform.position = movement.moveEntityRandomly(hunter.transform.position);
    }

    void detectCollision()
    {
        int hunterPosX = Mathf.RoundToInt(hunter.transform.position.x);
        int hunterPosY = Mathf.RoundToInt(hunter.transform.position.y);

        Debug.Log("#############################");

        for (int x = hunterPosX - 1; x <= hunterPosX + 1; x++)
        {
            for(int y = hunterPosY - 1; y <= hunterPosY + 1; y++)
            {
                Debug.Log("(" + ((hunterPosX+x)- hunterPosX) + ", " + ((hunterPosY+y)- hunterPosY) + ")");

                foreach (GameObject hunt in hunts)
                {
                    if (hunt.transform.position == new Vector3(x, y, 0))
                    {
                        hunt.SetActive(false);
                    }
                }
            }
        }
    }

    // 8  11 htr
    // 7  12 ht     -1 +1
    //

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
