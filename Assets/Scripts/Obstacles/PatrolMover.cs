using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMover : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform locationOneGrabber, locationTwoGrabber;
    Vector3 currentTarget, locationOne, locationTwo;
    Rigidbody rigidBody;

    void Start()
    {
        locationOne = locationOneGrabber.position;
        locationTwo = locationTwoGrabber.position;

        transform.position = locationOne;
        currentTarget = locationTwo;
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsAtThisLocation(currentTarget)) SwitchTarget();
        else MovesTowardsCurrentTarget();
    }

    void MovesTowardsCurrentTarget()
    {
        Vector3 positionToMoveTo = Vector3.MoveTowards(transform.position, currentTarget, speed);
        rigidBody.MovePosition(positionToMoveTo);
    }

    bool IsAtThisLocation(Vector3 location)
    {
        return (Vector3.Distance(transform.position, location) <= 0.01);
    }

    void SwitchTarget()
    {
        if (currentTarget == locationOne) currentTarget = locationTwo;
        else currentTarget = locationOne;
    }

}
