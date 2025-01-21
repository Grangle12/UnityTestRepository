using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    GameManager gameManager;
    DisplayText displayText;
    

    SoundManager soundManager;
    //Collider2D coinCollider;

    public int coinId;
    public bool collected = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = gameManager.gameObject.GetComponent<SoundManager>();

        displayText = gameManager.gameObject.GetComponent<DisplayText>(); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            soundManager.PlayCoinPickupSound();
            gameManager.coinAmount++;
            displayText.UpdateCoinCounter();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            collected = true;
        }
        //Debug.Log("something has triggered me!");
    }

    public void LoadCoinData()
    {
        gameManager.coinDictionary.TryGetValue(coinId, out collected);

        if(collected)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    

}
