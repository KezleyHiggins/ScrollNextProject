using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables

    PlayerController player;
    Helicopter helicopter;
    GameManager gameManager;
    bool hasLookedAtTheHelicopter = false;
    [SerializeField] Vector3 offsetFromPlayer, offsetFromHelicopter;
    [SerializeField] ParticleSystem confetti;

    #endregion

    #region Methods

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        helicopter = FindObjectOfType<Helicopter>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        HandleCameraMovement();
    }

    void HandleCameraMovement()
    {
        if (ShouldFollowThePlayer() == true) FollowThePlayer();
        else if (ShouldLookAtHelicopter() == true && hasLookedAtTheHelicopter == false) LookAtTheHelicopter();
    }

    void FollowThePlayer()
    {
        transform.position = player.transform.position + offsetFromPlayer;
    }

    bool ShouldFollowThePlayer()
    {
        return player.PlayerHasLost == false && gameManager.PlayerHasWon == false;
    }

    void LookAtTheHelicopter()
    {
        transform.position = helicopter.transform.position + offsetFromHelicopter;
        hasLookedAtTheHelicopter = true;
        StartConfetti();
    }

    bool ShouldLookAtHelicopter()
    {
        return gameManager.PlayerHasWon == true;
    }

    void StartConfetti()
    {
        confetti.gameObject.SetActive(true);
    }

    #endregion
}
