using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : MonoBehaviour
{
    public PlayerLook look;

    [SerializeField] Transform animationStart;
    private GameObject player;
    private Animator animator;
    private bool isInRange;
    private bool isSitting;


    // Start is called before the first frame update
    void Start()
    {
        look = FindObjectOfType(typeof(PlayerLook)) as PlayerLook;

        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
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

        if (isSitting)
        {
            look.SittingLookAround();
        }

    }

    void InteractSeat()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = animationStart.position;
                player.transform.rotation = animationStart.rotation;

                animator.SetBool("sit", true);
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.Find("Camera").GetComponent<PlayerLook>().enabled = false;
                isSitting = true;
            }
        }

        if (isSitting && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("sit", false);
            player.GetComponent<CharacterController>().enabled = true;
            player.transform.Find("Camera").GetComponent<PlayerLook>().enabled = true;
            isSitting = false;
        }
    }
}
