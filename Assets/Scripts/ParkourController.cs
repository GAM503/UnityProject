using UnityEngine;

public class ParkourController : MonoBehaviour
{
    EnvironmentScanner environmentScanner;
    void Awake()
    {
        environmentScanner = GetComponent<EnvironmentScanner>();
    }

    void Update()
    {
        var hitData = environmentScanner.HasObstacleInFront();

        if (hitData.forwardHitFound)
        {
            Debug.Log("Obstacle in front!");
        }
    }
}
