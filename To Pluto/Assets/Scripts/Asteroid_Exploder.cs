using UnityEngine;

public class Asteroid_Exploder : MonoBehaviour
{
    Rigidbody2D rigidbody;

    [SerializeField] GameObject[] resources;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        //ExplodeWithResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExplodeWithResources()
    {
        foreach(var resource in resources)
        {
            float randX = Random.Range(-360f, 360f);
            float randY = Random.Range(-360f, 360f);
            GameObject newGO = Instantiate<GameObject>(resource, this.transform.position, Quaternion.identity);
            newGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(randX, randY));//, ForceMode2D.Force);
        }

        Destroy(this.gameObject);
    }

}
