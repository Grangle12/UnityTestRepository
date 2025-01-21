using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGraphics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.forwardMovement += ExhaustParticleSystem;
    }

    private void ExhaustParticleSystem()
    {

    }
}
