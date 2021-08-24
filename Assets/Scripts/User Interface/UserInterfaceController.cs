using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    #region Variables

    GameManager gameManager;
    [SerializeField] GameObject tutorialHolder, gameOverHolder, youWinHolder;
    [SerializeField] Text levelNumberText;

    #endregion

    #region Methods

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        AssignMethodsToTheirEvents();
    }

    void AssignMethodsToTheirEvents()
    {
        gameManager.OnShowTutorialScreen.AddListener(ShowTutorial);
        gameManager.OnShowGameOverScreen.AddListener(ShowGameOverScreen);
        gameManager.OnShowYouWinScreen.AddListener(ShowYouWinScreen);
        gameManager.OnHideNotShownDuringPlayUI.AddListener(HideUINotShownDuringPlayer);
        gameManager.OnUpdateLevelNumber.AddListener(UpdateLevelNumber);
    }

    void ShowTutorial()
    {
        tutorialHolder.SetActive(true);
    }

    void ShowGameOverScreen()
    {
        gameOverHolder.SetActive(true);
    }

    void ShowYouWinScreen()
    {
        youWinHolder.SetActive(true);
    }

    void HideUINotShownDuringPlayer()
    {
        tutorialHolder.SetActive(false);
        gameOverHolder.SetActive(false);
        youWinHolder.SetActive(false);
    }

    void UpdateLevelNumber()
    {
        levelNumberText.text = "Level " + LevelNumber.ToString();
    }

    #endregion

    #region Properties

    int LevelNumber => gameManager.CurrentLevel;

    #endregion
}
