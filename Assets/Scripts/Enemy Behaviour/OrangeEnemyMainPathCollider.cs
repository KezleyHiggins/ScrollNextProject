using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEnemyMainPathCollider : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        OrangeEnemy orangeEnemy = other.gameObject.GetComponent<OrangeEnemy>();

        if (orangeEnemy != null)
        {
            orangeEnemy.IsOnMainPath = true;
        }
    }

}
