using System.Collections;
using Mono.Cecil.Cil;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParkourController : MonoBehaviour
{
    EnvironmentScanner environmentScanner;

    Animator animator;

    PlayerController playerController;

    public bool inAction = false;
    void Awake()
    {
        environmentScanner = GetComponent<EnvironmentScanner>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

    }

    void Update()
    {
        if(Input.GetButton("Jump") && !inAction)
        {
            var hitData = environmentScanner.HasObstacleInFront();

            if (hitData.forwardHitFound)
            {
                Debug.Log("Obstacle in front!");
                StartCoroutine(DoParkourAction());
            }
        }
        
    }

    IEnumerator DoParkourAction()
    {
        inAction = true;
        playerController.SetControl(false);
        animator.CrossFade("Step Up", 0.2f);
        yield return null;

        var animState = animator.GetNextAnimatorStateInfo(0);

        yield return new WaitForSeconds(animState.length);
        playerController.SetControl(true);
        inAction = false;
    }
}
