using UnityEngine;

public class ShipSway : MonoBehaviour
{
    float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= 5)
        {
            this.gameObject.transform.Rotate(Vector3.forward/5);
        }
        else if (timer < 10)
        {
            this.gameObject.transform.Rotate(-Vector3.forward/5);
        }
        else
        {
            timer = 0;
        }
    }
}
