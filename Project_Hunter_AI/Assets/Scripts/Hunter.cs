using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public HuntManager huntManager;

    public bool isHunting;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private List<Vector3> directions;
    private Movement movement;

    private int posX;
    private int posY;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();

        directions = new List<Vector3> { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

        positionateHunter();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void positionateHunter()
    {
        bool obstructedTile;

        do
        {
            obstructedTile = false;

            posX = Random.Range(0, 30);
            posY = Random.Range(0, 30);

            Vector3 tempVector3 = new Vector3(posX, posY, 0);

            if (huntManager.hunts.Count > 0)
            {
                foreach (GameObject hunt in huntManager.hunts)
                {
                    if (tempVector3 == hunt.transform.position)
                        obstructedTile = true;
                }
            }
        }
        while (obstructedTile);

        transform.position = new Vector3(posX, posY, 0);
    }

    public void randomMovement()
    {
        int randomIndex = Random.Range(0, 4);

        moveHunter(directions[randomIndex]);
    }

    public void moveHunter(Vector3 direction)
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + direction;

        transform.position = targetPosition;
    }

}
