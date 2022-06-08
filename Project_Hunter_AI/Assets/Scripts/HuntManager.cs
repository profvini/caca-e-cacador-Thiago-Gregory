using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntManager : MonoBehaviour
{
    public GameObject hunt;
    public GameObject hunter;

    public int huntToSpawn;

    private int posX;
    private int posY;

    public List<GameObject> hunts;

    // Start is called before the first frame update
    void Start()
    {
        if (huntToSpawn < 5)
            huntToSpawn = 5;

        else if (huntToSpawn > 10)
            huntToSpawn = 10;

        positionateHunts();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (GameObject hunt in hunts)
                Destroy(hunt);

            hunts.Clear();

            positionateHunts();
        }
    }

    void positionateHunts()
    {
        System.Random random = new System.Random();

        bool obstructedTile;

        for (int i = 0; i < huntToSpawn; i++)
        {
            do
            {
                posX = Random.Range(0, 30);
                posY = Random.Range(0, 30);

                Vector3 tempVector3 = new Vector3(posX, posY, 0);

                obstructedTile = false;

                if (hunts.Count > 0)
                {
                    foreach (GameObject hunt in hunts)
                    {
                        if (hunt.transform.position == tempVector3)
                            obstructedTile = true;
                    }
                }

                if (tempVector3 == hunter.transform.position)
                    obstructedTile = true;

                if (!obstructedTile)
                    hunts.Add(Instantiate(hunt, tempVector3, Quaternion.identity));
            }
            while (obstructedTile);
        }
    }
}
