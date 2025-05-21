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
        float randomTimeDistribution = Random.Range(spawnTime, 5);
        float randomDistribution = Random.Range(-5, 5);
        float randomForce = Random.Range(100, 500);
        float randomTorqueX = Random.Range(-10, 10);
        float randomTorqueY = Random.Range(-10, 10);
        float randomTorqueZ = Random.Range(-10, 10);


        Vector3 randomPosition = this.transform.position + new Vector3(-2, randomDistribution, 0);

        if (currentTime > randomTimeDistribution) 
        {
            GameObject newGO;

            int resourceLevelChance = Random.Range(0, 100);
            if(GameManager.instance.shipController.detectorLevel == 0)
            {
                //newGO = Instantiate(resourceList[0].gameObject, randomPosition, Random.rotation);
                newGO = Instantiate(resourceList[0].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                AssignAsteroidResource(newGO, resourceList[0]);

            }
            else if (GameManager.instance.shipController.detectorLevel == 1)
            {
                if (resourceLevelChance < 90)
                {
                    newGO = Instantiate(resourceList[0].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[0]);
                }
                else
                {
                    newGO = Instantiate(resourceList[1].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[1]);
                }
            }
            else if (GameManager.instance.shipController.detectorLevel == 2)
            {
                if (resourceLevelChance < 90)
                {
                    newGO = Instantiate(resourceList[0].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[0]);
                }
                else if (resourceLevelChance > 95)
                {
                    newGO = Instantiate(resourceList[1].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[1]);
                }
                else
                {
                    newGO = Instantiate(resourceList[2].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[2]);
                }
            }
            else if (GameManager.instance.shipController.detectorLevel == 3)
            {
                if (resourceLevelChance < 85)
                {
                    newGO = Instantiate(resourceList[0].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[0]);
                }
                else if (resourceLevelChance > 85 && resourceLevelChance < 90)
                {
                    newGO = Instantiate(resourceList[1].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[1]);
                }
                else if (resourceLevelChance > 90 && resourceLevelChance < 95)
                {
                    newGO = Instantiate(resourceList[2].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[2]);
                }
                else
                {
                    newGO = Instantiate(resourceList[3].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                    AssignAsteroidResource(newGO, resourceList[3]);
                }
            }
            else
            {
                Debug.Log("We are at a higher level than anticipated....");
                newGO = Instantiate(resourceList[0].gameObject, randomPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                AssignAsteroidResource(newGO, resourceList[0]);
            }

            //If we do 3D
            if (newGO.GetComponent<Rigidbody>())
            {
                Rigidbody rb = newGO.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(-randomForce, 0, 0));
                rb.AddTorque(new Vector3(randomTorqueX, randomTorqueY, randomTorqueZ));
            }
            //If we do 2D
            else
            {
                Rigidbody2D rb = newGO.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector3(-randomForce, 0, 0));
                rb.AddTorque(randomTorqueX);
            }
            currentTime = 0;
        }
    }

    private void AssignAsteroidResource(GameObject newGO, Resource_SO resource)
    {
      
        newGO.GetComponent<Renderer>().material.color = resource.color;
        newGO.GetComponent<AsteroidResources>().fuelAmt = resource.fuelAmt;
        newGO.GetComponent<AsteroidResources>().rareResourceAmt = resource.rareResourceAmt;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
