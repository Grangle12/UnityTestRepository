using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text coinCount;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        time.text = gameManager.currentTime.ToString("F3");
        
    }

    public void UpdateCoinCounter()
    {
        coinCount.text = gameManager.coinAmount.ToString();
    }
}
