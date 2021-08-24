using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSpinner : MonoBehaviour
{
    [SerializeField] float spinSpeed;
    [SerializeField] Vector3 rotateBy;

    void Update()
    {
        transform.Rotate(rotateBy * spinSpeed);
    }
}
