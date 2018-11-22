using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour {

    private Rigidbody2D rigid;
    private PlayerAnimation playerAnimation;

    private bool attacking = false;

    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (!attacking)
        {
            PlayerAttacking();
        }
    }

    private void PlayerAttacking()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || CrossPlatformInputManager.GetButtonDown("A_Button"))
        {
            float velocity = rigid.velocity.y;
            playerAnimation.AttackAnimation(velocity);
            StartCoroutine(AttackResetCoroutine());
        }
    }

    IEnumerator AttackResetCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
}
