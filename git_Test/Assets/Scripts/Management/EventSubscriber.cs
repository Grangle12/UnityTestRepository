using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager testingEvents = GetComponent<EventManager>();
        testingEvents.OnSpacePressed += TestingEvents_OnSpacePressed;
        testingEvents.OnFloatEvent += TestingEvents_OnFloatEvent;
        testingEvents.OnActionEvent += TestingEvents_OnActionEvent;
    }

    private void TestingEvents_OnActionEvent(bool arg1, int arg2)
    {
        Debug.Log(arg1 + "  " + arg2);
    }

    private void TestingEvents_OnFloatEvent(float f)
    {
        Debug.Log("Float: " + f);
    }

    private void TestingEvents_OnSpacePressed(object sender,EventManager.OnSpacePressedEventArges e)
    {
        Debug.Log("Space! " + e.spaceCount);
        EventManager testingEvents = GetComponent<EventManager>();
        //testingEvents.OnSpacePressed -= TestingEvents_OnSpacePressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
