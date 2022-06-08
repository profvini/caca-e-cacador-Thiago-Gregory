using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt : MonoBehaviour
{
    private Movement movement;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomMovement()
    {
        int randomIndex = Random.Range(0, 4);

        movement.moveEntityRandomly(transform.position);
    }

    /*
    public void moveHunter(Vector3 direction)
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + direction;

        transform.position = targetPosition;
    }
    */
}
