using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void PlayFootstepSFX()
    {
        if(animator.GetFloat("moveAmount") > 0.2f){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f)){
                Ground ground = hit.collider.gameObject.GetComponent<Ground>();
                if(ground?.audiuble == true){
                    audioSource.PlayOneShot(ground.getRandomFootstepSound);
                }
            }
        }
        
    }
}
