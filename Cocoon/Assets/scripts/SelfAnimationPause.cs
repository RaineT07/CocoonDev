using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAnimationPause : MonoBehaviour
{
    Animator animator;
    AnimationManager animationM;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animationM = GetComponent<AnimationManager>();

        
    }

    // Update is called once per frame
    void Update()
    {

        animationM.PauseAnimation();

    }
}
