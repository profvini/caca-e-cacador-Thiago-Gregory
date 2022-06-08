using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //private Vector3 originalPosition;
    private Vector3 targetPosition;

    private List<Vector3> directions;

    private void Awake()
    {
        directions = new List<Vector3> { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 moveEntityRandomly(Vector3 originalPosition)
    {
        //Debug.Log("moveEntRand()");

        Vector3 tempTargetPosition;
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, 4);

            tempTargetPosition = moveEntity(originalPosition, directions[randomIndex]);
        }
        while (tempTargetPosition.x < 0 || tempTargetPosition.x > 29 || tempTargetPosition.y < 0 || tempTargetPosition.y > 29);

        return tempTargetPosition;
    }

    public Vector3 moveEntity(Vector3 originalPosition, Vector3 direction)
    {
        targetPosition = originalPosition + direction;

        return targetPosition;
    }
}
