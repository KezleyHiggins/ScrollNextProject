using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemiesToSpawn;

    public void SpawnTheseEnemies()
    {
        foreach (Enemy enemy in enemiesToSpawn)
        {
            enemy.gameObject.SetActive(true);
            enemy.StartRunning();
        }
    }
}
