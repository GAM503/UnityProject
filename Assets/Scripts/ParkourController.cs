using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    EnvironmentScanner environmentScanner;

    Animator animator;

    PlayerController playerController;

    public List<ParkourAction> parkourActions;

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
                foreach (var action in parkourActions)
                {
                    if (action.CanPerformAction(hitData, transform.position.y))
                    {
                        StartCoroutine(DoParkourAction(action));
                        break;
                    }
                }
            }
        }
        
    }

    IEnumerator DoParkourAction(ParkourAction action)
    {
        inAction = true;
        playerController.SetControl(false);
        animator.CrossFade(action.animationName, 0.2f);
        yield return null;

        var animState = animator.GetNextAnimatorStateInfo(0);

        float timer = 0;
        while(timer <= animState.length)
        {
            timer += Time.deltaTime;
            if(action.rotateToObstacle)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.targetRotation, playerController.rotationSpeed * Time.deltaTime);
            if (action.enableTargetMatching)
                MatchTarget(action);
            yield return null;
        }
        playerController.SetControl(true);
        inAction = false;
    }

    void MatchTarget(ParkourAction action)
    {
        if (animator.isMatchingTarget)
        {
            return;
        }
        animator.MatchTarget(action.MatchPos, transform.rotation, action.targetBodyPart, new MatchTargetWeightMask(action.matchTargetWeightMask,0), action.matchStarTime, action.matchTargetTime);
    }
}
