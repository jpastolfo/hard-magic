using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeltAtk : MonoBehaviour
{
    private GameObject player;
    public float RaioDeVis�o= 6.1f;
    public float RaioDeAtk = 3.1f;
    private skeltnavmesh sknavmesh;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sknavmesh = GetComponent<skeltnavmesh>();
        animator= GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < RaioDeVis�o)
        {
            sknavmesh.enabled = true;
        }else { sknavmesh.enabled = false; }
        if (Vector3.Distance(transform.position, player.transform.position) < RaioDeAtk)
        {
            animator.SetBool("Atk",true);
        }
        else { animator.SetBool("Atk", false); }
    }
}
