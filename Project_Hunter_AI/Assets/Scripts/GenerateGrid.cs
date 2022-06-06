using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField] GameObject gridCell;

    private GameObject[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = new GameObject[30, 30];

        for(int i = 0; i < 30; i++)
        {
            for(int j = 0; j < 30; j++)
            {
                cells[i,j] = Instantiate(gridCell);
                cells[i,j].transform.position = new Vector3(i, 0, j);
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
