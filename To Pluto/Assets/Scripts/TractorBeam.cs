using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public PartSO tractorBeamPart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid") ;
        {
            AsteroidResources resource = other.gameObject.GetComponent<AsteroidResources>();
            float fuelAmount = resource.fuelAmt;

            if (GameManager.instance.shipController.fuel + fuelAmount < GameManager.instance.shipController.maxFuel)
            {
                GameManager.instance.shipController.fuel += fuelAmount;
                //Debug.Log("Gained : " + fuelAmount + " Fuel");
                GameManager.instance.shipController.resourceCount += (int)(resource.rareResourceAmt);
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

}
