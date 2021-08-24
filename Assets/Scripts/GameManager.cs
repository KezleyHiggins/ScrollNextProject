using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables

    PlayerController player;
    bool levelHasStarted = false;
    bool playerHasWon = false;
    int currentLevel = 0;
    [SerializeField] int amountOfLevels = 6;

    UnityEvent onUpdateLevelNumber = new UnityEvent();
    UnityEvent onShowTutorialScreen = new UnityEvent();
    UnityEvent onShowGameOverScreen = new UnityEvent();
    UnityEvent onShowYouWinScreen = new UnityEvent();
    UnityEvent onHideNotShownDuringPlayUI = new UnityEvent();
    UnityEvent enableEnemyMovement = new UnityEvent();

    #endregion

    #region Methods

    void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("LevelNumber");
        if (currentLevel == 0) currentLevel = 1;
        AssignToOnPlayerEntersEndZoneEvent();
        player = FindObjectOfType<PlayerController>();
        player.OnPlayerDeath.AddListener(OnPlayerLoses);
    }

    void Start()
    {
        onHideNotShownDuringPlayUI.Invoke();
        onUpdateLevelNumber.Invoke();
        onShowTutorialScreen.Invoke();
    }

    void Update()
    {
        if (levelHasStarted == false && player.IsMoving == true)
        {
            onHideNotShownDuringPlayUI.Invoke();
            levelHasStarted = true;
            enableEnemyMovement.Invoke();
        }
    }

    void AssignToOnPlayerEntersEndZoneEvent()
    {
        EndZoneCollider endZoneCollider = FindObjectOfType<EndZoneCollider>();
        endZoneCollider.OnPlayerEnteringEndZone.AddListener(delegate { OnPlayerWins(); });
        endZoneCollider.OnPlayerEnteringEndZone.AddListener(delegate { FindObjectOfType<Helicopter>().BeginFlyingAway(); });
    }

    void OnPlayerWins()
    {
        playerHasWon = true;
        player.gameObject.SetActive(false);
        onShowYouWinScreen.Invoke();
    }

    void OnPlayerLoses()
    {
        if (playerHasWon == true) return;

        onShowGameOverScreen.Invoke();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("LevelNumber", currentLevel);
        if (CurrentScene >= amountOfLevels) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(CurrentScene + 1);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LevelNumber", 1);
    }

    #endregion

    #region Properties

    int CurrentScene => SceneManager.GetActiveScene().buildIndex;
    public int CurrentLevel => currentLevel;
    public bool PlayerHasWon => playerHasWon;

    public UnityEvent OnUpdateLevelNumber => onUpdateLevelNumber;
    public UnityEvent OnShowTutorialScreen => onShowTutorialScreen;
    public UnityEvent OnShowGameOverScreen => onShowGameOverScreen;
    public UnityEvent OnShowYouWinScreen => onShowYouWinScreen;
    public UnityEvent OnHideNotShownDuringPlayUI => onHideNotShownDuringPlayUI;
    public UnityEvent EnableEnemyMovement => enableEnemyMovement;

    #endregion
}
