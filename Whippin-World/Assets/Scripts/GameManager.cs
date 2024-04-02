using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject messageEquipment;
    [SerializeField] GameObject buyingEquipment;

    [SerializeField] float messageDuration = 3f;
    [SerializeField] GameObject player;
    private bool isPlayerOnFirstHouse = false;
    private bool isFirstBuyingMessageOpen = false;
    private bool isPlayerHaveFirstEquipment = false;
    private int playerMoney = 0;
    [SerializeField] TMP_Text playerMoneyText;
    [SerializeField] TMP_Text firstBuyingPriceText;
    private int firstBuyingPrice = 50;
    // Start is called before the first frame update
    void Start()
    {
        playerMoneyText.text = "Money : " + playerMoney;
        firstBuyingPriceText.text = "Prix : " + firstBuyingPrice + " €";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isPlayerOnFirstHouse);
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

    }
    IEnumerator DisplayMessageCoroutine()
    {
        messageEquipment.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messageEquipment.SetActive(false);
    }
}
