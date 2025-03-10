using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static EventManager current;

    bool triggered = false;

    private void Awake()
    {
        current = this;
        
    }

    public event Action forwardMovement;
    //public event Action coinPickup;
    public event Action gamePaused;

    public void forwardTrigger()
    {
        if (forwardMovement != null)
        {
            forwardMovement();
        }
        if (gamePaused != null)
        {
            gamePaused();
        }
    }


    public event EventHandler<OnSpacePressedEventArges> OnSpacePressed;
    public event EventHandler<OnGameOverEventArges> OnGameOver;
    public class OnSpacePressedEventArges : EventArgs
    {
        public int spaceCount;
    }
    public class OnGameOverEventArges : EventArgs
    {
        public bool gameOverEvent;
    }


    public event TestEventDelegate OnFloatEvent;
    public delegate void TestEventDelegate(float f);

    public event Action<bool, int> OnActionEvent;



    private int spaceCount;

    private void Start()
    {
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Space Presed
            /*if (OnSpacePressed != null)
            {
                OnSpacePressed(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log("no subscribers to OnSpacePressed");
            }*/
            spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArges { spaceCount = spaceCount } );

            OnFloatEvent?.Invoke(5.5f);

            OnActionEvent?.Invoke(true, 56);
        }
        if(GameManager.instance.gameOver == true)
        {
            if (!triggered)
            {
                OnGameOver?.Invoke(this, new OnGameOverEventArges { gameOverEvent = true });
                triggered = true;
            }
        }
    }
}
