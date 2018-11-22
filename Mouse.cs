using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour, IDamageable {


    private Rigidbody2D mouseRigid;
    private Transform mouseTrans;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleDeath;
    private Animator animator;
    [SerializeField] private bool mouseTriggered = false;
    [SerializeField] private float runForce = 5.0f;
    [SerializeField] private float safeDistance = 7.0f;
    [SerializeField] private Vector2 dashVector;

    [SerializeField] public int StartingHealth { get; set; }

    void Awake () {
        Init();
        StartCoroutine(MouseIdleCoroutine());
    }

    void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mouseRigid = GetComponent<Rigidbody2D>();
        mouseTrans = GetComponent<Transform>();
        particleDeath = GetComponent<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

	void Update () {
       TriggerCheck();
        if(mouseTriggered)
        {
            MouseRun();
        }
 	}

    public void Damage()
    {
        runForce = 0.0f;
        animator.SetBool("Death", true);
        particleDeath.Play();
        Destroy(gameObject, particleDeath.main.duration);
    }


    //Random Patrol Pattern - No Edge Detection

    private void MouseIdleMovement()
    {     
        float randomMovement = Random.Range(-1.0f, 1.0f);
        Move(randomMovement * runForce);
 
    }

    private void TriggerCheck()
    {
        dashVector = mouseTrans.position - player.transform.position;
        if (dashVector.x < safeDistance)
        {
            mouseTriggered = true;
        } else if (dashVector.x > safeDistance)
        {
            mouseTriggered = false;
        }
    }

    private void MouseRun()
    {
            if (dashVector.x < 0)
            {
                Move(-runForce);
            }
            else
            {
                Move(runForce);
            }
    }

    private void Move(float move)
    {
        if (move >= 0.0f)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }
        mouseRigid.velocity = new Vector2(move, mouseRigid.velocity.y);
    }

    IEnumerator MouseIdleCoroutine() {
        while (mouseTriggered == false)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 5.0f));
            MouseIdleMovement();
        }
    }
}
