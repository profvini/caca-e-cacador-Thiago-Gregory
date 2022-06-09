using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopButton : MonoBehaviour
{
    public EventSystem eventSystem;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        else
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
    }

    public void OnMouseDown()
    {
        eventSystem.manageCoroutine();
        isActive = !isActive;
    }
}
