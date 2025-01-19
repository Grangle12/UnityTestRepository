using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{

    public static TestingEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action forwardMovement;

    public void forwardTrigger()
    {
        if (forwardMovement != null)
        {
            forwardMovement();
        }
    }

    public event EventHandler<OnSpacePressedEventArges> OnSpacePressed;
    public class OnSpacePressedEventArges : EventArgs
    {
        public int spaceCount;
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
    }
}
