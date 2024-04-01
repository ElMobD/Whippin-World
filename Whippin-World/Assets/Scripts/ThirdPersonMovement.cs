using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ThirdPersonMovement : MonoBehaviour
{
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
    private bool isShift = false;
    private bool isOnGround = false;
    private bool isOnLadder = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchModeRunWalk();
        Attack();
        if (isOnGround) Jump();
        if(isOnLadder) Climb();
        if (isShift) Move(speedRun);
        else Move(speed);

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
        if (Input.GetKeyDown(KeyCode.LeftShift)) isShift = !isShift;
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E)) playerAnim.SetBool("attack", true); 
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void Climb()
    {
        if(Input.GetKey(KeyCode.Q) && isOnLadder) 
        {
            transform.Translate(Vector3.up * speedClimb * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }else if (collision.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;
            playerRb.useGravity = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = false;
            playerRb.useGravity = true;
        }
    }
}
