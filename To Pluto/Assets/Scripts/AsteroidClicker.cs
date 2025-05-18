using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AsteroidClicker : MonoBehaviour
{
    private Camera mainCamera;

    float fuelGain = 50;
    int resourceGain = 10;

    
    public GameObject asteroidChunk;
    [SerializeField] GameObject asteroidSign1, asteroidSign2;

    public Transform ArmSpawnpoint;
    public Transform ArmMidpoint;
    public Transform armEndPoint;
    public float vertexCount;
    public float bendStrenth;
    public LineRenderer lineRender;

    Vector3 ArmEndPointStartPos;


    public float armMoveSpeed = 1;

    bool armMoving, armReturning;
    private Transform colliderTransform;
    private Transform asteroidTransform;

    private void Awake()
    {
        mainCamera = Camera.main;
        //Debug.Log(asteroidChunk);
    }
    private void Start()
    {
        ArmEndPointStartPos = armEndPoint.position;
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
                armEndPoint.position = Vector3.MoveTowards(armEndPoint.position, colliderTransform.position, armMoveSpeed);
                Vector3 Direction = colliderTransform.position - armEndPoint.position;
                Vector3 Direction2 = Vector3.ProjectOnPlane(transform.forward, Direction);
                Quaternion rotation = Quaternion.LookRotation(Direction2, Direction);
                armEndPoint.rotation = rotation;

                List<Vector3> points = new List<Vector3>();
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for (float i = 0; i <= 1; i += 1 / vertexCount)
                {
                    Vector3 t1 = Vector3.Lerp(ArmSpawnpoint.transform.position, ArmMidpoint.position, i);
                    Vector3 t2 = Vector3.Lerp(ArmMidpoint.position, armEndPoint.position, i);
                    Vector3 t = Vector3.Lerp(t1, t2, i);
                    points.Add(t);


                }
                lineRender.positionCount = points.Count;
                lineRender.SetPositions(points.ToArray());

                if (!armReturning)
                {
                    if (armEndPoint.position == colliderTransform.position)
                    {
                        asteroidTransform = colliderTransform;
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
                    GameManager.instance.shipController.resourceCount = GameManager.instance.shipController.maxResourceCount;
                }

            }
        }
        //2D Clicking
        else 
        {
            if (!armMoving)
            {
                var rayHit = Physics2D.GetRayIntersection(ray);
                if (rayHit.collider != null && rayHit.collider.gameObject.tag == "Asteroid")
                {
                    colliderTransform = rayHit.collider.transform;
                    armMoving = true;



                    // Destroy(rayHit.collider.gameObject);
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
                        GameManager.instance.shipController.resourceCount = GameManager.instance.shipController.maxResourceCount;
                    }

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
