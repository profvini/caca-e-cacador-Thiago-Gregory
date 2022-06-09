using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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

    public Vector3 moveHunter(Vector3 originalPosition)
    {
        Vector3 tempTargetPosition;
        Vector3 tempDirection;
        Quaternion tempRotation;
        float minDistance;
        int moveToCenter;
        int directionIndex;

        do
        {
            directionIndex = Random.Range(0, 4);
            minDistance = 9999;
            tempDirection = directions[directionIndex];
            moveToCenter = Random.Range(1, 6);

            if (moveToCenter == 1)
            {
                for(int i = 0; i < 4; i++)
                {
                    if (Vector3.Distance(hunter.transform.position + directions[i], centerPoint) < minDistance)
                    {
                        minDistance = Vector3.Distance(hunter.transform.position + directions[i], centerPoint);
                        tempDirection = directions[i];
                        directionIndex = i;
                    }
                }
            }

            foreach (GameObject hunt in hunts)
            {
                if (hunt.GetComponent<Hunt>().isFleeing && hunt.GetComponent<Hunt>().isAlive)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            minDistance = Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position);
                            tempDirection = directions[i];
                            directionIndex = i;
                        }
                        else
                        {
                            Vector3 tempv3 = hunter.transform.position + directions[i] - hunt.transform.position;

                            if (Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position) < minDistance)
                            {
                                minDistance = Vector3.Distance(hunter.transform.position + directions[i], hunt.transform.position);
                                tempDirection = directions[i];
                                directionIndex = i;
                            }
                        }
                    }
                }
            }

            tempTargetPosition = originalPosition + tempDirection;
        }
        while (tempTargetPosition.x < 0 || tempTargetPosition.x > 29 || tempTargetPosition.y < 0 || tempTargetPosition.y > 29);

        switch (directionIndex)
        {
            case 0: //up
                tempRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1: //down
                tempRotation = Quaternion.Euler(0, 0, 180);
                break;
            case 2: //left
                tempRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3: //right
                tempRotation = Quaternion.Euler(0, 0, -90);
                break;
            default:
                tempRotation = Quaternion.Euler(0, 0, 0);
                break;
        }

        hunter.transform.rotation = tempRotation;

        return tempTargetPosition;
    }

    public Vector3 moveHunt(int huntIndex, Vector3 originalPosition, bool isFleeing)
    {
        Vector3 targetPosition;
        Vector3 tempDirection;
        Quaternion tempRotation;

        bool targetObstructed;
        int directionIndex;

        do
        {
            float maxDistance = -10;
            float distanceFromHunter;
            float tempDistance;

            targetObstructed = false;

            directionIndex = Random.Range(0, 4);

            tempDirection = directions[directionIndex];

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

            if (isFleeing || randomFleeNum == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    targetPosition = originalPosition + directions[i];
                    tempDistance = Vector3.Distance(originalPosition + directions[i], hunter.transform.position);

                    if (tempDistance >= maxDistance && (targetPosition.x >= 0 && targetPosition.x <= 29 && targetPosition.y >= 0 && targetPosition.y <= 29))
                    {
                        maxDistance = tempDistance;
                        tempDirection = directions[i];
                        directionIndex = i;
                    }
                }
            }

            targetPosition = originalPosition + tempDirection;

            switch (directionIndex)
            {
                case 0: //up
                    tempRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1: //down
                    tempRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 2: //left
                    tempRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 3: //right
                    tempRotation = Quaternion.Euler(0, 0, -90);
                    break;
                default:
                    tempRotation = Quaternion.Euler(0, 0, 0);
                    break;
            }

            hunts[huntIndex].gameObject.transform.rotation = tempRotation;
        }
        while (targetObstructed || targetPosition.x < 0 || targetPosition.x > 29 || targetPosition.y < 0 || targetPosition.y > 29);

        return targetPosition;
    }
}
