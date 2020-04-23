using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    public Text text;
    public bool update = true;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            SetText();
        }
    }

    void SetText()
    {
        //Displays the time and number of colectables
        if (GameManager.colectables == 0)
        {
            text.text = "Time : " + GameManager.timer.ToString("#.00");
        }
        else
        {
            text.text = "Time : " + GameManager.timer.ToString("#.00") + " Ärtsoppa : " + GameManager.colectables + " / ???";
        }
    }
}
