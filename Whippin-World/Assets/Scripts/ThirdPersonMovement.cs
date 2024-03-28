using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody playerRb;
    private Animator playerAnim;
    [SerializeField] float speed;
    [SerializeField] Transform cam;
    [SerializeField] float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpForce;
    private bool isShift = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchModeRunWalk();
        Attack();
        Move();
    }

    void Move()
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
}
