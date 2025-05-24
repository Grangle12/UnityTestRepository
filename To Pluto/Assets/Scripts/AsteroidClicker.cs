using UnityEngine;
using UnityEngine.InputSystem;
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

    private void Awake()
    {
        mainCamera = Camera.main;
        //Debug.Log(asteroidChunk);
    }
    private void Start()
    {
        ArmEndPointStartPos = armEndPoint.position;
        ArmRender(armSpawnPoint, armMidPoint, armEndPoint, armRestingPoint);
    }

    private void Update()
    {
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
    }

    void ArmRender(Transform startPoint, Transform midPoint, Transform endPoint, Transform targetTransform)
    {
        endPoint.position = Vector3.MoveTowards(endPoint.position, targetTransform.position, armMoveSpeed);
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
            if (hit.collider != null && hit.collider.gameObject.tag == "Asteroid")
            {
                Destroy(hit.collider.gameObject);
                GameManager.instance.asteroidClickCounter++;
                Debug.Log("you have destroyed: " + GameManager.instance.asteroidClickCounter + "asteroids!");
                GameManager.instance.shipController.speedKmps += 500;
                if (GameManager.instance.shipController.fuel + fuelGain < GameManager.instance.shipController.maxFuel)
                {
                    GameManager.instance.shipController.ShowFloatingText(fuelGain.ToString(), Color.white);
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
        }
        //2D Clicking
        else 
        {
            if (!armMoving)
            {
                var rayHit = Physics2D.GetRayIntersection(ray);
                if (rayHit.collider != null && (rayHit.collider.gameObject.tag == "Asteroid" || rayHit.collider.gameObject.tag == "ExploderAsteroid"))
                {
                    colliderTransform = rayHit.collider.transform;
                    targetGO = colliderTransform.gameObject;
                    armMoving = true;



                    // Destroy(rayHit.collider.gameObject);
                    GameManager.instance.asteroidClickCounter++;
                    /*
                    Debug.Log("you have destroyed: " + GameManager.instance.asteroidClickCounter + "asteroids!");
                    GameManager.instance.shipController.speedKmps += 500;
                    Debug.Log("fuel plus gain is: " + GameManager.instance.shipController.fuel + fuelGain);
                    Debug.Log("MAx fuel is: " + GameManager.instance.shipController.maxFuel);
                    if (GameManager.instance.shipController.fuel + fuelGain < GameManager.instance.shipController.maxFuel)
                    {
                        GameManager.instance.shipController.ShowFloatingText(fuelGain.ToString(), Color.white);
                        GameManager.instance.shipController.fuel += fuelGain;
                        
                        
                    }
                    else
                    {
                        GameManager.instance.shipController.fuel = GameManager.instance.shipController.maxFuel;
                        //GameManager.instance.shipController.resourceCount = GameManager.instance.shipController.maxResourceCount;
                        
                    }
                   // GameManager.instance.shipController.resourceCount += resourceGain;
                    */

                }
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
    }


}
