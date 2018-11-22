using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    //Patrol Variables
    [SerializeField] protected float speed;
    [SerializeField] protected float rayDistance;
    public Transform groundRay;
    [SerializeField] protected bool movingLeft;

   
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected AudioManager audioManager;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public virtual void Update()
    {
        PatrolMovement();
    }

    public virtual void PatrolMovement()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        RaycastHit2D grounded = Physics2D.Raycast(groundRay.position, Vector2.down, rayDistance);

        if (grounded.collider == false) {
            animator.SetTrigger("Idle");
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }
}

