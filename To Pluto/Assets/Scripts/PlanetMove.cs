using UnityEngine;

public class PlanetMove : MonoBehaviour
{

    [SerializeField] CheckPointSO checkPoint;

    //Speed of graphic going by
    [SerializeField] float speed;
    bool departed = false;
    int disablePosition = -150;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(checkPoint.hasArrived && !departed)
        {

            this.gameObject.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        }
        if(this.transform.position.x < disablePosition)
        {
            this.gameObject.SetActive(false);
        }
    }
}
