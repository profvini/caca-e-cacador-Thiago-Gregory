using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntSpawner : MonoBehaviour
{
    [SerializeField] GameObject hunt;
    [SerializeField] int totalHunt;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < totalHunt; i++)
        {
            GameObject newHunt = Instantiate(hunt);
            newHunt.transform.position = new Vector3(Random.Range(0, 30), 0, Random.Range(0, 30));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
