using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceTweener : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        StartSwiping();
    }

    public void StartSwiping()
    {
        rectTransform.LeanMoveX(-100, 2).setLoopPingPong();
    }
}
