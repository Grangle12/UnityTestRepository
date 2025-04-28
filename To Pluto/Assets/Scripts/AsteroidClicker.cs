using UnityEngine;
using UnityEngine.InputSystem;

public class AsteroidClicker : MonoBehaviour
{
    private Camera mainCamera;

    float fuelGain = 50;
    int resourceGain = 10;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    public void OnCLick(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

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

    }
}
