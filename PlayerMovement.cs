using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private AudioManager audioManager;
    //Instances
    private Rigidbody2D rigid;
    private PlayerAnimation playerAnimation;

    //Parameters
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float jumpForce = 3.0f;
    [SerializeField] private bool playerClimbing = false;
    [SerializeField] private bool playerGrounded = true;
    [SerializeField] private Transform groundTransform;
    private float inputHorizontal;
    private float inputVertical;

    //RayCasting
    [SerializeField] private float rayGround = 0.52f;
    [SerializeField] private float rayLadder = 0.52f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsLadder;

    //DustSpawning
    [SerializeField] private GameObject spawnDustPS;
    [SerializeField] private bool spawnDustOnLanding = false;
    [SerializeField] private float startTimeBtwTrail = 0.5f;
    [SerializeField] private float timeBtwTrail;


    // Use this for initialization
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate()
    {
        PlayerCanClimb();
        PlayerIsGrounded();
        Movement();
    }

    private void Movement()
    {
        //Move
        inputHorizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        //Climb
        if (playerClimbing == true)
        {

            inputVertical = CrossPlatformInputManager.GetAxisRaw("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x, inputVertical * playerSpeed);
            rigid.gravityScale = 0.0f;
        }
        else
        {

            rigid.gravityScale = 1.0f;
        }

        //Jump
        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button"))  && (playerGrounded == true || playerClimbing == true))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            Instantiate(spawnDustPS, groundTransform.position, Quaternion.identity);
            spawnDustOnLanding = true;
        }

        Vector2 playerMovement = new Vector2(inputHorizontal * playerSpeed, rigid.velocity.y);
        rigid.velocity = playerMovement;

        playerAnimation.Movement(playerMovement, playerGrounded, playerClimbing);
        DustParticles();
    }

    bool PlayerIsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, rayGround, whatIsGround);
        Debug.DrawRay(transform.position, Vector2.down * rayGround, Color.green);
        if (hitInfo.collider != null)
        {
            playerGrounded = true;
            return true;
        }
        playerGrounded = false;
        return false;
    }

    bool PlayerCanClimb()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, rayLadder, whatIsLadder);
        Debug.DrawRay(transform.position, Vector2.up * rayLadder, Color.red);
        if (hitInfo.collider != null)
        {
            playerClimbing = true;
            return true;
        }
        playerClimbing = false;
        return false;
    }

    private void DustParticles()
    {
        if (playerGrounded == true)
        {
            if (spawnDustOnLanding == true && rigid.velocity.y < 0)
            {
                Instantiate(spawnDustPS, groundTransform.position, Quaternion.identity);
                audioManager.Play("PlayerLanding");
                spawnDustOnLanding = false;
            }
            else if (inputHorizontal != 0)
            {
                if (timeBtwTrail <= 0)
                {
                    Instantiate(spawnDustPS, groundTransform.position, Quaternion.identity);
                    audioManager.Play("PlayerWalking");
                    timeBtwTrail = startTimeBtwTrail;
                }
                else
                {
                    timeBtwTrail -= Time.deltaTime;
                }
            }
        }
    }

}
