using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFallingTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) enemy.IsFalling();
    }
}
