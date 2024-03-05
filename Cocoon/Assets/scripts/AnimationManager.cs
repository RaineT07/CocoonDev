using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    float randomOffset;
    public string animationName;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        randomOffset = Random.Range(0f, 15f);

        animator.Play(animationName, 0, randomOffset);
    }

}
