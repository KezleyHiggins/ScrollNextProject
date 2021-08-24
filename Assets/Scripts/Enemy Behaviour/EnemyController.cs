using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        enemy.DoEnemyBehaviour();
    }

}
