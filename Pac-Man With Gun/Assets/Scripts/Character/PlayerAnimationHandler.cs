using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void SetAnimatorBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }
}
