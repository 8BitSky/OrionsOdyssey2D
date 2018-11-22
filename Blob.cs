using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Enemy, IDamageable {

    public bool spriteInverted;
    public GameObject blobDeathPart;

    [SerializeField] private int health;
    [SerializeField] public int StartingHealth { get; set; }

    public override void Init()
    {
        base.Init();
        StartingHealth = health;
    }

    public void Damage()
    {
        StartingHealth--;
        audioManager.Play("BlobDamage");
        if (StartingHealth < 1)
        {
            Instantiate(blobDeathPart, transform.position, Quaternion.identity);
            StartCoroutine(KillBlobCoRoutine());
        }
    }

    IEnumerator KillBlobCoRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        NotifyUIManager();
        KillBlobNow();
    }

    public void NotifyUIManager()
    {
        UIManager.Instance.UpdateBlobsKilled();
    }

    private void KillBlobNow()
    {
        Destroy(gameObject);
    }

}
