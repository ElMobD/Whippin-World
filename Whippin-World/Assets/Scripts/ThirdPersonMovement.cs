using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirsPerson : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody playerRb;
    private Animator playerAnim;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float turnSmoothVelocity;
    [SerializeField] Transform cam;
    [SerializeField] float jumpForce;


    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("HorizontalPlayer");
        float vertical = Input.GetAxisRaw("VerticalPlayer");
        Vector3 direction = new Vector3(-horizontal, 0f, -vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
            playerAnim.SetFloat("speed_w", 0.3f);
        }
        else
        {
            playerAnim.SetFloat("speed_w", 0f);
        }
    }
}
