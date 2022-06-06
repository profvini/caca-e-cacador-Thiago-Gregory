using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpawner : MonoBehaviour
{
    [SerializeField] GameObject hunter;


    // Start is called before the first frame update
    void Start()
    {
        GameObject newHunter = Instantiate(hunter);

        newHunter.transform.localPosition = new Vector3(Random.Range(0,30), 0, Random.Range(0, 30));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 newv3 = new Vector3(Random.Range(0, 30), 1, Random.Range(0, 30));
            Debug.Log(newv3);
        }
    }
}
