using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public PartSO tractorBeamPart;

    bool currentlyTractoring;
    Collider tractorbeamCollider;

    public int tractoringSpeed = 1;

    Vector3 directionToShip;

    GameObject asteroidGO;

    private void Awake()
    {
        tractorbeamCollider = this.GetComponent<Collider>();
        if(this.gameObject.tag != "Ship")
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void Update()
    {
        if(asteroidGO == null && currentlyTractoring)
        {
            tractorbeamCollider.enabled = true;
            currentlyTractoring = false;
            if (this.gameObject.tag != "Ship")
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Ship")
        {
            if (other.gameObject.tag == "Asteroid")
            {
                AsteroidResources resource = other.gameObject.GetComponent<AsteroidResources>();
                float fuelAmount = resource.fuelAmt;

                if (GameManager.instance.shipController.fuel + fuelAmount < GameManager.instance.shipController.maxFuel)
                {
                    GameManager.instance.shipController.ShowFloatingText(fuelAmount.ToString(), Color.white);
                    GameManager.instance.shipController.fuel += fuelAmount;
                    //Debug.Log("Gained : " + fuelAmount + " Fuel");
                    GameManager.instance.shipController.resourceCount += (int)(resource.rareResourceAmt);
                    GameManager.instance.shipController.ShowFloatingText(fuelAmount.ToString(), Color.blue);
                    //Debug.Log("Gained : " + (int)(resource.rareResourceAmt) + " Resource");
                }
                else
                {
                    GameManager.instance.shipController.fuel = GameManager.instance.shipController.maxFuel;
                    GameManager.instance.shipController.resourceCount = GameManager.instance.shipController.maxResourceCount;
                }
                Destroy(other.gameObject);
            }
        }
        else
        {
            //Debug.Log("Not A ship: " + this.gameObject);
            if (other.gameObject.tag == "Asteroid" && !other.gameObject.GetComponent<AsteroidResources>().beingTractored) 
            {
                asteroidGO = other.gameObject;
                tractorbeamCollider.enabled = false;
                
                //Debug.Log("currently Tractoring: ");
                if (!currentlyTractoring)
                {
                    currentlyTractoring = true;
                    asteroidGO.GetComponent<AsteroidResources>().beingTractored = true;
                    //asteroidGO.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                    asteroidGO.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
                    this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
                asteroidGO.transform.LookAt(GameManager.instance.shipController.gameObject.transform);
                asteroidGO.GetComponent<Rigidbody>().linearVelocity = asteroidGO.transform.forward * tractoringSpeed;
                
            }
        }
    }


}
