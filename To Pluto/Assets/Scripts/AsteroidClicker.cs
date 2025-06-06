using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class AsteroidClicker : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] float fuelGain = 5;
    [SerializeField] int resourceGain = 10;

    
    public GameObject asteroidChunk;
    [SerializeField] GameObject asteroidSign1, asteroidSign2;

    public Transform armSpawnPoint;
    public Transform armMidPoint;
    public Transform armEndPoint;
    public Transform armRestingPoint;
    public float vertexCount;
    public float bendStrenth;
    public LineRenderer lineRender;

    Vector3 ArmEndPointStartPos;


    public float armMoveSpeed = 1;

    bool armMoving, armReturning;
    private Transform colliderTransform;
    private Transform asteroidTransform;

    GameObject targetGO;

    [SerializeField] GameObject armGameObjectReference;
    public int armCount;
    public List<GameObject> armGameObjects = new List<GameObject>();

    [SerializeField] bool noClickNeeded;
    private void Awake()
    {
        mainCamera = Camera.main;
        //Debug.Log(asteroidChunk);
    }
    private void Start()
    {
        ArmEndPointStartPos = armEndPoint.position;
        ArmRender(armSpawnPoint, armMidPoint, armEndPoint, armRestingPoint);

        InitiateMapArms();
    }

    private void Update()
    {
        NoClickJustHover();
        /*
        if(armMoving)
        {
            if (colliderTransform == null)
            {
                colliderTransform = GameManager.instance.shipController.gameObject.transform;

                armReturning = true;
            }
            else
            {
                ArmRender(armSpawnPoint, armMidPoint, armEndPoint, colliderTransform);

                if (!armReturning)
                {
                    if (armEndPoint.position == colliderTransform.position)
                    {
                        
                        asteroidTransform = colliderTransform;
                        if (colliderTransform.gameObject.tag == "ExploderAsteroid")
                        {
                           
                            colliderTransform.gameObject.GetComponent<Asteroid_Exploder>().ExplodeWithResources();
                        }
                        colliderTransform = GameManager.instance.shipController.gameObject.transform;

                        armReturning = true;

                       
                    }
                }
                else
                {
                    if (asteroidTransform != null)
                    {
                        asteroidTransform.position = armEndPoint.position;
                    }
                    if (armEndPoint.position == colliderTransform.position)
                    {
                        armReturning = false;
                        armMoving = false;
                    }
                }
            }
        }
        else if (armEndPoint.position != armMidPoint.position)
        {
            ArmRender(armSpawnPoint, armMidPoint, armEndPoint, armRestingPoint);

        }
        */
        for(int i = 0; i < armGameObjects.Count; i++)
        {
            
            ArmRenderer armInstance = armGameObjects[i].GetComponent<ArmRenderer>();
            
            if (armInstance.colliderTransform == null)
            {
                armInstance.colliderTransform = GameManager.instance.shipController.gameObject.transform;

                armInstance.armReturning = true;
            }

            if (!armInstance.armMoving)
            {
              //  if (!armInstance.armStaticDrawn)
                {
                    armInstance.ArmRender(armInstance.armSpawnPoint, armInstance.armMidPoint, armInstance.armEndPoint, armInstance.armRestingPoint);
                    armInstance.HandLookAt(armInstance.armRestingLookAtPoint, armInstance.armEndPoint);
                    //Debug.Log("I, " + armGameObjects[i] + i + ", am looking at " + armInstance.armRestingLookAtPoint);
                    //armInstance.armEndPoint.
                 //   armInstance.armStaticDrawn = true;
                }
            }
            else if (armInstance.armMoving)
            {
                armInstance.armStaticDrawn = false;

                armInstance.ArmRender(armInstance.armSpawnPoint, armInstance.armMidPoint, armInstance.armEndPoint, armInstance.colliderTransform);

                if (!armInstance.armReturning)
                {
                    if (armInstance.armEndPoint.position == armInstance.colliderTransform.position)
                    {

                        armInstance.asteroidTransform = armInstance.colliderTransform;

                        if (armInstance.colliderTransform.gameObject.tag == "ExploderAsteroid")
                        {

                            armInstance.colliderTransform.gameObject.GetComponent<Asteroid_Exploder>().ExplodeWithResources();
                        }
                        armInstance.colliderTransform = GameManager.instance.shipController.gameObject.transform;

                        armInstance.armReturning = true;
                        Debug.Log("setting return to true");

                    }
                }
                else
                {
                    if (armInstance.asteroidTransform != null)
                    {
                        armInstance.asteroidTransform.position = armInstance.armEndPoint.position;
                    }
                    if (armInstance.armEndPoint.position == armInstance.colliderTransform.position)
                    {
                        armInstance.armReturning = false;
                        armInstance.armMoving = false;
                    }
                }
            }
            else if (armInstance.armEndPoint.position != armInstance.armRestingPoint.position)
            {
                ArmRender(armInstance.armSpawnPoint, armInstance.armMidPoint, armInstance.armEndPoint, armInstance.armRestingPoint);

            }
        }
    }

    void InitiateMapArms()
    {

        while (armGameObjects.Count < armCount && armGameObjectReference != null)
        {
            Vector3 tempVector3 = new Vector3(0, Random.Range(-.5f, .5f), 0);
            GameObject newGO = Instantiate(armGameObjectReference);//, armGameObjectReference.transform.position, Quaternion.identity, GameManager.instance.shipController.transform));
            armGameObjects.Add(newGO);
            newGO.transform.position += tempVector3;
            newGO.transform.Find("LineRenderMid").position += tempVector3;
            newGO.transform.Find("LineRenderEnd").position += tempVector3;
        }

        for (int i = 0; i < armGameObjects.Count; i++)
        {

            ArmRenderer armInstance = armGameObjects[i].GetComponent<ArmRenderer>();

            if (armInstance.colliderTransform == null)
            {
                armInstance.colliderTransform = GameManager.instance.shipController.gameObject.transform;

            }
        }
    }

    public void AddArm()
    {
        armCount++;
        Vector3 tempVector3 = new Vector3(0, Random.Range(-.5f, .5f), 0);
        GameObject newGO = Instantiate(armGameObjectReference);//, armGameObjectReference.transform.position, Quaternion.identity, GameManager.instance.shipController.transform));
        armGameObjects.Add(newGO);
        newGO.transform.position += tempVector3;
        newGO.transform.Find("LineRenderMid").position += tempVector3;
        newGO.transform.Find("LineRenderEnd").position += tempVector3;

        newGO.GetComponent<ArmRenderer>().colliderTransform = GameManager.instance.shipController.gameObject.transform;
    }

    void ArmRender(Transform startPoint, Transform midPoint, Transform endPoint, Transform targetTransform)
    {
        var step = armMoveSpeed * Time.deltaTime;
        Debug.Log("Step is: " + step);
        //endPoint.position = Vector2.MoveTowards(new Vector2(endPoint.position.x, endPoint.position.y), new Vector2(targetTransform.position.x, targetTransform.position.y), step);
        endPoint.position = new Vector3(Mathf.Lerp(10, 100, step), Mathf.Lerp(10, 100, step), 0);
        //endPoint.position = Vector3.MoveTowards(endPoint.position, targetTransform.position, step);
        Vector3 Direction = targetTransform.position - endPoint.position;
        Vector3 Direction2 = Vector3.ProjectOnPlane(transform.forward, Direction);
        Quaternion rotation = Quaternion.LookRotation(Direction2, Direction);
        endPoint.rotation = rotation;

        List<Vector3> points = new List<Vector3>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (float i = 0; i <= 1; i += 1 / vertexCount)
        {
            Vector3 t1 = Vector3.Lerp(startPoint.position, midPoint.position, i);
            Vector3 t2 = Vector3.Lerp(midPoint.position, endPoint.position, i);
            Vector3 t = Vector3.Lerp(t1, t2, i);
            points.Add(t);


        }
        lineRender.positionCount = points.Count;
        lineRender.SetPositions(points.ToArray());
    }

    void CheckIfArmAvailableForAction()
    {
        for(int i = 0; i < armGameObjects.Count; i++)
        {
            if(!armGameObjects[i].GetComponent<ArmRenderer>().armMoving && !armGameObjects[i].GetComponent<ArmRenderer>().armReturning)
            {
                armGameObjects[i].GetComponent<ArmRenderer>().colliderTransform = colliderTransform;
            }
        }
    }

    public void OnCLick(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


        //3D Clicking
        if (Physics.Raycast(ray, out hit))
        {
            /*
            if (hit.collider != null && hit.collider.gameObject.tag == "Asteroid")
            {
                Destroy(hit.collider.gameObject);
                GameManager.instance.asteroidClickCounter++;
                Debug.Log("you have destroyed: " + GameManager.instance.asteroidClickCounter + "asteroids!");
                //GameManager.instance.shipController.speedKmps += 500;
                if (GameManager.instance.shipController.fuel + fuelGain < GameManager.instance.shipController.maxFuel)
                {
                    GameManager.instance.shipController.ShowFloatingText(fuelGain.ToString(), Color.white, "Energy");
                    GameManager.instance.shipController.fuel += fuelGain;
                    GameManager.instance.shipController.resourceCount += resourceGain;
                    
                }
                else
                {
                    GameManager.instance.shipController.fuel = GameManager.instance.shipController.maxFuel;
                    //GameManager.instance.shipController.resourceCount = GameManager.instance.shipController.maxResourceCount;
                    Debug.Log("triggered1");
                }

            }
            */
        }
        //2D Clicking
        else 
        {
            for (int i = 0; i < armGameObjects.Count; i++)
            {
                ArmRenderer armInstance = armGameObjects[i].GetComponent<ArmRenderer>();
                if (!armInstance.armMoving)
                {
                    var rayHit = Physics2D.GetRayIntersection(ray);
                    if (rayHit.collider != null && (rayHit.collider.gameObject.tag == "Asteroid" || rayHit.collider.gameObject.tag == "ExploderAsteroid"))
                    {
                        if (!rayHit.collider.GetComponent<AsteroidResources>().beingTractored)
                        {
                            Debug.Log("it looks like arm: # " + i + " is available for use");
                            armInstance.colliderTransform = rayHit.collider.transform;
                            armInstance.targetGO = armInstance.colliderTransform.gameObject;
                            armInstance.armMoving = true;

                            GameManager.instance.asteroidClickCounter++;
                            rayHit.collider.GetComponent<AsteroidResources>().beingTractored = true;
                            break;
                        }
                    }

                    // SIGN Asteroid collapse
                    else if (rayHit.collider != null && rayHit.collider.gameObject.tag == "Sign")
                    {
                        Debug.Log("we hit the sign");
                        if (asteroidChunk != null)
                        {
                            Instantiate(asteroidChunk, new Vector3(rayHit.collider.transform.position.x, rayHit.collider.transform.position.y, rayHit.collider.transform.position.z - .05f), Quaternion.identity);
                            asteroidSign1.SetActive(false);
                            asteroidSign2.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("returning null" + rayHit.collider.gameObject);
                        }
                    }
                }
            }
            /*
            if (!armMoving)
            {
                var rayHit = Physics2D.GetRayIntersection(ray);
                if (rayHit.collider != null && (rayHit.collider.gameObject.tag == "Asteroid" || rayHit.collider.gameObject.tag == "ExploderAsteroid"))
                {

                    colliderTransform = rayHit.collider.transform;
                    targetGO = colliderTransform.gameObject;
                    armMoving = true;

                    GameManager.instance.asteroidClickCounter++;
                   

                }
                
                // SIGN Asteroid collapse
                else if (rayHit.collider != null && rayHit.collider.gameObject.tag == "Sign")
                {
                    Debug.Log("we hit the sign");
                    if (asteroidChunk != null)
                    {
                        Instantiate(asteroidChunk, new Vector3(rayHit.collider.transform.position.x, rayHit.collider.transform.position.y, rayHit.collider.transform.position.z - .05f), Quaternion.identity);
                        asteroidSign1.SetActive(false);
                        asteroidSign2.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("returning null" + rayHit.collider.gameObject);
                    }
                }
            }
            */
        }
    }
    
    public void NoClickJustHover()
    {

        //RaycastHit hit;
       
        
        if (noClickNeeded)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var rayHit = Physics2D.GetRayIntersection(ray);

          //  Debug.Log(rayHit.collider);
            for (int i = 0; i < armGameObjects.Count; i++)
            {
                    ArmRenderer armInstance = armGameObjects[i].GetComponent<ArmRenderer>();
                    if (!armInstance.armMoving)
                    {
                        
                        if (rayHit.collider != null && (rayHit.collider.gameObject.tag == "Asteroid" || rayHit.collider.gameObject.tag == "ExploderAsteroid"))
                        {
                            if (!rayHit.collider.GetComponent<AsteroidResources>().beingTractored)
                            {
                                //Debug.Log("it looks like arm: # " + i + " is available for use");
                                armInstance.colliderTransform = rayHit.collider.transform;
                                armInstance.targetGO = armInstance.colliderTransform.gameObject;
                                armInstance.armMoving = true;

                               // GameManager.instance.asteroidClickCounter++;
                                rayHit.collider.GetComponent<AsteroidResources>().beingTractored = true;
                                break;
                            }
                        }

                       
                    }
              }
        }
        
    }   
   
}
