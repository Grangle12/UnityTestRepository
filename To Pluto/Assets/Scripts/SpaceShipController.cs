using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public long playerStartPosition;
    public long playerPosition;

    float playerPosChange = 0;

    public long speedKmps = 10000; 

    float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //starting point is earth
        playerStartPosition = (long)GameManager.instance.earthPos*10000;
        playerPosition = (long)GameManager.instance.earthPos*10000;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        playerPosChange += (speedKmps * Time.deltaTime) / 3600;

        if (playerPosChange > 1)
        {
            playerPosition += (long)playerPosChange;
            playerPosChange = 0;
        }

        if (timer > 10)
        {
            
            Debug.Log("time elapsed is: " + GameManager.instance.currentTime);
            Debug.Log("current position is: " + playerPosition);
            timer = 0;
            
        }
    }
}
