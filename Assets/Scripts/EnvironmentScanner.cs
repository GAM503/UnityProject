using UnityEngine;

public class EnvironmentScanner : MonoBehaviour
{
    public Vector3 forwardRayOffset = new Vector3(0, 0.25f, 0);
    public float forwardRayLength = 0.8f;
    public float heightRayLength= 5f;
    public LayerMask obstacleLayer;

    public ObstaceHitData HasObstacleInFront()
    {
        ObstaceHitData hitData = new ObstaceHitData();
        var forwardRayOrigin = transform.TransformPoint(forwardRayOffset);
        hitData.forwardHitFound = Physics.Raycast(forwardRayOrigin, transform.forward, out hitData.forwardHitInfo, forwardRayLength, obstacleLayer);

        Debug.DrawRay(transform.TransformPoint(forwardRayOffset), transform.forward * forwardRayLength, (hitData.forwardHitFound) ? Color.red : Color.white);

        if (hitData.forwardHitFound)
        {
            var heightOrigin = hitData.forwardHitInfo.point + Vector3.up * heightRayLength;
            hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightHitInfo, heightRayLength, obstacleLayer);

            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.heightHitFound) ? Color.red : Color.white);
        }
        return hitData;
    }
}

public struct ObstaceHitData
{
    public bool forwardHitFound;
    public bool heightHitFound;
    public RaycastHit forwardHitInfo;
    public RaycastHit heightHitInfo;
}
