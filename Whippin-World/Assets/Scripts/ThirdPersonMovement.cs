using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ThirdPersonMovement : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject player;
    private Rigidbody playerRb;
    private Animator playerAnim;
    [SerializeField] float speed;
    [SerializeField] float speedRun;
    [SerializeField] Transform cam;
    [SerializeField] float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpForce;
    [SerializeField] float speedClimb;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip itemNeedingSound;
    private AudioSource playerAudio;
    private bool isShift = false;
    private bool isOnGround = false;
    private bool isOnLadder = false;
    private bool isJumping;
    private bool isFalling;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAnim = player.GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver)
        {
            float verticalVelocity = playerRb.velocity.y;
            IsPlayerJumping(verticalVelocity);
            SwitchModeRunWalk();
            Attack();
            if (isOnGround) Jump();
            if (isOnLadder) Climb();
            if (isShift) Move(speedRun);
            else Move(speed);
            SetPlayerJumpAnimation();
            FirstBuying();
        }
        else playerAnim.SetBool("dead", true);
    }

    void Move(float speed)
    {
        float horizontal = Input.GetAxisRaw("HorizontalPlayer");
        float vertical = Input.GetAxisRaw("VerticalPlayer");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.position += moveDir.normalized * speed * Time.deltaTime;
            if(!isShift) playerAnim.SetFloat("speed_w", 0.3f);
            else playerAnim.SetFloat("speed_w", 0.7f);
        }
        else
        {
            playerAnim.SetFloat("speed_w", 0f);
        }
    }
    void SwitchModeRunWalk()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround) isShift = !isShift;
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnGround) { 
            playerAnim.SetBool("attack", true); 
            playerAudio.PlayOneShot(attackSound, 1.0f); 
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    void Climb()
    {
        if(Input.GetKey(KeyCode.Q) && isOnLadder) 
        {
            if (gameManager.IsClimbEquip())
            {
                transform.Translate(Vector3.up * speedClimb * Time.deltaTime);
            }
            else
            {
                gameManager.DisplayClimbEquipMessage();
                playerAudio.PlayOneShot(itemNeedingSound, 1.0f);
            }
        }
    }
    void IsPlayerJumping(float verticalVelocity)
    {
        if (verticalVelocity > 0.1f)
        {
            isJumping = true;
            isFalling = false;
        }
        else if (verticalVelocity < -0.1f)
        {
            isJumping = false;
            isFalling = true;
        }
        else
        {
            isJumping = false;
            isFalling = false;
        }
    }

    void SetPlayerJumpAnimation()
    {
        if(isJumping)
        {
            playerAnim.SetTrigger("jump_trig");
            playerAnim.SetBool("jump_b", true);
            playerAnim.SetBool("grounded", false);
        }
        else if (isFalling)
        {
            playerAnim.SetBool("jump_b", false);
            playerAnim.SetBool("grounded", false);
        }

        if(isOnGround) 
        {
            playerAnim.SetBool("jump_b", false);
            playerAnim.SetBool("grounded", true);
        }
    }

    public void FirstBuying()
    {
        if(Input.GetKeyDown(KeyCode.I) && gameManager.IsPlayerOnFirstHouse && isOnGround)
        {
            gameManager.HandleFirstBuyingMessage();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.SetPlayerHealth(-1);
        }
    }
    private void OnCollisionExit(Collision collision)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstEquip"))
        {
            gameManager.IsPlayerOnFirstHouse = true;
        }
        else if (other.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;
            playerRb.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FirstEquip"))
        {
            gameManager.IsPlayerOnFirstHouse = false;
        }
        else if (other.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = false;
            isOnGround = false;
            playerRb.useGravity = true;
        }
    }
}
