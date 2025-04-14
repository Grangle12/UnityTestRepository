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

            int resourceLevelChance = Random.Range(0, 100);
            if (resourceLevelChance < 90)
            {
                newGO.GetComponent<AsteroidResources>().resourceLevel = 0;
                newGO.GetComponent<Renderer>().material.color = Color.blue;
                newGO.GetComponent<AsteroidResources>().fuelAmt = resourceList[0].fuelAmt;
                newGO.GetComponent<AsteroidResources>().rareResourceAmt = resourceList[0].rareResourceAmt;
            }
            else
            {
                newGO.GetComponent<AsteroidResources>().resourceLevel = 1;
                newGO.GetComponent<AsteroidResources>().fuelAmt = resourceList[1].fuelAmt;
                newGO.GetComponent<AsteroidResources>().rareResourceAmt = resourceList[1].rareResourceAmt;
                

                if (GameManager.instance.shipController.detectorLevel >= 1)
                {
                    newGO.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    Debug.Log("spawned a red, but detector isnt high enough to see it");
                }
            }
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
