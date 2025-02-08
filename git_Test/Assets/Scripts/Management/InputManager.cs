using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;
    public static PlayerInput PlayerInput;

    public Vector2 MoveInput { get; private set; }

    public bool MenuOpenInput { get; private set; }
    public bool MenuCloseInput { get; private set; }


    private InputAction moveInputAction;
    private InputAction menuOpenAction;
    private InputAction menuCloseAction;

    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();

        moveInputAction = PlayerInput.actions["Move"];

        menuOpenAction = PlayerInput.actions["MenuOPEN"];
        menuCloseAction = PlayerInput.actions["MenuCLOSE"];

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput = moveInputAction.ReadValue<Vector2>();
        MenuOpenInput = menuOpenAction.WasPressedThisFrame();
        MenuCloseInput = menuCloseAction.WasPressedThisFrame();
        
    }
}
