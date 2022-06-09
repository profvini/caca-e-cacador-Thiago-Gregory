using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    public GameObject tile1;
    public GameObject tile2;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    Instantiate(tile1, new Vector3(i, j, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(tile2, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
