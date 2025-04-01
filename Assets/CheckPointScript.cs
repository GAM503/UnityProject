using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private RespawnScript respawn;
    private BoxCollider CheckPointCollider;
     void Awake()
    {
        CheckPointCollider = GetComponent<BoxCollider>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();  
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.RespawnPoint = this.gameObject;
            GetComponent<Collider>().enabled = false;
        }


    }
}
