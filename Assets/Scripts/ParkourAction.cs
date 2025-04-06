using UnityEngine;

[CreateAssetMenu(fileName = "ParkourAction", menuName = "Scriptable Objects/ParkourAction")]
public class ParkourAction : ScriptableObject
{
    public string animationName;
    public float minHeight;
    public float maxHeight;
    public bool rotateToObstacle;

    [Header("Target Matching")]
    public bool enableTargetMatching;
    public AvatarTarget targetBodyPart;
    public float matchStarTime;
    public float matchTargetTime;
    public Vector3 matchTargetWeightMask = new Vector3(0, 1, 0);

    public Quaternion targetRotation { get; set; }

    public Vector3 MatchPos { get; set; }

    public bool CanPerformAction(ObstaceHitData hitData, float playerHeight)
    {
        float heightDifference = hitData.heightHitInfo.point.y - playerHeight;

        if (heightDifference < minHeight || heightDifference > maxHeight) {
            return false;
        }

        if (rotateToObstacle)
        {
            targetRotation = Quaternion.LookRotation(-hitData.forwardHitInfo.normal);
        }
        if (enableTargetMatching)
        {
            MatchPos = hitData.heightHitInfo.point;
        }
        return true;
    }
}
