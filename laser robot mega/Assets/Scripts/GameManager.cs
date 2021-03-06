﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float timer = 0;
    public static int colectables;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        timer += Time.deltaTime;
    }

    public void LoadLevel(int i)
    {
        Application.LoadLevel(i);
    }

    public void StartTimer()
    {
        timer = 0;
    }
}
