using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<Resource_SO> resourceList = new List<Resource_SO>();
    public float spawnTime = 1;

    float currentTime = 0;

    public bool spawner;
    public bool despawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner)
        {
            currentTime += Time.deltaTime;
            SpawnResource();
        }
        else if (despawner)
        {

        }
        else
        {
            Debug.Log("spawner or despawner not selected");
        }
    }


    void SpawnResource()
    {
        float randomTimeDistribution = Random.Range(1, 5);
        float randomDistribution = Random.Range(-5, 5);
        float randomForce = Random.Range(100, 500);
        float randomTorqueX = Random.Range(-10, 10);
        float randomTorqueY = Random.Range(-10, 10);
        float randomTorqueZ = Random.Range(-10, 10);


        Vector3 randomPosition = this.transform.position + new Vector3(-2, randomDistribution, 0);

        if (currentTime > randomTimeDistribution) 
        {
            GameObject newGO = Instantiate(resourceList[0].gameObject, randomPosition, Random.rotation);
            Rigidbody rb = newGO.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(-randomForce, 0, 0));
            rb.AddTorque(new Vector3(randomTorqueX, randomTorqueY, randomTorqueZ));
            currentTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

}
