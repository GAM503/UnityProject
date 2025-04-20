using UnityEngine;

public class SendOnTriggerEnter : TriggerCommand
{
    public LayerMask layers;
    void OnTriggerEnter(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            Send();
        }
    }
}
