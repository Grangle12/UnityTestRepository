using UnityEngine;
using UnityEngine.InputSystem;

public class AsteroidClicker : MonoBehaviour
{
    private Camera mainCamera;

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
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
                GameManager.instance.asteroidClickCounter++;
                Debug.Log("you have destroyed: " + GameManager.instance.asteroidClickCounter + "asteroids!");
                GameManager.instance.shipController.speedKmps += 500;
            }
        }

    }
}
