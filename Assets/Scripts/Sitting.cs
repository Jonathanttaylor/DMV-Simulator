using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : MonoBehaviour
{
    [SerializeField] Transform chair;
    private GameObject player;
    private Animator animator;
    private bool isInRange;
    private bool sitting;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        animator.SetBool("sit", false);
        sitting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InteractSeat();
    }

    void InteractSeat()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = chair.position;
                player.transform.rotation = chair.rotation;

                animator.SetBool("sit", true);
                player.GetComponent<CharacterController>().enabled = false;
                sitting = true;
            }
        }

        if (sitting && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("sit", false);
            player.GetComponent<CharacterController>().enabled = true;
            sitting = false;
        }
    }

    public bool isSitting()
    {
        return sitting;
    }
}
