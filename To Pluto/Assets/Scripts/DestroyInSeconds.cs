using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }


}
