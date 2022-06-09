using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt : MonoBehaviour
{
    private Movement movement;

    public bool isAlive;
    public bool isFleeing;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();
        isAlive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isFleeing)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
    }

    public void randomMovement()
    {
        int randomIndex = Random.Range(0, 4);

        movement.moveEntityRandomly("hunt", transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Range"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
        }
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
