using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text coinCount;
    
    // Update is called once per frame
    void Update()
    {
        
        time.text = GameManager.instance.currentTime.ToString("F3");
        
    }

    public void UpdateCoinCounter()
    {
        coinCount.text = GameManager.instance.coinAmount.ToString();
    }
}
