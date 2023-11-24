using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaulAnim : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Cast", true);
        }
        else { anim.SetBool("Cast", false); }
    }
}
