using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControlDuracao : MonoBehaviour
{
    public float duracao = 0;
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        AnimationClip clip = animator.runtimeAnimatorController.animationClips[0]; // Replace 0 with the index of your animation clip

        float desiredDurationInSeconds = duracao; // Replace with your desired duration in seconds

        float speed = clip.length / desiredDurationInSeconds;

        animator.speed = speed;

        Destroy(gameObject, duracao);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
