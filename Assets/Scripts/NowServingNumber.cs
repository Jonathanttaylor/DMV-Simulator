using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowServingNumber : MonoBehaviour
{
    [SerializeField] string letter;
    [SerializeField] int number;
    [SerializeField] float timeUntilNextNumber;

    // Start is called before the first frame update
    void Start()
    {
        letter = "A";

        number = 24;

        timeUntilNextNumber = Random.Range(20f, 60f);
    }

    // Update is called once per frame
    void Update()
    { 
        transform.GetComponent<TextMesh>().text = "" + letter + " " + number;

        UpdateNumber();

    }

    void UpdateNumber()
    {
        timeUntilNextNumber -= 1 * Time.deltaTime;

        if (timeUntilNextNumber < 1)
        {
            number += 1;
            timeUntilNextNumber = Random.Range(20f, 60f);

            if (number > 99)
            {
                number = 1;
                letter = "B";
            }
        }
    }

    public string GetLetter()
    {
        return letter;
    }

    public int GetNumber()
    {
        return number;
    }
}
