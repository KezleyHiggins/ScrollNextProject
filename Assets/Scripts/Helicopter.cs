using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{

    #region Variables

    [Header("Simple Animations")]
    [SerializeField] Transform topBlades;
    [SerializeField] Transform tailBlades;
    [SerializeField] float bladeSpinSpeed;
    [SerializeField] Vector3 rotationBeforeFlyingAway;
    new Rigidbody rigidbody;
    bool shouldFlyAway = false;

    #endregion

    #region Methods
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SpinHelicopterBlades();
        if (RotationIsDone() && shouldFlyAway) FlyAway();
    }

    void SpinHelicopterBlades()
    {
        topBlades.Rotate(new Vector3(0, 1, 0) * bladeSpinSpeed);
        tailBlades.Rotate(new Vector3(1, 0, 0) * bladeSpinSpeed);
    }

    public void BeginFlyingAway()
    {
        shouldFlyAway = true;
        Quaternion rotation = Quaternion.Euler(rotationBeforeFlyingAway);
        rigidbody.MoveRotation(rotation);
    }

    void FlyAway()
    {
        rigidbody.MovePosition(transform.position + (Vector3.right*0.3f));
    }

    bool RotationIsDone()
    {
        return rigidbody.rotation == Quaternion.Euler(rotationBeforeFlyingAway);
    }

    #endregion

    public Vector3 Location => transform.position;
}
