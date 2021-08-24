using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndZoneCollider : MonoBehaviour
{
    UnityEvent onPlayerEnteringEndZone = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        else onPlayerEnteringEndZone.Invoke();
    }

    public UnityEvent OnPlayerEnteringEndZone => onPlayerEnteringEndZone;
}
