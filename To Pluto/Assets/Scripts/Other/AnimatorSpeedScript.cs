using UnityEngine;



public class AnimatorSpeedScript : MonoBehaviour
{

    public Animator animator;
    public Animation animationClip;
    [SerializeField] float speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", speed);
        //animationClip["HandNet_3_Clip"].speed = speed;
    }
}
