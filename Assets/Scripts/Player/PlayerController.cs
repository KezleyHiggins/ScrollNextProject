using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    #region Variables

    [SerializeField] float speed;
    [SerializeField] float distanceToFallBeforeFailure = -3f;
    Rigidbody rigidBody;
    AnimationManager animationManager;
    float cameraWidth;
    bool playerHasLost = false;
    UnityEvent onPlayerDeath = new UnityEvent();

    #endregion

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animationManager = GetComponentInChildren<AnimationManager>();
        cameraWidth = Camera.main.pixelWidth;
    }

    void Update()
    {
        HandlePlayerMovement();
        UpdatePlayerState();
    }

    #region Tracking Player State

    void UpdatePlayerState()
    {
        if (HasThePlayerFallenOffTheMap() == true)
        {
            SetPlayerHasLost();
        }
    }

    bool HasThePlayerFallenOffTheMap()
    {
        return (transform.position.y < distanceToFallBeforeFailure);
    }

    public void GetJumpedOnByAnEnemy()
    {
        animationManager.Jump();
        SetPlayerHasLost();
    }

    void SetPlayerHasLost()
    {
        playerHasLost = true;
        onPlayerDeath.Invoke();
    }

    public void ResetPlayerState(Vector3 pointToResetTo)
    {
        transform.position = pointToResetTo;
        playerHasLost = false;
    }

    #endregion

    #region Handling Player Movement

    void HandlePlayerMovement()
    {
        bool playerShouldMove = IsThePlayerTouchingTheScreen();

        if (playerShouldMove == true)
        {
            Vector3 movementDirection = GetPlayerMovementDirection();
            MovePlayerInThisDirection(movementDirection);
            animationManager.Run();
        }
        else animationManager.Idle();
    }

    void MovePlayerInThisDirection(Vector3 movementDirection)
    {
        rigidBody.MovePosition(transform.position + (movementDirection * speed));
    }

    Vector3 GetPlayerMovementDirection()
    {
        if (IsThePlayerTouchingTheScreen() == true)
        {
            Vector3 directionModifier = GetDirectionModifier();
            return Vector3.forward + directionModifier;
        }
        else return Vector3.zero;
    }

    bool IsThePlayerTouchingTheScreen()
    {
        return UserIsTouchingTheScreen || UserIsHoldingDownTheMouse;
    }

    Vector3 GetDirectionModifier()
    {
        float fingerPosition = 0;
        float directionModifier = 0;

        if (UserIsTouchingTheScreen) fingerPosition = Input.touches[0].position.x;
        else if (UserIsHoldingDownTheMouse) fingerPosition = Input.mousePosition.x;

        if (fingerPosition < 0) fingerPosition = 0;
        if (fingerPosition > cameraWidth) fingerPosition = cameraWidth;

        float cameraSpaceSplitInSixteenParts = cameraWidth / 16;
        float cameraSpaceBeingChecked = 0;
        int screenSpaceTheFingerIsIn = 0;

        for (int i = 0; i < 16; i++)
        {
            if (fingerPosition >= cameraSpaceBeingChecked && fingerPosition <= (cameraSpaceBeingChecked + cameraSpaceSplitInSixteenParts))
            {
                screenSpaceTheFingerIsIn = i;
                break;
            }
            cameraSpaceBeingChecked += cameraSpaceSplitInSixteenParts;
        }

        if (screenSpaceTheFingerIsIn >= 9)
        {
            int positiveScreenSpace = screenSpaceTheFingerIsIn - 8;
            directionModifier = 0.125f * positiveScreenSpace;
        }
        else
        {
            directionModifier = -0.125f * screenSpaceTheFingerIsIn;
        }

        return new Vector3(directionModifier, 0, 0);
    }

    #endregion

    #region Properties

    public UnityEvent OnPlayerDeath => onPlayerDeath;

    public bool PlayerHasLost => playerHasLost;

    bool UserIsTouchingTheScreen => (Input.touchCount >= 1);

    bool UserIsHoldingDownTheMouse => (Input.GetMouseButton(0));

    public bool IsMoving => (UserIsTouchingTheScreen || UserIsHoldingDownTheMouse) && playerHasLost == false;

    #endregion

}
