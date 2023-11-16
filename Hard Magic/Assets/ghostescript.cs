using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghostescript : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public float speed, dashDistance = 3f, dashDuration = 2f, dashCoolDown = 1f, dano = 10f;
    Vector3 movedir, dashDirection;
    UnityEngine.AI.NavMeshAgent nma;
    bool atacando = false, incd = false;
    float dashTimer = 0, dashCdTimer = 0;
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
        if(incd){
            dashCdTimer += Time.deltaTime;
            if(dashCdTimer >= dashCoolDown){
                incd = false;
                dashCdTimer = 0;
                nma.enabled = true;
            }
        }
        if(nma.enabled)
            nma.SetDestination(player.transform.position);
        if(atacando){
            dashTimer += Time.deltaTime;
            transform.position += dashDirection.normalized * speed * Time.deltaTime;
            if(dashTimer >= dashDuration){
                atacando = false;
                incd = true;
            }
        }
        if(Vector3.Distance(player.transform.position, transform.position) <= dashDistance && !atacando && !incd){
            nma.enabled = false;
            atacando = true;
            dashDirection = player.transform.position - transform.position;
            dashTimer = 0;
        }
    }

    private void FixedUpdate() {
        //rb.velocity = movedir.normalized * speed;
    }

    private void OnTriggerEnter(Collider other) {
        battleManager bm = other.GetComponent<battleManager>();
        if(bm != null){
            if(bm.time == 0){
                bm.aplicadano(dano);
            }
        }
    }
}
