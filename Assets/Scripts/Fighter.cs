using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public float coolDownTime = 2f;
    private float nextfiretime = 0;
    public static int noOfClicks = 0;
    float lastclickedTime = 0;
    float maxComboDelay = 1;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            anim.SetBool("Hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))
        {
            anim.SetBool("Hit3", false);
            noOfClicks = 0;
        }
        if(Time.time - lastclickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if(Time.time > nextfiretime)
        {
            if (Input.GetMouseButtonDown(0))
                OnClick();
        }
    }
    private void OnClick()
    {
        lastclickedTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            anim.SetBool("Hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
            anim.SetBool("Hit2", true);

        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("Hit2"))
        {
            anim.SetBool("Hit2", false);
            anim.SetBool("Hit3", true);

        }

    }
}
