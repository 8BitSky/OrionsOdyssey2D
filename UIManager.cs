using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {

    //Singleton Pattern
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }

            return _instance;
        }
    }


    [SerializeField] private bool gamePaused = false;

    public int totalCoins = 0;
    public int playerHealth = 2;
    public int totalLives = 3;
    public int blobsKilled = 0;

    public int UICoinsToWin;
    public int UIBlobsToWin;

    public TextMeshProUGUI coinCounter;
    public TextMeshProUGUI blobCounter;
    public TextMeshProUGUI lifeCounter;
    public TextMeshProUGUI pauseStats;

    public GameObject MenuPanel;
    public GameObject PausePanel;
    public GameObject HUDPanel;
    public GameObject DeathPanel;

    public GameObject retryButton;

    public Image healthDisplay;
    [SerializeField] private Sprite fullHealth;
    [SerializeField] private Sprite halfHealth;
    [SerializeField] private Sprite noHealth;

    private void Awake()
    {
        _instance = this;
        int numUIManagers = FindObjectsOfType<UIManager>().Length;
        if (numUIManagers > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            GameObject.DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
    }

    public void UpdateHealth(int health)
    {
        switch(health)
        {
            case 2:
                healthDisplay.sprite = fullHealth;
                break;
            case 1:
                healthDisplay.sprite = halfHealth;
                break;
            case 0:
                healthDisplay.sprite = noHealth;
                break;

        }
    }

    public void ResetCounters(int coins, int blobs)
    {
        UICoinsToWin = coins;
        UIBlobsToWin = blobs;
        coinCounter.text = UICoinsToWin.ToString();
        blobCounter.text = UIBlobsToWin.ToString();

    }

    public void UpdateScore(int coins)
    {
        totalCoins += coins;
        UICoinsToWin -= coins;
        if (UICoinsToWin > 0)
        {
            coinCounter.text = UICoinsToWin.ToString();
        } else if ( UICoinsToWin <= 0)
        {
            coinCounter.text = " :) ";
        }
    }

    public void UpdateBlobsKilled()
    {
        blobsKilled += 1;
        UIBlobsToWin--;
        if (UIBlobsToWin > 0)
        {
            blobCounter.text = UIBlobsToWin.ToString();
        }
        else if (UIBlobsToWin <= 0)
        {
            blobCounter.text = " :) ";
        }

    }

    public void ExtraLife()
    {
        totalLives += 1;
    }

    public void DeathPanelActive()
    {
        PauseToggle(true);
        DeathPanel.SetActive(true);
        totalLives -= 1;
        lifeCounter.text = totalLives.ToString();
        if (totalLives <= 0)
        {
            retryButton.SetActive(false);
        }
    }

    public void GameRetry()
    {
        PauseToggle(false);
        DeathPanel.SetActive(false);
        UpdateHealth(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void GamePaused()
    {
        PauseToggle(true);
        PausePanel.SetActive(true);
    }

    public void ReturnToGame()
    {
        PauseToggle(false);
        PausePanel.SetActive(false);
    }


    public void StartFirstLevel()
    {
        PauseToggle(false);
        MenuPanel.SetActive(false);
        HUDPanel.SetActive(true);
        SceneManager.LoadScene("Level1");
        Debug.Log("StartFirstLevel Called");
    }

    public void StartTutorial()
    {
        PauseToggle(false);
        SceneManager.LoadScene("Tutorial");
        MenuPanel.SetActive(false);
        HUDPanel.SetActive(true);
    }


    public void LoadNextLevel()
    {
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        totalCoins = 0;
        blobsKilled = 0;

        if (nextSceneIndex < totalSceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        PauseToggle(false);
        SceneManager.LoadScene(0);
        PausePanel.SetActive(false);
        HUDPanel.SetActive(false);
        DeathPanel.SetActive(false);
        MenuPanel.SetActive(true);

        totalCoins = 0;
        playerHealth = 3;
        totalLives = 3;
        blobsKilled = 0;
}

    private void PauseToggle(bool pauseToggle)
    {
        gamePaused = pauseToggle;
        if (gamePaused == true)
        {
            Time.timeScale = 0.0f;
            return;
        }
        if (gamePaused == false)
        {
            Time.timeScale = 1.0f;
            return;
        }
    }

}
