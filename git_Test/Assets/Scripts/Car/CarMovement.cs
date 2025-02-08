using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CarMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    //public InputAction playerControls;
    public float moveSpeed = 5f;

    Vector2 moveDirection = Vector2.zero;
    //float oldMovement = 0;
   // float newMovement;

    private void OnEnable()
    {
       // playerControls.Enable();
    }

    private void OnDisable()
    {
       // playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //TestingEvents.current.forwardMovement += OnMoveForward;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.MoveInput.y > 0)
        {
            EventManager.current.forwardTrigger();
            transform.Translate(new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime);
        }
        else if (InputManager.instance.MoveInput.y < 0)
        {
            transform.Translate(new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime);
        }
        if (InputManager.instance.MoveInput.x > 0)
        {

            transform.Translate(new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime);

        }
        else if (InputManager.instance.MoveInput.x < 0)
        {
            transform.Translate(new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime);
        }

        /*
        moveDirection = playerControls.ReadValue<Vector2>();
        if(moveDirection.y > 0)
        {
            EventManager.current.forwardTrigger();
            transform.Translate(new Vector3(0,1,0) * moveSpeed * Time.deltaTime);

        }
        else if (moveDirection.y < 0)
        {
            transform.Translate(new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime);
        }
        if (moveDirection.x > 0)
        {

            transform.Translate(new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime);

        }
        else if (moveDirection.x < 0)
        {
            transform.Translate(new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime);
        }

        /*
        newMovement = moveDirection.y;

        if(oldMovement < newMovement && newMovement > 0)
        {
            TestingEvents.current.forwardTrigger();

        }
        oldMovement = newMovement;
        */


    }



    private void FixedUpdate()
    {
        
        //rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
       /* if (moveDirection.y > 0)
        {
            TestingEvents.current.forwardTrigger();
        }
       */
    }
}
