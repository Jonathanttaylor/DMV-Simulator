using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusSitting : MonoBehaviour
{
    // Creating Seralized Fields and private members
    [SerializeField] Transform animationStart;
    private Animator animator;
    private PlayerLook look;
    private BusMovement move;
    private GameObject player;
    private bool isInRange;
    private bool isSitting;


    // Start is called before the first frame update
    void Start()
    {
        look = FindObjectOfType(typeof(PlayerLook)) as PlayerLook;
        move = FindObjectOfType(typeof(BusMovement)) as BusMovement;

        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
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
                // Moves player infront of chair and starts sitting animation
                player.transform.position = animationStart.position;
                player.transform.rotation = animationStart.rotation;
                animator.SetBool("sit", true);

                // Disables "W" "A" "S" "D" controls. Locks camera at 90 on x and y
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.Find("Camera").GetComponent<PlayerLook>().enabled = false;

                isSitting = true;

                player.transform.parent = move.transform; // Makes player object a child of Bus object
                move.StartDriving(); // Calls BusMovement Script to start driving the bus
            }
        }

        if (move.GetIsBraking() && move.GetCurrentSpeed() > -0.1 && Input.GetKeyDown(KeyCode.W))
        {
            print("Here");
            animator.SetBool("sit", false); // Starts standing animation

            // Resets player controls to normal 
            player.GetComponent<CharacterController>().enabled = true;
            player.transform.Find("Camera").GetComponent<PlayerLook>().enabled = true;

            isSitting = false;

            player.transform.parent = null; // Makes player object no longer child of Bus object
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("MainPlayer")); // Moves player object back inside MainPlayer scene
        }
    }
}
