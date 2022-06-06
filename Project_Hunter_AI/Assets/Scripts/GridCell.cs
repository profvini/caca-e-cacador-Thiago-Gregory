using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool hasHunter;
    public bool hasHunt;

    public bool isColliding;

    public Material defaultColor;
    public Material hasHunterColor;
    public Material hasHuntColor;
    public Material hasBothColor;

    private void Awake()
    {
        hasHunter = false;
        hasHunt = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (isColliding)
        {
            Debug.Log("I'm tired..");
        }

        if(hasHunter && hasHunt)
        {
            GetComponentInChildren<MeshRenderer>().material = hasBothColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GridCell"))
        {
            //this.isColliding = true;
        }
        else if (other.CompareTag("Hunter"))
        {
            this.hasHunter = true;
            GetComponentInChildren<MeshRenderer>().material = hasHunterColor;
        }
        else if (other.CompareTag("Hunt"))
        {
            this.hasHunt = true;
            GetComponentInChildren<MeshRenderer>().material = hasHuntColor;
        }

        if (other.CompareTag("GridCell"))
        {
            if(this.hasHunter && other.GetComponent<GridCell>().getHasHunt())
            {
                isColliding = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hunter"))
        {
            this.hasHunter = false;
            GetComponentInChildren<MeshRenderer>().material = defaultColor;
        }
        else if (other.CompareTag("Hunt"))
        {
            this.hasHunt = false;
            GetComponentInChildren<MeshRenderer>().material = defaultColor;
        }
    }

    public void setHasHunter(bool value)
    {
        this.hasHunter = value;
    }

    public void setHasHunt(bool value)
    {
        this.hasHunt = value;
    }

    public bool getHasHunter()
    {
        return this.hasHunter;
    }

    public bool getHasHunt()
    {
        return this.hasHunt;
    }
}
