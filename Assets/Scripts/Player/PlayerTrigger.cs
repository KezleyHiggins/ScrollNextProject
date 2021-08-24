using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onPlayerEnter = new UnityEvent();

    PlayerController player;
    bool hasFired = false;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject && hasFired == false)
        {
            onPlayerEnter.Invoke();
            hasFired = true;
        }
    }
}
