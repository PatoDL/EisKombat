using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPF;

    Vector3[] spawnPoints;
    int spawnPointsAmount = 4;

    void Start()
    {
        spawnPoints = new Vector3[spawnPointsAmount];

        spawnPoints[0] = new Vector3(-20, 25, -20);
        spawnPoints[1] = new Vector3(-20, 25, 70);
        spawnPoints[2] = new Vector3(70, 25, -20);
        spawnPoints[3] = new Vector3(70, 25, 70);

        for(int i=0;i<spawnPointsAmount;i++)
        {
            GameObject g = Instantiate(enemyPF);
            g.transform.position = spawnPoints[i];
            g.transform.eulerAngles = new Vector3(0f, Random.Range(-360f, 360f),0f);
        }
    }
}
