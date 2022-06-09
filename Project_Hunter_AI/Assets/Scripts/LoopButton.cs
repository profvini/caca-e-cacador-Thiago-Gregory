using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopButton : MonoBehaviour
{
    public EventSystem eventSystem;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        else
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.66f);
    }

    private void OnMouseDown()
    {
        eventSystem.executing = !eventSystem.executing;
        active = !active;
    }
}
