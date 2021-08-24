using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    void Awake()
    {
        Button button = GetComponent<Button>();
        GameManager gameManager = FindObjectOfType<GameManager>();
        button.onClick.AddListener(gameManager.LoadNextLevel);
    }
}
