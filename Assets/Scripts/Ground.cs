using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Ground : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField]
    private bool m_isAudioble;
    public bool audiuble
    {
        get { return m_isAudioble; }
        set { m_isAudioble = value; }
    }
    public AudioClip[] footstepSounds;
    public AudioClip getRandomFootstepSound
    {
        get { return footstepSounds[Random.Range(0, footstepSounds.Length)]; }
    }
    public AudioClip getFootstepSound(int index)
    {
        if (index < 0 || index >= footstepSounds.Length)
        {
            Debug.LogError("Index out of range for footstep sounds array.");
            return null;
        }
        return footstepSounds[index];
    }
}
