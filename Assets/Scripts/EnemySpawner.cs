﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawns;
    public GameObject[] enemies;
    public Text timeCounter;

    private int[][] wavesTab;
    private int wavesNumber;
    private int actualWave = 0;
    private float startTime;

    void Start()
    {
        startTime = Time.time;

        wavesTab = new int[10][];
        wavesNumber = wavesTab.Length;

        //ilosci przeciwnikow w kolejnych falach
        wavesTab[0] = new int[] { 0, 4, 0, 0 };
        wavesTab[1] = new int[] { 15, 4, 4, 0 };
        wavesTab[2] = new int[] { 35, 8, 4, 2 };
        wavesTab[3] = new int[] { 60, 4, 10, 2 };
        wavesTab[4] = new int[] { 80, 10, 0, 8 };
        wavesTab[5] = new int[] { 150, 6, 10, 10 };
        wavesTab[6] = new int[] { 170, 0, 20, 0 };
        wavesTab[7] = new int[] { 200, 10, 10, 10 };
        wavesTab[8] = new int[] { 230, 15, 5, 20 };
        wavesTab[9] = new int[] { 270, 20, 20, 20 };
        //sec w ktorej sie zaczyna, ilosc lvl1, ilosc lvl2...
    }

    void Update()
    {
        timeCounter.text = ((int)(startTime + wavesTab[actualWave][0] - Time.time)).ToString();

        if (actualWave < wavesNumber && Time.time > startTime + wavesTab[actualWave][0])
        {
            spawnWave(actualWave);
            actualWave++;
        }
    }

    void spawnWave(int id)
    {
        for (int i = 1; i < wavesTab[i].Length; i++)
        {
            for (int j = 0; j < wavesTab[id][i]; j++)
            {
                GameObject enemy = Instantiate(enemies[i-1], spawns[j % spawns.Length].position, Quaternion.identity);
            }
        }
    }
}
