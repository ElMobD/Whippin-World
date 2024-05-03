using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject messageEquipment;
    [SerializeField] GameObject buyingEquipment;
    [SerializeField] float messageDuration = 3f;
    [SerializeField] GameObject player;
    private int playerHealth = 10;
    private bool isPlayerOnFirstHouse = false;
    private bool isFirstBuyingMessageOpen = false;
    private bool isPlayerHaveFirstEquipment = false;
    private int playerMoney = 0;
    [SerializeField] TMP_Text playerMoneyText;
    [SerializeField] TMP_Text firstBuyingPriceText;
    [SerializeField] GameObject gameOverPanel;
    private int firstBuyingPrice = 50;
    private HealthBar healthBar;
    private bool isGameOver;
    private AudioSource gameAudio;
    [SerializeField] AudioClip buyingSound;



    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        playerMoneyText.text = "Money : " + playerMoney + " €";
        firstBuyingPriceText.text = "Prix : " + firstBuyingPrice + " €";
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver) 
        {
            playerMoneyText.text = "Money : " + playerMoney + " €";
        }
        if (playerHealth <= 0) { isGameOver = true; gameOverPanel.gameObject.SetActive(true);};
    }

    public bool IsClimbEquip()
    {
        return isPlayerHaveFirstEquipment;
    }
    public bool IsPlayerOnFirstHouse
    {
        get { return isPlayerOnFirstHouse;}
        set { isPlayerOnFirstHouse = value;}
    }
    public GameObject BuyingEquipment
    {
        get { return buyingEquipment;}
        set {}
    }

    public int PlayerHealth
    {
        get { return playerHealth;}
        set { }
    }
    public int PlayerMoney
    {
        get { return playerMoney; }
        set { playerMoney = value; }
    }
    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
    public void DisplayClimbEquipMessage()
    {
        StartCoroutine(DisplayMessageCoroutine());
    }
    public void DisplayFirstBuyingMessage()
    {
        BuyingEquipment.SetActive(true);
        isFirstBuyingMessageOpen=true;
    }
    public void CloseFirstBuyingMessage()
    {
        BuyingEquipment.SetActive(false);
        isFirstBuyingMessageOpen=false;
    }
    public void HandleFirstBuyingMessage()
    {
        if (isFirstBuyingMessageOpen) CloseFirstBuyingMessage();
        else DisplayFirstBuyingMessage();
    }
    public void BuyFirstItem()
    {
        if( playerMoney >= firstBuyingPrice)
        {
            isPlayerHaveFirstEquipment = true;
            playerMoney -= firstBuyingPrice;
            CloseFirstBuyingMessage();
            gameAudio.PlayOneShot(buyingSound, 1.0f);
        }
    }
    public void SetPlayerHealth(int a)
    {
        playerHealth += a;
    }
    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    IEnumerator DisplayMessageCoroutine()
    {
        messageEquipment.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messageEquipment.SetActive(false);
    }
}
