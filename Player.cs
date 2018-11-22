using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

    private AudioSource audioSource;

    [SerializeField] public int health;
    [SerializeField] public bool playerAlive = true;
    private AudioManager audioManager;

    public int StartingHealth {get;set;}
 
    private void Start () {
        StartingHealth = health;
        audioManager = FindObjectOfType<AudioManager>();

    }
	
    public void Damage()
    {
        StartingHealth--;
        audioManager.Play("PlayerHit");
        UIManager.Instance.UpdateHealth(StartingHealth);
        if (StartingHealth < 1)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        audioManager.Play("PlayerDeath");
        playerAlive = false;
        UIManager.Instance.DeathPanelActive();

    }
}
