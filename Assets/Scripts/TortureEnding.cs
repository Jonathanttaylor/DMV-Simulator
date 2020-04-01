using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TortureEnding : MonoBehaviour
{
    [SerializeField] float speed = 0.25f;
    [SerializeField] AudioClip main;
    [SerializeField] AudioClip badComedians;
    private AudioSource audioSource;
    private bool closeDoor = false;
    [SerializeField] GameObject target;
    [SerializeField] GameObject door;
    private DoorCloseEnding doorScript;
    [SerializeField] float delayBeforeMove = 1.0f;
    [SerializeField] float delayBeforeDoor = 1.0f;
    [SerializeField] float delayBeforeAmy = 1.5f;
    [SerializeField] Image Black;
    [SerializeField] bool test = false;
    [SerializeField] GameObject Amy;
    private PlayAmy schumer;
    // Start is called before the first frame update
    void Start()
    {
        Black.canvasRenderer.SetAlpha(0.0f);
        audioSource = GetComponent<AudioSource>();
        doorScript = door.GetComponent<DoorCloseEnding>();
        schumer = Amy.GetComponent<PlayAmy>();
      //  audioSource.PlayOneShot(main);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("moveBack", delayBeforeMove);
        Invoke("enableDoor", delayBeforeDoor);
        Invoke("playAmy", delayBeforeAmy);
        if (transform.position == target.transform.position)
        {
            Black.canvasRenderer.SetAlpha(1.0f);
        }
    }

    private void moveBack()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void enableDoor()
    {
        test = true;
        doorScript.closeDoor = true;
    }

    private void playAmy()
    {
        schumer.start = true;
    }
}
