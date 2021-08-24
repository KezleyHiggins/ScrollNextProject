using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    [SerializeField] float delayBeforeRagdollAfterJump;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = true;

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void Jump()
    {
        animator.SetBool("Has Jumped", true);
        Invoke("Ragdoll", delayBeforeRagdollAfterJump);
    }

    public void Run()
    {
        animator.SetBool("Is Running", true);
    }

    public void Idle()
    {
        animator.SetBool("Is Running", false);
    }

    public void Ragdoll()
    {
        animator.enabled = false;
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

}
