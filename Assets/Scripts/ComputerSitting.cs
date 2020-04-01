using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSitting : MonoBehaviour
{
    [SerializeField] GameObject computer;
    [SerializeField] Transform animationStart;
    private Animator animator;
    private bool isSitting;

    // Start is called before the first frame update
    void Start()
    {
        animator = computer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InteractSeat();
    }

    void InteractSeat()
    {
    }
}
