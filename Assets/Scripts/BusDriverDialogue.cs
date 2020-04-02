using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusDriverDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    private PickupWallet wallet;
    private GameObject player;
    private Canvas dialogue1;
    private Canvas dialogue2;
    private Transform playerTransform;
    private PlayerLook lookingScript;
    private PlayerWalking walkingScript;
    private BusDoors busDoorScript;
    private Quaternion initialPlayer;
    private bool hasInteracted = false;
    private bool isInRange = false;
    [SerializeField] AudioClip nice;
    [SerializeField] AudioClip mean;
    private AudioSource audioSource;
  //  private bool isPlaying = false;

    void Start()
    {
        player = GameObject.Find("MainPlayer");
        playerTransform = player.GetComponent<Transform>();
        dialogue1 = GameObject.Find("BusDriverDialogue1").GetComponent<Canvas>();
        dialogue1.enabled = false;
        dialogue2 = GameObject.Find("BusDriverDialogue2").GetComponent<Canvas>();
        dialogue2.enabled = false;
        wallet = player.GetComponent<PickupWallet>();
        lookingScript = GameObject.Find("Camera").GetComponent<PlayerLook>();
        walkingScript = player.GetComponent<PlayerWalking>();
        busDoorScript = GameObject.Find("FrontDoors").GetComponent<BusDoors>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider collide)
    {
        if (collide.tag == "Player")
        {
            isInRange = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKey(KeyCode.E))
        {
            dialogue1.enabled = false;
            dialogue2.enabled = false;
            playerTransform.SetPositionAndRotation(playerTransform.position, initialPlayer);
            walkingScript.enabled = true;
            lookingScript.enabled = true;
            if (!wallet.isPickedup)
            {
                busDoorScript.rejected = true;
            }
        }
        else if (isInRange && !hasInteracted)
        {
            walkingScript.enabled = false;
            lookingScript.enabled = false;
            initialPlayer = player.GetComponent<Transform>().rotation;
            playerTransform.LookAt(gameObject.transform);
           // transform.LookAt(playerTransform);
            if (wallet.isPickedup)
            {
                audioSource.PlayOneShot(nice);
                hasInteracted = true;
                dialogue2.enabled = true;
            }
            else
            {
                dialogue1.enabled = true; 
                hasInteracted = true;
                audioSource.PlayOneShot(mean);
            }
        }
    }

    public void SetHasInteracted()
    {
        hasInteracted = false;
    }
}
