using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private float timeF;
    private int timeI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeF = (Time.timeSinceLevelLoad) / 60;
        timeI = Mathf.FloorToInt(timeF) + 15;

        if (timeI == 60)
        {
            transform.GetComponent<TextMesh>().text = "3:00";
        }
        else
        {
            transform.GetComponent<TextMesh>().text = "2:" + timeI;
        }
    }
}
