using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt : MonoBehaviour
{
    private Movement movement;

    public bool isAlive;
    public bool isFleeing;

    public int direction;

    private int initialRotation;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();
        isAlive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = -1;

        int tempRotZ = Random.Range(0, 4) * 90;

        transform.rotation = Quaternion.Euler(0, 0, tempRotZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFleeing)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
        }
        else
        {
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }

        if(direction > -1)
        {
            Quaternion tempRotation;

            switch (direction)
            {
                case 0: //up
                    tempRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1: //down
                    tempRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 2: //left
                    tempRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 3: //right
                    tempRotation = Quaternion.Euler(0, 0, -90);
                    break;
                default:
                    tempRotation = Quaternion.Euler(0, 0, 0);
                    break;
            }

            transform.rotation = tempRotation;
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
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
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
