using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickANumber : MonoBehaviour
{
    Rect rect;
    Texture texture;
    bool isInRange;
    bool displayNumber;

    // Start is called before the first frame update
    void Start()
    {
        float size = Screen.width * 0.2f;
        rect = new Rect(Screen.width * 0.1f, Screen.height * 0.7f, size, size);
        texture = Resources.Load<Texture2D>("Textures/number");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("InRange");
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Not In Range");
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        TakeNumber();
    }

    void TakeNumber()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            displayNumber = true;
        }
    }

    private void OnGUI()
    {
        if (displayNumber)
        {
            GUI.DrawTexture(rect,texture);
        }
    }
}
