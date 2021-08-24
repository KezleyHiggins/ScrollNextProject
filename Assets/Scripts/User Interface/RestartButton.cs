using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    void Awake()
    {
        Button button = GetComponent<Button>();
        GameManager gameManager = FindObjectOfType<GameManager>();
        button.onClick.AddListener(gameManager.RestartLevel);
    }
}
