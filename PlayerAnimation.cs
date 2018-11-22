using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator animator;
    private bool facingRight = true;
    

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

public void Movement(Vector2 playerMovement, bool playerGrounded, bool playerClimbing)
    {
        animator.SetFloat("Horizontal", Mathf.Abs(playerMovement.x));
        animator.SetFloat("Vertical", playerMovement.y);
        animator.SetBool("Climbing", playerClimbing);
        animator.SetBool("Ground", playerGrounded);

        Vector3 spriteTransformScale = transform.localScale;
        if (playerMovement.x > 0 && !facingRight) {
            Flip();
        } else if (playerMovement.x < 0 && facingRight)
        {
            Flip();
        }
    }

private void Flip()
    {
        facingRight = !facingRight;
        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }



    public void AttackAnimation(float velocity)
    {
        if (velocity != 0)
        {
            animator.SetTrigger("AirAttack");
        } else { 
            animator.SetTrigger("Attacking"); 
        }
    }

    

}
