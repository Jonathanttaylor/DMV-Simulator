using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusDoors : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool toggleDoors = false;

    // Start is called before the first frame update
    void Start()
    {
            animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.tag == "Player")
        {
            toggleDoors = true;
        }
    }

    private void OnTriggerExit(Collider collide)
    {
        if (collide.tag == "Player")
        {
            toggleDoors = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        interactDoor();
    }

    private void interactDoor()
    {
        if (toggleDoors)
        {
            if (isOpen)
            {
                animator.SetBool("open", false);

                toggleDoors = false;
                isOpen = false;
            }
            else
            {
                animator.SetBool("open", true);

                toggleDoors = false;
                isOpen = true;
            }
        }
    }
}
