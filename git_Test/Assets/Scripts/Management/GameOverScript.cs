using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.gameOver = true;    
    }

}
