using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public PartSO tractorBeamPart;

    bool currentlyTractoring;
    Collider tractorbeamCollider;
    Collider2D tractorBeamCollider2D;

    public float tractoringSpeed = 1;

    Vector3 directionToShip;

    GameObject asteroidGO;
    [SerializeField] GameObject dustCloud;

    private void Awake()
    {
        tractorbeamCollider = this.GetComponent<Collider>();
        tractorBeamCollider2D = this.GetComponent<Collider2D>();
        //if(this.gameObject.tag != "Ship")
        //this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void Update()
    {
        if(asteroidGO == null && currentlyTractoring)
        {
            if (tractorbeamCollider)
            {
                tractorbeamCollider.enabled = true;
            }
            else if (tractorBeamCollider2D)
            {
                tractorBeamCollider2D.enabled = true;
            }
            currentlyTractoring = false;
           // if (this.gameObject.tag != "Ship")
               // this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (this.gameObject.tag == "Ship")
        {

            if (other.gameObject.tag == "Asteroid")
            {
                GameObject newDust = Instantiate(dustCloud, other.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                //Debug.Log("have we made " + newDust);

                AsteroidResources resource = other.gameObject.GetComponent<AsteroidResources>();
                float fuelAmount = resource.fuelAmt;

                if (GameManager.instance.shipController.fuel + fuelAmount < GameManager.instance.shipController.maxFuel)
                {
                    GameManager.instance.shipController.ShowFloatingText(fuelAmount.ToString(), Color.white);
                    GameManager.instance.shipController.fuel += fuelAmount;
                    //Debug.Log("Gained : " + fuelAmount + " Fuel");

                    //Debug.Log("Gained : " + (int)(resource.rareResourceAmt) + " Resource");
                }
                else
                {
                    GameManager.instance.shipController.fuel = GameManager.instance.shipController.maxFuel;
                    //GameManager.instance.shipController.resourceCount = GameManager.instance.shipController.maxResourceCount;
                }
                if (GameManager.instance.shipController.resourceCount + (int)(resource.rareResourceAmt) < GameManager.instance.shipController.maxResourceCount)
                {
                    GameManager.instance.shipController.resourceCount += (int)(resource.rareResourceAmt);
                    GameManager.instance.shipController.ShowFloatingText(fuelAmount.ToString(), Color.blue);
                }
                else
                {
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
                if (tractorbeamCollider)
                {
                    tractorbeamCollider.enabled = false;
                    //Debug.Log("currently Tractoring: ");
                    if (!currentlyTractoring)
                    {
                        currentlyTractoring = true;
                        asteroidGO.GetComponent<AsteroidResources>().beingTractored = true;
                        //asteroidGO.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                        asteroidGO.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
                       // this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    }
                    asteroidGO.transform.LookAt(GameManager.instance.shipController.gameObject.transform);
                    Vector3.MoveTowards(asteroidGO.transform.position, GameManager.instance.shipController.gameObject.transform.position, tractoringSpeed);
                    //asteroidGO.GetComponent<Rigidbody>().linearVelocity = asteroidGO.transform.forward * tractoringSpeed;
                }
                
                else if (tractorBeamCollider2D)
                {
                    tractorBeamCollider2D.enabled = false;

                    if (!currentlyTractoring)
                    {
                        currentlyTractoring = true;
                        asteroidGO.GetComponent<AsteroidResources>().beingTractored = true;
                        //asteroidGO.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                        asteroidGO.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0, 0, 0);
                        //this.gameObject.GetComponent<Renderer2D>().material.color = Color.red;
                    }
                    // asteroidGO.transform.LookAt(GameManager.instance.shipController.gameObject.transform);
                    Vector3 newVect3 = Vector3.MoveTowards(asteroidGO.transform.position, this.transform.position, 500);
                    Debug.Log("newvect is: " + newVect3);
                    Vector2 vect2 = new Vector2(newVect3.y, newVect3.x);
                    //asteroidGO.GetComponent<Rigidbody2D>().linearVelocity = Vector2.MoveTowards(asteroidGO.transform.position, this.transform.position, 500); // asteroidGO.transform.forward * tractoringSpeed;
                    asteroidGO.GetComponent<Rigidbody2D>().linearVelocity = vect2; // asteroidGO.transform.forward * tractoringSpeed;
                    Debug.Log("The shipcontroller parent is: " + GameManager.instance.shipController.gameObject.transform.parent);
                }
                

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (this.gameObject.tag == "Ship")
        {

            if (other.gameObject.tag == "Asteroid")
            {
                Debug.Log("3D HIT!!!");
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
                   // GameManager.instance.shipController.resourceCount = GameManager.instance.shipController.maxResourceCount;
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
