using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerDog;
    private Animator playerAnim;
    [SerializeField] float movementSpeed = 5f;
    private Rigidbody enemyRb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        playerAnim = playerDog.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < 10)
            {
                Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                enemyRb.AddForce(lookDirection * movementSpeed);
            }
        }
    }
    void EnemyDie()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerAnim.GetBool("attack"))
        {
            EnemyDie();
            gameManager.PlayerMoney += 10;
        }
    }

}
