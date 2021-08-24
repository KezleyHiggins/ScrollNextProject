using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCube : MonoBehaviour
{
    #region Variables

    // Falling Info
    [Header("Floor Cube Info: ")]
    [SerializeField] float fallingSpeed;
    [SerializeField] float colourChangeSpeed;
    [SerializeField] Color defaultColour, fallingColour;
    float currentColourChangeSpeed = 0;
    bool shouldFall = false;
    bool isFalling = false;

    [Space] [Space] [Space]

    // Object & Component References
    PlayerController player;
    MeshRenderer meshRender;
    Rigidbody rigidBody;
    [Header("Nav Mesh Interactions: ")]
    [SerializeField] float navMeshObstacleSpawnDelay;
    [SerializeField] GameObject navMeshObstaclePrefab;
    Vector3 navMeshObstacleLocation;

    #endregion

    #region Methods

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        meshRender = GetComponent<MeshRenderer>();
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.isKinematic = true;
        meshRender.material.color = defaultColour;
        navMeshObstacleLocation = transform.position + Vector3.up;
    }

    void Update()
    {
        if (shouldFall == true && isFalling == false)
        {
            StartFalling();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            shouldFall = true;
        }
    }

    void StartFalling()
    {
        isFalling = true;
        rigidBody.isKinematic = false;
        StartCoroutine(FallingRoutine());
        StartCoroutine(SpawnNavMeshObstacleAfterDelay());
    }

    IEnumerator FallingRoutine()
    {
        yield return new WaitForSeconds(0.01f);

        DecreaseDragOnObject();
        LerpColourTowardsFallingColour();

        StartCoroutine(FallingRoutine());
    }

    void DecreaseDragOnObject()
    {
        rigidBody.drag -= fallingSpeed;
    }

    void LerpColourTowardsFallingColour()
    {
        meshRender.material.color = Color.Lerp(defaultColour, fallingColour, currentColourChangeSpeed);
        currentColourChangeSpeed += colourChangeSpeed;
    }

    IEnumerator SpawnNavMeshObstacleAfterDelay()
    {
        yield return new WaitForSeconds(navMeshObstacleSpawnDelay);
        Instantiate(navMeshObstaclePrefab).transform.position = navMeshObstacleLocation;
    }

    #endregion
}
