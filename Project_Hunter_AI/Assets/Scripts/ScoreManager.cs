using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameObject huntCountHolder;
    public HuntManager huntManager;

    public int huntCount;

    private float startPosX;

    private void Awake()
    {
        huntCount = 0;
        startPosX = -0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < (startPosX + huntManager.huntToSpawn); i++)
        {
            Instantiate(huntCountHolder, new Vector3(startPosX + (i * 3), 32, -4), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
