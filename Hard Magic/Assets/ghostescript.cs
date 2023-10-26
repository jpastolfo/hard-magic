using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghostescript : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public float speed;
    Vector3 movedir;
    UnityEngine.AI.NavMeshAgent nma;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //movedir = player.transform.position - transform.position;
        nma.SetDestination(player.transform.position);
    }

    private void FixedUpdate() {
        //rb.velocity = movedir.normalized * speed;
    }
}
