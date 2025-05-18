using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class LineController : MonoBehaviour
{

    public Transform point2;
    public float vertexCount;
    public float bendStrenth;
    public LineRenderer lineRender;

    private void OnMouseDrag()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for(float i=0; i <= 1; i+=1/vertexCount)
        {
            Vector3 t1 = Vector3.Lerp(transform.position, point2.transform.position, i);
            Vector3 t2 = Vector3.Lerp(point2.transform.position, mousePos, i);
            Vector3 t = Vector3.Lerp(t1, t2, i);
            points.Add(t);


        }
        lineRender.positionCount = points.Count;
        lineRender.SetPositions(points.ToArray());


    }
    //private void OnMouseDown()
    //{
    /*
    public void OnCLick(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        List<Vector3> points = new List<Vector3>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (float i = 0; i <= 1; i += 1 / vertexCount)
        {
            Vector3 t1 = Vector3.Lerp(transform.position, point2.transform.position, i);
            Vector3 t2 = Vector3.Lerp(point2.transform.position, mousePos, i);
            Vector3 t = Vector3.Lerp(t1, t2, i);
            points.Add(t);


        }
        lineRender.positionCount = points.Count;
        lineRender.SetPositions(points.ToArray());
   
    }
     */







}
