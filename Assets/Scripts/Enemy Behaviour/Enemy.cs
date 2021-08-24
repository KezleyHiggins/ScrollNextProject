using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Variables

    // Inspector exposed variables
    [Header("Enemy Behaviour:")]
    [Tooltip("When the enemy is not behind the player, this is the speed they will run at.")]
    [SerializeField] protected float forwardRunSpeed;
    [Tooltip("The distance that the enemy needs to be from the enemy before jumping on them and ending the run.")]
    [SerializeField] protected float jumpOnPlayerDistance;
    [Tooltip("The starting distance that the enemy will stay behind the player.")]
    [SerializeField] protected float startingBufferFromThePlayer;
    [Tooltip("The amount that the buffer will decrease by every second.")]
    [SerializeField] protected float bufferDecreaseRate;

    protected Rigidbody rigidBody;
    protected PlayerController player;
    protected EnemyPathfinding pathfinding;
    protected AnimationManager animationManager;
    protected bool shouldRun = false;
    protected bool hasJumped = false;
    protected float bufferBetweenEnemyAndPlayer;

    #endregion

    #region Methods

    void Awake()
    {
        InitializeThisEnemy();
    }

    void InitializeThisEnemy()
    {
        rigidBody = GetComponent<Rigidbody>();
        pathfinding = GetComponent<EnemyPathfinding>();
        player = FindObjectOfType<PlayerController>();
        animationManager = GetComponentInChildren<AnimationManager>();
        FindObjectOfType<GameManager>().EnableEnemyMovement.AddListener(StartRunning);
        bufferBetweenEnemyAndPlayer = startingBufferFromThePlayer;
    }

    public virtual void DoEnemyBehaviour()
    {
        Debug.Log("Virtual method being called that isnt intended to ever happen.");
    }

    public void StartRunning()
    {
        shouldRun = true;
        animationManager.Run();
        InvokeRepeating("DecreaseBufferDistance", 0, 1f);
    }

    protected void RunTowardsTheEndZone()
    {
        if (IsBehindThePlayer() == true)
        {
            float zCoordOfPlayer = player.transform.position.z;
            float distanceBetweenThisEnemyAndThePlayer = zCoordOfPlayer - transform.position.z;
            distanceBetweenThisEnemyAndThePlayer -= bufferBetweenEnemyAndPlayer;
            Vector3 forwardVector = new Vector3(0, 0, distanceBetweenThisEnemyAndThePlayer);

            rigidBody.MovePosition(transform.position + (forwardVector * forwardRunSpeed));
        }
        else rigidBody.MovePosition(transform.position + (Vector3.forward * forwardRunSpeed));
    }

    protected void JumpOnThePlayer()
    {
        hasJumped = true;
        player.GetJumpedOnByAnEnemy();
        animationManager.Jump();
        if (pathfinding != null) pathfinding.TurnOffNavMeshAgent();
        GetComponent<Collider>().enabled = false;
    }

    protected bool CanJumpOnThePlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= jumpOnPlayerDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IsFalling()
    {
        animationManager.Ragdoll();
        enabled = false;
    }

    bool IsBehindThePlayer()
    {
        return transform.position.z < player.transform.position.z;
    }

    void DecreaseBufferDistance()
    {
        bufferBetweenEnemyAndPlayer -= bufferDecreaseRate;
    }

    #endregion

}
