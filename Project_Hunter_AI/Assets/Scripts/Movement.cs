using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector3 centerPoint;

    private List<Vector3> directions;

    public Hunter hunter;
    public GameObject[] hunts;

    private void Awake()
    {
        directions = new List<Vector3> { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

        centerPoint = new Vector3(14.5f, 14.5f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        hunts = GameObject.FindGameObjectsWithTag("Hunt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 moveEntityRandomly(string entityType, Vector3 originalPosition)
    {
        //Debug.Log("moveEntRand()");

        Vector3 tempTargetPosition;
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, 4);

            tempTargetPosition = moveEntity(entityType, originalPosition, directions[randomIndex]);
        }
        while (tempTargetPosition.x < 0 || tempTargetPosition.x > 29 || tempTargetPosition.y < 0 || tempTargetPosition.y > 29);

        return tempTargetPosition;
    }

    public Vector3 moveHunter(Vector3 originalPosition)
    {
        Vector3 tempTargetPosition;

        do
        {
            float minDistance = 9999;

            Vector3 tempDirection = directions[Random.Range(0, 4)];

            int moveToCenter = Random.Range(1, 6);

            if (moveToCenter == 1)
            {
                for(int i = 0; i < 4; i++)
                {
                    if (Vector3.Distance(hunter.transform.position + directions[i], centerPoint) < minDistance)
                    {
                        minDistance = Vector3.Distance(hunter.transform.position + directions[i], centerPoint);
                        tempDirection = directions[i];
                    }
                }
            }

            foreach (GameObject hunt in hunts)
            {
                if (hunt.GetComponent<Hunt>().isFleeing && hunt.GetComponent<Hunt>().isAlive)
                {
                    //Debug.Log("Following Hunt!!");

                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            //minDistance = hunter.transform.position + directions[i] - hunt.transform.position;
                            minDistance = Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position);
                            tempDirection = directions[i];
                        }
                        else
                        {
                            Vector3 tempv3 = hunter.transform.position + directions[i] - hunt.transform.position;

                            //Debug.Log("tempv3 = " + tempv3);

                            if (Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position) < minDistance)
                            {
                                minDistance = Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position);
                                tempDirection = directions[i];
                            }
                        }
                    }
                }
            }

            tempTargetPosition = originalPosition + tempDirection;

            /*
            if (tempTargetPosition.x < 0 || tempTargetPosition.x > 29 || tempTargetPosition.y < 0 || tempTargetPosition.y > 29)
            {
                //Debug.Log("Out of Bounds!!");
            }
            */
        }
        while (tempTargetPosition.x < 0 || tempTargetPosition.x > 29 || tempTargetPosition.y < 0 || tempTargetPosition.y > 29);

        return tempTargetPosition;
    }

    public Vector3 moveHunt(Vector3 originalPosition, bool isFleeing)
    {
        Vector3 targetPosition;
        Vector3 tempDirection;

        bool targetObstructed;

        do
        {
            float maxDistance = -10;
            float distanceFromHunter;
            float tempDistance;

            targetObstructed = false;

            tempDirection = directions[Random.Range(0, 4)];

            //if (tempTargetPosition.x > 0 || tempTargetPosition.x < 29 || tempTargetPosition.y > 0 || tempTargetPosition.y < 29)

            distanceFromHunter = Vector3.Distance(originalPosition, hunter.transform.position);
            int maxRandomRange;

            if (distanceFromHunter < 5f)
            {
                maxRandomRange = 1;
            }
            else if (distanceFromHunter < 10f)
            {
                maxRandomRange = 10;
            }
            else if (distanceFromHunter < 15f)
            {
                maxRandomRange = 40;
            }
            else
            {
                maxRandomRange = 100;
            }

            int randomFleeNum = Random.Range(1, maxRandomRange + 1);

            //Debug.Log("Flee Num = " + randomFleeNum);

            if (isFleeing || randomFleeNum == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    targetPosition = originalPosition + directions[i];
                    tempDistance = Vector3.Distance(originalPosition + directions[i], hunter.transform.position);

                    //Debug.Log("DISTANCE = " + tempDistance);

                    //Debug.Log(tempDistance + " >= " + maxDistance + " ?");

                    if (tempDistance >= maxDistance && (targetPosition.x >= 0 && targetPosition.x <= 29 && targetPosition.y >= 0 && targetPosition.y <= 29))
                    {
                        maxDistance = tempDistance;
                        tempDirection = directions[i];
                    }
                }
            }

            targetPosition = originalPosition + tempDirection;

            //Prevent hunts overlapping
            /*
            foreach (GameObject hunt in hunts)
            {
                if (targetPosition.Equals(hunt.transform.position))
                {
                    targetObstructed = true;
                }
            }
            */
        }
        while (targetObstructed || targetPosition.x < 0 || targetPosition.x > 29 || targetPosition.y < 0 || targetPosition.y > 29);

        return targetPosition;
    }

    public Vector3 moveEntity(string entityType, Vector3 originalPosition, Vector3 direction)
    {
        if (entityType.Equals("Hunter"))
        {
            /*
            for(int i = 0; i < 4; i++)
            {
                Vector3 minDistance;

                if (i == 0)
                {
                    minDistance = directions[i];
                }

                foreach(GameObject hunt in hunts)
                {
                    if (hunt.GetComponent<Hunt>().isBeingHunted)
                    {

                    }
                }
            }
            */

            float minDistance = 9999;
            Vector3 tempDirection;

            foreach (GameObject hunt in hunts)
            {
                if (hunt.GetComponent<Hunt>().isFleeing)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            //minDistance = hunter.transform.position + directions[i] - hunt.transform.position;
                            minDistance = Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position);
                            tempDirection = directions[i];
                        }
                        else
                        {
                            Vector3 tempv3 = hunter.transform.position + directions[i] - hunt.transform.position;

                            if (Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position) < minDistance)
                            {
                                tempDirection = directions[i];
                            }
                        }
                    }
                }
            }


        }
        else
        {

        }

        targetPosition = originalPosition + direction;

        return targetPosition;


    }
}
