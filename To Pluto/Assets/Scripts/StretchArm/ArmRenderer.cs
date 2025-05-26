using UnityEngine;
using System.Collections.Generic;



//THIS IS CURRENTLY A DUPLICATE OF A PORTION OF ASTERIOD CLICKER AND IS NOT YET IMPLEMENTED
public class ArmRenderer : MonoBehaviour
{
    public Transform armSpawnPoint;
    public Transform armMidPoint;
    public Transform armEndPoint;
    public Transform armRestingPoint;
    public Transform armRestingLookAtPoint;

    public LineRenderer lineRender;


    public float vertexCount =5;
    public float bendStrenth = 1;
    public float armMoveSpeed = 1;

    public bool armMoving, armReturning, armStaticDrawn;

    public Transform colliderTransform;
    public Transform asteroidTransform;

    public Vector3 ArmEndPointStartPos;

    public GameObject targetGO;

    public void ArmRender(Transform startPoint, Transform midPoint, Transform endPoint, Transform targetTransform)
    {
        endPoint.position = Vector3.MoveTowards(endPoint.position, targetTransform.position, armMoveSpeed);

        HandLookAt(targetTransform, endPoint);
        
        /*
        Vector3 Direction = targetTransform.position - endPoint.position;
        Vector3 Direction2 = Vector3.ProjectOnPlane(transform.forward, Direction);
        Quaternion rotation = Quaternion.LookRotation(Direction2, Direction);
        endPoint.rotation = rotation;
        */

        List<Vector3> points = new List<Vector3>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (float i = 0; i <= 1; i += 1 / vertexCount)
        {
            Vector3 t1 = Vector3.Lerp(startPoint.position, midPoint.position, i);
            Vector3 t2 = Vector3.Lerp(midPoint.position, endPoint.position, i);
            Vector3 t = Vector3.Lerp(t1, t2, i);
            points.Add(t);


        }
        lineRender.positionCount = points.Count;
        lineRender.SetPositions(points.ToArray());
    }


    public void HandLookAt(Transform targetTransform, Transform endPoint)
    {
        Vector3 Direction = targetTransform.position - endPoint.position;
        Vector3 Direction2 = Vector3.ProjectOnPlane(transform.forward, Direction);
        Quaternion rotation = Quaternion.LookRotation(Direction2, Direction);
        endPoint.rotation = rotation;
    }
}
