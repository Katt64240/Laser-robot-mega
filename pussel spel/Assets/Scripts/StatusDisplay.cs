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
        text.text = "Time : " + GameManager.timer.ToString("#.00");
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            text.text = "Time : " + GameManager.timer.ToString("#.00");
        }
    }
}
