using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour {

    [SerializeField] private int coinsToWin;
    [SerializeField] private int blobsToKill;

    private int coinsCollected;
    private int blobsKilled;

    private void Awake()
    {
        UIManager.Instance.ResetCounters(coinsToWin, blobsToKill);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with Win Condition");
        if (other.tag == "Player")
        {
            coinsCollected = UIManager.Instance.totalCoins;
            blobsKilled = UIManager.Instance.blobsKilled;
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        if(coinsCollected >= coinsToWin && blobsKilled >= blobsToKill)
        {
            UIManager.Instance.LoadNextLevel();
        }
    }
}
